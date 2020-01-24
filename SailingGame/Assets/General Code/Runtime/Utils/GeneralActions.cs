using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralActions
{
    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
