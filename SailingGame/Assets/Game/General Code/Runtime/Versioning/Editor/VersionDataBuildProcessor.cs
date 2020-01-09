using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

// Sets all files related to version when application is built
public class VersionDataBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        VersionData.PopulateFromGit();
        VersionData.StoreInFile();
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        VersionData.ClearFiles();
    }
}
