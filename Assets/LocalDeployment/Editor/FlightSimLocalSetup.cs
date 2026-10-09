using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class FlightSimLocalSetup
{
    private const string Scene = "Assets/FlightSimPlatform/Samples/Scenes/FlightSim_KTEX_V2.unity";

    public static void PrepareEditor()
    {
        if (!File.Exists(Scene)) throw new FileNotFoundException("FlightSim scene is missing", Scene);
        OpenDefaultScene();
        PlayerSettings.runInBackground = true;
        AssetDatabase.SaveAssets();
        string root = Directory.GetParent(Application.dataPath).Parent.FullName;
        string summary = $"Editor setup complete. No player build requested.\nScene: {Scene}\nUnity: {Application.unityVersion}\n";
        File.WriteAllText(Path.Combine(root, "Deployment", "editor-setup-result.txt"), summary);
        Debug.Log(summary);
    }

    public static void OpenDefaultScene() => EditorSceneManager.OpenScene(Scene);
}
