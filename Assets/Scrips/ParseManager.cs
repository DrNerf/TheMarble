using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class ParseManager : MonoBehaviour 
{
    public static ParseManager Instance;
    public List<ScoreItem> Top10 = new List<ScoreItem>();
	// Use this for initialization
	void Awake () 
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;	
	}

    public void SaveScore(ScoreItem item)
    {
        ParseObject ladderBoardItem = new ParseObject("LadderBoard");
        ladderBoardItem["score"] = item.Score;
        ladderBoardItem["player"] = item.Player;
        ladderBoardItem["isBoosted"] = item.IsBoosted;
        Task saveTask = ladderBoardItem.SaveAsync();
    }

    public void Start()
    {
        var query = ParseObject.GetQuery("LadderBoard")
            .OrderByDescending("score")
            .Limit(10);
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;
            foreach (ParseObject item in results)
            {
                Top10.Add(new ScoreItem
                {
                    IsBoosted = bool.Parse(item["isBoosted"].ToString()),
                    Score = int.Parse(item["score"].ToString()),
                    Player = item["player"].ToString()
                });
            }
        });
    }
}

[Serializable]
public class ScoreItem
{
    public int Score;
    public string Player;
    public bool IsBoosted;
}
