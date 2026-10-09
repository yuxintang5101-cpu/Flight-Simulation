using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FlightSim.Platform.Integration
{
    public sealed class NdjsonTelemetryServer : IDisposable
    {
        public const int DefaultPort = 46001;

        private readonly object sync = new object();
        private readonly IPAddress address;
        private readonly int requestedPort;
        private readonly int queueCapacity;
        private readonly List<ClientConnection> clients = new List<ClientConnection>();
        private TcpListener listener;
        private Thread acceptThread;
        private bool running;
        private long droppedContinuousFrames;
        private long eventOverflowDisconnects;

        public NdjsonTelemetryServer(IPAddress address = null, int port = DefaultPort, int queueCapacity = 128)
        {
            this.address = address ?? IPAddress.Loopback;
            if (!IPAddress.IsLoopback(this.address)) throw new ArgumentException("NDJSON telemetry defaults to loopback only.", nameof(address));
            if (port < 0 || port > 65535) throw new ArgumentOutOfRangeException(nameof(port));
            if (queueCapacity < 2) throw new ArgumentOutOfRangeException(nameof(queueCapacity));
            requestedPort = port;
            this.queueCapacity = queueCapacity;
        }

        public bool IsRunning { get { lock (sync) return running; } }
        public int ClientCount { get { lock (sync) return clients.Count; } }
        public int LocalPort => listener == null ? 0 : ((IPEndPoint)listener.LocalEndpoint).Port;
        public long DroppedContinuousFrames => Interlocked.Read(ref droppedContinuousFrames);
        public long EventOverflowDisconnects => Interlocked.Read(ref eventOverflowDisconnects);

        public void Start()
        {
            lock (sync)
            {
                if (running) return;
                listener = new TcpListener(address, requestedPort);
                try
                {
                    listener.Start();
                }
                catch (SocketException)
                {
                    listener = null;
                    running = false;
                    return;
                }
                running = true;
                acceptThread = new Thread(AcceptLoop) { IsBackground = true, Name = "FlightSim NDJSON Accept" };
                acceptThread.Start();
            }
        }

        public void Publish(string line, bool isEvent, string replacementKey)
        {
            if (string.IsNullOrEmpty(line)) return;
            ClientConnection[] snapshot;
            lock (sync)
            {
                if (!running) return;
                snapshot = clients.ToArray();
            }

            for (int i = 0; i < snapshot.Length; i++)
            {
                EnqueueResult result = snapshot[i].Enqueue(new OutboundLine(line, isEvent, replacementKey ?? string.Empty));
                if (result == EnqueueResult.DroppedContinuous) Interlocked.Increment(ref droppedContinuousFrames);
                else if (result == EnqueueResult.EventOverflow)
                {
                    Interlocked.Increment(ref eventOverflowDisconnects);
                    RemoveClient(snapshot[i]);
                }
            }
        }

        public void Stop()
        {
            ClientConnection[] snapshot;
            Thread thread;
            lock (sync)
            {
                if (!running) return;
                running = false;
                try { listener.Stop(); } catch (SocketException) { }
                snapshot = clients.ToArray();
                clients.Clear();
                thread = acceptThread;
                acceptThread = null;
            }
            for (int i = 0; i < snapshot.Length; i++) snapshot[i].Dispose();
            if (thread != null && thread != Thread.CurrentThread) thread.Join(1000);
        }

        public void Dispose() => Stop();

        private void AcceptLoop()
        {
            while (IsRunning)
            {
                try
                {
                    TcpClient tcp = listener.AcceptTcpClient();
                    tcp.NoDelay = true;
                    var client = new ClientConnection(tcp, queueCapacity, OnClientClosed);
                    client.Enqueue(new OutboundLine(NdjsonTelemetrySerializer.SerializeHello(2, 1), true, "hello"));
                    lock (sync)
                    {
                        if (!running) { client.Dispose(); return; }
                        clients.Add(client);
                    }
                    client.Start();
                }
                catch (SocketException) { if (IsRunning) Thread.Sleep(10); }
                catch (ObjectDisposedException) { return; }
            }
        }

        private void OnClientClosed(ClientConnection client) => RemoveClient(client);

        private void RemoveClient(ClientConnection client)
        {
            lock (sync) clients.Remove(client);
            client.Dispose();
        }

        internal enum EnqueueResult { Accepted, DroppedContinuous, EventOverflow, Closed }

        internal readonly struct OutboundLine
        {
            public OutboundLine(string text, bool isEvent, string key) { Text = text; IsEvent = isEvent; Key = key; }
            public string Text { get; }
            public bool IsEvent { get; }
            public string Key { get; }
        }

        private sealed class ClientConnection : IDisposable
        {
            private readonly object sync = new object();
            private readonly TcpClient tcp;
            private readonly int capacity;
            private readonly Action<ClientConnection> closedCallback;
            private readonly List<OutboundLine> queue;
            private Thread sender;
            private bool closed;

            public ClientConnection(TcpClient tcp, int capacity, Action<ClientConnection> closedCallback)
            {
                this.tcp = tcp;
                this.capacity = capacity;
                this.closedCallback = closedCallback;
                queue = new List<OutboundLine>(capacity);
            }

            public void Start()
            {
                sender = new Thread(SendLoop) { IsBackground = true, Name = "FlightSim NDJSON Client" };
                sender.Start();
            }

            internal EnqueueResult Enqueue(OutboundLine line)
            {
                lock (sync)
                {
                    if (closed) return EnqueueResult.Closed;
                    if (queue.Count >= capacity)
                    {
                        if (line.IsEvent) return EnqueueResult.EventOverflow;
                        int replace = queue.FindIndex(item => !item.IsEvent && string.Equals(item.Key, line.Key, StringComparison.Ordinal));
                        if (replace < 0) replace = queue.FindIndex(item => !item.IsEvent);
                        if (replace < 0) return EnqueueResult.DroppedContinuous;
                        queue.RemoveAt(replace);
                        queue.Add(line);
                        Monitor.Pulse(sync);
                        return EnqueueResult.DroppedContinuous;
                    }
                    queue.Add(line);
                    Monitor.Pulse(sync);
                    return EnqueueResult.Accepted;
                }
            }

            public void Dispose()
            {
                lock (sync)
                {
                    if (closed) return;
                    closed = true;
                    Monitor.PulseAll(sync);
                }
                try { tcp.Close(); } catch (SocketException) { }
            }

            private void SendLoop()
            {
                try
                {
                    NetworkStream stream = tcp.GetStream();
                    while (true)
                    {
                        OutboundLine line;
                        lock (sync)
                        {
                            while (!closed && queue.Count == 0) Monitor.Wait(sync, 500);
                            if (closed) return;
                            line = queue[0];
                            queue.RemoveAt(0);
                        }
                        byte[] bytes = Encoding.UTF8.GetBytes(line.Text + "\n");
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception exception) when (exception is IOException || exception is SocketException || exception is ObjectDisposedException)
                {
                }
                finally
                {
                    lock (sync) closed = true;
                    closedCallback?.Invoke(this);
                }
            }
        }
    }
}
