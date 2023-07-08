using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    GameManager gm;
    private static InGameManager instance;

    public AnimationCurve timeBetweenTicektcurve;
    public AnimationCurve timeForLevelcurve;
    public GameObject ListTickets;
    public GameObject DefaultTicket;

    List<Level> Levels = new List<Level>();

    int score = 0;
    float timeLevel = 0;
    float timeNextLevel;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        timeNextLevel = timeBetweenTicektcurve.Evaluate(timeLevel);

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
        timeLevel += Time.deltaTime;
        timeNextLevel -= Time.deltaTime;
        if(timeNextLevel < 0)
        {
            NewLevel();
            timeNextLevel = timeBetweenTicektcurve.Evaluate(timeLevel);
        }

        if (Levels.Count > 0)
        {
            foreach(Level l in Levels)
            {
                l.time -= Time.deltaTime;
                int timeInt = (int)l.time;
                l.ticket.time.text = timeInt.ToString();

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
        newLevel.time = timeForLevelcurve.Evaluate(timeLevel);
        Debug.Log(timeForLevelcurve.Evaluate(timeLevel));

        GameObject newObject = Instantiate(DefaultTicket);
        Ticket newTicket = newObject.GetComponent<Ticket>();
        newTicket.nickName.text = "test";
        newTicket.time.text = newLevel.time.ToString();

        newTicket.transform.parent = ListTickets.transform;

        newLevel.ticket = newTicket;

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
        Destroy(instance.ListTickets.transform.GetChild(0).gameObject);
        //Debug.Log(score);
    }
    public void ResetManager()
    {
        Levels.Clear();
        score = 0;
        timeLevel = 0;
    }
}