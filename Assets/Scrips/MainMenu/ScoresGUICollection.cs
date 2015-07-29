using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScoresGUICollection : MonoBehaviour 
{
    public GameObject ScoreGUIItemPrefab;
    public RectTransform ScoresPanel;

    private bool AreScoresListed = false;

    public void ListScores(bool isLocal)
    {
        if (!AreScoresListed)
        {
            AreScoresListed = true;
            var scores = isLocal ? LocalScoresManager.Instance.ReadScores() : ParseManager.Instance.Top10;
            if (isLocal)
            {
                scores = scores.OrderByDescending(x => x.Score).ToList();
            }
            for (int i = 0; i < scores.Count; i++)
            {
                GameObject item = Instantiate(ScoreGUIItemPrefab);
                item.transform.SetParent(ScoresPanel.transform);
                Vector3 position = ScoresPanel.position;
                position.y += ScoresPanel.rect.height / 2 - 15;// +35;
                position.y -= i * 40;
                item.GetComponent<RectTransform>().position = position;
                item.GetComponent<ScoreGUIItem>().SetProperties(scores[i]);
            } 
        }
    }

    //public void ListRemoteScores()
    //{
    //    if (!AreScoresListed)
    //    {
    //        AreScoresListed = true;
    //        var scores = ParseManager.Instance.Top10;
    //        for (int i = 0; i < scores.Count; i++)
    //        {
    //            GameObject item = Instantiate(ScoreGUIItemPrefab);
    //            item.transform.SetParent(ScoresPanel.transform);
    //            Vector3 position = ScoresPanel.position;
    //            position.y += ScoresPanel.rect.height / 2 - 15;// +35;
    //            position.y -= i * 40;
    //            item.GetComponent<RectTransform>().position = position;
    //            item.GetComponent<ScoreGUIItem>().SetProperties(scores[i]);
    //        }
    //    }
    //}
}
