using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Serialization;

public class LoadGameRankScript : MonoBehaviour
{
    //Fields for display the player info
    public Text bestPlayerName;

    //Static variables for holding the best player data
    private static int _bestScore;
    private static string _bestPlayer;

    private void Awake()
    {
        LoadGameRank();
        SetBestPlayer();
    }

    public void SetBestPlayer()
    {
        if (_bestPlayer == null && _bestScore == 0)
        {
            bestPlayerName.text = "";
        }
        else
        {
            bestPlayerName.text = $"Best Score - {_bestPlayer}: {_bestScore}";
        }
    }

    public static void SaveGameRank(string bestPlayerName, int bestPlayerScore)
    {
        SaveData data = new SaveData();

        data.bestPlayer = bestPlayerName;
        data.highestScore = bestPlayerScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            _bestPlayer = data.bestPlayer;
            _bestScore = data.highestScore;
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int highestScore;
        public string bestPlayer;
    }
}