using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class VersionDataOnBuildInject : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        VersionData.PopulateFromGit();
        VersionData.SerializeTo(report.summary.outputPath);
    }
}
