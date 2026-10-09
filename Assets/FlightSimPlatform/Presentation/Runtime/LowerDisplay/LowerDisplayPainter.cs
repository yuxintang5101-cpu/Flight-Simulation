using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public static class MfdColors
    {
        public static readonly Color Cyan=new Color32(51,255,255,255),Green=new Color32(158,255,102,255),White=Color.white,
            Amber=new Color32(255,186,51,255),Red=new Color32(255,51,51,255),Muted=new Color32(145,168,154,255),Line=new Color32(74,91,82,255),Panel=new Color32(15,25,20,255);
    }
    /// <summary>Pooled uGUI vector/text display. Geometry uses the approved 2048 x 1024 top-left coordinate system.</summary>
    public sealed class LowerDisplayPainter
    {
        private sealed class Layer
        {
            public HudVectorGraphic Graphic;public HudVectorCommandBuffer Vectors=new HudVectorCommandBuffer(10000);
            public readonly List<Text> Labels=new List<Text>();public RectTransform Root;public int Used;
        }
        private readonly Layer[] layers=new Layer[2];private Layer current;private readonly Font font;private readonly int unityLayer;
        private Vector2 origin;private float scale=1,brightness=1;
        public RawImage BackgroundImage {get;}
        public bool Overflowed => layers[0].Vectors.Overflowed||layers[1].Vectors.Overflowed;
        public int ActiveLabels=>layers[0].Used+layers[1].Used;
        public LowerDisplayPainter(RectTransform parent,Font font,int layer)
        {
            this.font=font;unityLayer=layer;
            var bg=new GameObject("Scenery",typeof(RectTransform),typeof(RawImage));bg.layer=layer;bg.transform.SetParent(parent,false);
            BackgroundImage=bg.GetComponent<RawImage>();BackgroundImage.raycastTarget=false;SetRect(BackgroundImage.rectTransform,new Rect(0,176,1608,776));
            for(int i=0;i<2;i++)
            {
                var group=new GameObject(i==0?"Instruments":"Menus",typeof(RectTransform));group.layer=layer;group.transform.SetParent(parent,false);
                var root=group.GetComponent<RectTransform>();root.anchorMin=root.anchorMax=root.pivot=new Vector2(.5f,.5f);root.sizeDelta=new Vector2(2048,1024);
                var go=new GameObject("Vectors",typeof(RectTransform),typeof(CanvasRenderer),typeof(HudVectorGraphic));go.layer=layer;go.transform.SetParent(root,false);
                var rect=go.GetComponent<RectTransform>();rect.anchorMin=Vector2.zero;rect.anchorMax=Vector2.one;rect.offsetMin=rect.offsetMax=Vector2.zero;
                var graphic=go.GetComponent<HudVectorGraphic>();graphic.raycastTarget=false;
                layers[i]=new Layer{Root=root,Graphic=graphic};
            }
            current=layers[0];
        }
        public void Begin(float brightness)
        {
            this.brightness=brightness;foreach(var layer in layers){layer.Used=0;layer.Vectors.Clear();}
            current=layers[0];Global();BackgroundImage.gameObject.SetActive(false);
        }
        public void Overlay(){current=layers[1];Global();}
        public void End()
        {
            foreach(var layer in layers){for(int i=layer.Used;i<layer.Labels.Count;i++)layer.Labels[i].gameObject.SetActive(false);layer.Graphic.SetCommands(layer.Vectors);}
        }
        public void Global(){origin=Vector2.zero;scale=1;}
        public void Region(Rect rect,float width,float height)
        {scale=Mathf.Min(rect.width/width,rect.height/height);origin=new Vector2(rect.x+(rect.width-width*scale)/2,rect.y+(rect.height-height*scale)/2);}
        public Rect DisplayRect(Rect rect)=>new Rect(origin.x+rect.x*scale,origin.y+rect.y*scale,rect.width*scale,rect.height*scale);
        private Vector2 Point(float x,float y)=>new Vector2(origin.x+x*scale-1024,512-origin.y-y*scale);
        private Color ColorOf(Color c)=>new Color(c.r*brightness,c.g*brightness,c.b*brightness,c.a);
        public void Line(float x1,float y1,float x2,float y2,Color color,float width=1.5f)=>current.Vectors.AddLine(Point(x1,y1),Point(x2,y2),width*scale,ColorOf(color));
        public void Dash(float x1,float y1,float x2,float y2,Color color,float width=1.5f)=>current.Vectors.AddDashedLine(Point(x1,y1),Point(x2,y2),width*scale,ColorOf(color),8*scale,5*scale);
        public void Fill(float x,float y,float w,float h,Color color){if(w>0&&h>0)Line(x,y+h/2,x+w,y+h/2,color,h);}
        public void Box(float x,float y,float w,float h,Color color,float line=1.5f){Line(x,y,x+w,y,color,line);Line(x+w,y,x+w,y+h,color,line);Line(x+w,y+h,x,y+h,color,line);Line(x,y+h,x,y,color,line);}
        public void Circle(float x,float y,float r,Color color,float width=1.5f)=>Arc(x,y,r,0,360,color,width);
        public void Arc(float x,float y,float r,float start,float end,Color color,float width=5)
        {
            int n=Mathf.Max(6,Mathf.CeilToInt(Mathf.Abs(end-start)/5));float px=x+Mathf.Cos(start*Mathf.Deg2Rad)*r,py=y+Mathf.Sin(start*Mathf.Deg2Rad)*r;
            for(int i=1;i<=n;i++){float a=Mathf.Lerp(start,end,(float)i/n)*Mathf.Deg2Rad,nx=x+Mathf.Cos(a)*r,ny=y+Mathf.Sin(a)*r;Line(px,py,nx,ny,color,width);px=nx;py=ny;}
        }
        public void Text(float x,float y,string text,float size,Color color,int align=0,bool bold=true,float width=500)
        {
            Text label;
            if(current.Used<current.Labels.Count)label=current.Labels[current.Used];else
            {
                var go=new GameObject("Readout "+current.Labels.Count,typeof(RectTransform),typeof(CanvasRenderer),typeof(Text));go.layer=unityLayer;go.transform.SetParent(current.Root,false);
                label=go.GetComponent<Text>();label.font=font;label.raycastTarget=false;label.supportRichText=false;label.horizontalOverflow=HorizontalWrapMode.Wrap;label.verticalOverflow=VerticalWrapMode.Truncate;current.Labels.Add(label);
            }
            current.Used++;label.gameObject.SetActive(true);label.text=text??"";label.fontSize=Mathf.Max(10,Mathf.RoundToInt(size*scale));label.color=ColorOf(color);label.fontStyle=bold?FontStyle.Bold:FontStyle.Normal;
            label.alignment=align==1?TextAnchor.MiddleCenter:align==2?TextAnchor.MiddleRight:TextAnchor.MiddleLeft;
            float left=align==1?x-width/2:align==2?x-width:x;
            SetRect(label.rectTransform,DisplayRect(new Rect(left,y-size*1.13f,width,size*1.65f)));
        }
        public void Image(Texture texture,Rect rect)
        {BackgroundImage.texture=texture;SetRect(BackgroundImage.rectTransform,rect);BackgroundImage.color=new Color(brightness,brightness,brightness,1);BackgroundImage.gameObject.SetActive(texture!=null);}
        public static void SetRect(RectTransform rect,Rect box)
        {rect.anchorMin=rect.anchorMax=rect.pivot=new Vector2(0,1);rect.anchoredPosition=new Vector2(box.x,-box.y);rect.sizeDelta=box.size;}
    }
}
