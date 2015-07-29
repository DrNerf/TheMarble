using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreGUIItem : MonoBehaviour
{
    public Text Player;
    public Text Score;
    public Text IsBoosted;

    public void SetProperties(ScoreItem item)
    {
        Player.text = "Player: " + item.Player;
        Score.text = "Score: " + item.Score;
        IsBoosted.text = "Is Boosted: " + (item.IsBoosted ? "YES" : "NO");
    }
}
