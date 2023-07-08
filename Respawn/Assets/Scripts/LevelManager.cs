using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    List<LDBrick> ldBricks = new List<LDBrick>();

    TMP_Text advancementTMP;
    bool levelFinished = false;
    private void Awake()
    {
        advancementTMP = GetComponentInChildren<TMP_Text>();

        foreach (LDBrick bricks in FindObjectsOfType<LDBrick>())
        {
            ldBricks.Add(bricks);
        }

        Debug.Log(ldBricks.Count);
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
        GameManager.EnablePanel(true);
        GameManager.FinishLevel();
        SceneManager.LoadScene("WaitingRoom");
    }

    public int CheckAdvancement()
    {
        int actualAdvancement = 0;
        foreach (LDBrick brick in ldBricks)
        {
            if (brick.bFinished)
                actualAdvancement++;
        }

        if (actualAdvancement == ldBricks.Count)
            levelFinished = true;

        return actualAdvancement;
    }
}