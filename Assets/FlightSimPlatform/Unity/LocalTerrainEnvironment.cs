using System;
using CesiumForUnity;
using FlightSim.Platform.Missions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace FlightSim.Platform.Unity
{
    /// <summary>Offline mesh from the shipped Cesium-sampled KTEX cache. Keeps online scenery optional.</summary>
    public sealed class LocalTerrainEnvironment : MonoBehaviour
    {
        public bool UsesLocalCache { get; private set; }
        public string Status { get; private set; }
        private GameObject terrainRoot;
        private Mesh mesh;
        private Material material;
        private Cesium3DTileset[] tilesets;
        private bool[] tileStates;
        public void Initialize()
        {
            if(tilesets!=null)return;
            tilesets=FindObjectsOfType<Cesium3DTileset>(true);tileStates=new bool[tilesets.Length];
            for(int i=0;i<tilesets.Length;i++)tileStates[i]=tilesets[i].enabled;
            SetLocalCache(PlayerPrefs.GetInt("FlightSim.LocalTerrain",1)!=0,false);
        }
        public void SetLocalCache(bool local,bool persist=true)
        {
            if(local && terrainRoot==null) BuildMesh();
            UsesLocalCache=local && terrainRoot!=null;
            if(terrainRoot!=null)terrainRoot.SetActive(UsesLocalCache);
            for(int i=0;i<tilesets.Length;i++)if(tilesets[i]!=null)tilesets[i].enabled=!UsesLocalCache && tileStates[i];
            Status=UsesLocalCache?"本地地形缓存":"Cesium 在线地形";
            if(persist){PlayerPrefs.SetInt("FlightSim.LocalTerrain",UsesLocalCache?1:0);PlayerPrefs.Save();}
        }
        private void BuildMesh()
        {
            var source=Resources.Load<TextAsset>("FlightSim/KTEX_TerrainCache");var geo=FindObjectOfType<CesiumGeoreference>();
            if(source==null || geo==null){Debug.LogWarning("Local terrain cache or Cesium georeference missing.");return;}
            var cache=JsonUtility.FromJson<MissionTerrainCache>(source.text);
            if(cache==null || cache.ColumnCount<2 || cache.RowCount<2 || cache.HeightM.Length!=cache.ColumnCount*cache.RowCount)return;
            terrainRoot=new GameObject("KTEX Local Terrain Cache");terrainRoot.transform.SetParent(geo.transform,false);
            int layer=LayerMask.NameToLayer(FlightTerrainSampler.DefaultQueryLayerName);if(layer>=0)terrainRoot.layer=layer;
            var anchor=terrainRoot.AddComponent<CesiumGlobeAnchor>();anchor.detectTransformChanges=false;
            double longitude=(cache.LongitudeMinRad+cache.LongitudeMaxRad)*.5,latitude=(cache.LatitudeMinRad+cache.LatitudeMaxRad)*.5;
            anchor.positionGlobeFixed=geo.ellipsoid.LongitudeLatitudeHeightToCenteredFixed(new double3(longitude*180/Math.PI,latitude*180/Math.PI,0));
            // 500 m visual grid from the 250 m source. Every vertex retains its ellipsoid height.
            const int stride=2;int width=(cache.ColumnCount-1+stride-1)/stride+1,height=(cache.RowCount-1+stride-1)/stride+1;
            var vertices=new Vector3[width*height];var colors=new Color[vertices.Length];var triangles=new int[(width-1)*(height-1)*6];
            for(int y=0;y<height;y++)for(int x=0;x<width;x++)
            {
                int cx=Math.Min(x*stride,cache.ColumnCount-1),cy=Math.Min(y*stride,cache.RowCount-1),i=y*width+x;
                double lon=cache.LongitudeMinRad+(cache.LongitudeMaxRad-cache.LongitudeMinRad)*cx/(cache.ColumnCount-1),lat=cache.LatitudeMinRad+(cache.LatitudeMaxRad-cache.LatitudeMinRad)*cy/(cache.RowCount-1);
                double elevation=cache.HeightM[cy*cache.ColumnCount+cx];
                double3 ecef=geo.ellipsoid.LongitudeLatitudeHeightToCenteredFixed(new double3(lon*180/Math.PI,lat*180/Math.PI,elevation));
                double3 point=geo.TransformEarthCenteredEarthFixedPositionToUnity(ecef);
                vertices[i]=terrainRoot.transform.InverseTransformPoint(geo.transform.TransformPoint(new Vector3((float)point.x,(float)point.y,(float)point.z)));
                float band=Mathf.InverseLerp(2200,4300,(float)elevation);
                colors[i]=band<.68f?Color.Lerp(new Color(.16f,.23f,.17f),new Color(.4f,.38f,.29f),band/.68f):Color.Lerp(new Color(.4f,.38f,.29f),new Color(.8f,.83f,.8f),(band-.68f)/.32f);
                if(x<width-1 && y<height-1){int t=(y*(width-1)+x)*6;triangles[t]=i;triangles[t+1]=i+width;triangles[t+2]=i+1;triangles[t+3]=i+1;triangles[t+4]=i+width;triangles[t+5]=i+width+1;}
            }
            mesh=new Mesh{name="KTEX cached elevation mesh",indexFormat=IndexFormat.UInt32};mesh.vertices=vertices;mesh.colors=colors;mesh.triangles=triangles;mesh.RecalculateNormals();mesh.RecalculateBounds();
            terrainRoot.AddComponent<MeshFilter>().sharedMesh=mesh;
            material=new Material(Shader.Find("FlightSim/Terrain Vertex Color"));terrainRoot.AddComponent<MeshRenderer>().sharedMaterial=material;
            terrainRoot.AddComponent<MeshCollider>().sharedMesh=mesh;
            terrainRoot.AddComponent<CachedTerrainSurface>();
        }
        private void OnDestroy(){if(terrainRoot!=null)Destroy(terrainRoot);if(mesh!=null)Destroy(mesh);if(material!=null)Destroy(material);}
    }
}
