using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    InGameManager inGameManager;

    private void Awake()
    {
        inGameManager = FindObjectOfType<InGameManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            FinishLevel();
        }

    }

    void FinishLevel()
    {
        InGameManager.FinishLevel();
        SceneManager.LoadScene("WaitingRoom");
    }
}
