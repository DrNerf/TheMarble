using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LocalScoresManager : MonoBehaviour 
{
    public static LocalScoresManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Write(ScoreItem item)
    {
        try
        {
            List<ScoreItem> scores = ReadScores();
            scores.Add(item);
            string path = Application.persistentDataPath + "/playerInfo.dat";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);

            bf.Serialize(file, new PlayerData { LocalScores = scores });
            file.Close();
            Debug.Log("Player data saved!");
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Player data not saved!");
            Debug.LogError(ex);
        }
    }

    public List<ScoreItem> ReadScores()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(file);
                file.Close();

                Debug.Log("Player data loaded!");
                return data.LocalScores;
            }
            else
            {
                Debug.LogWarning("Player data not loaded!");
                return new List<ScoreItem>();
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Player data not loaded!");
            Debug.LogError(ex);
            return new List<ScoreItem>();
        }
    }


    [Serializable]
    private class PlayerData
    {
        public List<ScoreItem> LocalScores { get; set; }
    }
}
