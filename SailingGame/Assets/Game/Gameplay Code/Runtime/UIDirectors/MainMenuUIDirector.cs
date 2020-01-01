using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIDirector : MonoBehaviour
{
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button quitButton = null;
    [Space]
    [SerializeField] private ScenesConfig.SceneID gameScene = ScenesConfig.SceneID.MainGame;


    private void Start()
    {
        playButton.onClick.AddListener(OnPlayPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
    }

    private void OnPlayPressed()
    {
        Configuration.Get<ScenesConfig>().SwitchSceneTo(gameScene);
    }

    public void OnQuitPressed()
    {
        GeneralActions.Quit();
    }
}
