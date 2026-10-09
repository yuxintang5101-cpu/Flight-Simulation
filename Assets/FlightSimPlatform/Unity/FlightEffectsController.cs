using System;
using System.Collections.Generic;
using CesiumForUnity;
using FlightSim.Platform.Contracts;
using FlightSim.Platform.Data;
using Unity.Mathematics;
using UnityEngine;

namespace FlightSim.Platform.Unity
{
    /// <summary>Bounded pooled presentation effects. Does not modify flight dynamics or combat results.</summary>
    public sealed class FlightEffectsController : MonoBehaviour
    {
        private sealed class Effect
        {
            public GameObject Root;
            public CesiumGlobeAnchor Anchor;
            public ParticleSystem Flame, Smoke, Sparks;
            public Light Flash;
            public AircraftId Follow;
            public bool Following, Active, Sustained, Stopped, Preview;
            public float Age, End;
        }
        [SerializeField] private FlightEffectProfile profile;
        private FlightSimulationHost host;
        private CesiumGeoreference georeference;
        private readonly List<Effect> pool = new List<Effect>();
        private readonly HashSet<string> burning = new HashSet<string>();
        private readonly Dictionary<string,double> crashTimes = new Dictionary<string,double>();
        private ulong sequence;
        private Material material;
        private Texture2D texture;
        private bool ownedProfile;
        private Effect lastSpawned;
        public int ActiveCount { get { int count=0;foreach(var e in pool) if(e.Active) count++;return count; } }
        public int PoolCount => pool.Count;

        public void Initialize(FlightSimulationHost value)
        {
            if(host!=null) host.MissionLoaded-=OnMissionLoaded;
            host=value;georeference=FindObjectOfType<CesiumGeoreference>();
            if(profile==null) profile=Resources.Load<FlightEffectProfile>("FlightSim/DefaultEffects");
            if(profile==null){profile=ScriptableObject.CreateInstance<FlightEffectProfile>();ownedProfile=true;}
            CreateMaterial();host.MissionLoaded+=OnMissionLoaded;
        }
        private void OnMissionLoaded(MissionDefinition _) => Clear();
        public void Clear()
        {
            foreach(var e in pool) Release(e);
            sequence=0;burning.Clear();crashTimes.Clear();
        }
        private void OnDestroy()
        {
            if(host!=null)host.MissionLoaded-=OnMissionLoaded;
            foreach(var e in pool)if(e.Root!=null)Destroy(e.Root);
            if(material!=null)Destroy(material);if(texture!=null)Destroy(texture);if(ownedProfile)Destroy(profile);
        }
        private void Update()
        {
            if(host==null || profile==null) return;
            bool paused=host.IsSessionPaused;
            foreach(var e in pool)
            {
                if(!e.Active)continue;
                if(paused){e.Flame.Pause();e.Smoke.Pause();e.Sparks.Pause();continue;}
                if(e.Flame.isPaused)e.Flame.Play();if(e.Smoke.isPaused)e.Smoke.Play();if(e.Sparks.isPaused)e.Sparks.Play();
                e.Age+=Time.deltaTime;
                if(e.Preview && Camera.main!=null)e.Anchor.positionGlobeFixed=PreviewPosition();
                if(e.Following && host.TryGetLatest(e.Follow,out var snapshot))
                    e.Anchor.positionGlobeFixed=Position(snapshot.Fast);
                if(e.Sustained && !e.Stopped && e.Age>e.End){e.Flame.Stop(true,ParticleSystemStopBehavior.StopEmitting);e.Smoke.Stop(true,ParticleSystemStopBehavior.StopEmitting);e.Stopped=true;}
                e.Flash.intensity=e.Sustained?Mathf.Max(0,2.5f*(1-e.Age/e.End)):Mathf.Max(0,10*(1-e.Age/.6f));
                if(e.Age>e.End+8)Release(e);
            }
            if(paused || host.DataHub==null)return;
            // Event queue and particles are bounded, even during rapid combat or replay.
            for(int i=0;i<256 && host.DataHub.TryReadEvent(sequence,out var data);i++)
            {
                sequence=data.Sequence;
                if(data.Domain==FlightDataDomain.Simulation && data.Simulation.Type==SimulationEventType.Crash)
                {
                    string id=data.Simulation.Aircraft.Value;
                    if(!crashTimes.TryGetValue(id,out double time) || data.Simulation.SimulationTimeS-time>5)
                    {crashTimes[id]=data.Simulation.SimulationTimeS;AtAircraft(data.Simulation.Aircraft,false);AtAircraft(data.Simulation.Aircraft,true);}
                }
                if(data.Domain==FlightDataDomain.Mission && data.Mission.Type==MissionEventType.WeaponImpact)
                {AtAircraft(data.Mission.Target,false);AtAircraft(data.Mission.Target,true);}
            }
            for(int i=0;i<host.DataHub.AircraftCount;i++)
            {
                if(!host.DataHub.TryGetAircraftId(i,out var id) || !host.DataHub.TryGetAircraft(id,out var data) || !data.HasSystems)continue;
                if(data.Systems.Warnings.FireWarning)
                {
                    Effect ongoing=pool.Find(e=>e.Active && e.Sustained && e.Following && e.Follow==id);
                    if(ongoing!=null){ongoing.End=ongoing.Age+2;continue;}
                    AtAircraft(id,true,true);burning.Add(id.Value);
                }
                else if(burning.Remove(id.Value))foreach(var e in pool)if(e.Active&&e.Following&&e.Follow==id)e.End=e.Age;
            }
        }
        private void AtAircraft(AircraftId id,bool fire,bool follow=false)
        {
            if(host.TryGetLatest(id,out var snapshot)) Spawn(Position(snapshot.Fast),fire,follow,id);
        }
        public void Preview(bool fire)
        {
            if(georeference==null || Camera.main==null)return;
            Spawn(PreviewPosition(),fire,false,default);
            if(lastSpawned!=null){lastSpawned.Preview=true;if(fire)lastSpawned.End=8;}
        }
        private double3 PreviewPosition()
        {
            Vector3 world=Camera.main.transform.position+Camera.main.transform.forward*45;
            Vector3 local=georeference.transform.InverseTransformPoint(world);
            return georeference.TransformUnityPositionToEarthCenteredEarthFixed(new double3(local.x,local.y,local.z));
        }
        public void Spawn(double3 ecef,bool fire,bool follow=false,AircraftId aircraft=default)
        {
            if(georeference==null || profile==null)return;
            Effect e=pool.Find(x=>!x.Active);
            if(e==null && pool.Count<Mathf.Clamp(profile.MaximumInstances,1,32)){e=Create();pool.Add(e);}
            if(e==null){e=pool[0];foreach(var candidate in pool)if(candidate.Age>e.Age)e=candidate;Release(e);}
            lastSpawned=e;e.Preview=false;e.Active=true;e.Sustained=fire;e.Stopped=false;e.Following=follow;e.Follow=aircraft;e.Age=0;e.End=fire?profile.DebrisFireSeconds:2;
            e.Root.SetActive(true);e.Anchor.positionGlobeFixed=ecef;
            float scale=profile.SizeMultiplier;
            Configure(e.Flame,fire,fire?profile.FireEmission:0,fire?0:profile.ExplosionParticles,fire?1.1f:1.7f,fire?6:22,fire?3:4,scale,false);
            Configure(e.Smoke,fire,fire?profile.SmokeEmission:0,fire?0:45,7,fire?3:7,fire?5:8,scale,true);
            Configure(e.Sparks,false,0,fire?8:65,1.3f,32,.13f,scale,false);
            e.Flame.Play();e.Smoke.Play();e.Sparks.Play();e.Flash.enabled=true;
        }
        private Effect Create()
        {
            var go=new GameObject("Pooled Flight Effect");go.transform.SetParent(georeference.transform,false);
            var e=new Effect {Root=go,Anchor=go.AddComponent<CesiumGlobeAnchor>()};
            e.Anchor.detectTransformChanges=false;
            e.Flame=Layer(go,"Flame");e.Smoke=Layer(go,"Smoke");e.Sparks=Layer(go,"Sparks");
            e.Flash=go.AddComponent<Light>();e.Flash.type=LightType.Point;e.Flash.range=55;e.Flash.color=new Color(1,.43f,.12f);e.Flash.shadows=LightShadows.None;
            return e;
        }
        private ParticleSystem Layer(GameObject root,string name)
        {
            var go=new GameObject(name);go.transform.SetParent(root.transform,false);
            var ps=go.AddComponent<ParticleSystem>();ps.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
            var renderer=go.GetComponent<ParticleSystemRenderer>();renderer.sharedMaterial=material;renderer.shadowCastingMode=UnityEngine.Rendering.ShadowCastingMode.Off;renderer.receiveShadows=false;
            return ps;
        }
        private void Configure(ParticleSystem ps,bool loop,float rate,int burst,float lifetime,float speed,float size,float scale,bool smoke)
        {
            ps.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
            var main=ps.main;main.loop=loop;main.playOnAwake=false;main.duration=2;main.startLifetime=new ParticleSystem.MinMaxCurve(lifetime*.7f,lifetime);main.startSpeed=new ParticleSystem.MinMaxCurve(speed*.45f*scale,speed*scale);
            main.startSize=new ParticleSystem.MinMaxCurve(size*.55f*scale,size*scale);main.startRotation=new ParticleSystem.MinMaxCurve(-Mathf.PI,Mathf.PI);main.maxParticles=500;main.simulationSpace=ParticleSystemSimulationSpace.Local;main.cullingMode=ParticleSystemCullingMode.AlwaysSimulate;
            main.startColor=Color.white;main.gravityModifier=smoke?-.025f:.06f;
            var emission=ps.emission;emission.enabled=true;emission.rateOverTime=rate;emission.SetBursts(burst>0?new[]{new ParticleSystem.Burst(0,(short)burst)}:Array.Empty<ParticleSystem.Burst>());
            var shape=ps.shape;shape.enabled=true;shape.shapeType=loop?ParticleSystemShapeType.Cone:ParticleSystemShapeType.Sphere;shape.radius=loop?1.1f*scale:1.8f*scale;shape.angle=18;shape.rotation=new Vector3(-90,0,0);
            var color=ps.colorOverLifetime;color.enabled=true;var gradient=new Gradient();
            gradient.SetKeys(smoke?new[]{new GradientColorKey(profile.SmokeColor,0),new GradientColorKey(new Color(.32f,.34f,.36f),1)}:new[]{new GradientColorKey(new Color(1,.94f,.62f),0),new GradientColorKey(profile.FlameColor,.35f),new GradientColorKey(new Color(.25f,.07f,.025f),1)},new[]{new GradientAlphaKey(0,0),new GradientAlphaKey(smoke?.58f:1,.1f),new GradientAlphaKey(0,1)});color.color=gradient;
            var sizeLife=ps.sizeOverLifetime;sizeLife.enabled=true;sizeLife.size=new ParticleSystem.MinMaxCurve(1,new AnimationCurve(new Keyframe(0,.4f),new Keyframe(.5f,smoke?1.6f:1),new Keyframe(1,smoke?2.8f:.3f)));
            var noise=ps.noise;noise.enabled=true;noise.strength=smoke?1.7f:.7f;noise.frequency=.25f;noise.scrollSpeed=.25f;noise.quality=ParticleSystemNoiseQuality.Low;
            var velocity=ps.velocityOverLifetime;velocity.enabled=true;velocity.space=ParticleSystemSimulationSpace.Local;velocity.y=smoke?2f:0;
        }
        private static double3 Position(AircraftFastState state) => new double3(state.EcefPositionXM,state.EcefPositionYM,state.EcefPositionZM);
        private void Release(Effect e)
        {
            e.Flame.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);e.Smoke.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);e.Sparks.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
            e.Active=false;e.Root.SetActive(false);
        }
        private void CreateMaterial()
        {
            if(material!=null)return;
            texture=new Texture2D(64,64,TextureFormat.RGBA32,false){name="FlightSim soft particle",wrapMode=TextureWrapMode.Clamp};
            var pixels=new Color[64*64];
            for(int y=0;y<64;y++)for(int x=0;x<64;x++)
            {float r=new Vector2((x-31.5f)/31.5f,(y-31.5f)/31.5f).magnitude;float alpha=Mathf.Pow(Mathf.Clamp01(1-r),1.5f);pixels[y*64+x]=new Color(1,1,1,alpha);}
            texture.SetPixels(pixels);texture.Apply(false,true);
            material=new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended")){name="FlightSim particle material",mainTexture=texture};
        }
    }
}
