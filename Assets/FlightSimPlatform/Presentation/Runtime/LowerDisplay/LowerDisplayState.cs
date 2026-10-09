using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlightSim.Platform.Presentation.LowerDisplay
{
    public enum MfdPage { Integrated, Map, Vision }
    public enum MfdModule { None=-1, Stores, Tactical, Systems, Fuel, Flight, Radar, Wing }
    public enum MfdOperation { Page, Menu, Assign, Expand, Hide, Back, ZoomIn, ZoomOut, Orientation, Units, BrightnessDown, BrightnessUp, Night, Reset, Target, Station, Arm, Radar, Link, Alerts, Mission, Acknowledge, Check, Help, DetailPage, ContactPage }
    public struct MfdHit
    {
        public string Id, Help; public Rect Bounds; public MfdOperation Operation; public int Argument; public bool Enabled;
        public MfdHit(string id, Rect bounds, MfdOperation operation, int argument, string help, bool enabled=true)
        {Id=id;Bounds=bounds;Operation=operation;Argument=argument;Help=help;Enabled=enabled;}
    }
    [Serializable]
    public sealed class LowerDisplayState
    {
        public MfdPage Page;
        public MfdModule[] Layout = DefaultLayout();
        public MfdModule Expanded=MfdModule.None;
        public int MenuSlot=-1, SelectedTrack=-1, InspectedStation=-1;
        public string Dialog="", FocusId="page-0";
        public int DetailPage, ContactPage;
        public float RangeNm=20, Brightness=1;
        public bool NorthUp, Metric, Night;
        public readonly HashSet<string> Acknowledged=new HashSet<string>();
        public readonly HashSet<int> Checklist=new HashSet<int>();
        public float EffectiveBrightness => Mathf.Clamp(Brightness,.2f,1)*(Night?.53f:1);
        public static MfdModule[] DefaultLayout() => new[]{MfdModule.Stores,MfdModule.Tactical,MfdModule.Systems,MfdModule.Fuel,MfdModule.Flight,MfdModule.Radar,MfdModule.Wing};
        public void SetPage(int page) {Page=(MfdPage)((page%3+3)%3);Expanded=MfdModule.None;MenuSlot=-1;Dialog="";FocusId="page-"+(int)Page;}
        public void Assign(int slot,MfdModule module)
        {
            if(slot<0||slot>=7||module<MfdModule.Stores||module>MfdModule.Wing)return;
            int previous=Array.IndexOf(Layout,module);var old=Layout[slot];if(previous>=0&&previous!=slot)Layout[previous]=old;
            Layout[slot]=module;MenuSlot=-1;FocusId="menu-"+slot;
        }
        public void ResetLayout(){Layout=DefaultLayout();SetPage(0);}
        public bool Back()
        {
            if(!string.IsNullOrEmpty(Dialog)){Dialog="";FocusId="alerts";return true;}
            if(MenuSlot>=0){FocusId="menu-"+MenuSlot;MenuSlot=-1;return true;}
            if(Expanded!=MfdModule.None){Expanded=MfdModule.None;FocusId="page-"+(int)Page;return true;}
            return false;
        }
        public void Zoom(bool inward){RangeNm=Mathf.Clamp(RangeNm*(inward?.5f:2),5,80);}
        public static int Navigate(IList<MfdHit> hits,string current,Vector2 direction)
        {
            int selected=-1;for(int i=0;i<hits.Count;i++)if(hits[i].Id==current&&hits[i].Enabled){selected=i;break;}
            if(selected<0){for(int i=0;i<hits.Count;i++)if(hits[i].Enabled)return i;return -1;}
            float best=float.MaxValue;int result=selected;Vector2 origin=hits[selected].Bounds.center;
            for(int i=0;i<hits.Count;i++)
            {
                if(i==selected||!hits[i].Enabled)continue;
                Vector2 delta=hits[i].Bounds.center-origin;float forward=Vector2.Dot(delta,direction);
                if(forward<=1)continue;float side=Mathf.Abs(delta.x*direction.y-delta.y*direction.x);
                float score=forward+side*3+side*side/Mathf.Max(1,forward);
                if(score<best){best=score;result=i;}
            }
            return result;
        }
    }
}
