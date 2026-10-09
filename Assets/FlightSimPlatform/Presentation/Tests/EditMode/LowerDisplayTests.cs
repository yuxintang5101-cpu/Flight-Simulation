using System.Collections.Generic;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Data;
using FlightSim.Platform.Presentation.LowerDisplay;
using NUnit.Framework;
using UnityEngine;

namespace FlightSim.Platform.Presentation.Tests
{
    public sealed class LowerDisplayTests
    {
        [Test] public void PagesAndLayoutChangesRetainSharedSettings()
        {
            var s=new LowerDisplayState{SelectedTrack=7,Brightness=.65f,Metric=true,NorthUp=true,RangeNm=40};s.Assign(0,MfdModule.Fuel);
            Assert.That(s.Layout[0],Is.EqualTo(MfdModule.Fuel));Assert.That(s.Layout[3],Is.EqualTo(MfdModule.Stores));
            s.SetPage(1);s.SetPage(2);s.ResetLayout();Assert.That(s.SelectedTrack,Is.EqualTo(7));Assert.That(s.RangeNm,Is.EqualTo(40));Assert.That(s.Brightness,Is.EqualTo(.65f));Assert.That(s.Metric&&s.NorthUp,Is.True);Assert.That(s.Layout,Is.EqualTo(LowerDisplayState.DefaultLayout()));
        }
        [Test] public void AcknowledgementDoesNotChangeAuthoritativeWarning()
        {
            var data=PoweredData();var aircraft=data.Aircraft;var systems=aircraft.Systems;systems.Warnings.FireWarning=true;aircraft.Systems=systems;data.Aircraft=aircraft;data.EvaluateAlerts();var view=new LowerDisplayState();view.Acknowledged.Add(data.Alerts[0].Id);
            data.EvaluateAlerts();Assert.That(data.Alerts.Count,Is.EqualTo(1));Assert.That(data.Alerts[0].Danger,Is.True);Assert.That(data.Systems.Warnings.FireWarning,Is.True);
            systems.Warnings.FireWarning=false;aircraft.Systems=systems;data.Aircraft=aircraft;data.EvaluateAlerts();view.Acknowledged.RemoveWhere(id=>!data.Alerts.Exists(a=>a.Id==id));Assert.That(view.Acknowledged,Is.Empty);
        }
        [Test] public void MissingStaleAndPowerOffAreDistinct()
        {
            var d=PoweredData();Assert.That(d.Powered,Is.True);d.Stale=true;Assert.That(d.FastValid,Is.False);Assert.That(d.Powered,Is.False);
            d.Stale=false;var a=d.Aircraft;var sys=a.Systems;sys.Electrical.AvionicsBusPowered=false;a.Systems=sys;d.Aircraft=a;Assert.That(d.SystemsValid,Is.True);Assert.That(d.Powered,Is.False);
            a.HasSystems=false;d.Aircraft=a;Assert.That(d.SystemsValid,Is.False);
        }
        [Test] public void GearTransitionAndInvalidValuesAreNotReportedUpOrDown()
        {
            var gear=new LandingGearState{NoseGearPositionNormalized=1,LeftMainGearPositionNormalized=1,RightMainGearPositionNormalized=.4};Assert.That(LowerDisplayData.GearState(in gear),Is.EqualTo("TRANSIT"));
            gear.RightMainGearPositionNormalized=double.NaN;Assert.That(LowerDisplayData.GearState(in gear),Is.EqualTo("N/A"));
        }
        [Test] public void DirectionalFocusSkipsDisabledButtonsAndFollowsGeometry()
        {
            var hits=new List<MfdHit>{new MfdHit("a",new Rect(0,0,30,30),MfdOperation.Page,0,""),new MfdHit("disabled",new Rect(40,0,30,30),MfdOperation.Page,0,"",false),new MfdHit("b",new Rect(80,0,30,30),MfdOperation.Page,0,""),new MfdHit("below",new Rect(0,50,30,30),MfdOperation.Page,0,"")};
            Assert.That(LowerDisplayState.Navigate(hits,"a",Vector2.right),Is.EqualTo(2));Assert.That(LowerDisplayState.Navigate(hits,"a",Vector2.up),Is.EqualTo(3));
        }
        private static LowerDisplayData PoweredData()=>new LowerDisplayData{Aircraft=new AircraftDataSnapshot{IsValid=true,HasFast=true,HasSystems=true,Fast=new AircraftFastState{SimulationTimeS=1},Systems=new AircraftSystemsState{SimulationTimeS=1,Electrical=new ElectricalState{AvionicsBusPowered=true}}}};
    }
}
