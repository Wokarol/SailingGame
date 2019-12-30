using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIDirector : MonoBehaviour
{
    [SerializeField] private Button playButton = null;
    [SerializeField] private Button quitButton = null;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
    }

    private void OnPlayPressed()
    {
        Debug.Log("I'm dummy message for when play button is pressed");
    }

    public void OnQuitPressed()
    {
        GeneralActions.Quit();
    }
}
