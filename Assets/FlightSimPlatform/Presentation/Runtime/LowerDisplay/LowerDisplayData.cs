using System;
using System.Collections.Generic;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using FlightSim.Platform.Data;
using FlightSim.Platform.Unity;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public struct MfdAlert {public string Id,Label,Chinese;public bool Danger;public MfdAlert(string id,string label,string chinese,bool danger){Id=id;Label=label;Chinese=chinese;Danger=danger;}}
    public struct MfdWaypoint {public string Name;public double Longitude,Latitude,Height;public bool Active;}
    public sealed class LowerDisplayData
    {
        public AircraftDataSnapshot Aircraft, Wing;
        public MissionSnapshot Mission;
        public bool HasMission,HasWing,Stale;
        public string SessionKey="",Title="",Status="NO DATA";
        public readonly List<MfdAlert> Alerts=new List<MfdAlert>();
        public readonly List<MfdWaypoint> Waypoints=new List<MfdWaypoint>();
        public bool FastValid => Aircraft.IsValid&&Aircraft.HasFast&&!Stale;
        public bool SystemsValid => FastValid&&Aircraft.HasSystems&&Math.Abs(Aircraft.Fast.SimulationTimeS-Aircraft.Systems.SimulationTimeS)<2;
        public bool TacticalValid => FastValid&&Aircraft.HasTactical&&Math.Abs(Aircraft.Fast.SimulationTimeS-Aircraft.Tactical.SimulationTimeS)<2;
        public bool Powered => SystemsValid&&Aircraft.Systems.Electrical.AvionicsBusPowered;
        public AircraftFastState Fast=>Aircraft.Fast;
        public AircraftSystemsState Systems=>Aircraft.Systems;
        public AircraftCombatState Combat=>Aircraft.Combat;
        public bool CombatValid=>SystemsValid&&Aircraft.HasCombat&&Combat.IsValid;
        public double InternalCapacityKg=3175;
        public static string GearState(in LandingGearState gear)
        {
            double n=gear.NoseGearPositionNormalized,l=gear.LeftMainGearPositionNormalized,r=gear.RightMainGearPositionNormalized;
            if(!Finite(n)||!Finite(l)||!Finite(r)||n<0||l<0||r<0||n>1||l>1||r>1)return "N/A";
            if(n>.98&&l>.98&&r>.98)return "DOWN";if(n<.02&&l<.02&&r<.02)return "UP";return "TRANSIT";
        }
        public static bool Finite(double v)=>!double.IsNaN(v)&&!double.IsInfinity(v);
        public void EvaluateAlerts()
        {
            Alerts.Clear();if(!SystemsValid)return;var w=Systems.Warnings;
            if(w.FireWarning)Alerts.Add(new MfdAlert("fire","ENGINE FIRE","发动机火警有效，检查机电页面。",true));
            if(w.StallWarning)Alerts.Add(new MfdAlert("stall","STALL","失速告警。",true));
            if(w.LowAltitudeWarning)Alerts.Add(new MfdAlert("terrain","LOW ALTITUDE","低高度告警。",true));
            if(w.OverspeedWarning)Alerts.Add(new MfdAlert("speed","OVERSPEED","超速告警。",true));
            if(w.HydraulicWarning)Alerts.Add(new MfdAlert("hyd","HYDRAULIC","液压系统告警。",false));
            if(w.ElectricalWarning)Alerts.Add(new MfdAlert("elec","ELECTRICAL","电气系统告警。",false));
            if(w.FuelWarning||Systems.Fuel.LowFuelWarning)Alerts.Add(new MfdAlert("fuel","BINGO FUEL","燃油系统告警，核对总量与返航阈值。",false));
            if(w.LandingGearWarning)Alerts.Add(new MfdAlert("gear","LANDING GEAR","起落架告警。",false));
            if(w.CanopyWarning)Alerts.Add(new MfdAlert("canopy","CANOPY","座舱盖告警。",false));
            if((w.MasterCaution||w.MasterWarning)&&Alerts.Count==0)Alerts.Add(new MfdAlert("master","MASTER ALERT","系统主告警有效。",w.MasterWarning));
        }
    }
    public interface ILowerDisplayDataSource {LowerDisplayData Read(float realtime);CommandResult Execute(MfdOperation operation,int argument);}
    public sealed class UnityLowerDisplayDataSource : ILowerDisplayDataSource
    {
        private readonly FlightSessionController session;private IFlightDataHub previousHub;private ulong previousTick;private float lastUpdate;private readonly LowerDisplayData data=new LowerDisplayData();
        public UnityLowerDisplayDataSource(FlightSessionController session){this.session=session;}
        public LowerDisplayData Read(float realtime)
        {
            var host=session.Host;var hub=host.DataHub;data.Title=session.CurrentTitle;
            data.Aircraft=default;data.HasWing=false;data.HasMission=false;
            if(hub==null||!hub.TryGetAircraft(host.LocalAircraft,out data.Aircraft)){data.Stale=true;data.Status="NO DATA";data.Alerts.Clear();return data;}
            if(hub!=previousHub||previousTick!=data.Fast.Tick||host.IsSessionPaused){lastUpdate=realtime;previousTick=data.Fast.Tick;previousHub=hub;}
            data.Stale=realtime-lastUpdate>2;data.HasMission=hub.TryGetMission(out data.Mission);
            data.SessionKey=session.SessionGeneration+":"+(data.HasMission?data.Mission.Mission.RunId:"FREE");
            data.Waypoints.Clear();
            if(data.HasMission&&host.LoadedMission!=null)
                foreach(var actor in data.Mission.Actors)if(actor.Aircraft==host.LocalAircraft)
                    foreach(var definition in host.LoadedMission.Actors)if(definition.AircraftId==actor.Aircraft.Value&&definition.Route!=null)
                        for(int i=0;i<definition.Route.Length;i++){var wp=definition.Route[i];data.Waypoints.Add(new MfdWaypoint{Name=wp.WaypointId,Longitude=wp.LongitudeRad,Latitude=wp.LatitudeRad,Height=wp.EllipsoidHeightM,Active=i==actor.CurrentWaypointIndex});}
            if(data.Waypoints.Count==0&&data.TacticalValid)for(int i=0;i<data.Aircraft.Tactical.Waypoints.Length;i++){var wp=data.Aircraft.Tactical.Waypoints[i];if(wp.IsValid)data.Waypoints.Add(new MfdWaypoint{Name=wp.Name,Longitude=wp.LongitudeRad,Latitude=wp.LatitudeRad,Height=wp.EllipsoidHeightM,Active=wp.IsActive});}
            data.InternalCapacityKg=F16AircraftDefinition.CreateDefault().InternalFuelCapacityKg;
            data.Status=data.Stale?"DATA STALE":!data.SystemsValid?"SYS N/A":!data.Powered?"DISPLAY OFF":host.IsSessionPaused?"PAUSED":"LIVE";
            if(data.SystemsValid&&data.Systems.Avionics.DataLinkEnabled)
            {
                for(int i=0;i<hub.AircraftCount;i++)
                {
                    if(!hub.TryGetAircraftId(i,out var id)||id==host.LocalAircraft||!hub.TryGetAircraft(id,out var wing))continue;
                    if(wing.IsValid&&wing.HasFast&&Math.Abs(wing.Fast.SimulationTimeS-data.Fast.SimulationTimeS)<2&&wing.Identity.Side==data.Aircraft.Identity.Side&&wing.Identity.Role==AircraftRole.Wingman){data.Wing=wing;data.HasWing=true;break;}
                }
            }
            data.EvaluateAlerts();return data;
        }
        public CommandResult Execute(MfdOperation op,int arg)
        {
            if(!data.Powered||data.Stale||session.Screen!=FlightScreen.Flying)return CommandResult.Rejected(3070,"显示器未供电或遥测不可用，操作未提交。");
            switch(op)
            {
                case MfdOperation.Station:return session.Host.SelectDisplayStation(arg);
                case MfdOperation.Arm:return session.Host.CycleDisplayMasterArm();
                case MfdOperation.Radar:return session.Host.SetSystemSwitch(AircraftSystemSwitch.Radar,!data.Systems.Avionics.RadarEnabled);
                case MfdOperation.Link:return session.Host.SetSystemSwitch(AircraftSystemSwitch.DataLink,!data.Systems.Avionics.DataLinkEnabled);
                default:return CommandResult.Rejected(3071,"不支持的系统操作。");
            }
        }
    }
}
