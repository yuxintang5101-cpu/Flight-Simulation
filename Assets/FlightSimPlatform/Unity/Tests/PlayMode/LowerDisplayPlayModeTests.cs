using System.Collections;
using System.IO;
using System.Reflection;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using FlightSim.Platform.Presentation.LowerDisplay;
using FlightSim.Platform.Unity.Controls;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TestTools;

namespace FlightSim.Platform.Unity.Tests.PlayMode
{
    public sealed class LowerDisplayPlayModeTests
    {
        private static string Output => Path.GetFullPath(Path.Combine(Application.dataPath,"../../Development/LowerDisplay/Captures"));
        [UnityTest]
        public IEnumerator NativeScreenBindsAndRetainsStateAcrossPagesAndFocus()
        {
            SceneManager.LoadScene("FlightSim_KTEX_V2",LoadSceneMode.Single);
            yield return null;yield return null;
            var session=Object.FindObjectOfType<FlightSessionController>();
            var display=Object.FindObjectOfType<LowerDisplayController>();
            Assert.That(display,Is.Not.Null);Assert.That(display.SurfaceBound,Is.True);
            Directory.CreateDirectory(Output);
            File.WriteAllText(Path.Combine(Output,"hardware.txt"),session.Input.ConnectionSummary+"\nStick="+session.Input.StickConnected+"\nThrottle="+session.Input.ThrottleConnected);
            session.Input.Profile.Mode=PilotInputMode.KeyboardMouse;
            session.FreePreset=StartupPreset.Airborne;Assert.That(session.BeginFreeFlight(),Is.True);
            yield return new WaitForSeconds(.2f);display.RenderNow();
            Assert.That(display.Data.Powered,Is.True);Assert.That(display.Data.Status,Is.EqualTo("LIVE"));
            Assert.That(display.Data.Fast.CalibratedAirspeedMps,Is.EqualTo(session.Host.LatestSnapshot.Fast.CalibratedAirspeedMps).Within(.5));
            Assert.That(display.DisplayTexture.width,Is.EqualTo(2048));Assert.That(display.HitCount,Is.GreaterThan(30));
            Capture(display,"01-day-integrated");
            display.Apply(MfdOperation.Night,0);Capture(display,"02-night-integrated");display.Apply(MfdOperation.Night,0);
            display.Apply(MfdOperation.Expand,(int)MfdModule.Fuel);Capture(display,"03-fuel-expanded");display.Apply(MfdOperation.Back,0);
            display.Apply(MfdOperation.Expand,(int)MfdModule.Systems);Capture(display,"04-systems-expanded");display.Apply(MfdOperation.Back,0);
            display.Apply(MfdOperation.Expand,(int)MfdModule.Stores);Capture(display,"05-stores-expanded");display.Apply(MfdOperation.Back,0);
            display.Apply(MfdOperation.ZoomIn,0);display.Apply(MfdOperation.Units,0);display.Apply(MfdOperation.Orientation,0);
            display.Apply(MfdOperation.Page,1);Capture(display,"06-map");
            display.Apply(MfdOperation.Page,2);Capture(display,"07-vision");
            Assert.That(display.State.RangeNm,Is.EqualTo(10));Assert.That(display.State.Metric,Is.True);Assert.That(display.State.NorthUp,Is.True);
            display.Apply(MfdOperation.Page,0);display.Apply(MfdOperation.Assign,3);Assert.That(display.State.Layout[0],Is.EqualTo(MfdModule.Fuel));
            display.Apply(MfdOperation.Hide,0);Assert.That(display.State.Layout[0],Is.EqualTo(MfdModule.None));
            display.Apply(MfdOperation.Reset,0);CollectionAssert.AreEqual(LowerDisplayState.DefaultLayout(),display.State.Layout);
            display.SetFocused(true);Assert.That(session.Input.DisplayFocused,Is.True);
            display.Apply(MfdOperation.Expand,(int)MfdModule.Fuel);
            Assert.That(session.DisplayEscapeHandler(),Is.True);Assert.That(display.State.Expanded,Is.EqualTo(MfdModule.None));Assert.That(display.Focused,Is.True);
            Assert.That(session.DisplayEscapeHandler(),Is.True);Assert.That(display.Focused,Is.False);
            display.SetFocused(true);session.Show(FlightScreen.Paused);Assert.That(display.Focused,Is.False);
            session.Show(FlightScreen.Flying);display.Apply(MfdOperation.Units,0);display.Apply(MfdOperation.ZoomOut,0);
            var rig=Object.FindObjectOfType<FlightCameraRig>();rig.SetMode(FlightCameraMode.Cockpit);
            yield return null;yield return null;display.RenderNow();
            var camera=rig.GetComponent<Camera>();var original=camera.targetTexture;var capture=new RenderTexture(1920,1080,24);capture.Create();
            camera.targetTexture=capture;camera.Render();Save(capture,"08-cockpit");camera.targetTexture=original;capture.Release();Object.Destroy(capture);
            // Baked UV mapping must agree with the physical central screen after aircraft movement.
            var profile=Resources.Load<LowerDisplaySurfaceProfile>("LowerDisplay/Surface");var surface=new LowerDisplaySurface();
            Assert.That(surface.Bind(rig.AircraftTarget,profile,display.DisplayTexture),Is.True);
            Vector3 center=surface.VisualRoot.TransformPoint((profile.TopLeft+profile.BottomRight)*.5f);
            Assert.That(surface.TryHit(new Ray(camera.transform.position,center-camera.transform.position),out var pixel),Is.True);
            Assert.That(pixel.x,Is.EqualTo(1024).Within(2));Assert.That(pixel.y,Is.EqualTo(512).Within(2));surface.Dispose();
        }
        [UnityTest]
        public IEnumerator CommandsUseSimulationAuthorityAndPowerLossBlanksTheScreen()
        {
            SceneManager.LoadScene("FlightSim_KTEX_V2",LoadSceneMode.Single);yield return null;yield return null;
            var session=Object.FindObjectOfType<FlightSessionController>();var display=Object.FindObjectOfType<LowerDisplayController>();
            session.Input.Profile.Mode=PilotInputMode.KeyboardMouse;
            Assert.That(session.BeginMission(DefaultMissionCatalog.CreateAll()[1],false),Is.True);
            yield return new WaitForSeconds(.2f);display.RenderNow();Assert.That(display.Data.CombatValid,Is.True);
            Assert.That(display.Data.HasWing,Is.True);Assert.That(display.Data.TacticalValid,Is.True);
            int selected=-1;for(int i=0;i<display.Data.Aircraft.Tactical.Tracks.Length;i++)if(display.Data.Aircraft.Tactical.Tracks[i].IsValid){selected=display.Data.Aircraft.Tactical.Tracks[i].TrackId;break;}
            Assert.That(selected,Is.GreaterThanOrEqualTo(0));display.Apply(MfdOperation.Target,selected);display.Apply(MfdOperation.Page,1);display.RenderNow();Assert.That(display.State.SelectedTrack,Is.EqualTo(selected));
            Capture(display,"11-mission-map");display.Apply(MfdOperation.Page,0);Capture(display,"12-mission-integrated");
            bool previous=display.Data.Systems.Avionics.RadarEnabled;display.Apply(MfdOperation.Radar,0);Assert.That(display.LastCommandAccepted,Is.True);
            yield return AwaitTelemetry(display,()=>display.Data.Systems.Avionics.RadarEnabled==!previous,"Radar command reaches the data hub");
            display.Apply(MfdOperation.Radar,0);display.Apply(MfdOperation.Station,1);Assert.That(display.LastCommandAccepted,Is.True);
            var before=display.Data.Combat.MasterArm;display.Apply(MfdOperation.Arm,0);Assert.That(display.LastCommandAccepted,Is.True);
            yield return new WaitForSeconds(.25f);display.RenderNow();Assert.That(display.Data.Combat.MasterArm,Is.Not.EqualTo(before));Assert.That(display.Data.Combat.SelectedStationIndex,Is.EqualTo(1));
            display.Apply(MfdOperation.Mission,0);display.Apply(MfdOperation.Check,0);Capture(display,"09-mission");Assert.That(display.State.Checklist.Contains(0),Is.True);display.Apply(MfdOperation.Back,0);
            Assert.That(session.Host.SetPrimarySystemsEnabled(false),Is.True);
            yield return AwaitTelemetry(display,()=>display.Data.SystemsValid&&!display.Data.Powered,"Avionics power off");
            Capture(display,"10-display-off");display.Apply(MfdOperation.Radar,0);Assert.That(display.LastCommandAccepted,Is.False);
            Assert.That(session.Host.SetPrimarySystemsEnabled(true),Is.True);
            yield return AwaitTelemetry(display,()=>display.Data.Powered,"Avionics power restored");
            session.Show(FlightScreen.Paused);display.Apply(MfdOperation.Arm,0);Assert.That(display.LastCommandAccepted,Is.False);
            var lowFuel=TrainingMissionFactory.CreateNavigationTraining();lowFuel.Actors[0].InitialCondition.InternalFuelFraction=.05;
            Assert.That(session.BeginMission(lowFuel,false),Is.True);yield return new WaitForSeconds(.25f);display.RenderNow();
            Assert.That(display.Data.Alerts.Exists(a=>a.Id=="fuel"),Is.True);display.Apply(MfdOperation.Alerts,0);display.Apply(MfdOperation.Acknowledge,0);Capture(display,"13-fuel-alert-acknowledged");
            Assert.That(display.Data.Alerts.Exists(a=>a.Id=="fuel"),Is.True);Assert.That(display.State.Acknowledged.Contains("fuel"),Is.True);
            display.Apply(MfdOperation.Check,0);session.FreePreset=StartupPreset.Airborne;session.BeginFreeFlight();yield return new WaitForSeconds(.1f);display.RenderNow();Assert.That(display.State.Checklist,Is.Empty);Assert.That(display.State.Acknowledged,Is.Empty);
            display.Apply(MfdOperation.Check,1);session.Restart();yield return new WaitForSeconds(.1f);display.RenderNow();Assert.That(display.State.Checklist,Is.Empty,"A new free flight resets its checklist even when DataHub is reused.");
        }
        [UnityTest]
        public IEnumerator HardwarePressEventsStayInTheDisplayInputContext()
        {
            InputSystem.RegisterLayout<Gamepad>("MfdPlayModePad");var device=(Gamepad)InputSystem.AddDevice("MfdPlayModePad");var go=new GameObject("Input event validation");
            try
            {
                var router=go.AddComponent<PilotInputRouter>();router.Profile.Mode=PilotInputMode.Hotas;
                router.Profile.DisplayButtons[(int)DisplayAction.Confirm]=new HardwareBinding{Layout=device.layout,ControlPath="buttonSouth"};
                router.Profile.Buttons[(int)PilotAction.WeaponRelease]=new HardwareBinding{Layout=device.layout,ControlPath="buttonSouth"};
                router.SetDisplayFocus(true);yield return null;
                Assert.That(router.DisplayPressed(DisplayAction.Confirm),Is.False);
                InputSystem.QueueStateEvent(device,new GamepadState().WithButton(GamepadButton.South));UpdatePlayerInput();
                Assert.That(device.buttonSouth.isPressed,Is.True,"Injected held state");Assert.That(device.buttonSouth.wasPressedThisFrame,Is.True,"Input System frame event");
                Assert.That(router.DisplayPressed(DisplayAction.Confirm),Is.True,"Routed display event");Assert.That(router.Pressed(PilotAction.WeaponRelease),Is.False);
                router.SetDisplayFocus(false);Assert.That(router.Pressed(PilotAction.WeaponRelease),Is.False);
                InputSystem.QueueStateEvent(device,new GamepadState());UpdatePlayerInput();yield return null;
                InputSystem.QueueStateEvent(device,new GamepadState().WithButton(GamepadButton.South));UpdatePlayerInput();
                Assert.That(router.Pressed(PilotAction.WeaponRelease),Is.True,"Fresh press after leaving display mode reaches flight controls.");
            }
            finally{if(device.added)InputSystem.RemoveDevice(device);InputSystem.RemoveLayout("MfdPlayModePad");Object.Destroy(go);}
        }
        private static IEnumerator AwaitTelemetry(LowerDisplayController display,System.Func<bool> condition,string message)
        {
            float deadline=Time.realtimeSinceStartup+5;
            do{yield return null;display.RenderNow();}while(!condition()&&Time.realtimeSinceStartup<deadline);
            Assert.That(condition(),Is.True,message+"; t="+display.Data.Fast.SimulationTimeS+" / "+display.Data.Status);
        }
        // Batch editors have no focused Game view. Explicitly process a player update for this virtual-device test.
        private static void UpdatePlayerInput()
        {
            typeof(InputSystem).GetMethod("Update",BindingFlags.Static|BindingFlags.NonPublic,null,new[]{typeof(InputUpdateType)},null).Invoke(null,new object[]{InputUpdateType.Dynamic});
        }
        private static void Capture(LowerDisplayController display,string name)
        {
            display.RenderNow();Assert.That(display.VectorOverflow,Is.False,name);Save(display.DisplayTexture,name);
        }
        private static void Save(RenderTexture render,string name)
        {
            Directory.CreateDirectory(Output);var previous=RenderTexture.active;var texture=new Texture2D(render.width,render.height,TextureFormat.RGB24,false);
            try{RenderTexture.active=render;texture.ReadPixels(new Rect(0,0,render.width,render.height),0,0);texture.Apply();File.WriteAllBytes(Path.Combine(Output,name+".png"),texture.EncodeToPNG());}
            finally{RenderTexture.active=previous;Object.Destroy(texture);}
        }
    }
}
