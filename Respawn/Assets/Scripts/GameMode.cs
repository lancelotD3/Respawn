using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]


public class Level
{
    public Level() { }


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
    public Level tutoLevel;
    public List<string> playersNames;

    private FMOD.Studio.EventInstance music;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        music = FMODUnity.RuntimeManager.CreateInstance("event:/music/Snow_level");
        music.start();
        music.setParameterByName("Transi", 1);
    }

    public static void PlayButton()
    {
        SceneManager.LoadScene("WaitingRoom");

        //music.setParameterByName("Transi", 0);
    }
}