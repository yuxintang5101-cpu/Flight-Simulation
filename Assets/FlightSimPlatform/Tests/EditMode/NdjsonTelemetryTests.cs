using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Data;
using FlightSim.Platform.Integration;
using NUnit.Framework;

namespace FlightSim.Platform.Tests.EditMode
{
    public sealed class NdjsonTelemetryTests
    {
        [Test]
        public void HelloAndEnvelopeContainVersionedReadOnlyMetadata()
        {
            string hello = NdjsonTelemetrySerializer.SerializeHello(2, 1);
            Assert.That(hello, Does.Contain("\"type\":\"hello\""));
            Assert.That(hello, Does.Contain("\"contractVersion\":2"));
            Assert.That(hello, Does.Contain("\"schemaVersion\":1"));
            Assert.That(hello, Does.Contain("\"readOnly\":true"));
            Assert.That(hello, Does.Contain("\"simulationEvent\""));
            Assert.That(hello, Does.Contain("\"missionEvent\""));
            Assert.That(hello, Does.Contain("\"weaponEngagementEvent\""));

            AircraftFastState state = AircraftFastState.CreateDefault(new AircraftId("VIPER-01"));
            state.Tick = 42;
            state.SimulationTimeS = 1.25;
            state.CalibratedAirspeedMps = 150.5;
            string line = NdjsonTelemetrySerializer.SerializeEnvelope(
                "data", "fastState", 2, 1, state.Tick, state.SimulationTimeS, state.Aircraft, true, state);

            Assert.That(line, Does.Contain("\"type\":\"data\""));
            Assert.That(line, Does.Contain("\"domain\":\"fastState\""));
            Assert.That(line, Does.Contain("\"tick\":42"));
            Assert.That(line, Does.Contain("\"simulationTimeS\":1.25"));
            Assert.That(line, Does.Contain("\"aircraftId\":\"VIPER-01\""));
            Assert.That(line, Does.Contain("\"valid\":true"));
            Assert.That(line, Does.Contain("\"CalibratedAirspeedMps\":150.5"));
        }

        [Test]
        public void SerializerEscapesStringsAndWritesEnumsByName()
        {
            MissionEvent missionEvent = new MissionEvent
            {
                RunId = "run-\"one\"",
                Type = MissionEventType.Started,
                Message = "line1\nline2"
            };
            string line = NdjsonTelemetrySerializer.SerializeEnvelope(
                "event", "missionEvent", 2, 1, 7, 2.0, default(AircraftId), true, missionEvent);

            Assert.That(line, Does.Contain("run-\\\"one\\\""));
            Assert.That(line, Does.Contain("line1\\nline2"));
            Assert.That(line, Does.Contain("\"Type\":\"Started\""));
            Assert.That(line, Does.Contain("\"type\":\"event\""));
            Assert.That(line, Does.Contain("\"domain\":\"missionEvent\""));
        }

        [TestCase("fastState")]
        [TestCase("systemsState")]
        [TestCase("missionState")]
        public void DataEnvelopeUsesGenericTypeAndSeparateDomain(string domain)
        {
            string line = NdjsonTelemetrySerializer.SerializeEnvelope(
                "data", domain, 2, 1, 1, 0.02, new AircraftId("VIPER-01"), true, new { Value = 1 });

            Assert.That(line, Does.StartWith("{\"type\":\"data\",\"domain\":\"" + domain + "\""));
        }

        [Test]
        public void TcpServerSendsHelloThenPublishedLine()
        {
            using (var server = new NdjsonTelemetryServer(IPAddress.Loopback, 0, 8))
            {
                server.Start();
                using (var client = new TcpClient())
                {
                    client.Connect(IPAddress.Loopback, server.LocalPort);
                    Assert.That(SpinWait.SpinUntil(() => server.ClientCount == 1, 3000), Is.True);
                    client.ReceiveTimeout = 3000;
                    string payload = "{\"type\":\"health\"}";
                    server.Publish(payload, false, "health");

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[4096];
                    int count = stream.Read(buffer, 0, buffer.Length);
                    string received = Encoding.UTF8.GetString(buffer, 0, count);
                    if (!received.Contains(payload))
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        received += Encoding.UTF8.GetString(buffer, 0, count);
                    }

                    Assert.That(received, Does.StartWith("{\"type\":\"hello\""));
                    Assert.That(received, Does.Contain(payload));
                }
            }
        }

        [Test]
        public void NetworkSurfaceContainsNoCommandReceiverOrSimulationSubmitDependency()
        {
            Type endpoint = typeof(UdpIntegrationEndpoint);
            Assert.That(endpoint.GetMethod("PollCommands"), Is.Null);
            Assert.That(endpoint.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(method => method.Name.IndexOf("Submit", StringComparison.OrdinalIgnoreCase) >= 0), Is.False);
            Assert.That(endpoint.GetConstructors().SelectMany(ctor => ctor.GetParameters())
                .Any(parameter => parameter.ParameterType == typeof(IFlightSimulationService)), Is.False);
            Assert.That(typeof(UdpPacketCodec).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Any(method => method.Name.IndexOf("Command", StringComparison.OrdinalIgnoreCase) >= 0 ||
                               method.Name.IndexOf("Acknowledgement", StringComparison.OrdinalIgnoreCase) >= 0), Is.False);
            Assert.That(typeof(NdjsonTelemetryServer).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Any(method => method.Name.IndexOf("Command", StringComparison.OrdinalIgnoreCase) >= 0), Is.False);
        }

        [Test]
        public void PublisherReadsCanonicalHubAndEmitsWithoutCommands()
        {
            var hub = new FlightDataHub();
            AircraftId aircraft = new AircraftId("VIPER-01");
            hub.OnFastState(new AircraftFastState { Aircraft = aircraft, Tick = 1, SimulationTimeS = 0.02 });
            hub.OnSystemsState(new AircraftSystemsState { Aircraft = aircraft, SimulationTimeS = 0.02 });
            hub.OnTacticalPictureState(TacticalPictureState.CreateDefault(aircraft));

            using (var publisher = new ReadOnlyTelemetryPublisher(hub, 0, 0, 0))
            {
                publisher.Start();
                publisher.Pump(0.02);
                TelemetryPublisherHealth health = publisher.GetHealth();
                Assert.That(health.IsRunning, Is.True);
                Assert.That(health.LastSimulationTimeS, Is.EqualTo(0.02));
                publisher.Stop();
            }
        }
    }
}
