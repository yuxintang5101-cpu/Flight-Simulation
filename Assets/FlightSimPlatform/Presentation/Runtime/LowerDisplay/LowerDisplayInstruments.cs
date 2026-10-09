using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using UnityEngine;
using C=FlightSim.Platform.Presentation.LowerDisplay.MfdColors;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public sealed partial class LowerDisplayComposer
    {
        private static readonly Vector2[] Airframe={new Vector2(0,-166),new Vector2(13,-119),new Vector2(19,-56),new Vector2(108,30),new Vector2(112,60),new Vector2(34,28),new Vector2(27,94),new Vector2(60,145),new Vector2(59,162),new Vector2(10,140),new Vector2(0,155),new Vector2(-10,140),new Vector2(-59,162),new Vector2(-60,145),new Vector2(-27,94),new Vector2(-34,28),new Vector2(-112,60),new Vector2(-108,30),new Vector2(-19,-56),new Vector2(-13,-119)};
        private void Plane(float x,float y,float scale,Color color)
        {for(int i=0;i<Airframe.Length;i++){var a=Airframe[i]*scale+new Vector2(x,y);var b=Airframe[(i+1)%Airframe.Length]*scale+new Vector2(x,y);p.Line(a.x,a.y,b.x,b.y,color,2);}p.Line(x,y-110*scale,x,y+130*scale,color,1);}
        private int SelectedStation()=>Mathf.Clamp(d.CombatValid?d.Combat.SelectedStationIndex:s.InspectedStation>=0?s.InspectedStation:0,0,8);
        private void Stores(Rect r,bool compact)
        {
            float h=compact?270:436;p.Region(r,640,h);int selected=SelectedStation();Plane(320,compact?129:203,compact?.49f:.84f,C.Muted);
            for(int i=0;i<9;i++)
            {
                var station=d.Systems.Stores.Stations[i];float x=compact?79+i*60.25f:64+i*64,y=compact?85:136;Color color=i==selected?C.Cyan:station.Quantity>0?C.White:C.Line;
                p.Text(x,y-28,(i+1).ToString("00"),21,C.Green,1,true,55);p.Text(x,y-5,station.Quantity>0?station.Quantity.ToString():"—",20,color,1,true,55);
                if(station.Quantity>0){float[] px={0,7,7,17,17,6,6,0,-6,-6,-17,-17,-7,-7,0};float[] py={0,17,59,75,83,79,95,102,95,79,83,75,59,17,0};for(int j=1;j<px.Length;j++)p.Line(x+px[j-1],y+py[j-1],x+px[j],y+py[j],color,2);if(i==selected)p.Line(x,y+12,x,y+95,color,8);}
                else p.Dash(x,y+18,x,y+92,color);
                if(i==selected)p.Box(x-25,y-58,50,166,C.Cyan);
                Hits.Add(new MfdHit("station-"+i,p.DisplayRect(new Rect(x-26,y-60,52,170)),MfdOperation.Station,i,"挂点 "+(i+1)+" · "+(station.Quantity>0?station.StoreType:"EMPTY")));
            }
            var store=d.Systems.Stores.Stations[selected];string name=store.Quantity>0?store.StoreType:"EMPTY";string arm=d.CombatValid?d.Combat.MasterArm.ToString().ToUpperInvariant():"N/A";
            if(!compact){p.Text(24,35,"STATIONS",20,C.Green);p.Text(612,35,"09",25,C.White,2);p.Fill(155,328,330,72,new Color(.025f,.105f,.08f));p.Box(155,328,330,72,C.Cyan);p.Text(176,356,name,27,C.Cyan);p.Text(176,383,"STA "+(selected+1).ToString("00")+"  "+(store.IsReady?"READY":store.Quantity>0?"NOT READY":"EMPTY"),20,C.Green);p.Text(464,372,store.Quantity.ToString("00"),39,C.Cyan,2);int count=0;for(int i=0;i<9;i++)count+=d.Systems.Stores.Stations[i].Quantity;p.Text(25,421,"MASTER "+arm,23,arm=="ARM"?C.Amber:C.Green);p.Text(612,421,count+" STORES",23,C.Cyan,2);}
            else{p.Text(26,247,name,26,C.Cyan);p.Text(612,247,arm,25,C.Green,2);}
        }
        private void Gauge(float x,float y,string label,double value,double max,string unit,Color color,float radius=54)
        {p.Arc(x,y,radius,180,360,new Color(.14f,.23f,.17f),7);if(LowerDisplayData.Finite(value))p.Arc(x,y,radius,180,180+180*Mathf.Clamp01((float)(value/max)),color,7);p.Text(x,y+4,N(value,label=="N2"?"0.0":"0"),31,color,1,true,190);p.Text(x,y+39,label,23,C.Green,1,true,204);p.Text(x,y+61,unit,17,C.Muted,1,false,200);}
        private void Systems(Rect r,bool compact)
        {
            p.Region(r,640,compact?270:436);var e=d.Systems.Propulsion;var h=d.Systems.Hydraulics;var el=d.Systems.Electrical;bool fire=d.Systems.Warnings.FireWarning;
            if(compact){Gauge(115,87,"N1",e.N1Percent,110,"%",C.Cyan,52);Gauge(320,87,"N2",e.N2Percent,110,"%",C.Cyan,52);Gauge(525,87,"EGT",e.ExhaustGasTemperatureC,1200,"°C",fire?C.Red:C.Cyan,52);p.Text(25,213,"MAIN BUS",21,C.Green);p.Text(215,213,N(el.MainBusVoltageV,"0.0")+" V",25,C.Cyan,2);p.Text(280,213,"HYD A/B",21,C.Green);p.Text(610,213,h.SystemAOnline&&h.SystemBOnline?"NORM":"DEGRADED",25,h.SystemAOnline&&h.SystemBOnline?C.Cyan:C.Amber,2);return;}
            Gauge(110,75,"N1",e.N1Percent,110,"%",C.Cyan);Gauge(320,75,"N2",e.N2Percent,110,"%",C.Cyan);Gauge(530,75,"EGT",e.ExhaustGasTemperatureC,1200,"°C",fire?C.Red:C.Cyan);
            Gauge(110,208,"THRUST",e.ThrustN/1000,130,"kN",C.Cyan);Gauge(320,208,"NOZZLE",e.NozzlePositionNormalized*100,100,"%",C.Cyan);Gauge(530,208,"FUEL FLOW",e.FuelFlowKgps*3600*(s.Metric?1:2.204623),9000*(s.Metric?1:2.204623),FuelUnit+"/H",C.Cyan);
            for(int i=0;i<2;i++){double value=i==0?h.SystemAPressurePa:h.SystemBPressurePa;float y=325+i*42;Color color=value<14490000?C.Amber:C.Cyan;p.Text(25,y,i==0?"HYD A":"HYD B",22,C.Green);p.Fill(143,y-22,275,19,new Color(.05f,.1f,.07f));p.Box(143,y-22,275,19,C.Line);p.Fill(143,y-22,275*Mathf.Clamp01((float)(value/20700000)),19,color);p.Text(613,y,Pressure(value),22,color,2);}
            p.Text(25,419,"ELEC",21,C.Green);p.Text(95,419,N(el.MainBusVoltageV,"0.0")+" V",24,C.Cyan);p.Text(285,419,"FCC",21,C.Green);p.Text(349,419,d.Systems.FlightControls.FlightControlComputerEnabled?"ON":"OFF",24,C.Cyan);p.Text(487,419,"GEN",21,C.Green);p.Text(612,419,el.GeneratorOnline?"ON":"OFF",24,C.Cyan,2);
        }
        private void Ring(float x,float y,float r,string value,double ratio,string label,string unit,Color color)
        {p.Circle(x,y,r+8,C.Green);p.Circle(x,y,r,new Color(.14f,.23f,.17f),8);if(ratio>0)p.Arc(x,y,r,-90,-90+360*Mathf.Clamp01((float)ratio),color,8);p.Text(x,y-9,label,21,C.Green,1,true,160);p.Text(x,y+23,value,32,color,1,true,180);p.Text(x,y+r+35,unit,18,C.Muted,1,false,150);}
        private void FuelPanel(Rect r,bool compact)
        {
            var f=d.Systems.Fuel;Color color=f.TotalFuelKg<f.BingoFuelKg?C.Amber:C.Cyan;double cap=d.InternalCapacityKg;
            if(compact){p.Region(r,640,270);Ring(105,94,58,Fuel(f.LeftFuelKg),f.LeftFuelKg/(cap/2),"LEFT",FuelUnit,color);Ring(320,94,58,Fuel(f.TotalFuelKg),f.TotalFuelKg/cap,"TOTAL",FuelUnit,color);Ring(535,94,58,Fuel(f.RightFuelKg),f.RightFuelKg/(cap/2),"RIGHT",FuelUnit,color);p.Text(26,241,"EXT",21,C.Green);p.Text(137,241,Fuel(f.ExternalFuelKg)+" "+FuelUnit,24,C.Cyan);p.Text(298,241,"BINGO",21,C.Green);p.Text(611,241,Fuel(f.BingoFuelKg)+" "+FuelUnit,25,C.Amber,2);return;}
            p.Region(r,800,600);Plane(400,295,1.42f,new Color(.17f,.29f,.21f));p.Line(400,185,240,304,C.Line);p.Line(400,185,560,304,C.Line);p.Line(240,304,400,473,C.Line);p.Line(560,304,400,473,C.Line);
            Ring(400,111,79,Fuel(f.TotalFuelKg),f.TotalFuelKg/cap,"TOTAL",FuelUnit,color);Ring(214,309,78,Fuel(f.LeftFuelKg),f.LeftFuelKg/(cap/2),"LEFT",FuelUnit,color);Ring(586,309,78,Fuel(f.RightFuelKg),f.RightFuelKg/(cap/2),"RIGHT",FuelUnit,color);Ring(400,487,55,Fuel(f.ExternalFuelKg),0,"EXT",FuelUnit,C.Muted);
            p.Text(43,66,"INTERNAL",20,C.Green);p.Text(43,101,Fuel(f.InternalFuelKg),29,C.Cyan);p.Text(759,66,"CG / % MAC",20,C.Green,2);p.Text(759,101,N(f.CenterOfGravityPercentMac,"0.0"),29,C.Cyan,2);p.Text(42,523,"JOKER",20,C.Green);p.Text(42,557,Fuel(f.JokerFuelKg),29,C.Cyan);p.Text(759,523,"BINGO",20,C.Green,2);p.Text(759,557,Fuel(f.BingoFuelKg),29,C.Amber,2);
        }
        private void Flight(Rect r,bool compact)
        {
            float height=compact?270:436,cy=compact?132:210;p.Region(r,640,height);var f=d.Fast;float pitch=(float)f.PitchRad*Mathf.Rad2Deg,roll=-(float)f.RollRad,ps=compact?3:5;
            for(int a=-20;a<=20;a+=10)
            {
                float y=(pitch-a)*ps,len=a==0?104:66;if(Mathf.Abs(y)>125)continue;
                Vector2 l=Rotate(new Vector2(-len,y),roll),li=Rotate(new Vector2(-25,y),roll),ri=Rotate(new Vector2(25,y),roll),rr=Rotate(new Vector2(len,y),roll);
                if(a<0){p.Dash(320+l.x,cy+l.y,320+li.x,cy+li.y,C.Green,2);p.Dash(320+ri.x,cy+ri.y,320+rr.x,cy+rr.y,C.Green,2);}else{p.Line(320+l.x,cy+l.y,320+li.x,cy+li.y,C.Green,2);p.Line(320+ri.x,cy+ri.y,320+rr.x,cy+rr.y,C.Green,2);}
                if(a!=0){p.Text(320+l.x-12,cy+l.y+6,Math.Abs(a).ToString(),18,C.Green,2,true,50);p.Text(320+rr.x+12,cy+rr.y+6,Math.Abs(a).ToString(),18,C.Green,0,true,50);}
            }
            p.Circle(320,cy,7,C.Cyan,2);p.Line(281,cy,310,cy,C.Cyan,3);p.Line(330,cy,359,cy,C.Cyan,3);p.Line(320,cy-15,320,cy-7,C.Cyan,2);
            float speed=(float)(f.CalibratedAirspeedMps*(s.Metric?3.6:1.943844)),alt=(float)(f.MeanSeaLevelAltitudeM*(s.Metric?1:3.28084));float top=compact?63:98,bottom=compact?212:327,step=compact?33:48;
            p.Line(116,top,116,bottom,C.White);p.Line(524,top,524,bottom,C.White);
            for(int i=-2;i<=2;i++){float sv=Mathf.Round(speed/10)*10+i*10,av=Mathf.Round(alt/100)*100+i*100,sy=cy-(sv-speed)/10*step,ay=cy-(av-alt)/100*step;if(sy>=top&&sy<=bottom){p.Line(105,sy,116,sy,C.White);if(Mathf.Abs(sy-cy)>40)p.Text(93,sy+6,N(sv),18,C.White,2,true,90);}if(ay>=top&&ay<=bottom){p.Line(524,ay,535,ay,C.White);if(Mathf.Abs(ay-cy)>40)p.Text(549,ay+6,N(av),18,C.White,0,true,90);}}
            p.Fill(25,cy-23,99,44,new Color(0,.06f,.04f));p.Box(25,cy-23,99,44,C.Cyan);p.Text(111,cy+9,N(speed),29,C.Cyan,2,true,90);p.Fill(515,cy-23,112,44,new Color(0,.06f,.04f));p.Box(515,cy-23,112,44,C.Cyan);p.Text(616,cy+9,N(alt,"#,0"),27,C.Cyan,2,true,105);
            p.Text(25,compact?28:37,"CAS / "+SpeedUnit,19,C.Green);p.Text(614,compact?28:37,"MSL / "+AltUnit,19,C.Green,2);p.Text(320,compact?39:48,N(f.HeadingRad*Mathf.Rad2Deg,"000")+"°",28,C.Cyan,1);
            p.Text(25,height-14,"M "+N(f.Mach,"0.00"),21,C.Green);p.Text(320,height-14,"G "+N(f.NormalLoadFactorG,"0.0"),21,C.Green,1);p.Text(615,height-14,"AGL "+(f.TerrainSampleValid&&f.TerrainSampleAgeS<2?Alt(f.AboveGroundLevelAltitudeM):"N/A"),21,C.Cyan,2);
            if(!compact){p.Text(25,height-58,"AOA "+N(f.AngleOfAttackRad*Mathf.Rad2Deg,"0.0")+"°",22,C.Green);p.Text(615,height-58,"VS "+N(f.ClimbRateMps*(s.Metric?60:196.8504)),22,C.Cyan,2);}
        }
        private static Vector2 Rotate(Vector2 v,float radians)=>new Vector2(v.x*Mathf.Cos(radians)-v.y*Mathf.Sin(radians),v.x*Mathf.Sin(radians)+v.y*Mathf.Cos(radians));
        private Vector2 Relative(double lon,double lat)=>new Vector2((float)((lon-d.Fast.LongitudeRad)*6378137*Math.Cos(d.Fast.LatitudeRad)),(float)((lat-d.Fast.LatitudeRad)*6378137));
        private Vector2 TrackRelative(in TacticalTrack track)
        {
            var rel=new DVector3(track.EcefPositionXM-d.Fast.EcefPositionXM,track.EcefPositionYM-d.Fast.EcefPositionYM,track.EcefPositionZM-d.Fast.EcefPositionZM);var basis=GeoMath.CreateEnuBasis(d.Fast.LongitudeRad,d.Fast.LatitudeRad);return new Vector2((float)DVector3.Dot(rel,basis.East),(float)DVector3.Dot(rel,basis.North));
        }
        private bool TrackVisible(in TacticalTrack t)
        {if(!d.TacticalValid||!t.IsValid)return false;return t.Affiliation==TacticalTrackAffiliation.Friendly?d.Systems.Avionics.DataLinkEnabled:d.Systems.Avionics.RadarEnabled&&t.RangeM<=d.Systems.Avionics.RadarRangeM;}
        private bool SelectedTrack(out TacticalTrack selected)
        {for(int i=0;i<d.Aircraft.Tactical.Tracks.Length;i++){var track=d.Aircraft.Tactical.Tracks[i];if(track.TrackId==s.SelectedTrack&&TrackVisible(track)){selected=track;return true;}}selected=default;return false;}
        private Vector2 MapPoint(Vector2 en,float cx,float cy,float radius)
        {float heading=s.NorthUp?0:(float)d.Fast.HeadingRad;Vector2 local=Rotate(en,heading);return new Vector2(cx+local.x*radius/(s.RangeNm*1852),cy-local.y*radius/(s.RangeNm*1852));}
        private readonly System.Collections.Generic.List<Rect> trackLabels=new System.Collections.Generic.List<Rect>();
        private void TrackSymbol(Vector2 point,in TacticalTrack track,bool small=false,string context="tsd",float width=640,float height=436)
        {
            float x=point.x,y=point.y,size=small?10:14;Color color=track.Affiliation==TacticalTrackAffiliation.Hostile?C.Red:track.Affiliation==TacticalTrackAffiliation.Friendly?C.Green:C.White;
            if(track.Affiliation==TacticalTrackAffiliation.Hostile){p.Line(x,y-size,x+size,y,color,2.5f);p.Line(x+size,y,x,y+size,color,2.5f);p.Line(x,y+size,x-size,y,color,2.5f);p.Line(x-size,y,x,y-size,color,2.5f);}
            else if(track.Affiliation==TacticalTrackAffiliation.Friendly){p.Line(x,y-size,x+size,y+size*.75f,color,2.5f);p.Line(x+size,y+size*.75f,x,y+size*.25f,color,2.5f);p.Line(x,y+size*.25f,x-size,y+size*.75f,color,2.5f);p.Line(x-size,y+size*.75f,x,y-size,color,2.5f);}else p.Box(x-size*.8f,y-size*.8f,size*1.6f,size*1.6f,color,2);
            if(track.TrackId==s.SelectedTrack)p.Box(x-24,y-24,48,48,C.Cyan);// Separate labels for close formation contacts without moving their geographic symbols.
            for(int attempt=0;attempt<8;attempt++)
            {
                float lx=attempt<4?x+26:x-113,ly=y+5+(attempt%4==0?0:attempt%4==1?-34:attempt%4==2?34:-68);
                var label=new Rect(lx,ly-25,90,30);if(label.x<18||label.xMax>width-18||label.y<50||label.yMax>height-45)continue;
                bool overlap=false;foreach(var used in trackLabels)if(used.Overlaps(label)){overlap=true;break;}if(overlap)continue;
                trackLabels.Add(label);if(attempt>0)p.Line(x,y,attempt<4?label.x:label.xMax,ly-9,color,1);
                p.Text(lx,ly,"T"+track.TrackId.ToString("00"),small?20:23,color,0,true,90);
                Hits.Add(new MfdHit(context+"-label-"+track.TrackId,p.DisplayRect(label),MfdOperation.Target,track.TrackId,"选择目标 T"+track.TrackId.ToString("00")));break;
            }
            Hits.Add(new MfdHit(context+"-track-"+track.TrackId,p.DisplayRect(new Rect(x-22,y-22,44,44)),MfdOperation.Target,track.TrackId,"选择目标 T"+track.TrackId.ToString("00")));
        }
        private void Tactical(Rect r,bool map)
        {
            float w=r.width,h=r.height;p.Region(r,w,h);trackLabels.Clear();float cx=w*.5f,cy=h*(map?.53f:.49f),radius=h*(map?.43f:.37f),heading=s.NorthUp?0:(float)d.Fast.HeadingRad*Mathf.Rad2Deg;
            for(int i=1;i<=4;i++)p.Circle(cx,cy,radius*i/4,C.Line,i==4?1.8f:1.2f);p.Line(cx,cy-radius,cx,cy+radius,C.Line);p.Line(cx-radius,cy,cx+radius,cy,C.Line);
            for(int bearing=0;bearing<360;bearing+=10){float a=(bearing-heading-90)*Mathf.Deg2Rad;Vector2 unit=new Vector2(Mathf.Cos(a),Mathf.Sin(a));p.Line(cx+unit.x*(radius-7),cy+unit.y*(radius-7),cx+unit.x*radius,cy+unit.y*radius,C.Muted);if(bearing%30==0)p.Text(cx+unit.x*(radius+20),cy+unit.y*(radius+20)+7,bearing==0?"N":bearing.ToString("000"),h<300?17:20,C.Muted,1,false,74);}
            Vector2 previous=default;bool previousValid=false;
            foreach(var wp in d.Waypoints){var point=MapPoint(Relative(wp.Longitude,wp.Latitude),cx,cy,radius);bool inside=point.x>35&&point.x<w-75&&point.y>70&&point.y<h-70;if(previousValid&&inside)p.Dash(previous.x,previous.y,point.x,point.y,C.Green,2);if(inside){p.Circle(point.x,point.y,wp.Active?7:4,C.Green,wp.Active?5:1.5f);if(map||wp.Active||d.Waypoints.Count<2)p.Text(point.x-18,point.y+22,wp.Name,22,wp.Active?C.Green:C.Muted,2,true,180);}previous=point;previousValid=inside;}
            for(int i=0;i<d.Aircraft.Tactical.Tracks.Length;i++){var track=d.Aircraft.Tactical.Tracks[i];if(!TrackVisible(track))continue;var point=MapPoint(TrackRelative(track),cx,cy,radius);if(point.x>32&&point.x<w-114&&point.y>75&&point.y<h-75)TrackSymbol(point,track,h<300,"tsd",w,h);}
            float ownAngle=s.NorthUp?(float)d.Fast.HeadingRad:0;Vector2[] points={new Vector2(0,-19),new Vector2(7,-3),new Vector2(18,7),new Vector2(5,5),new Vector2(5,19),new Vector2(0,15),new Vector2(-5,19),new Vector2(-5,5),new Vector2(-18,7),new Vector2(-7,-3)};for(int i=0;i<points.Length;i++){Vector2 a=Rotate(points[i],ownAngle),b=Rotate(points[(i+1)%points.Length],ownAngle);p.Line(cx+a.x,cy+a.y,cx+b.x,cy+b.y,C.Cyan,3);}
            p.Text(24,34,Distance(s.RangeNm*1852)+" "+RangeUnit,25,C.Cyan);p.Text(w-24,34,s.NorthUp?"N-UP":"HDG-UP",22,C.White,2);p.Text(24,h-23,"D/L "+(d.Systems.Avionics.DataLinkEnabled?"ON":"OFF"),20,d.Systems.Avionics.DataLinkEnabled?C.Green:C.Muted);
            if(SelectedTrack(out var selected)){Vector2 en=TrackRelative(selected);p.Text(w-24,h-23,"T"+selected.TrackId.ToString("00")+"  "+N((Math.Atan2(en.x,en.y)*Mathf.Rad2Deg+360)%360,"000")+"° / "+Distance(en.magnitude)+" "+RangeUnit,24,C.Cyan,2,true,w-200);}
        }
        private void Radar(Rect r,bool compact)
        {
            float h=compact?270:436;p.Region(r,640,h);trackLabels.Clear();if(!d.Systems.Avionics.RadarEnabled){p.Text(320,h/2,"RADAR OFF",32,C.Muted,1);p.Text(320,h/2+38,"NO SENSOR DATA",22,C.Muted,1);return;}
            float left=75,right=568,top=compact?42:52,bottom=compact?228:374;double range=s.RangeNm*1852;
            for(int i=0;i<=4;i++){float y=Mathf.Lerp(top,bottom,i/4f);p.Dash(left,y,right,y,C.Line,1);p.Text(left-12,y+6,Distance(range*(1-i/4.0)),18,C.Muted,2,true,70);float x=Mathf.Lerp(left,right,i/4f);p.Dash(x,top,x,bottom,C.Line,1);p.Text(x,bottom+28,(-60+i*30).ToString(),18,C.Muted,1,true,60);}
            p.Text(25,26,"RWS "+Distance(range)+" "+RangeUnit,20,C.Cyan);p.Text(612,26,"±60°",20,C.Green,2);int count=0;
            for(int i=0;i<d.Aircraft.Tactical.Tracks.Length;i++){var t=d.Aircraft.Tactical.Tracks[i];if(!TrackVisible(t)||t.Affiliation==TacticalTrackAffiliation.Friendly)continue;Vector2 rel=TrackRelative(t);float bearing=Mathf.DeltaAngle((float)d.Fast.HeadingRad*Mathf.Rad2Deg,Mathf.Atan2(rel.x,rel.y)*Mathf.Rad2Deg);if(Mathf.Abs(bearing)>60||rel.magnitude>range)continue;TrackSymbol(new Vector2(left+(bearing+60)/120*(right-left),bottom-(float)(rel.magnitude/range)*(bottom-top)),t,true,"radar",640,h);count++;}
            if(!compact){p.Text(28,419,"VISIBLE TRACKS",21,C.Green);p.Text(615,419,count.ToString(),27,C.Cyan,2);}
        }
        private void Wing(Rect r,bool compact)
        {
            float h=compact?270:436;p.Region(r,640,h);if(!d.HasWing){p.Text(320,h/2-5,"NO D/L CONTACT",30,C.Muted,1);p.Text(320,h/2+34,"WINGMAN DATA UNAVAILABLE",21,C.Muted,1);return;}
            var wing=d.Wing;Plane(115,compact?126:174,compact?.49f:.73f,C.Green);p.Text(225,compact?47:58,wing.Identity.Callsign,30,C.Cyan);p.Text(226,compact?82:98,"FORMATION / DATALINK",20,C.Green);
            float y=compact?128:165,step=compact?43:59;string[] names={"RANGE","FUEL","ALT"};string[] values={Distance(Relative(wing.Fast.LongitudeRad,wing.Fast.LatitudeRad).magnitude)+" "+RangeUnit,wing.HasSystems&&Math.Abs(wing.Systems.SimulationTimeS-d.Fast.SimulationTimeS)<2?Fuel(wing.Systems.Fuel.TotalFuelKg)+" "+FuelUnit:"N/A",Alt(wing.Fast.MeanSeaLevelAltitudeM)+" "+AltUnit};for(int i=0;i<3;i++){p.Text(225,y+i*step,names[i],22,C.Green);p.Text(608,y+i*step,values[i],25,C.Cyan,2);}
            if(!compact){p.Text(30,387,"LINK",22,C.Green);p.Text(225,387,"CONNECTED",27,C.Cyan);}
        }
    }
}
