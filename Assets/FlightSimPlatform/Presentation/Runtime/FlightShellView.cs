using System;
using System.Text;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Unity;
using FlightSim.Platform.Unity.Controls;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FlightSim.Platform.Presentation
{
    /// <summary>Presentation only. All navigation and simulation ownership live in FlightSessionController.</summary>
    public sealed class FlightShellView : MonoBehaviour
    {
        private FlightSessionController session;
        private Canvas canvas;
        private RectTransform surface;
        private Font font;
        private Text connection, live, message, axisValue, objectives;
        private Image axisFill;
        private int missionIndex, axisIndex, controlTab;
        private float refreshAt;
        private static readonly Color Background = new Color(.025f, .049f, .072f, 1);
        private static readonly Color PanelColor = new Color(.048f, .083f, .111f, 1);
        private static readonly Color Accent = new Color(.24f, .9f, .76f, 1);
        private static readonly Color Muted = new Color(.51f, .64f, .7f, 1);
        private static readonly Color White = new Color(.89f, .95f, .97f, 1);
        private static readonly string[] AxisNames = { "俯仰 / PITCH", "横滚 / ROLL", "方向舵 / YAW", "左油门 / THROTTLE L", "右油门 / THROTTLE R", "轮刹 / BRAKE" };
        private static readonly string[] ActionNames = { "切换视角", "起落架", "减速板（按住）", "轮刹（按住）", "启动发动机（按住）", "切换目标", "切换武器", "主武器开关", "武器释放", "自动 / 手动", "方向舵左（按住）", "方向舵右（按住）" };
        private static readonly string[] DisplayActionNames = { "打开 / 关闭下显示器", "焦点上", "焦点下", "焦点左", "焦点右", "确认", "返回", "下一显示页", "上一显示页", "缩小地图范围", "扩大地图范围" };

        public FlightSessionController Session => session;
        public void Initialize(FlightSessionController controller)
        {
            session = controller;
            font = Resources.Load<Font>("Fonts/NotoSansSC-Regular") ?? Resources.Load<Font>("Fonts/NotoSansSC") ?? Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            var root = new GameObject("FlightShell", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            root.transform.SetParent(transform, false);
            canvas = root.GetComponent<Canvas>(); canvas.renderMode = RenderMode.ScreenSpaceOverlay; canvas.sortingOrder = 300;
            var scaler = root.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1600, 900); scaler.matchWidthOrHeight = .5f;
            if (FindObjectOfType<EventSystem>() == null) new GameObject("FlightShellEventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            session.ScreenChanged += Rebuild;
            session.Input.Changed += OnInputChanged;
            Rebuild();
        }
        private void OnInputChanged() { if(session.Screen==FlightScreen.Controls) Rebuild(); }
        private void OnDestroy() { if (session != null) { session.ScreenChanged -= Rebuild; if(session.Input!=null)session.Input.Changed-=OnInputChanged; } }
        public void Rebuild()
        {
            if (canvas == null) return;
            foreach (Transform child in canvas.transform) { child.gameObject.SetActive(false); Destroy(child.gameObject); }
            live = message = axisValue = connection = objectives = null; axisFill = null;
            bool flight = session.Screen == FlightScreen.Flying;
            var bg = Box(canvas.transform, 0, 0, 1600, 900, flight ? Color.clear : Background, "Background");
            bg.anchorMin = Vector2.zero; bg.anchorMax = Vector2.one; bg.offsetMin = bg.offsetMax = Vector2.zero;
            bg.GetComponent<Image>().raycastTarget = !flight;
            surface = new GameObject("DesignSurface", typeof(RectTransform)).GetComponent<RectTransform>();
            surface.SetParent(canvas.transform, false); surface.anchorMin = surface.anchorMax = surface.pivot = new Vector2(.5f, .5f); surface.sizeDelta = new Vector2(1600, 900);
            if (flight) { BuildFlight(); return; }
            Label(surface, "FLIGHTSIM  /  飞行仿真", 64, 28, 700, 45, 25, White, true);
            Label(surface, "KTEX  ·  COLORADO     /     SIMULATION LAB", 895, 35, 640, 30, 17, Muted, false, TextAnchor.MiddleRight);
            Box(surface, 64, 90, 1472, 1, new Color(.18f, .27f, .31f));
            connection = Label(surface, "", 64, 842, 1080, 32, 17, Muted);
            Label(surface, "ESC  返回 / 暂停", 1270, 842, 266, 32, 17, Muted, false, TextAnchor.MiddleRight);
            switch (session.Screen)
            {
                case FlightScreen.Home: BuildHome(); break;
                case FlightScreen.FreeFlight: BuildFree(); break;
                case FlightScreen.Missions: BuildMissions(); break;
                case FlightScreen.Controls: BuildControls(); break;
                case FlightScreen.Paused: BuildPause(); break;
                case FlightScreen.Debrief: BuildDebrief(); break;
            }
            if (!string.IsNullOrEmpty(session.LastError)) Label(surface, session.LastError, 64, 797, 1460, 38, 19, new Color(1,.6f,.4f));
        }
        private void Heading(string index, string title, string subtitle)
        {
            Label(surface, index, 64, 123, 1460, 26, 16, Accent, true);
            Label(surface, title, 64, 159, 1380, 65, 43, White, true);
            Label(surface, subtitle, 64, 234, 1472, 45, 21, Muted);
        }
        private void BuildHome()
        {
            Heading("01 / FLIGHT OPERATIONS", "选择你的下一次飞行", "自由探索，或接受任务。控制器配置与任务资料独立保存，可持续扩展。");
            var a = Box(surface,64,324,718,334,PanelColor);
            Box(a,0,0,4,334,Accent);
            Label(a,"01    /    FREE FLIGHT",30,27,620,32,17,Accent,true);
            Label(a,"自由飞行",30,79,620,64,37,White,true);
            Label(a,"选择冷舱、跑道待命或空中起飞。\n练习操纵、熟悉座舱，按自己的节奏飞行。",30,159,650,75,22,Muted);
            Button(a,"设置飞行   →",30,260,650,48,()=>session.Show(FlightScreen.FreeFlight),true);
            var b=Box(surface,810,324,726,334,PanelColor);
            Label(b,"02    /    MISSION CENTER",30,27,650,32,17,Accent,true);
            Label(b,"任务中心",30,79,650,64,37,White,true);
            Label(b,"阅读简报、明确目标，选择人工或自动执行。\n任务结束后查看结果与飞行记录。",30,159,665,75,22,Muted);
            Button(b,"选择任务   →",30,260,666,48,()=>session.Show(FlightScreen.Missions),true);
            Button(surface,"控制器与校准",64,706,345,58,session.OpenControls);
            Label(surface,$"{session.Catalog.Missions.Count:00} 个可用任务   /   100 Hz 仿真核心   /   HOTAS WARTHOG",449,710,1080,50,20,Muted);
        }
        private void BuildFree()
        {
            Heading("02 / FREE FLIGHT", "自由飞行设置", "机场：KTEX Telluride   ·   机型：F-16 仿真模型 / F-35 视觉外形");
            string[] titles={"冷舱启动","跑道待命","空中进入"};
            string[] descriptions={"发动机关闭，系统待启动。\n适合熟悉电源与启动流程。\n\nP  接通系统 / I  启动发动机","位于跑道，系统已就绪。\n增加油门，开始滑跑与起飞。\n\n推荐第一次手动试飞使用。","在机场上空进入飞行。\n立即练习转弯、爬升与操纵。\n\n起始速度约 350 节。"};
            for(int i=0;i<3;i++)
            {
                int chosen=i; bool selected=(int)session.FreePreset==i;
                var card=Box(surface,64+i*499,327,474,338,PanelColor);
                Box(card,0,0,474,3,selected?Accent:Muted);
                Label(card,$"0{i+1}",24,24,400,30,17,Accent,true);
                Label(card,titles[i],24,77,420,50,29,White,true);
                Label(card,descriptions[i],24,144,426,124,20,Muted);
                Button(card,selected?"已选择":"选择",24,281,426,40,()=>{session.FreePreset=(StartupPreset)chosen;Rebuild();},selected);
            }
            Button(surface,"返回",64,724,210,57,session.Home);
            Button(surface,"控制器设置",298,724,280,57,session.OpenControls);
            Button(surface,"进入座舱   →",1168,724,368,57,()=>session.BeginFreeFlight(),true);
        }
        private void BuildMissions()
        {
            Heading("03 / MISSION CENTER", "选择任务", "人工操纵用于飞行训练；自动运行用于观察流程和验证任务。");
            var missions=session.Catalog.Missions; missionIndex=Mathf.Clamp(missionIndex,0,Mathf.Max(0,missions.Count-1));
            var viewport=Box(surface,64,321,520,373,PanelColor,"MissionList"); viewport.gameObject.AddComponent<RectMask2D>();
            var content=Box(viewport,0,0,502,Mathf.Max(373,missions.Count*72+8),Color.clear);
            content.GetComponent<Image>().raycastTarget=false;
            var scroll=viewport.gameObject.AddComponent<ScrollRect>();scroll.viewport=viewport;scroll.content=content;scroll.horizontal=false;scroll.movementType=ScrollRect.MovementType.Clamped;scroll.scrollSensitivity=35;
            for(int i=0;i<missions.Count;i++)
            {
                int selection=i;var m=missions[i];
                Button(content,$"{i+1:00}   {m.DisplayName}",12,i*72+10,488,62,()=>{missionIndex=selection;Rebuild();},missionIndex==i);
            }
            var detail=Box(surface,612,321,924,373,PanelColor);
            if(missions.Count>0)
            {
                var m=missions[missionIndex];
                Label(detail,m.MissionId,30,23,855,32,17,Accent,true);
                Label(detail,m.DisplayName,30,68,855,53,30,White,true);
                Label(detail,m.Briefing,30,138,855,121,21,White);
                var goals=new StringBuilder(); foreach(var objective in m.Objectives) goals.Append("• ").Append(objective.DisplayName).Append("  ");
                Label(detail,goals.ToString(),30,272,855,58,19,Muted);
                Label(detail,$"{m.Actors.Length} 架飞机     /     时限 {m.MaximumDurationS/60:0.#} 分钟     /     种子 {m.DefaultSeed}",30,335,855,27,17,Accent);
            }
            Button(surface,"返回",64,726,160,55,session.Home);
            Button(surface,"刷新任务",244,726,180,55,session.ReloadMissions);
            if(missions.Count>0) {
                Button(surface,"自动运行",1016,726,220,55,()=>session.BeginMission(missions[missionIndex],true));
                Button(surface,"人工飞行   →",1260,726,276,55,()=>session.BeginMission(missions[missionIndex],false),true);
            }
            if(session.Catalog.Issues.Count>0) Label(surface,$"{session.Catalog.Issues.Count} 个任务文件未载入：{session.Catalog.Issues[0]}",64,790,1460,34,16,new Color(1,.66f,.4f));
        }
        private void BuildControls()
        {
            Heading("04 / CONTROLS", "HOTAS 控制与校准", "WARTHOG 摇杆 + 双油门；无脚舵时用摇杆 POV 帽左右或 Q / E 控制方向舵。");
            var router=session.Input;
            string[] modes={"自动识别","键盘 / 鼠标","HOTAS 优先"};
            for(int i=0;i<3;i++){int mode=i;Button(surface,modes[i],64+i*207,300,193,45,()=>{router.SetMode((PilotInputMode)mode);Rebuild();},(int)router.Profile.Mode==i);}
            Button(surface,"操纵轴",836,300,164,45,()=>{controlTab=0;Rebuild();},controlTab==0);
            Button(surface,"飞行按钮",1013,300,164,45,()=>{controlTab=1;Rebuild();},controlTab==1);
            Button(surface,"下显示器",1190,300,164,45,()=>{controlTab=3;Rebuild();},controlTab==3);
            Button(surface,"辅助设置",1367,300,169,45,()=>{controlTab=2;Rebuild();},controlTab==2);
            if(controlTab==0) BuildAxes(); else if(controlTab==1) BuildButtons(); else if(controlTab==3) BuildDisplayButtons(); else BuildAuxiliary();
            message=Label(surface,router.Message,64,716,1472,51,19,Accent);
            Button(surface,"返回",64,780,210,45,session.CloseControls);
            Button(surface,"恢复 WARTHOG 默认",1190,780,346,45,()=>{router.ApplyWarthogDefaults(true);Rebuild();});
        }
        private void BuildAxes()
        {
            var router=session.Input;
            for(int i=0;i<AxisNames.Length;i++){int axis=i;Button(surface,AxisNames[i],64,368+i*52,372,44,()=>{router.CancelLearning();axisIndex=axis;Rebuild();},axisIndex==i);}
            var panel=Box(surface,462,368,1074,328,PanelColor);var binding=router.Profile.Axes[axisIndex];
            Label(panel,AxisNames[axisIndex],26,18,1000,38,26,White,true);
            Label(panel,router.BindingLabel(binding),26,65,1000,31,18,Muted);
            Box(panel,26,118,718,17,new Color(.11f,.18f,.22f));axisFill=Box(panel,26,118,359,17,Accent).GetComponent<Image>();
            axisValue=Label(panel,"",772,104,270,40,20,Accent);
            Button(panel,"移动轴以绑定",26,157,214,42,()=>router.BeginLearning(false,axisIndex));
            Button(panel,"清除",252,157,106,42,()=>{router.ClearBinding(false,axisIndex);Rebuild();});
            Button(panel,"开始校准",374,157,190,42,()=>router.BeginCalibration(axisIndex));
            Button(panel,"完成校准",576,157,190,42,router.FinishCalibration);
            Button(panel,binding.Invert?"方向：反向":"方向：正向",780,157,267,42,()=>{binding.Invert=!binding.Invert;router.Save();Rebuild();});
            Button(panel,$"死区 {binding.DeadZone:P0}   −",26,225,230,43,()=>{binding.DeadZone=Mathf.Max(0,binding.DeadZone-.01f);router.Save();Rebuild();});
            Button(panel,"+",265,225,65,43,()=>{binding.DeadZone=Mathf.Min(.4f,binding.DeadZone+.01f);router.Save();Rebuild();});
            Button(panel,$"曲线 {binding.Exponent:0.0}   −",374,225,230,43,()=>{binding.Exponent=Mathf.Max(.5f,binding.Exponent-.1f);router.Save();Rebuild();});
            Button(panel,"+",616,225,65,43,()=>{binding.Exponent=Mathf.Min(3,binding.Exponent+.1f);router.Save();Rebuild();});
            Label(panel,"校准：慢慢走遍全行程；摇杆最后回中。油门为线性 0–100%，不应用死区与曲线。",26,280,1020,35,17,Muted);
        }
        private void BuildButtons()
        {
            var router=session.Input;
            for(int i=0;i<ActionNames.Length;i++)
            {
                int index=i;float x=64+(i/6)*746, y=368+(i%6)*54;
                var row=Box(surface,x,y,726,48,PanelColor);
                Label(row,ActionNames[i],12,5,255,36,19,White);
                Label(row,router.BindingLabel(router.Profile.Buttons[i]),274,6,275,34,14,Muted);
                Button(row,"绑定",554,6,80,36,()=>router.BeginLearning(true,index));
                Button(row,"清除",642,6,73,36,()=>{router.ClearBinding(true,index);Rebuild();});
            }
        }
        private void BuildAuxiliary()
        {
            var panel=Box(surface,64,368,1472,328,PanelColor);var router=session.Input;
            Label(panel,"单发动机油门来源",28,21,680,38,24,White,true);
            string[] sources={"双油门平均","左油门","右油门"};
            for(int i=0;i<3;i++){int source=i;Button(panel,sources[i],28+i*231,82,214,43,()=>{router.Profile.Throttle=(ThrottleSelection)source;router.Save();Rebuild();},(int)router.Profile.Throttle==i);}
            Button(panel,"鼠标操纵飞机："+(router.Profile.MouseFlightControl?"开启":"关闭"),28,154,670,45,()=>{router.Profile.MouseFlightControl=!router.Profile.MouseFlightControl;router.Save();Rebuild();});
            Label(panel,"键盘：↑↓ 俯仰  A/D 横滚  Q/E 方向舵  W/S 油门\nSpace 轮刹  B 减速板  P 系统电源  I 启动  G 起落架\nC 视角  Tab 目标  X 武器  M 主武器  Ctrl 发射  F8 自动",755,28,681,155,21,Muted);
            var terrain=session.Host.GetComponent<LocalTerrainEnvironment>();
            if(terrain!=null)Button(panel,"场景数据："+terrain.Status+"  /  切换",28,220,670,42,()=>{terrain.SetLocalCache(!terrain.UsesLocalCache);Rebuild();});
            Label(panel,"本地模式无需网络；在线模式需要可用的 Cesium 授权。\n飞行中 ESC 暂停，可预览爆炸 / 火焰。",755,215,681,54,16,Muted);
            Label(panel,"配置位置："+router.ProfilePath,28,278,1400,35,14,Muted);
        }
        private void BuildDisplayButtons()
        {
            var router=session.Input;
            for(int i=0;i<DisplayActionNames.Length;i++)
            {
                int index=i;float x=64+(i/6)*746,y=368+(i%6)*54;
                var row=Box(surface,x,y,726,48,PanelColor);
                Label(row,DisplayActionNames[i],12,5,255,36,19,White);
                Label(row,router.BindingLabel(router.Profile.DisplayButtons[i]),274,6,275,34,14,Muted);
                Button(row,"绑定",554,6,80,36,()=>router.BeginDisplayLearning(index));
                Button(row,"清除",642,6,73,36,()=>{router.ClearDisplayBinding(index);Rebuild();});
            }
            Label(surface,"F6 打开 / 关闭 · 方向键移动 · Enter 确认\nBackspace 返回 · PgUp/PgDn 切页 · +/- 缩放",820,640,710,62,17,Muted);
        }
        private void BuildFlight()
        {
            var top=Box(surface,22,18,810,48,new Color(.025f,.049f,.072f,.85f));
            live=Label(top,session.CurrentTitle,15,5,770,36,17,White);
            Button(surface,"暂停 / ESC",1376,18,202,48,()=>session.Show(FlightScreen.Paused));
            Button(surface,"下显示器 / F6",1138,18,222,48,session.RequestDisplay);
            var footer=Box(surface,22,841,1210,37,new Color(.025f,.049f,.072f,.85f));
            connection=Label(footer,"",12,3,1190,30,15,Muted);
            var information=Box(surface,1220,594,358,221,new Color(.025f,.049f,.072f,.82f));
            objectives=Label(information,"",16,12,326,197,17,White);
        }
        private void BuildPause()
        {
            Heading("05 / FLIGHT PAUSED", "飞行已暂停", session.CurrentTitle+"   ·   仿真时间停止，设备校准不会操纵飞机。");
            Button(surface,"继续飞行   →",64,340,690,67,()=>session.Show(FlightScreen.Flying),true);
            Button(surface,"控制器设置",64,429,690,59,session.OpenControls);
            Button(surface,"重新开始",64,510,690,59,session.Restart);
            Button(surface,"结束并复盘",64,591,690,59,session.EndFlight);
            var p=Box(surface,802,340,734,310,PanelColor);
            Label(p,"视觉效果实验",28,22,670,40,25,White,true);
            Label(p,"在相机前方演示粒子效果，不改变飞机损伤。\n实际坠机、起火和武器命中由事件自动驱动。",28,84,675,90,21,Muted);
            Button(p,"预览爆炸",28,220,319,53,()=>PreviewEffect(false));
            Button(p,"预览火焰",368,220,338,53,()=>PreviewEffect(true));
            Button(surface,"返回主菜单",64,730,286,53,session.Home);
        }
        private void PreviewEffect(bool fire)
        {
            session.Show(FlightScreen.Flying);
            var effects=session.Host.GetComponent<FlightEffectsController>();
            if(effects!=null) effects.Preview(fire);
        }
        private void BuildDebrief()
        {
            var report=session.LastReport;
            Heading("06 / FLIGHT DEBRIEF", "飞行复盘", report==null?session.CurrentTitle:report.Title);
            var p=Box(surface,64,327,1472,330,PanelColor);
            if(report!=null)
            {
                string result=report.Result=="Succeeded"?"任务完成":report.Result=="Failed"?"任务失败":report.Result=="Aborted"?"任务中止":"飞行结束";
                Label(p,result,30,26,1390,55,35,Accent,true);
                Label(p,report.Reason,30,95,1390,54,22,White);
                Label(p,$"飞行时间   {report.SimulationTimeS/60:0.0} 分钟        高度   {report.AltitudeM:0} m        速度   {report.SpeedMps*1.94384:0} kt",30,169,1390,45,24,White);
                Label(p,"报告已保存："+session.ReportPath,30,261,1390,40,16,Muted);
            }
            Button(surface,"返回主菜单",64,720,318,58,session.Home);
            Button(surface,"再飞一次   →",1218,720,318,58,session.Restart,true);
        }
        private void Update()
        {
            if(session==null || Time.unscaledTime<refreshAt) return;refreshAt=Time.unscaledTime+.08f;
            if(connection!=null) connection.text=session.Input.ConnectionSummary+(session.Screen==FlightScreen.Flying?"    |    C 视角   ·   G 起落架   ·   ESC 暂停":"");
            if(message!=null) message.text=session.Input.Message;
            if(axisValue!=null) {
                session.Input.TryReadRaw((FlightAxis)axisIndex,out float raw);
                var b=session.Input.Profile.Axes[axisIndex];bool uni=PilotControlProfile.IsUnipolar((FlightAxis)axisIndex);
                float v=b.Normalize(raw,uni);axisValue.text=$"原值 {raw:0.00}   →   {v:0.00}";
                axisFill.rectTransform.sizeDelta=new Vector2(718*(uni?v:(v+1)*.5f),17);
            }
            if(live!=null) {
                var s=session.Host.LatestSnapshot.Fast;
                live.text=$"{session.CurrentTitle}  |  {s.SimulationTimeS:0}s  |  {(session.Host.IsPlayerAutomationEnabled?"自动":"手动")}  |  地形 {(s.TerrainSampleValid?"已就绪":"加载中")}";
                var information=new StringBuilder($"IAS  {s.CalibratedAirspeedMps*1.94384:0} kt    ALT  {s.MeanSeaLevelAltitudeM:0} m\nHDG  {s.HeadingRad*180/Math.PI:000}°\n");
                if(session.Host.TryGetMissionSnapshot(out var mission))
                {
                    for(int i=0;i<mission.Objectives.Length && i<3;i++) information.Append($"\n{mission.Objectives[i].DisplayName}  {mission.Objectives[i].ProgressNormalized:P0}");
                    var definition=session.Host.LoadedMission;
                    foreach(var actor in mission.Actors)if(actor.Aircraft==session.Host.LocalAircraft)
                    foreach(var source in definition.Actors)if(source.AircraftId==actor.Aircraft.Value && source.Route!=null && source.Route.Length>0)
                    {
                        var waypoint=source.Route[Mathf.Min(actor.CurrentWaypointIndex,source.Route.Length-1)];
                        double dx=(waypoint.LongitudeRad-s.LongitudeRad)*Math.Cos(s.LatitudeRad),dy=waypoint.LatitudeRad-s.LatitudeRad;
                        double bearing=(Math.Atan2(dx,dy)*180/Math.PI+360)%360,km=Math.Sqrt(dx*dx+dy*dy)*6371;
                        information.Append($"\n\n{waypoint.WaypointId}   {bearing:000}° / {km:0.0} km\n目标高度  {waypoint.EllipsoidHeightM:0} m");
                    }
                }
                else information.Append("\n自由飞行\n\nESC  暂停与设置\nR  重新开始");
                if(objectives!=null)objectives.text=information.ToString();
            }
        }
        private RectTransform Box(Transform parent,float x,float y,float w,float h,Color color,string name="Panel")
        {
            var go=new GameObject(name,typeof(RectTransform),typeof(Image));var rect=go.GetComponent<RectTransform>();rect.SetParent(parent,false);
            rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(0,1);rect.anchoredPosition=new Vector2(x,-y);rect.sizeDelta=new Vector2(w,h);
            go.GetComponent<Image>().color=color; return rect;
        }
        private Text Label(Transform parent,string value,float x,float y,float w,float h,int size,Color color,bool bold=false,TextAnchor align=TextAnchor.UpperLeft)
        {
            var go=new GameObject("Text",typeof(RectTransform),typeof(Text));var rect=go.GetComponent<RectTransform>();rect.SetParent(parent,false);
            rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(0,1);rect.anchoredPosition=new Vector2(x,-y);rect.sizeDelta=new Vector2(w,h);var text=go.GetComponent<Text>();
            text.font=font;text.fontSize=size;text.color=color;text.text=value;text.fontStyle=bold?FontStyle.Bold:FontStyle.Normal;text.alignment=align;
            text.horizontalOverflow=HorizontalWrapMode.Wrap;text.verticalOverflow=VerticalWrapMode.Truncate;text.raycastTarget=false;
            return text;
        }
        private Button Button(Transform parent,string title,float x,float y,float w,float h,UnityAction action,bool primary=false)
        {
            var rect=Box(parent,x,y,w,h,primary?Accent:new Color(.1f,.16f,.2f),title);
            var button=rect.gameObject.AddComponent<Button>();button.targetGraphic=rect.GetComponent<Image>();
            var colors=button.colors;colors.highlightedColor=new Color(.76f,.9f,.94f);colors.pressedColor=new Color(.55f,.7f,.76f);colors.selectedColor=Color.white;button.colors=colors;
            button.navigation=new Navigation{mode=Navigation.Mode.None};button.onClick.AddListener(action);
            Label(rect,title,8,0,w-16,h,21,primary?Background:White,primary,TextAnchor.MiddleCenter);return button;
        }
    }
}
