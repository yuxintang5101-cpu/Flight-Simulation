using UnityEngine;
namespace FlightSim.Platform.Presentation.LowerDisplay
{
    /// <summary>Owns only the runtime material instance; source assets are restored on teardown.</summary>
    public sealed class LowerDisplaySurface
    {
        public Renderer Renderer {get;private set;} public Transform VisualRoot {get;private set;}
        private Material[] original;private Material material;private LowerDisplaySurfaceProfile profile;
        public bool Bind(Transform aircraft,LowerDisplaySurfaceProfile profile,RenderTexture texture)
        {
            this.profile=profile;if(aircraft==null||profile==null)return false;
            foreach(var t in aircraft.GetComponentsInChildren<Transform>(true))if(t.name==profile.VisualRootName){VisualRoot=t;break;}
            if(VisualRoot==null)return false;var target=VisualRoot.Find(profile.RendererPath);Renderer=target==null?null:target.GetComponent<Renderer>();
            if(Renderer==null)return false;original=Renderer.sharedMaterials;if(profile.MaterialIndex<0||profile.MaterialIndex>=original.Length)return false;
            var shader=Shader.Find("FlightSim/Lower Display Unlit");if(shader==null)return false;
            material=new Material(shader){name="Lower Display Live (Runtime)",mainTexture=texture};var replacement=(Material[])original.Clone();replacement[profile.MaterialIndex]=material;Renderer.sharedMaterials=replacement;return true;
        }
        public bool TryHit(Ray ray,out Vector2 pixel)
        {
            pixel=default;if(Renderer==null||!Renderer.enabled||!Renderer.gameObject.activeInHierarchy||VisualRoot==null)return false;
            Vector3 tl=VisualRoot.TransformPoint(profile.TopLeft),tr=VisualRoot.TransformPoint(profile.TopRight),bl=VisualRoot.TransformPoint(profile.BottomLeft);
            Vector3 right=tr-tl,down=bl-tl;var plane=new Plane(Vector3.Cross(right,down),tl);if(!plane.Raycast(ray,out float distance)||distance<0)return false;
            Vector3 point=ray.GetPoint(distance)-tl;float u=Vector3.Dot(point,right)/right.sqrMagnitude,v=Vector3.Dot(point,down)/down.sqrMagnitude;
            if(u<0||u>1||v<0||v>1)return false;pixel=new Vector2(u*2048,v*1024);return true;
        }
        public void Dispose(){if(Renderer!=null&&original!=null)Renderer.sharedMaterials=original;if(material!=null)Object.Destroy(material);Renderer=null;}
    }
}
