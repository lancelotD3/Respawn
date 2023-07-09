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
    float comp;

    public GameObject Arrow;

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
        
        var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        emitter.SetParameter("Level_comp", comp);


        string textAdvancement = CheckAdvancement().ToString() + " / " + ldBricks.Count.ToString();
        advancementTMP.text = textAdvancement;
    }

    void FinishLevel()
    {
        //MARIUS

        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Level_comp", 0);
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

    public float CheckAdvancement()
    {
        float actualAdvancement = 0;
        foreach (LDBrick brick in ldBricks)
        {
            //MARIUS
            
            if (brick.bFinished)
                actualAdvancement++;
            comp = (actualAdvancement / (ldBricks.Count+1));
            instance.setParameterByName("Level_comp", comp);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Level_comp", comp);

        }

        if (actualAdvancement == ldBricks.Count)
            levelFinished = true;

        if (levelFinished)
            Arrow.SetActive(true);

        return actualAdvancement;
    }
}