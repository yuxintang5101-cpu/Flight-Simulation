using System;
using System.IO;
using System.Reflection;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Missions;
using FlightSim.Platform.Unity.Controls;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace FlightSim.Platform.Unity.Tests.EditMode
{
    public sealed class FlightFoundationTests
    {
        [Test] public void LegacyProfileKeepsCalibrationAndAddsDisplayBindings()
        {
            var profile=new PilotControlProfile{DisplayButtons=null};profile.Axes[0].Minimum=-.75f;profile.Buttons[2].ControlPath="button8";profile.EnsureShape();
            Assert.That(profile.DisplayButtons.Length,Is.EqualTo(11));Assert.That(profile.Axes[0].Minimum,Is.EqualTo(-.75f));Assert.That(profile.Buttons[2].ControlPath,Is.EqualTo("button8"));
        }
        [Test] public void DisplayFocusConsumesButtonsButKeepsFlightAxes()
        {
            InputSystem.RegisterLayout<Gamepad>("MfdInputIsolationPad");var device=(Gamepad)InputSystem.AddDevice("MfdInputIsolationPad");var go=new GameObject("Display input isolation");
            try
            {
                var router=go.AddComponent<PilotInputRouter>();if(router.Profile==null)typeof(PilotInputRouter).GetMethod("Awake",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(router,null);
                router.Profile.Mode=PilotInputMode.Hotas;router.Profile.Axes[(int)FlightAxis.Roll]=new AxisBinding{Layout=device.layout,ControlPath="leftStick/x",DeadZone=0,Exponent=1};
                router.Profile.Buttons[(int)PilotAction.WeaponRelease]=new HardwareBinding{Layout=device.layout,ControlPath="buttonSouth"};router.Profile.DisplayButtons[(int)DisplayAction.Confirm]=new HardwareBinding{Layout=device.layout,ControlPath="buttonSouth"};
                router.SetDisplayFocus(true);InputSystem.Update();Assert.That(router.DisplayHeld(DisplayAction.Confirm),Is.False);
                InputSystem.QueueStateEvent(device,new GamepadState{leftStick=new Vector2(.7f,0)}.WithButton(GamepadButton.South));InputSystem.Update();
                Assert.That(device.buttonSouth.isPressed,Is.True,"Injected button state");Assert.That(router.DisplayHeld(DisplayAction.Confirm),Is.True,"Display confirmation remains readable");
                Assert.That(router.Held(PilotAction.WeaponRelease),Is.False,"No flight command while focused");Assert.That(router.TryReadAxis(FlightAxis.Roll,out float axis),Is.True,"Axis remains connected");Assert.That(axis,Is.GreaterThan(.6f));
                router.SetDisplayFocus(false);Assert.That(router.Held(PilotAction.WeaponRelease),Is.False,"The close frame must not leak its held button into flight controls.");
            }
            finally{if(device.added)InputSystem.RemoveDevice(device);InputSystem.RemoveLayout("MfdInputIsolationPad");UnityEngine.Object.DestroyImmediate(go);}
        }
        [TestCase(-1,-1)] [TestCase(0,0)] [TestCase(1,1)] [TestCase(.01f,0)]
        public void BipolarCalibrationPreservesRangeAndDeadZone(float raw,float expected)
        {Assert.That(new AxisBinding().Normalize(raw,false),Is.EqualTo(expected).Within(.0001));}
        [Test] public void AsymmetricCenterAndInversionAreHonored()
        {
            var axis=new AxisBinding{Minimum=-.8f,Maximum=.9f,Center=.1f,DeadZone=0,Exponent=1,Invert=true};
            Assert.That(axis.Normalize(-.8f,false),Is.EqualTo(1));Assert.That(axis.Normalize(.1f,false),Is.EqualTo(0));Assert.That(axis.Normalize(.9f,false),Is.EqualTo(-1));
            Assert.That(axis.Normalize(.5f,false),Is.EqualTo(-.5f).Within(.001));
        }
        [Test] public void ThrottleIsLinearAndRejectsInvalidRanges()
        {
            var axis=new AxisBinding{Minimum=-1,Maximum=1,Invert=true,Exponent=3};
            Assert.That(axis.Normalize(-1,true),Is.EqualTo(1));Assert.That(axis.Normalize(1,true),Is.EqualTo(0));Assert.That(axis.Normalize(0,true),Is.EqualTo(.5f));
            Assert.That(axis.Normalize(float.NaN,true),Is.EqualTo(0));axis.Maximum=axis.Minimum;Assert.That(axis.Normalize(.5f,true),Is.EqualTo(0));
        }
        [Test] public void ProfilesRoundTripAndRecoverFromMalformedFiles()
        {
            string directory=Path.Combine(Path.GetTempPath(),"FlightFoundationTests-"+Guid.NewGuid());string path=Path.Combine(directory,"controls.json");
            try {
                var p=new PilotControlProfile{Mode=PilotInputMode.Hotas,Throttle=ThrottleSelection.Right};p.Axes[0].Invert=true;p.Buttons[0].ExplicitlyUnbound=true;
                PilotProfileStorage.Save(path,p);p.Name="Second";PilotProfileStorage.Save(path,p);
                var loaded=PilotProfileStorage.Load(path,out var warning);Assert.That(warning,Is.Empty);Assert.That(loaded.Name,Is.EqualTo("Second"));Assert.That(loaded.Axes[0].Invert,Is.True);Assert.That(loaded.Buttons[0].ExplicitlyUnbound,Is.True);Assert.That(File.Exists(path+".bak"),Is.True);
                File.WriteAllText(path,"invalid{");loaded=PilotProfileStorage.Load(path,out warning);Assert.That(warning,Is.Not.Empty);Assert.That(loaded.Axes.Length,Is.EqualTo(6));
            } finally {if(Directory.Exists(directory))Directory.Delete(directory,true);}
        }
        [Test] public void MissionCatalogClonesDefinitionsAndRejectsDuplicates()
        {
            var original=TrainingMissionFactory.CreateNavigationTraining();var catalog=new FlightMissionCatalog();
            Assert.That(catalog.Add(original,false,"test"),Is.True);original.Actors[0].AircraftId="CHANGED";
            Assert.That(catalog.Find(original.MissionId).Actors[0].AircraftId,Is.EqualTo("TRAINER-01"));
            Assert.That(catalog.Add(TrainingMissionFactory.CreateNavigationTraining(),false,"duplicate"),Is.False);
        }
        [Test] public void InvalidFuelAndWaypointDataCannotEnterRuntime()
        {
            var mission=TrainingMissionFactory.CreateNavigationTraining();Assert.That(MissionDefinitionValidator.Validate(mission).IsValid,Is.True);
            mission.Actors[0].InitialCondition.InternalFuelFraction=double.NaN;Assert.That(MissionDefinitionValidator.Validate(mission).IsValid,Is.False);
            mission=TrainingMissionFactory.CreateNavigationTraining();mission.Actors[0].Route[0].LatitudeRad=10;Assert.That(MissionDefinitionValidator.Validate(mission).IsValid,Is.False);
        }
        [Test] public void VirtualDeviceRoutesAxesThenReleasesAfterDisconnect()
        {
            var device=InputSystem.AddDevice<Gamepad>();var go=new GameObject("Input router test");
            try {
                var router=go.AddComponent<PilotInputRouter>();
                if(router.Profile==null)typeof(PilotInputRouter).GetMethod("Awake",BindingFlags.NonPublic|BindingFlags.Instance).Invoke(router,null);
                router.Profile.Mode=PilotInputMode.Hotas;
                router.Profile.Axes[(int)FlightAxis.Roll]=new AxisBinding{Layout=device.layout,ControlPath="leftStick/x",DeadZone=0,Exponent=1};
                InputSystem.QueueStateEvent(device,new GamepadState{leftStick=new Vector2(.8f,0)});InputSystem.Update();
                Assert.That(router.TryReadAxis(FlightAxis.Roll,out float value),Is.True);Assert.That(value,Is.GreaterThan(.6f));
                InputSystem.RemoveDevice(device);Assert.That(router.TryReadAxis(FlightAxis.Roll,out value),Is.False);Assert.That(value,Is.EqualTo(0));
            } finally {if(device.added)InputSystem.RemoveDevice(device);UnityEngine.Object.DestroyImmediate(go);}
        }
    }
}
