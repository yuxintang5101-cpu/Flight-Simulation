using System;
using FlightSim.Platform.Missions;
using UnityEngine;
namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public sealed class LowerDisplayMapRenderer
    {
        private MissionTerrainCache cache;private Texture2D texture;private readonly Color32[] pixels=new Color32[512*247];
        private double lastLon=double.MaxValue,lastLat,lastHeading;private float lastRange,lastRefresh=-10;private bool lastNorth;
        public Texture2D Texture=>texture;
        public LowerDisplayMapRenderer()
        {
            var asset=Resources.Load<TextAsset>("FlightSim/KTEX_TerrainCache");if(asset!=null)cache=JsonUtility.FromJson<MissionTerrainCache>(asset.text);
            if(cache!=null)texture=new Texture2D(512,247,TextureFormat.RGBA32,false){name="Lower Display Terrain",filterMode=FilterMode.Bilinear,wrapMode=TextureWrapMode.Clamp};
        }
        public void Update(LowerDisplayData data,LowerDisplayState view,float now)
        {
            if(cache==null||texture==null||!data.FastValid)return;var f=data.Fast;double heading=view.NorthUp?0:f.HeadingRad;
            bool changed=view.RangeNm!=lastRange||view.NorthUp!=lastNorth||Math.Abs(heading-lastHeading)>.0087||Math.Abs(f.LongitudeRad-lastLon)>0.00003||Math.Abs(f.LatitudeRad-lastLat)>0.00003;
            if(!changed&&now-lastRefresh<2)return;lastRefresh=now;lastLon=f.LongitudeRad;lastLat=f.LatitudeRad;lastHeading=heading;lastRange=view.RangeNm;lastNorth=view.NorthUp;
            double scale=view.RangeNm*1852/(247*.43),sin=Math.Sin(heading),cos=Math.Cos(heading),lonScale=6378137*Math.Cos(f.LatitudeRad);
            for(int y=0;y<247;y++)for(int x=0;x<512;x++)
            {
                double sx=(x-256)*scale,sy=(247*.53-y)*scale,east=sx*cos+sy*sin,north=-sx*sin+sy*cos,lon=f.LongitudeRad+east/lonScale,lat=f.LatitudeRad+north/6378137;
                Color color;
                if(!cache.TryGetHeightM(lon,lat,out double height)){float stripe=(x+y)%22<2?.025f:0;color=new Color(.019f+stripe,.043f+stripe,.033f+stripe);}
                else{cache.TryGetHeightM(lon+.00007,lat,out double e);cache.TryGetHeightM(lon,lat+.00007,out double n);if(e==0)e=height;if(n==0)n=height;float light=Mathf.Clamp((float)(.9+(height-e)*.002+(n-height)*.0015),.35f,1.4f),level=Mathf.Clamp01((float)((height-1700)/2300));color=new Color((23+level*43)/255,(36+level*39)/255,(29+level*24)/255)*light;if(height%250<12)color+=new Color(.035f,.039f,.031f);color.a=1;}
                pixels[(246-y)*512+x]=color;
            }
            texture.SetPixels32(pixels);texture.Apply(false,false);
        }
        public void Dispose(){if(texture!=null)UnityEngine.Object.Destroy(texture);}
    }
}
