using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
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
