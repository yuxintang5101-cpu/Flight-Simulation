using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class LowerDisplayInspection
{
    [Serializable] private class Result { public List<Surface> Surfaces = new List<Surface>(); }
    [Serializable] private class Surface { public string Path, Material, Texture; public int Submesh; public Vector3[] Position; public Vector2[] UV; public int[] Triangles; }
    public static void Run()
    {
        var prefab=AssetDatabase.LoadAssetAtPath<GameObject>("Assets/FlightSimPlatform/Prefabs/F35_Visual.prefab");
        var result=new Result();
        foreach(var r in prefab.GetComponentsInChildren<MeshRenderer>(true))
        {
            var mesh=r.GetComponent<MeshFilter>()?.sharedMesh;if(mesh==null)continue;
            for(int i=0;i<r.sharedMaterials.Length;i++)
            {
                var mat=r.sharedMaterials[i];if(mat==null||!(mat.name.StartsWith("f35_mfd")||mat.name.StartsWith("f35_pfd")))continue;
                var triangles=mesh.GetTriangles(i);var indices=triangles.Distinct().ToArray();
                var vertices=mesh.vertices;var uv=mesh.uv;var texture=mat.GetTexture("_EmissionMap");
                result.Surfaces.Add(new Surface{Path=AnimationUtility.CalculateTransformPath(r.transform,prefab.transform),Material=mat.name,Texture=AssetDatabase.GetAssetPath(texture),Submesh=i,
                    Position=indices.Select(n=>prefab.transform.InverseTransformPoint(r.transform.TransformPoint(vertices[n]))).ToArray(),UV=indices.Select(n=>uv[n]).ToArray(),Triangles=triangles.Select(n=>Array.IndexOf(indices,n)).ToArray()});
            }
        }
        Directory.CreateDirectory("../Development/LowerDisplay");
        File.WriteAllText("../Development/LowerDisplay/surface-inspection.json",JsonUtility.ToJson(result,true));
        Debug.Log("Lower display surfaces: "+result.Surfaces.Count);
    }
}
