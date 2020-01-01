using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesConfig : Configuration<ScenesConfig>
{
    public enum SceneID
    {
        MainMenu, MainGame
    }

    [SerializeField] private string MainMenu = "";
    [SerializeField] private string MainGame = "";

    public void SwitchSceneTo(SceneID id)
    {
        switch (id) {
            case SceneID.MainMenu:
                SceneManager.LoadScene(MainMenu);
                return;
            case SceneID.MainGame:
                SceneManager.LoadScene(MainGame);
                return;
        }

        Debug.LogError($"Switching to {id} is not supported");
    }
}
