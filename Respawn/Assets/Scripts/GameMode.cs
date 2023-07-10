using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using static Unity.Burst.Intrinsics.X86.Avx;
using System.Collections;

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

    [SerializeField]
    private bool resizableWindow = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);


    }

    private void LateUpdate()
    {
        if (!resizableWindow)
            return;

        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            float ratio = 16f / 9f;
            float height = Screen.width / ratio;
            Screen.SetResolution(Screen.width, (int)height, false);
        }
    }

    public static void PlayButton()
    {
        instance.StartCoroutine(instance.FadeThenLoad());
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Level_comp", 0);

    }
    IEnumerator FadeThenLoad()
    {
        FadeInFadeOut.FadeIn();
        GameObject.Find("Canvas").SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("WaitingRoom");
        FadeInFadeOut.FadeOut();
        yield return new WaitForSeconds(1f);
        FadeInFadeOut.Stop();
    }
}