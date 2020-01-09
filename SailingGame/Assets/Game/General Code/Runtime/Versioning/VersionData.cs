using System.IO;
using UnityEngine;

public static class VersionData
{
    private const string FileName = "6420C686-A94E-4FE4-9354-1194A731931C";

    private const string NoVersionPlaceholder = "__.__.__";
    private const string NoHashPlaceholder = "b5ff5f2574a503452e32737a4d116aa16b0808f2";

    private readonly static string directory = Path.Combine(Application.dataPath, "Resources");
    private readonly static string path = Path.Combine(directory, $"{FileName}.txt");


    // Holds data, have to be used because of serialization
    [System.Serializable]
    class VersionDataHolder
    {
        public string Version = "";
        public string Hash = "";
    }


    // variables and properties
    private static VersionDataHolder data;

    private static bool CanReturnData => Application.isPlaying && data != null;

    public static string Version => CanReturnData ? data.Version : NoVersionPlaceholder;
    public static string Hash => CanReturnData ? data.Hash : NoHashPlaceholder;

    // Loads data or creates it when game starts
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void DeserializeOnAppStat()
    {
#if !UNITY_EDITOR
        LoadFromFile();
#else
        PopulateFromGit();
#endif
    }

    public static void LoadFromFile()
    {
        string json = Resources.Load<TextAsset>(FileName).text;
        data = JsonUtility.FromJson<VersionDataHolder>(json);
    }

#if UNITY_EDITOR
    public static void StoreInFile()
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);

        // Writes json to path
        if (!Directory.Exists(directory)) {
            Directory.CreateDirectory(directory);
        }
        File.WriteAllText(path, json);
    }

    public static void ClearFiles()
    {
        if (File.Exists(path)) {
            File.Delete(path);
            File.Delete($"{path}.meta");
        }
    }

    public static void PopulateFromGit()
    {
        data = new VersionDataHolder() {
            Version = Wokarol.Build.Git.BuildVersion,
            Hash = Wokarol.Build.Git.Hash
        };
    }
#endif
}


