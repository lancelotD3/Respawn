using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class Level
{
    Level() { }
    public Level(Level level) 
    {
        name = level.name;
        map = level.map;
        timer = level.timer;
        timerCounter = timer;
    }

    public string name;
    public Sprite map;
    public float timer;
    public float timerCounter = -1f;
    public Ticket ticket;
}

public class GameMode : MonoBehaviour
{
    private static GameMode instance;
    public List<Level> playableLevels;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void PlayButton()
    {
        SceneManager.LoadScene("WaitingRoom");
    }
}