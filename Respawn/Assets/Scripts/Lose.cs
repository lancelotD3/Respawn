using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lose : MonoBehaviour
{
    GameManager gm;
    public TMP_Text score;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        StartCoroutine(CloseGame());
        score.text = "Score : " + gm.score.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Leave();
        }
    }
    IEnumerator CloseGame()
    {
        GameManager.FinishGame();
        FadeInFadeOut.Destroy();
        yield return new WaitForSeconds(60f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Leave()
    {
        SceneManager.LoadScene("MainMenu");
        //GameManager.FinishGame();
    }



}
