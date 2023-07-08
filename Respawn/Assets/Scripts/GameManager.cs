using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;

public class GameManager : MonoBehaviour
{
    GameMode gm;
    private static GameManager instance;

    public AnimationCurve timeBetweenTicektcurve;
    public AnimationCurve timeForLevelcurve;

    public GameObject ticketListPanel;
    public GameObject DefaultTicket;

    List<Level> idlingLevels = new List<Level>();

    int score = 0;
    float timeLevel = 0;
    float timeNextLevel;

    private void Awake()
    {
        gm = FindObjectOfType<GameMode>();
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
        if (timeNextLevel < 0)
        {
            NewLevel();
            timeNextLevel = timeBetweenTicektcurve.Evaluate(timeLevel);
        }

        bool abort = false;
        if (idlingLevels.Count > 0)
        {
            foreach (Level l in idlingLevels)
            {
                l.time -= Time.deltaTime;
                int timeInt = (int)l.time;
                l.ticket.time.text = timeInt.ToString();

                if (l.time <= 0)
                {
                    abort = true;
                    break;
                }
            }
            if (abort)
            {
                Debug.Log("t'as perdu gros looser");
                Destroy(gameObject);
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            NewLevel();
        }
    }

    private void NewLevel()
    {
        int levelid = Random.Range(0, gm.playableLevels.Count);
        Level newLevel = new Level(gm.playableLevels[levelid]);
        newLevel.time = timeForLevelcurve.Evaluate(timeLevel);
        Debug.Log(timeForLevelcurve.Evaluate(timeLevel));

        GameObject newObject = Instantiate(DefaultTicket);
        Ticket newTicket = newObject.GetComponent<Ticket>();
        newTicket.nickName.text = "test";
        newTicket.time.text = newLevel.time.ToString();

        newTicket.transform.parent = ticketListPanel.transform;

        newLevel.ticket = newTicket;

        idlingLevels.Add(newLevel);
    }

    public static void OpenNextLevel()
    {
        if (instance.idlingLevels.Count > 0)
        {
            EnablePanel(false);
            SceneManager.LoadScene(instance.idlingLevels[0].name);
        }
    }

    public static void EnablePanel(bool enable)
    {
        instance.ticketListPanel.SetActive(enable);
    }

    public static void FinishLevel()
    {
        instance.idlingLevels.RemoveAt(0);
        instance.score++;
        Destroy(instance.ticketListPanel.transform.GetChild(0).gameObject);
        //Debug.Log(score);
    }
}