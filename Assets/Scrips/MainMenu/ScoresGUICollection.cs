using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

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
            float height = 0;
            for (int i = 0; i < scores.Count; i++)
            {
                GameObject item = Instantiate(ScoreGUIItemPrefab);
                var rectTransform = item.GetComponent<RectTransform>();
                Vector3 position = rectTransform.position;
                position.y += (ScoresPanel.sizeDelta.y / 2) - (rectTransform.sizeDelta.y / 2);
                position.y -= i * 70;
                rectTransform.position = position;
                rectTransform.SetParent(ScoresPanel.transform, false);
                //rectTransform.localScale = new Vector3(1, rectTransform.localScale.y);
                item.GetComponent<ScoreGUIItem>().SetProperties(scores[i]);
                height = rectTransform.sizeDelta.y;
            }
            float panelHeight = height * scores.Count;
            ScoresPanel.sizeDelta = new Vector2(ScoresPanel.sizeDelta.x, panelHeight) * 0.80f;
            ScoresPanel.parent.GetComponent<ScrollRect>().normalizedPosition = new Vector2(1, 1);
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
