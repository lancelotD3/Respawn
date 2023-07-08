using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

[Serializable]
public class Level
{
    Level() {; }
    public Level(Level level) 
    {
        name = level.name;
        map = level.map;
        time = level.time;
    }

    public string name;
    public Texture map;
    public float time;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public List<Level> levels;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("WaitingRoom");
    }
}
