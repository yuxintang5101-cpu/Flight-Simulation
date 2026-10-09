using System;
using System.Collections.Generic;
using System.Globalization;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using UnityEngine;
using C=FlightSim.Platform.Presentation.LowerDisplay.MfdColors;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public sealed partial class LowerDisplayComposer
    {
        private readonly LowerDisplayPainter p;
        private LowerDisplayData d;private LowerDisplayState s;
        public readonly List<MfdHit> Hits=new List<MfdHit>();
        public string Notice="",HoverHelp="";
        public bool Focused;
        public Texture MapTexture,VisionTexture;
        public static readonly string[] Titles={"SMS","TSD","SYS","FUEL","PFD","RDR","WING"};
        public static readonly string[] Names={"武器挂载","态势地图","机电综合","燃油系统","飞行仪表","雷达态势","僚机状态"};
        private static readonly Rect[] Slots={new Rect(0,176,616,480),new Rect(616,176,816,480),new Rect(1432,176,616,480),new Rect(0,656,512,296),new Rect(512,656,512,296),new Rect(1024,656,512,296),new Rect(1536,656,512,296)};
        public LowerDisplayComposer(LowerDisplayPainter painter){p=painter;}
        private string N(double value,string format="0")=>LowerDisplayData.Finite(value)?value.ToString(format,CultureInfo.InvariantCulture):"N/A";
        private string Fuel(double v)=>N(v*(s.Metric?1:2.204623));
        private string Alt(double v)=>N(v*(s.Metric?1:3.28084),"#,0");
        private string Speed(double v)=>N(v*(s.Metric?3.6:1.943844));
        private string Distance(double v)=>N(v/(s.Metric?1000:1852),"0.0");
        private string FuelUnit=>s.Metric?"KG":"LB";private string AltUnit=>s.Metric?"M":"FT";private string SpeedUnit=>s.Metric?"KM/H":"KTS";private string RangeUnit=>s.Metric?"KM":"NM";
        private string Pressure(double v)=>N(v/(s.Metric?1e6:6894.757),s.Metric?"0.0":"0")+(s.Metric?" MPa":" PSI");
        public void Draw(LowerDisplayData data,LowerDisplayState state)
        {
            d=data;s=state;Hits.Clear();p.Begin(s.EffectiveBrightness);
            if(d.SystemsValid&&!d.Powered){p.End();return;}
            if(!d.FastValid){p.Text(1024,455,d.Stale?"DATA STALE":"NO FLIGHT DATA",58,C.Amber,1);p.Text(1024,535,"遥测不可用 · 等待有效飞行数据",29,C.Muted,1);p.End();return;}
            Header();
            if(s.Expanded!=MfdModule.None)Expanded();
            else if(s.Page==MfdPage.Integrated){for(int i=0;i<7;i++)Module(s.Layout[i],i,Slots[i],i>=3);}
            else if(s.Page==MfdPage.Map)MapPage();else VisionPage();
            Softbar();
            if(s.MenuSlot>=0)Menu();
            if(!string.IsNullOrEmpty(s.Dialog))Dialog();
            p.Overlay();
            if(Focused){foreach(var hit in Hits)if(hit.Id==s.FocusId&&hit.Enabled){p.Box(hit.Bounds.x-3,hit.Bounds.y-3,hit.Bounds.width+6,hit.Bounds.height+6,C.White,3);break;}}
            string help=!string.IsNullOrEmpty(Notice)?Notice:HoverHelp;
            if(!string.IsNullOrEmpty(help)&&string.IsNullOrEmpty(s.Dialog)&&s.MenuSlot<0){p.Fill(340,889,1368,53,new Color(.01f,.065f,.05f,.98f));p.Text(1024,925,help,24,C.White,1,false,1330);}
            p.End();
        }
        private void Button(string id,Rect r,string text,MfdOperation op,int arg=0,string help="",bool selected=false,bool enabled=true,Color? color=null,float size=24,bool border=true)
        {
            Rect global=p.DisplayRect(r);Hits.Add(new MfdHit(id,global,op,arg,help,enabled));
            if(selected)p.Fill(r.x,r.y,r.width,r.height,new Color(.07f,.24f,.23f));if(border)p.Box(r.x,r.y,r.width,r.height,selected?C.Cyan:C.Line);
            p.Text(r.center.x,r.center.y+size*.34f,text,size,enabled?(color??C.Cyan):C.Muted,1,true,r.width-8);
        }
        private void Header()
        {
            var f=d.Fast;var sys=d.Systems;p.Global();p.Line(0,124,2048,124,C.Line);p.Line(0,176,2048,176,C.Line);
            p.Text(24,62,string.IsNullOrEmpty(d.Aircraft.Identity.Callsign)?f.Aircraft.Value:d.Aircraft.Identity.Callsign,35,C.White,0,true,222);p.Text(24,101,"KTEX · FLIGHTSIM",21,C.Green,0,true,220);
            Metric(260,"CAS",Speed(f.CalibratedAirspeedMps),SpeedUnit);Metric(517,"ALT / MSL",Alt(f.MeanSeaLevelAltitudeM),AltUnit);
            Metric(775,"HDG",N(f.HeadingRad*Mathf.Rad2Deg,"000"),"°");Metric(1032,"TOTAL FUEL",d.SystemsValid?Fuel(sys.Fuel.TotalFuelKg):"N/A",FuelUnit);
            p.Text(1300,49,"MASTER ARM",21,C.Green);Button("arm",new Rect(1300,60,110,40),d.CombatValid?(d.Combat.MasterArm==MasterArmState.Simulate?"SIM":d.Combat.MasterArm.ToString().ToUpperInvariant()):"N/A",MfdOperation.Arm,0,"武器保险按 SAFE → SIM → ARM 循环",false,d.CombatValid,d.CombatValid&&d.Combat.MasterArm==MasterArmState.Arm?C.Amber:C.Green,22);
            p.Text(1560,49,"GEAR",21,C.Green);p.Text(1560,94,d.SystemsValid?LowerDisplayData.GearState(sys.LandingGear):"N/A",29,LowerDisplayData.GearState(sys.LandingGear)=="TRANSIT"?C.Amber:C.Cyan,0,true,190);
            p.Text(2024,63,TimeSpan.FromSeconds(Math.Max(0,f.SimulationTimeS)).ToString(@"hh\:mm\:ss"),32,C.Cyan,2);p.Text(2024,99,"SIM TIME · "+d.Status,18,C.Muted,2);
            string status="◇ SYSTEMS NORMAL";Color statusColor=C.Green;
            if(!d.SystemsValid){status="△ SYSTEMS N/A";statusColor=C.Amber;}else if(d.Alerts.Count>0){var alert=d.Alerts[0];status=(alert.Danger?"▲ ":"△ ")+alert.Label+(d.Alerts.Count>1?" +"+(d.Alerts.Count-1):"");if(s.Acknowledged.Count==d.Alerts.Count)status+="   ACK / ACTIVE";statusColor=alert.Danger?C.Red:C.Amber;}
            Button("alerts",new Rect(23,129,1000,40),status,MfdOperation.Alerts,0,"打开告警详情；确认不会清除仍存在的故障",false,true,statusColor,22,false);
            string task=d.HasMission?"MSN / "+d.Title:"FREE FLIGHT / 自由飞行";Button("mission",new Rect(1150,129,874,40),task+"  ›",MfdOperation.Mission,0,"任务进度与检查单",false,true,C.Cyan,22,false);
        }
        private void Metric(float x,string label,string value,string unit){p.Text(x,45,label,21,C.Green,0,true,248);p.Text(x,96,value,42,C.Cyan,0,true,224);p.Text(x+228,99,unit,17,C.Muted,2,false,70);}
        private void Softbar()
        {
            p.Global();p.Fill(0,952,2048,72,new Color(.015f,.028f,.021f));p.Line(0,952,2048,952,C.Line);
            string[] tabs={"INTEG","MAP","SVS"};for(int i=0;i<3;i++)Button("page-"+i,new Rect(17+i*151,964,142,49),tabs[i],MfdOperation.Page,i,"切换显示页面",s.Page==(MfdPage)i);
            Button("night",new Rect(494,964,96,49),s.Night?"NIGHT":"DAY",MfdOperation.Night,0,"切换日间 / 夜间模式");
            Button("help",new Rect(602,964,92,49),"HELP",MfdOperation.Help,0,"中文帮助");
            p.Text(731,998,"RANGE",17,C.Green,0,true,90);
            Button("zoom-out",new Rect(825,964,50,49),"−",MfdOperation.ZoomOut,0,"扩大地图范围");p.Text(930,998,Distance(s.RangeNm*1852),23,C.Cyan,1,true,100);p.Text(1008,998,RangeUnit,17,C.Green,1,true,58);
            Button("zoom-in",new Rect(1048,964,50,49),"+",MfdOperation.ZoomIn,0,"缩小地图范围");
            Button("orientation",new Rect(1110,964,145,49),s.NorthUp?"N-UP":"HDG-UP",MfdOperation.Orientation,0,"北朝上 / 航向朝上");
            Button("units",new Rect(1267,964,148,49),s.Metric?"METRIC":"AERO",MfdOperation.Units,0,"单位切换，不改变飞行模型");
            p.Text(1440,998,"BRT",19,C.Green,0,true,67);Button("brightness-down",new Rect(1502,964,50,49),"−",MfdOperation.BrightnessDown,0,"降低亮度");p.Text(1602,998,N(s.Brightness*100),24,C.Cyan,1,true,75);Button("brightness-up",new Rect(1654,964,50,49),"+",MfdOperation.BrightnessUp,0,"提高亮度");
            p.Text(1780,998,Focused?"FOCUS":"LIVE",19,Focused?C.White:C.Muted,1,true,100);Button("reset",new Rect(1875,964,155,49),"RESET",MfdOperation.Reset,0,"恢复默认模块布局");
        }
        private void Module(MfdModule module,int slot,Rect r,bool compact,bool dock=false)
        {
            p.Global();p.Fill(r.x,r.y,r.width,r.height,new Color(0,0,0,dock?.92f:1));p.Box(r.x,r.y,r.width,r.height,C.Line,1);
            float head=dock?34:44;p.Fill(r.x,r.y,r.width,head,C.Panel);
            if(module==MfdModule.None){Button("menu-"+slot,new Rect(r.x+20,r.y+head+30,r.width-40,90),"+ ADD MODULE",MfdOperation.Menu,slot,"添加模块");return;}
            Button("menu-"+slot,new Rect(r.x+10,r.y+2,r.width-114,head-4),Titles[(int)module],MfdOperation.Menu,slot,"更换"+Names[(int)module],false,true,C.White,dock?21:23,false);
            float arrowX=r.x+r.width-137,arrowY=r.y+head*.5f;p.Line(arrowX-5,arrowY-3,arrowX,arrowY+3,C.Muted,2);p.Line(arrowX,arrowY+3,arrowX+5,arrowY-3,C.Muted,2);
            Button("expand-"+slot,new Rect(r.x+r.width-95,r.y+2,42,head-4),"□",MfdOperation.Expand,(int)module,"展开"+Names[(int)module],false,true,C.Muted,25,false);
            Button("hide-"+slot,new Rect(r.x+r.width-48,r.y+2,40,head-4),"×",MfdOperation.Hide,slot,"收起模块",false,true,C.Muted,25,false);
            RenderModule(module,new Rect(r.x+3,r.y+head+2,r.width-6,r.height-head-4),compact);
        }
        private void RenderModule(MfdModule module,Rect r,bool compact)
        {
            bool available=(module==MfdModule.Flight?d.FastValid:module==MfdModule.Tactical||module==MfdModule.Radar?d.TacticalValid:d.SystemsValid);
            if(!available){p.Global();p.Text(r.center.x,r.center.y,"DATA N/A",28,C.Muted,1);return;}
            switch(module){case MfdModule.Stores:Stores(r,compact);break;case MfdModule.Tactical:Tactical(r,false);break;case MfdModule.Systems:Systems(r,compact);break;case MfdModule.Fuel:FuelPanel(r,compact);break;case MfdModule.Flight:Flight(r,compact);break;case MfdModule.Radar:Radar(r,compact);break;case MfdModule.Wing:Wing(r,compact);break;}
        }
        private void Menu()
        {
            p.Overlay();Hits.Clear();int slot=s.MenuSlot;Rect basis=s.Page==MfdPage.Vision?new Rect(160+Array.IndexOf(new[]{3,2,5,6},slot)*432,736,432,216):Slots[Mathf.Clamp(slot,0,6)];
            float y=basis.y>600?basis.y-485:basis.y+44;float x=Mathf.Clamp(basis.x+10,10,1714);
            p.Fill(x,y,324,485,new Color(.025f,.085f,.065f));p.Box(x,y,324,485,C.Cyan);
            for(int i=0;i<7;i++)Button("assign-"+i,new Rect(x+8,y+8+i*67,308,62),Titles[i]+"  /  "+Names[i],MfdOperation.Assign,slot*10+i,"更换后自动交换已有模块",s.Layout[slot]==(MfdModule)i,true,C.White,22,false);
        }
        private void Expanded()
        {
            p.Global();p.Fill(0,176,2048,776,Color.black);p.Fill(0,176,2048,79,C.Panel);p.Text(34,228,Titles[(int)s.Expanded],30,C.White);p.Text(160,228,Names[(int)s.Expanded],24,C.Muted);
            Button("back",new Rect(1815,190,200,51),"↙ RETURN",MfdOperation.Back,0,"返回综合画面");p.Line(1468,255,1468,952,C.Line);
            RenderModule(s.Expanded,new Rect(25,273,1410,657),false);Details(s.Expanded,new Rect(1498,281,520,650));
        }
        private void DetailRow(float x,ref float y,string label,string value)
        {p.Text(x,y,label,22,C.Muted,0,false,295);p.Text(x+518,y,value,24,C.Cyan,2,true,270);p.Line(x,y+17,x+518,y+17,C.Line,1);y+=53;}
        private void Details(MfdModule type,Rect r)
        {
            p.Global();float y=r.y+24;var f=d.Systems.Fuel;var el=d.Systems.Electrical;var hy=d.Systems.Hydraulics;
            if(type==MfdModule.Fuel){p.Text(r.x,y,"FUEL QUANTITY / "+FuelUnit,22,C.Green);y+=50;DetailRow(r.x,ref y,"TOTAL",Fuel(f.TotalFuelKg));DetailRow(r.x,ref y,"INTERNAL",Fuel(f.InternalFuelKg));DetailRow(r.x,ref y,"EXTERNAL",Fuel(f.ExternalFuelKg));DetailRow(r.x,ref y,"IMBALANCE",Fuel(f.FuelImbalanceKg));DetailRow(r.x,ref y,"JOKER",Fuel(f.JokerFuelKg));DetailRow(r.x,ref y,"BINGO",Fuel(f.BingoFuelKg));DetailRow(r.x,ref y,"PUMP",f.FuelPumpEnabled?"ON":"OFF");DetailRow(r.x,ref y,"TRANSFER",f.TransferMode.ToString().ToUpperInvariant());p.Text(r.x,y+17,"左右为内部油量分布，不与总量重复相加。",20,C.Muted,0,false,520);}
            else if(type==MfdModule.Systems){p.Text(r.x,y,"POWER & HYDRAULICS",22,C.Green);y+=50;DetailRow(r.x,ref y,"MAIN BUS",N(el.MainBusVoltageV,"0.0")+" V");DetailRow(r.x,ref y,"ESSENTIAL",N(el.EssentialBusVoltageV,"0.0")+" V");DetailRow(r.x,ref y,"AVIONICS",N(el.AvionicsBusVoltageV,"0.0")+" V");DetailRow(r.x,ref y,"GENERATOR",N(el.GeneratorCurrentA)+" A");DetailRow(r.x,ref y,"HYD A",Pressure(hy.SystemAPressurePa));DetailRow(r.x,ref y,"HYD B",Pressure(hy.SystemBPressurePa));DetailRow(r.x,ref y,"ENGINE",d.Systems.Propulsion.Mode.ToString().ToUpperInvariant());DetailRow(r.x,ref y,"OIL / ECS","N/A");p.Text(r.x,y+17,"油压 / 环控尚无数据；N/A 表示不可用。",20,C.Muted,0,false,520);}
            else if(type==MfdModule.Stores){int i=SelectedStation();var station=d.Systems.Stores.Stations[i];p.Text(r.x,y,"SELECTED STATION",22,C.Green);y+=50;DetailRow(r.x,ref y,"STATION",(i+1).ToString("00"));DetailRow(r.x,ref y,"TYPE",station.Quantity>0?station.StoreType:"EMPTY");DetailRow(r.x,ref y,"QUANTITY",station.Quantity.ToString());DetailRow(r.x,ref y,"STATUS",station.IsReady?"READY":station.Quantity>0?"NOT READY":"EMPTY");DetailRow(r.x,ref y,"MASTER ARM",d.CombatValid?(d.Combat.MasterArm==MasterArmState.Simulate?"SIM":d.Combat.MasterArm.ToString().ToUpperInvariant()):"N/A");Button("detail-arm",new Rect(r.x,y+10,270,50),"CYCLE ARM",MfdOperation.Arm,0,"切换任务中的武器保险",false,d.CombatValid);p.Text(r.x,y+110,d.CombatValid?"选择挂点会提交至任务系统。":"自由飞行：挂点仅供查看。",21,C.Muted,0,false,520);}
            else if(type==MfdModule.Tactical||type==MfdModule.Radar){TargetDetails(r.x,ref y,518);Button("radar-switch",new Rect(r.x,800,240,49),d.Systems.Avionics.RadarEnabled?"RDR ON":"RDR OFF",MfdOperation.Radar,0,"切换雷达",d.Systems.Avionics.RadarEnabled);Button("link-switch",new Rect(r.x+265,800,240,49),d.Systems.Avionics.DataLinkEnabled?"D/L ON":"D/L OFF",MfdOperation.Link,0,"切换数据链",d.Systems.Avionics.DataLinkEnabled);}
            else if(type==MfdModule.Flight){var c=d.Systems.FlightControls;p.Text(r.x,y,"FLIGHT CONTROL",22,C.Green);y+=50;DetailRow(r.x,ref y,"AOA",N(d.Fast.AngleOfAttackRad*Mathf.Rad2Deg,"0.0")+"°");DetailRow(r.x,ref y,"NORMAL G",N(d.Fast.NormalLoadFactorG,"0.0"));DetailRow(r.x,ref y,"AILERON",N(c.AileronDeflectionRad*Mathf.Rad2Deg,"0.0")+"°");DetailRow(r.x,ref y,"ELEVATOR",N(c.ElevatorDeflectionRad*Mathf.Rad2Deg,"0.0")+"°");DetailRow(r.x,ref y,"RUDDER",N(c.RudderDeflectionRad*Mathf.Rad2Deg,"0.0")+"°");DetailRow(r.x,ref y,"FCC",c.FlightControlComputerEnabled?"ON":"OFF");DetailRow(r.x,ref y,"AUTOPILOT",c.AutopilotEngaged?"ON":"OFF");DetailRow(r.x,ref y,"TERRAIN",d.Fast.TerrainSampleValid?"VALID":"N/A");}
            else{p.Text(r.x,y,"FORMATION / DATALINK",22,C.Green);y+=50;DetailRow(r.x,ref y,"LINK",d.Systems.Avionics.DataLinkEnabled?"ON":"OFF");DetailRow(r.x,ref y,"WINGMAN",d.HasWing?d.Wing.Identity.Callsign:"N/A");Button("link-switch",new Rect(r.x,y+25,300,49),"TOGGLE D/L",MfdOperation.Link,0,"切换数据链");}
        }
    }
}
