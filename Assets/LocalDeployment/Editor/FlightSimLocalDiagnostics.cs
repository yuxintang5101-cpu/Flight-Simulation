using System;
using System.IO;
using System.Linq;
using FlightSim.Platform.Unity;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Presentation.LowerDisplay;
using UnityEditor;
using UnityEngine;

// Local, one-shot deployment checks. No network listener or runtime component.
[InitializeOnLoad]
public static class FlightSimLocalDiagnostics
{
    private const string Request = "Temp/FlightSimLocalDiagnostics.request";
    static FlightSimLocalDiagnostics() { EditorApplication.update += CheckRequest; }

    private static void CheckRequest()
    {
        if (!File.Exists(Request) || EditorApplication.isCompiling || EditorApplication.isUpdating) return;
        string command;
        try { command = File.ReadAllText(Request).Trim(); File.Delete(Request); }
        catch(IOException) { return; }
        string root = Directory.GetParent(Application.dataPath).Parent.FullName;
        string output = Path.Combine(root, "Deployment");
        try
        {
            if (command == "devices")
            {
                var devices = UnityEngine.InputSystem.InputSystem.devices.Select(d => new DeviceInfo {
                    Name = d.name, Product = d.description.product, Layout = d.layout, Capabilities = d.description.capabilities,
                    Controls = d.allControls.Where(c => c is UnityEngine.InputSystem.Controls.AxisControl).Select(c => new ControlInfo {
                        Path = c.path, Layout = c.layout, Value = ((UnityEngine.InputSystem.Controls.AxisControl)c).ReadValue()
                    }).ToArray()
                }).ToArray();
                File.WriteAllText(Path.Combine(output, "hardware-inventory.json"), JsonUtility.ToJson(new Devices { Items = devices }, true));
            }
            if (command == "save-and-exit")
            {
                UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
                AssetDatabase.SaveAssets();
                EditorApplication.delayCall += () => EditorApplication.Exit(0);
            }
            if (command == "play" && !EditorApplication.isPlaying) EditorApplication.isPlaying = true;
            else if (command == "stop" && EditorApplication.isPlaying) EditorApplication.isPlaying = false;
            else if (command == "start-auto")
            {
                var host = UnityEngine.Object.FindObjectOfType<FlightSimulationHost>();
                if (host == null || !EditorApplication.isPlaying) throw new InvalidOperationException("Play mode host unavailable");
                var result = host.StartMission(true);
                if (!result.Accepted) throw new InvalidOperationException(result.Message);
            }
            else if (command == "capture")
            {
                if (!EditorApplication.isPlaying) throw new InvalidOperationException("Capture requires Play mode");
                ScreenCapture.CaptureScreenshot(Path.Combine(output, "editor-game-view.png"));
            }
            var session = UnityEngine.Object.FindObjectOfType<FlightSessionController>();
            if (EditorApplication.isPlaying && session != null)
            {
                if(command=="home")session.Home();
                if(command=="missions")session.Show(FlightScreen.Missions);
                if(command=="controls")session.OpenControls();
                if(command=="free-setup")session.Show(FlightScreen.FreeFlight);
                if(command=="free-air"){session.FreePreset=StartupPreset.Airborne;session.BeginFreeFlight();}
                if(command=="mission-auto")session.BeginMission(session.Catalog.Find("TRAINING_NAV_01"),true);
                if(command=="pause")session.Show(FlightScreen.Paused);
                if(command=="resume")session.Show(FlightScreen.Flying);
                if(command=="restart")session.Restart();
                if(command=="debrief")session.EndFlight();
                if(command=="preview-fire")session.Host.GetComponent<FlightEffectsController>().Preview(true);
                if(command=="preview-explosion")session.Host.GetComponent<FlightEffectsController>().Preview(false);
                if(command=="chase")session.Host.SetCameraMode(FlightCameraMode.Chase);
                if(command=="cockpit")session.Host.SetCameraMode(FlightCameraMode.Cockpit);
                var mfd=UnityEngine.Object.FindObjectOfType<LowerDisplayController>();
                if(command=="mfd-focus"&&mfd!=null)mfd.SetFocused(true);
                if(command=="mfd-close"&&mfd!=null)mfd.SetFocused(false);
                if(command.StartsWith("capture:"))
                {
                    string captureDirectory=Path.Combine(root,"Development","Captures");Directory.CreateDirectory(captureDirectory);
                    ScreenCapture.CaptureScreenshot(Path.Combine(captureDirectory,Path.GetFileName(command.Substring(8))+".png"));
                }
            }
            var current = UnityEngine.Object.FindObjectOfType<FlightSimulationHost>();
            var display = UnityEngine.Object.FindObjectOfType<LowerDisplayController>();
            File.WriteAllText(Path.Combine(output, "editor-status.json"), JsonUtility.ToJson(new Status
            {
                Command = command,
                LowerDisplayBound = display != null && display.SurfaceBound,
                LowerDisplayFocused = display != null && display.Focused,
                LowerDisplayStatus = display != null && display.Data != null ? display.Data.Status : "",
                IsPlaying = EditorApplication.isPlaying,
                Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path,
                HostFound = current != null,
                MissionRunning = current != null && current.IsMissionRunning,
                AutomationEnabled = current != null && current.IsPlayerAutomationEnabled,
                HasSnapshot = current != null && current.HasLatestSnapshot,
                Screen = session != null ? session.Screen.ToString() : "",
                Controller = session != null ? session.Input.ConnectionSummary : "",
                Focused = Application.isFocused,
                SimulationTime = current != null && current.HasLatestSnapshot ? current.LatestSnapshot.Fast.SimulationTimeS : 0,
                Effects = current != null && current.GetComponent<FlightEffectsController>() != null ? current.GetComponent<FlightEffectsController>().ActiveCount : 0,
                Timestamp = DateTime.Now.ToString("o")
            }, true));
        }
        catch (Exception exception)
        {
            File.WriteAllText(Path.Combine(output, "editor-status.json"), JsonUtility.ToJson(new Status { Command = command, Error = exception.ToString() }, true));
        }
    }

    [Serializable] private class Status
    {
        public string Command, Scene, Timestamp, Error, Screen, Controller, LowerDisplayStatus;
        public bool LowerDisplayBound, LowerDisplayFocused;
        public bool IsPlaying, HostFound, MissionRunning, AutomationEnabled, HasSnapshot, Focused;
        public double SimulationTime;
        public int Effects;
    }
    [Serializable] private class Devices { public DeviceInfo[] Items; }
    [Serializable] private class DeviceInfo { public string Name, Product, Layout, Capabilities; public ControlInfo[] Controls; }
    [Serializable] private class ControlInfo { public string Path, Layout; public float Value; }
}
