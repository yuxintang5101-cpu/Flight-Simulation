using System;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Core;
using UnityEngine;
using C=FlightSim.Platform.Presentation.LowerDisplay.MfdColors;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public sealed partial class LowerDisplayComposer
    {
        private void TargetDetails(float x,ref float y,float width)
        {
            p.Text(x,y,"SELECTED CONTACT",22,C.Green);y+=48;
            if(!SelectedTrack(out var target)){p.Text(x,y,d.TacticalValid?"SELECT A CONTACT":"TSD DATA N/A",23,C.Muted);y+=50;return;}
            Vector2 relative=TrackRelative(target);GeoMath.EcefToLla(new DVector3(target.EcefPositionXM,target.EcefPositionYM,target.EcefPositionZM),out _,out _,out double height);
            string[] labels={"TRACK","IDENT","BEARING","RANGE","ALT / ELLIP","SPEED"};
            double speed=Math.Sqrt(target.EcefVelocityXMps*target.EcefVelocityXMps+target.EcefVelocityYMps*target.EcefVelocityYMps+target.EcefVelocityZMps*target.EcefVelocityZMps);
            string[] values={"T"+target.TrackId.ToString("00"),target.Affiliation.ToString().ToUpperInvariant(),N((Math.Atan2(relative.x,relative.y)*Mathf.Rad2Deg+360)%360,"000")+"°",Distance(relative.magnitude)+" "+RangeUnit,Alt(height)+" "+AltUnit,Speed(speed)+" "+SpeedUnit};
            for(int i=0;i<labels.Length;i++){p.Text(x,y,labels[i],21,C.Muted,0,false,width*.52f);p.Text(x+width,y,values[i],23,C.Cyan,2,true,width*.54f);p.Line(x,y+16,x+width,y+16,C.Line,1);y+=49;}
        }
        private void MapPage()
        {
            p.Image(MapTexture,new Rect(0,176,1608,776));p.Global();p.Fill(1608,176,440,776,new Color(0,.018f,.01f));p.Line(1608,176,1608,952,C.Line);Tactical(new Rect(0,176,1608,776),true);p.Global();p.Fill(25,232,408,42,new Color(0,0,0,.7f));p.Text(38,262,"KTEX / TERRAIN · ELEVATION",21,C.Muted,0,true,390);
            p.Text(1637,234,"TACTICAL MAP",27,C.White);float y=286;TargetDetails(1637,ref y,382);int count=0,visible=0;for(int i=0;i<d.Aircraft.Tactical.Tracks.Length;i++)if(TrackVisible(d.Aircraft.Tactical.Tracks[i]))visible++;
            int pages=Mathf.Max(1,(visible+3)/4);s.ContactPage=Mathf.Clamp(s.ContactPage,0,pages-1);int skip=s.ContactPage*4;
            for(int i=0;i<d.Aircraft.Tactical.Tracks.Length&&count<4;i++){var t=d.Aircraft.Tactical.Tracks[i];if(!TrackVisible(t))continue;if(skip-->0)continue;Button("list-track-"+t.TrackId,new Rect(1637,y+12+count*58,382,50),"T"+t.TrackId.ToString("00")+" / "+t.Affiliation.ToString().ToUpperInvariant()+" ›",MfdOperation.Target,t.TrackId,"选择地图目标",s.SelectedTrack==t.TrackId,true,C.Cyan,22);count++;}
            if(pages>1){Button("contacts-prev",new Rect(1637,881,108,32),"‹",MfdOperation.ContactPage,s.ContactPage-1,"上一页目标",false,s.ContactPage>0);p.Text(1827,905,(s.ContactPage+1)+" / "+pages,20,C.White,1);Button("contacts-next",new Rect(1910,881,108,32),"›",MfdOperation.ContactPage,s.ContactPage+1,"下一页目标",false,s.ContactPage<pages-1);}
            if(count==0)p.Text(1637,y+45,d.TacticalValid?"NO CONTACTS":"TSD DATA N/A",23,C.Muted);p.Text(1637,940,"斜线区：地形无覆盖",18,C.Muted,0,false,382);
        }
        private void VisionPage()
        {
            p.Image(VisionTexture,new Rect(0,176,2048,776));p.Global();p.Fill(24,197,315,67,new Color(0,0,0,.6f));p.Text(37,224,"SVS / EXTERNAL VIEW",21,C.Muted);p.Text(37,250,"LIVE SCENE · KTEX",18,C.Muted);
            if(VisionTexture==null)p.Text(1024,460,"CAMERA N/A",36,C.Amber,1);
            float cx=1024,cy=564,focal=776/(2*Mathf.Tan(55*Mathf.Deg2Rad/2)),pitch=(float)d.Fast.PitchRad,roll=-(float)d.Fast.RollRad;
            for(int angle=-20;angle<=20;angle+=10){float y=Mathf.Tan(pitch-angle*Mathf.Deg2Rad)*focal;float half=angle==0?168:108;Vector2 a=Rotate(new Vector2(-half,y),roll),b=Rotate(new Vector2(-36,y),roll),c=Rotate(new Vector2(36,y),roll),e=Rotate(new Vector2(half,y),roll);if(cy+a.y<278||cy+a.y>696||cy+e.y<278||cy+e.y>696)continue;if(angle<0){p.Dash(cx+a.x,cy+a.y,cx+b.x,cy+b.y,C.Green,2);p.Dash(cx+c.x,cy+c.y,cx+e.x,cy+e.y,C.Green,2);}else{p.Line(cx+a.x,cy+a.y,cx+b.x,cy+b.y,C.Green,2);p.Line(cx+c.x,cy+c.y,cx+e.x,cy+e.y,C.Green,2);}if(angle!=0){p.Text(cx+a.x-16,cy+a.y+6,Math.Abs(angle).ToString(),24,C.Green,2,true,70);p.Text(cx+e.x+16,cy+e.y+6,Math.Abs(angle).ToString(),24,C.Green,0,true,70);}}
            p.Circle(cx,cy,10,C.Cyan,2);p.Line(cx-60,cy,cx-15,cy,C.Cyan,3);p.Line(cx+15,cy,cx+60,cy,C.Cyan,3);p.Line(cx,cy-24,cx,cy-12,C.Cyan,2);
            p.Text(cx,240,N(d.Fast.HeadingRad*Mathf.Rad2Deg,"000")+"°",36,C.Cyan,1);p.Text(620,310,"CAS / "+SpeedUnit,24,C.Green);p.Text(1425,310,"MSL / "+AltUnit,24,C.Green,2);
            p.Fill(615,cy-29,160,58,new Color(0,.04f,.028f,.85f));p.Box(615,cy-29,160,58,C.Cyan);p.Text(695,cy+14,Speed(d.Fast.CalibratedAirspeedMps),38,C.Cyan,1,true,155);
            p.Fill(1270,cy-29,187,58,new Color(0,.04f,.028f,.85f));p.Box(1270,cy-29,187,58,C.Cyan);p.Text(1363,cy+14,Alt(d.Fast.MeanSeaLevelAltitudeM),35,C.Cyan,1,true,178);
            p.Text(620,704,"AOA "+N(d.Fast.AngleOfAttackRad*Mathf.Rad2Deg,"0.0")+"°",25,C.Green);p.Text(1425,704,"AGL "+(d.Fast.TerrainSampleValid?Alt(d.Fast.AboveGroundLevelAltitudeM):"N/A"),25,C.Cyan,2);
            int[] slots={3,2,5,6};for(int i=0;i<4;i++)Module(s.Layout[slots[i]],slots[i],new Rect(160+i*432,736,432,216),true,true);
        }
        private void Pager(int count,int size,float y)
        {
            int pages=Mathf.Max(1,(count+size-1)/size);if(pages<=1)return;
            Button("detail-prev",new Rect(1070,y,190,38),"‹ PREV",MfdOperation.DetailPage,s.DetailPage-1,"上一页",false,s.DetailPage>0,C.Cyan,20);
            p.Text(1360,y+28,(s.DetailPage+1)+" / "+pages,22,C.White,1);
            Button("detail-next",new Rect(1450,y,250,38),"NEXT ›",MfdOperation.DetailPage,s.DetailPage+1,"下一页",false,s.DetailPage<pages-1,C.Cyan,20);
        }
        private void Dialog()
        {
            p.Overlay();Hits.Clear();p.Fill(0,176,2048,776,new Color(0,0,0,.94f));p.Fill(294,195,1460,740,new Color(.019f,.054f,.035f));p.Box(294,195,1460,740,C.Muted);
            string title=s.Dialog=="alerts"?"ICAWS / 告警详情":s.Dialog=="mission"?"MISSION / 任务与检查单":"HELP / 下显示器操作";p.Text(324,244,title,30,C.White);Button("dialog-back",new Rect(1500,211,225,49),"× CLOSE",MfdOperation.Back,0,"关闭窗口");p.Line(294,278,1754,278,C.Line);
            if(s.Dialog=="alerts")
            {
                if(d.Alerts.Count==0)p.Text(1024,520,d.SystemsValid?"◇ SYSTEMS NORMAL":"SYSTEMS DATA N/A",34,d.SystemsValid?C.Green:C.Amber,1);
                s.DetailPage=Mathf.Clamp(s.DetailPage,0,Mathf.Max(0,(d.Alerts.Count-1)/6));
                for(int row=0;row<6&&s.DetailPage*6+row<d.Alerts.Count;row++){var alert=d.Alerts[s.DetailPage*6+row];float y=326+row*78;Color color=alert.Danger?C.Red:C.Amber;p.Text(330,y+17,alert.Danger?"▲":"△",34,color);p.Text(400,y,alert.Label,26,color);p.Text(400,y+30,s.Acknowledged.Contains(alert.Id)?"ACK / ACTIVE":"UNACKNOWLEDGED",18,C.Muted);p.Text(795,y+7,alert.Chinese,23,C.White,0,false,900);p.Line(330,y+54,1720,y+54,C.Line,1);}
                Pager(d.Alerts.Count,6,810);
                bool all=d.Alerts.Count>0&&s.Acknowledged.Count==d.Alerts.Count;Button("ack",new Rect(1230,859,494,49),all?"ACKNOWLEDGED / 有效故障仍保留":"ACK ALL / 确认全部",MfdOperation.Acknowledge,0,"确认不改变飞机故障状态",false,d.Alerts.Count>0&&!all,C.Cyan,23);
            }
            else if(s.Dialog=="mission")
            {
                p.Text(330,326,d.Title,28,C.Cyan,0,true,1300);float y=377;
                if(d.HasMission){p.Text(330,y,d.Mission.Mission.Phase.ToString().ToUpperInvariant()+" / "+N(d.Mission.Mission.ElapsedTimeS)+" S",22,C.Green);y+=47;s.DetailPage=Mathf.Clamp(s.DetailPage,0,Mathf.Max(0,(d.Mission.Objectives.Length-1)/3));for(int row=0;row<3&&s.DetailPage*3+row<d.Mission.Objectives.Length;row++){var objective=d.Mission.Objectives[s.DetailPage*3+row];p.Text(330,y,objective.DisplayName,23,C.White,0,false,980);p.Text(1717,y,objective.Status.ToString().ToUpperInvariant()+"  "+N(objective.ProgressNormalized*100)+"%",21,C.Cyan,2,true,460);y+=38;}Pager(d.Mission.Objectives.Length,3,539);}
                else p.Text(330,y,"FREE FLIGHT / 无任务目标",24,C.Green);
                p.Text(330,596,"MANUAL CHECKLIST / 勾选仅记录核对，不操纵飞机",21,C.Muted);
                string[] checklist={"核对航路、当前航点与飞行高度","检查油量与返航阈值","检查发动机、液压和电气状态","确认起落架、武器保险与数据链"};
                for(int i=0;i<4;i++)Button("check-"+i,new Rect(330,617+i*66,1384,57),(s.Checklist.Contains(i)?"☑":"□")+"  "+(i+1).ToString("00")+"  "+checklist[i],MfdOperation.Check,i,"手动检查记录",s.Checklist.Contains(i),true,C.White,24);
            }
            else
            {
                string[] lines={"F6：打开 / 关闭放大的下显示器；双击座舱屏幕也可放大。","显示器模式：方向键或摇杆 POV 帽移动白色焦点框，Enter / 按钮 2 确认。","Backspace / 按钮 3 返回；PgUp / PgDn 切页；+ / − 改变地图范围。","摇杆按钮 5：进入 / 退出显示器模式。可在“控制器设置 → 下显示器”重新绑定。","显示器模式会占用摇杆按钮和 POV；俯仰、横滚及油门轴继续操纵飞机。","青色：数据 / 选择；绿色：固定标签；橙色：注意；红色：危险。","告警确认后仍存在的故障继续显示。断电时屏幕变黑，飞行控制保持可用。","SYS 的油压 / 环控无模型数据，显示 N/A。地图斜线区无缓存地形。","地图选中目标只改变查看对象；挂点与保险操作提交至当前任务系统。"};
                for(int i=0;i<lines.Length;i++)p.Text(330,330+i*62,lines[i],25,i==5?C.Green:C.White,0,false,1390);
            }
        }
    }
}
