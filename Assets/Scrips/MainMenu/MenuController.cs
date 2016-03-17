using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
    public GameObject NoConnectionAlert;

    void Awake()
    {
        CheckInternetConnection();
    }

    public void CheckInternetConnection()
    {
        if (NetworkHelper.IsConnectionAvailable())
        {
            Debug.Log("Internet connection available!");
            ParseManager.IsNetworkAvailable = true;
        }
        else
        {
            Debug.LogWarning("Internet connection not available!");
            ParseManager.IsNetworkAvailable = false;
            NoConnectionAlert.SetActive(true);
        }
    }

    public void StartGame()
    {
        Application.LoadLevelAsync(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GetLocalScores()
    {
        var scores = LocalScoresManager.Instance.ReadScores();
        Debug.Log(scores.Count);
    }
}
