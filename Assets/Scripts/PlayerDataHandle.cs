using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerDataHandle : MonoBehaviour
{
    //Static Class for save the current player data;
    //Singleton pattern
    public static PlayerDataHandle Instance;

    public string playerName;
    public int score;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
