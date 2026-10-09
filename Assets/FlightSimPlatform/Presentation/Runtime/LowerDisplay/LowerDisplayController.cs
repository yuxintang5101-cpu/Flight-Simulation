using System;
using System.Collections.Generic;
using FlightSim.Platform.Unity;
using FlightSim.Platform.Unity.Controls;
using UnityEngine;
using UnityEngine.UI;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    [DisallowMultipleComponent,DefaultExecutionOrder(-450)]
    public sealed class LowerDisplayController : MonoBehaviour
    {
        public LowerDisplayState State {get;private set;}=new LowerDisplayState();
        public LowerDisplayData Data {get;private set;}
        public RenderTexture DisplayTexture {get;private set;}
        public RenderTexture VisionTexture {get;private set;}
        public bool SurfaceBound {get;private set;}
        public bool Focused=>session!=null&&session.Input.DisplayFocused;
        public string LastCommandMessage {get;private set;}="";
        public bool LastCommandAccepted {get;private set;}
        public int HitCount=>composer?.Hits.Count??0;
        public int ActiveLabels=>painter?.ActiveLabels??0;
        public bool VectorOverflow=>painter!=null&&painter.Overflowed;
        private FlightSessionController session;private ILowerDisplayDataSource source;private FlightCameraRig rig;private Camera mainCamera,displayCamera,visionCamera;
        private LowerDisplayPainter painter;private LowerDisplayComposer composer;private LowerDisplayMapRenderer map;private LowerDisplaySurface surface;
        private GameObject runtimeRoot,popupRoot;private RectTransform popupDisplay;private Text popupTitle;
        private readonly Dictionary<GameObject,int> previousLayers=new Dictionary<GameObject,int>();private int previousCameraMask;private float nextDraw,noticeUntil,lastClick=-1,nextRepeat;private string sessionKey;
        private Vector2 repeatDirection;private bool dirty=true,initialized;private int displayLayer=31,ownLayer=30;private float lastWidth,lastHeight;
        public void Initialize(FlightSessionController controller)
        {
            if(initialized)return;session=controller;source=new UnityLowerDisplayDataSource(session);rig=FindObjectOfType<FlightCameraRig>();mainCamera=rig!=null?rig.GetComponent<Camera>():Camera.main;
            displayLayer=LayerMask.NameToLayer("FlightMfd")>=0?LayerMask.NameToLayer("FlightMfd"):31;ownLayer=LayerMask.NameToLayer("FlightMfdOwnAircraft")>=0?LayerMask.NameToLayer("FlightMfdOwnAircraft"):30;
            var font=Resources.Load<Font>("Fonts/NotoSansSC-Regular")??Resources.Load<Font>("Fonts/NotoSansSC")??Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            runtimeRoot=new GameObject("Lower Display Runtime");runtimeRoot.hideFlags=HideFlags.DontSave;
            DisplayTexture=new RenderTexture(2048,1024,24,RenderTextureFormat.ARGB32){name="Lower Display 2048x1024",filterMode=FilterMode.Trilinear,useMipMap=true,autoGenerateMips=true,wrapMode=TextureWrapMode.Clamp};DisplayTexture.Create();
            var cam=new GameObject("Lower Display UI Camera",typeof(Camera));cam.transform.SetParent(runtimeRoot.transform,false);cam.transform.localPosition=new Vector3(0,0,-2);displayCamera=cam.GetComponent<Camera>();displayCamera.enabled=false;displayCamera.orthographic=true;displayCamera.orthographicSize=.512f;displayCamera.aspect=2;displayCamera.nearClipPlane=.1f;displayCamera.farClipPlane=5;displayCamera.clearFlags=CameraClearFlags.SolidColor;displayCamera.backgroundColor=Color.black;displayCamera.cullingMask=1<<displayLayer;displayCamera.targetTexture=DisplayTexture;displayCamera.allowHDR=false;displayCamera.allowMSAA=false;
            var canvasObject=new GameObject("Lower Display Canvas",typeof(RectTransform),typeof(Canvas));canvasObject.layer=displayLayer;canvasObject.transform.SetParent(runtimeRoot.transform,false);var canvas=canvasObject.GetComponent<Canvas>();canvas.renderMode=RenderMode.WorldSpace;canvas.worldCamera=displayCamera;var rect=canvasObject.GetComponent<RectTransform>();rect.sizeDelta=new Vector2(2048,1024);rect.localScale=Vector3.one*.001f;
            painter=new LowerDisplayPainter(rect,font,displayLayer);composer=new LowerDisplayComposer(painter);map=new LowerDisplayMapRenderer();surface=new LowerDisplaySurface();
            var profile=Resources.Load<LowerDisplaySurfaceProfile>("LowerDisplay/Surface");SurfaceBound=surface.Bind(rig?.AircraftTarget,profile,DisplayTexture);
            if(!SurfaceBound)Debug.LogWarning("Lower display surface unavailable. F6 still opens the live display.",this);
            if(mainCamera!=null){previousCameraMask=mainCamera.cullingMask;mainCamera.cullingMask&=~(1<<displayLayer);}
            if(rig?.AircraftTarget!=null)foreach(var renderer in rig.AircraftTarget.GetComponentsInChildren<Renderer>(true)){if(previousLayers.ContainsKey(renderer.gameObject))continue;previousLayers[renderer.gameObject]=renderer.gameObject.layer;renderer.gameObject.layer=ownLayer;}
            VisionTexture=new RenderTexture(2048,776,24,RenderTextureFormat.ARGB32){name="Lower Display Live External View",filterMode=FilterMode.Bilinear,wrapMode=TextureWrapMode.Clamp};VisionTexture.Create();
            var vision=new GameObject("Lower Display External Camera",typeof(Camera));vision.transform.SetParent(runtimeRoot.transform,false);visionCamera=vision.GetComponent<Camera>();if(mainCamera!=null)visionCamera.CopyFrom(mainCamera);visionCamera.enabled=false;visionCamera.targetTexture=VisionTexture;visionCamera.cullingMask&=~((1<<displayLayer)|(1<<ownLayer));visionCamera.fieldOfView=55;visionCamera.aspect=2048f/776;visionCamera.nearClipPlane=.15f;visionCamera.farClipPlane=Mathf.Max(100000,visionCamera.farClipPlane);visionCamera.allowHDR=false;
            CreatePopup(font);session.ScreenChanged+=OnScreenChanged;session.DisplayRequested+=ToggleFocus;session.DisplayEscapeHandler=Escape;
            var driver=runtimeRoot.AddComponent<LowerDisplayRenderDriver>();driver.Owner=this;initialized=true;RenderNow();
        }
        private void CreatePopup(Font font)
        {
            popupRoot=new GameObject("Lower Display Focus",typeof(RectTransform),typeof(Canvas),typeof(GraphicRaycaster));popupRoot.transform.SetParent(runtimeRoot.transform,false);var canvas=popupRoot.GetComponent<Canvas>();canvas.renderMode=RenderMode.ScreenSpaceOverlay;canvas.sortingOrder=450;
            var bg=new GameObject("Backdrop",typeof(RectTransform),typeof(Image));bg.transform.SetParent(popupRoot.transform,false);var rect=bg.GetComponent<RectTransform>();rect.anchorMin=Vector2.zero;rect.anchorMax=Vector2.one;rect.offsetMin=rect.offsetMax=Vector2.zero;bg.GetComponent<Image>().color=new Color(0,.012f,.008f,.94f);
            var image=new GameObject("Live Screen",typeof(RectTransform),typeof(RawImage));image.transform.SetParent(popupRoot.transform,false);popupDisplay=image.GetComponent<RectTransform>();popupDisplay.anchorMin=popupDisplay.anchorMax=popupDisplay.pivot=new Vector2(.5f,.5f);popupDisplay.sizeDelta=new Vector2(2048,1024);image.GetComponent<RawImage>().texture=DisplayTexture;
            var title=new GameObject("Instructions",typeof(RectTransform),typeof(Text));title.transform.SetParent(popupRoot.transform,false);popupTitle=title.GetComponent<Text>();popupTitle.font=font;popupTitle.fontSize=17;popupTitle.color=MfdColors.Muted;popupTitle.alignment=TextAnchor.MiddleLeft;popupTitle.raycastTarget=false;var tr=title.GetComponent<RectTransform>();tr.anchorMin=new Vector2(0,1);tr.anchorMax=new Vector2(1,1);tr.pivot=new Vector2(.5f,1);tr.offsetMin=new Vector2(35,-48);tr.offsetMax=new Vector2(-120,-8);
            var close=new GameObject("Close",typeof(RectTransform),typeof(Image),typeof(Button));close.transform.SetParent(popupRoot.transform,false);var cr=close.GetComponent<RectTransform>();cr.anchorMin=cr.anchorMax=cr.pivot=new Vector2(1,1);cr.anchoredPosition=new Vector2(-24,-12);cr.sizeDelta=new Vector2(92,34);close.GetComponent<Image>().color=new Color(.06f,.16f,.12f);close.GetComponent<Button>().onClick.AddListener(()=>SetFocused(false));
            var label=new GameObject("Label",typeof(RectTransform),typeof(Text));label.transform.SetParent(close.transform,false);var lr=label.GetComponent<RectTransform>();lr.anchorMin=Vector2.zero;lr.anchorMax=Vector2.one;lr.offsetMin=lr.offsetMax=Vector2.zero;var text=label.GetComponent<Text>();text.font=font;text.fontSize=18;text.text="关闭 ×";text.color=Color.white;text.alignment=TextAnchor.MiddleCenter;text.raycastTarget=false;
            popupRoot.SetActive(false);ResizePopup();
        }
        private void ResizePopup(){if(popupDisplay==null)return;float factor=Mathf.Min(Screen.width*.95f/2048,(Screen.height-116)/1024f);popupDisplay.localScale=Vector3.one*Mathf.Max(.1f,factor);popupDisplay.anchoredPosition=new Vector2(0,-8);lastWidth=Screen.width;lastHeight=Screen.height;}
        private void Update()
        {
            if(!initialized||session==null)return;bool flying=session.Screen==FlightScreen.Flying;
            if(!flying){SetFocused(false);session.Input.DisplayPointerCaptured=false;return;}
            if(Input.GetKeyDown(KeyCode.F6)||session.Input.DisplayPressed(DisplayAction.ToggleFocus))ToggleFocus();
            if(popupRoot.activeSelf!=Focused)popupRoot.SetActive(Focused);
            if(lastWidth!=Screen.width||lastHeight!=Screen.height)ResizePopup();
            bool hit=TryPointer(Input.mousePosition,out Vector2 pixel);session.Input.DisplayPointerCaptured=hit;
            composer.HoverHelp="";
            if(hit)
            {
                for(int i=composer.Hits.Count-1;i>=0;i--)if(composer.Hits[i].Bounds.Contains(pixel)){composer.HoverHelp=composer.Hits[i].Help;break;}
                if(Input.GetMouseButtonDown(0))
                {
                    if(!Focused&&Time.unscaledTime-lastClick<.3f){SetFocused(true);lastClick=-1;}
                    else{lastClick=Time.unscaledTime;Click(pixel);}
                }
                if(Mathf.Abs(Input.mouseScrollDelta.y)>.01f)Apply(Input.mouseScrollDelta.y>0?MfdOperation.ZoomIn:MfdOperation.ZoomOut,0);
            }
            if(!Focused)return;
            if(Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.KeypadEnter)||session.Input.DisplayPressed(DisplayAction.Confirm))ActivateFocus();
            if(Input.GetKeyDown(KeyCode.Backspace)||session.Input.DisplayPressed(DisplayAction.Back))Escape();
            if(Input.GetKeyDown(KeyCode.PageDown)||session.Input.DisplayPressed(DisplayAction.NextPage))Apply(MfdOperation.Page,(int)State.Page+1);
            if(Input.GetKeyDown(KeyCode.PageUp)||session.Input.DisplayPressed(DisplayAction.PreviousPage))Apply(MfdOperation.Page,(int)State.Page-1);
            if(Input.GetKeyDown(KeyCode.Equals)||Input.GetKeyDown(KeyCode.KeypadPlus)||session.Input.DisplayPressed(DisplayAction.ZoomIn))Apply(MfdOperation.ZoomIn,0);
            if(Input.GetKeyDown(KeyCode.Minus)||Input.GetKeyDown(KeyCode.KeypadMinus)||session.Input.DisplayPressed(DisplayAction.ZoomOut))Apply(MfdOperation.ZoomOut,0);
            Vector2 direction=Vector2.zero;
            if(Input.GetKey(KeyCode.UpArrow)||session.Input.DisplayHeld(DisplayAction.Up))direction=Vector2.down;
            else if(Input.GetKey(KeyCode.DownArrow)||session.Input.DisplayHeld(DisplayAction.Down))direction=Vector2.up;
            else if(Input.GetKey(KeyCode.LeftArrow)||session.Input.DisplayHeld(DisplayAction.Left))direction=Vector2.left;
            else if(Input.GetKey(KeyCode.RightArrow)||session.Input.DisplayHeld(DisplayAction.Right))direction=Vector2.right;
            if(direction==Vector2.zero){repeatDirection=Vector2.zero;return;}
            if(direction!=repeatDirection||Time.unscaledTime>=nextRepeat){MoveFocus(direction);nextRepeat=Time.unscaledTime+(direction!=repeatDirection?.4f:.16f);repeatDirection=direction;}
        }
        public void MoveFocus(Vector2 direction){int next=LowerDisplayState.Navigate(composer.Hits,State.FocusId,direction);if(next>=0){State.FocusId=composer.Hits[next].Id;dirty=true;}}
        public void ActivateFocus(){foreach(var hit in composer.Hits)if(hit.Id==State.FocusId&&hit.Enabled){Apply(hit.Operation,hit.Argument);return;}}
        public bool Click(Vector2 pixel)
        {for(int i=composer.Hits.Count-1;i>=0;i--){var hit=composer.Hits[i];if(hit.Enabled&&hit.Bounds.Contains(pixel)){State.FocusId=hit.Id;Apply(hit.Operation,hit.Argument);return true;}}if(State.MenuSlot>=0){State.Back();dirty=true;}return false;}
        private bool TryPointer(Vector2 screen,out Vector2 pixel)
        {
            pixel=default;if(Focused){if(!RectTransformUtility.ScreenPointToLocalPointInRectangle(popupDisplay,screen,null,out var local))return false;pixel=new Vector2(local.x+1024,512-local.y);return pixel.x>=0&&pixel.x<=2048&&pixel.y>=0&&pixel.y<=1024;}
            return rig!=null&&rig.IsCockpitActive&&mainCamera!=null&&surface.TryHit(mainCamera.ScreenPointToRay(screen),out pixel);
        }
        public void ToggleFocus()=>SetFocused(!Focused);
        public void SetFocused(bool focused)
        {if(session==null)return;focused&=session.Screen==FlightScreen.Flying;session.Input.SetDisplayFocus(focused);if(popupRoot!=null)popupRoot.SetActive(focused);if(!focused){session.Input.DisplayPointerCaptured=false;repeatDirection=Vector2.zero;}dirty=true;}
        private bool Escape(){if(!Focused&&State.Expanded==MfdModule.None&&State.MenuSlot<0&&string.IsNullOrEmpty(State.Dialog))return false;if(!State.Back())SetFocused(false);dirty=true;return true;}
        private void OnScreenChanged(){if(session.Screen!=FlightScreen.Flying)SetFocused(false);dirty=true;}
        public void Apply(MfdOperation operation,int argument)
        {
            switch(operation)
            {
                case MfdOperation.Page:State.SetPage(argument);break;
                case MfdOperation.Menu:State.MenuSlot=State.MenuSlot==argument?-1:argument;State.FocusId="assign-0";break;
                case MfdOperation.Assign:State.Assign(argument/10,(MfdModule)(argument%10));break;
                case MfdOperation.Expand:State.Expanded=(MfdModule)argument;State.MenuSlot=-1;State.FocusId="back";break;
                case MfdOperation.Hide:if(argument>=0&&argument<7)State.Layout[argument]=MfdModule.None;State.MenuSlot=-1;break;
                case MfdOperation.Back:State.Back();break;
                case MfdOperation.ZoomIn:State.Zoom(true);break;case MfdOperation.ZoomOut:State.Zoom(false);break;
                case MfdOperation.Orientation:State.NorthUp=!State.NorthUp;break;case MfdOperation.Units:State.Metric=!State.Metric;break;
                case MfdOperation.BrightnessDown:State.Brightness=Mathf.Max(.2f,State.Brightness-.05f);break;case MfdOperation.BrightnessUp:State.Brightness=Mathf.Min(1,State.Brightness+.05f);break;
                case MfdOperation.Night:State.Night=!State.Night;break;case MfdOperation.Reset:State.ResetLayout();break;
                case MfdOperation.Target:State.SelectedTrack=argument;break;
                case MfdOperation.Alerts:State.Dialog="alerts";State.DetailPage=0;State.FocusId="dialog-back";State.MenuSlot=-1;break;
                case MfdOperation.Mission:State.Dialog="mission";State.DetailPage=0;State.FocusId="dialog-back";State.MenuSlot=-1;break;
                case MfdOperation.DetailPage:State.DetailPage=Mathf.Max(0,argument);break;
                case MfdOperation.ContactPage:State.ContactPage=Mathf.Max(0,argument);break;
                case MfdOperation.Help:State.Dialog="help";State.FocusId="dialog-back";State.MenuSlot=-1;break;
                case MfdOperation.Acknowledge:if(Data!=null)foreach(var alert in Data.Alerts)State.Acknowledged.Add(alert.Id);break;
                case MfdOperation.Check:if(!State.Checklist.Add(argument))State.Checklist.Remove(argument);break;
                default:
                    if(operation==MfdOperation.Station)
                    {
                        if(argument<0||argument>=Contracts.StoresState.StationCapacity)break;
                        State.InspectedStation=argument;
                        if(Data!=null&&!Data.CombatValid){composer.Notice="STA "+(argument+1).ToString("00")+" / 查看挂点；当前无任务武器控制。";noticeUntil=Time.unscaledTime+4;break;}
                    }
                    var result=source.Execute(operation,argument);LastCommandAccepted=result.Accepted;LastCommandMessage=result.Message;
                    if(operation==MfdOperation.Station&&result.Accepted)State.InspectedStation=-1;
                    composer.Notice=(result.Accepted?"✓ ":"△ ")+result.Message;noticeUntil=Time.unscaledTime+4;break;
            }
            dirty=true;
        }
        public void RenderNow()
        {
            if(!initialized||source==null)return;Data=source.Read(Time.unscaledTime);
            if(sessionKey!=Data.SessionKey){sessionKey=Data.SessionKey;State.Acknowledged.Clear();State.Checklist.Clear();State.SelectedTrack=-1;State.InspectedStation=-1;State.Dialog="";}
            if(Data.SystemsValid)State.Acknowledged.RemoveWhere(id=>!Data.Alerts.Exists(a=>a.Id==id));
            if(Data.TacticalValid&&State.SelectedTrack>=0){bool present=false;for(int i=0;i<Data.Aircraft.Tactical.Tracks.Length;i++){var track=Data.Aircraft.Tactical.Tracks[i];if(track.IsValid&&track.TrackId==State.SelectedTrack&&(track.Affiliation==Contracts.TacticalTrackAffiliation.Friendly?Data.Systems.Avionics.DataLinkEnabled:Data.Systems.Avionics.RadarEnabled))present=true;}if(!present)State.SelectedTrack=-1;}
            if(State.Page==MfdPage.Map&&State.Expanded==MfdModule.None&&Data.Powered){map.Update(Data,State,Time.unscaledTime);composer.MapTexture=map.Texture;}
            if(State.Page==MfdPage.Vision&&State.Expanded==MfdModule.None&&Data.Powered&&rig?.AircraftTarget!=null)
            {
                Transform eye=surface.VisualRoot!=null?surface.VisualRoot.Find("CockpitEye"):null;visionCamera.transform.SetPositionAndRotation(eye!=null?eye.position:rig.AircraftTarget.position+rig.AircraftTarget.up*2,rig.AircraftTarget.rotation);
                var hidden=new List<Canvas>();foreach(var c in FindObjectsOfType<Canvas>())if(c.enabled&&c.renderMode==RenderMode.ScreenSpaceCamera){hidden.Add(c);c.enabled=false;}
                try{visionCamera.Render();}finally{foreach(var c in hidden)if(c!=null)c.enabled=true;}
                composer.VisionTexture=VisionTexture;
            }
            if(Time.unscaledTime>noticeUntil)composer.Notice="";
            composer.Focused=Focused;composer.Draw(Data,State);Canvas.ForceUpdateCanvases();displayCamera.Render();
            if(popupTitle!=null)popupTitle.text="下显示器  /  "+Data.Status+"    ·    F6 关闭    ·    方向键 / POV 移动    ·    Enter 确认    ·    ESC 返回"+(Data.SystemsValid&&!Data.Powered?"    ·    显示器未供电":"");
            dirty=false;nextDraw=Time.unscaledTime+.05f;
        }
        public void RenderLate(){if(isActiveAndEnabled&&initialized&&(dirty||Time.unscaledTime>=nextDraw))RenderNow();}
        private void OnDisable(){SetFocused(false);}
        public void SetDataSource(ILowerDisplayDataSource provider){source=provider??throw new ArgumentNullException(nameof(provider));dirty=true;}
        private void OnDestroy()
        {
            if(session!=null){session.ScreenChanged-=OnScreenChanged;session.DisplayRequested-=ToggleFocus;session.DisplayEscapeHandler=null;session.Input?.SetDisplayFocus(false);if(session.Input!=null)session.Input.DisplayPointerCaptured=false;}
            surface?.Dispose();map?.Dispose();foreach(var pair in previousLayers)if(pair.Key!=null)pair.Key.layer=pair.Value;if(mainCamera!=null)mainCamera.cullingMask=previousCameraMask;
            if(runtimeRoot!=null)Destroy(runtimeRoot);if(DisplayTexture!=null){DisplayTexture.Release();Destroy(DisplayTexture);}if(VisionTexture!=null){VisionTexture.Release();Destroy(VisionTexture);}
        }
    }
}
