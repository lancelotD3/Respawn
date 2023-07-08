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
        time = level.time;
    }

    public string name;
    public Texture map;
    public float time;
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