using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class LevelManager : MonoBehaviour
{
    List<LDBrick> ldBricks = new List<LDBrick>();

    TMP_Text advancementTMP;
    bool levelFinished = false;
    private FMOD.Studio.EventInstance instance;
    private void Awake()
    {
        advancementTMP = GetComponentInChildren<TMP_Text>();

        foreach (LDBrick bricks in FindObjectsOfType<LDBrick>())
        {
            ldBricks.Add(bricks);
        }

        Debug.Log(ldBricks.Count);
        //MARIUS
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/music/Snow_level");
        instance.start();
        //MARIUS
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && levelFinished)
        {
            FinishLevel();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            FinishLevel();
        }

        

        string textAdvancement = CheckAdvancement().ToString() + " / " + ldBricks.Count.ToString();
        advancementTMP.text = textAdvancement;
    }

    void FinishLevel()
    {
        //MARIUS
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //MARIUS
        GameManager.FinishLevel();
        StartCoroutine(FadeThenLoad());

        
    }

    IEnumerator FadeThenLoad()
    {
        FadeInFadeOut.FadeIn();
        yield return new WaitForSeconds(1f);
        GameManager.EnablePanel(true);
        SceneManager.LoadScene("WaitingRoom");
        FadeInFadeOut.FadeOut();
        yield return new WaitForSeconds(1f);
        FadeInFadeOut.Stop();
    }

    public int CheckAdvancement()
    {
        int actualAdvancement = 0;
        foreach (LDBrick brick in ldBricks)
        {
            //MARIUS
            
            if (brick.bFinished)
                actualAdvancement++;
            instance.setParameterByName("Level_comp", actualAdvancement/ldBricks.Count);

        }

        if (actualAdvancement == ldBricks.Count)
            levelFinished = true;

        return actualAdvancement;
    }
}