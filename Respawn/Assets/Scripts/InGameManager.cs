using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    GameManager gm;
    private static InGameManager instance;

    List<Level> Levels = new List<Level>();

    int score = 0;
    float timeLevel;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        NewLevel();
    }

    private void Update()
    {
        if (Levels.Count > 0)
        {
            foreach(Level l in Levels)
            {
                l.time -= Time.deltaTime;
                //Debug.Log(l.time);
                if (l.time <= 0)
                {
                    Debug.Log("t'as perdu gros looser");
                    ResetManager();
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            NewLevel();
        }

    }

    private void NewLevel()
    {
        int levelid = Random.Range(0, gm.levels.Count);
        Level newLevel = new Level(gm.levels[levelid]);
        Levels.Add(newLevel);
    }

    public static void OpenNextLevel()
    {
        SceneManager.LoadScene(instance.Levels[0].name);
    }

    public static void FinishLevel()
    {
        instance.Levels.RemoveAt(0);
        instance.score++;
        //Debug.Log(score);
    }

    public void ResetManager()
    {
        Levels.Clear();
        score = 0;
        timeLevel = 0;
    }
}