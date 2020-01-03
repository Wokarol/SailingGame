public static class VersionData
{
    [System.Serializable]
    class VersionDataHolder
    {
        public string Version = "";
        public string Hash = "";
    }

    private static VersionDataHolder data;

    public static string Version => data.Version;
    public static string Hash => data.Hash;

    public static void DeserializeFrom(string path)
    {

    }

    public static void SerializeTo(string path)
    {

    }

    public static void PopulateFromGit()
    {
        data = new VersionDataHolder() {
            Version = Wokarol.Build.Git.BuildVersion,
            Hash = Wokarol.Build.Git.Hash
        };
    }
}


