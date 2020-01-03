using System.IO;
using UnityEngine;


public static class VersionData
{
    private const string FileName = "Version.json";

    [System.Serializable]
    class VersionDataHolder
    {
        public string Version = "";
        public string Hash = "";
    }

    private static VersionDataHolder data;

    public static string Version => data.Version;
    public static string Hash => data.Hash;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    public static void DeserializeOnAppStat()
    {
#if !UNITY_EDITOR
        DeserializeFromData();
#else
        PopulateFromGit();
#endif
    }

    public static void DeserializeFromData()
    {
        string path = Application.dataPath + $"/../{FileName}";
        var json = File.ReadAllText(path);
        data = JsonUtility.FromJson<VersionDataHolder>(json);
    }

    public static void SerializeTo(string path)
    {
        path = path.Substring(0, path.LastIndexOf('/'));
        path += $"/{FileName}";

        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static void PopulateFromGit()
    {
        data = new VersionDataHolder() {
            Version = Wokarol.Build.Git.BuildVersion,
            Hash = Wokarol.Build.Git.Hash
        };
    }
}


