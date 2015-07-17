using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; set; }
    public GameObject GameOverPanel;
    public bool AdsTestMode;

    public void KillPlayer()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    void Awake()
    {
        Instance = this;
        if (Advertisement.isSupported) {
            Advertisement.Initialize("52065", AdsTestMode);
        } else {
            Debug.LogError("Platform not supported");
        }
    }

    public void ShowAdForLevelRestart()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(options: new ShowOptions 
            {
                resultCallback = AdCallback
            });
        }
    }

    public void AdCallback(ShowResult result)
    {
        Application.LoadLevelAsync(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
