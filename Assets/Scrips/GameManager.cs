using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;

public class GameManager : MonoBehaviour 
{
    public static GameManager Instance { get; set; }
    public GameObject GameOverPanel;
    public bool AdsTestMode;
    public InputField PlayerNameField;
    public Text ScoreLabel;
    public Button BoostBtn;
    public GameObject PausePanel;

    private ObscuredInt Score;
    private bool IsBoosted = false;

    void Start()
    {
        if (!ParseManager.IsNetworkAvailable)
        {
            BoostBtn.interactable = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }

    public void LoadMainMenu()
    {
        Destroy(GameObject.Find("ScoresManager"));
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

    public void KillPlayer()
    {
        Score = PlayerController.Instance.Score;
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

    public void RestartLevel()
    {
        int rand = new System.Random().Next(100);
        Debug.Log(rand);
        if (rand <= 30)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show(options: new ShowOptions
                {
                    resultCallback = AdCallback
                });
                return;
            }
        }
        else
        {
            AdCallback(ShowResult.Finished);
            return;
        }
        //Ad was not ready
        AdCallback(ShowResult.Finished);
    }

    public void BoostScore()
    {
        if (Advertisement.IsReady("rewardedVideoZone"))
        {
            Advertisement.Show("rewardedVideoZone", options: new ShowOptions
            {
                resultCallback = AddTwentyPercentToScore
            });
            return;
        }
        else
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show(options: new ShowOptions
                {
                    resultCallback = AddTwentyPercentToScore
                });
                return;
            }
        }
        AddTwentyPercentToScore(ShowResult.Finished);
    }

    private void AddTwentyPercentToScore(ShowResult result)
    {
        float tenPercent = Score / 10;
        Score += (int)tenPercent * 2;
        ScoreLabel.text = Score.ToString();
        IsBoosted = true;
    }

    public void AdCallback(ShowResult result)
    {
        Application.LoadLevelAsync(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SaveScore()
    {
        if (true)
        {
            if (ParseManager.Instance == null)
            {
                Camera.main.gameObject.AddComponent<ParseManager>();
            }
            ScoreItem item = new ScoreItem
            {
                IsBoosted = IsBoosted,
                Score = Score,
                Player = PlayerNameField.text
            };

            ParseManager.Instance.SaveScore(item);
            LocalScoresManager.Instance.Write(item);
            IsBoosted = false; 
        }
    }
}
