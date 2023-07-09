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
    public GameObject primaryCanvas;
    public GameObject defaultTicketPrefab;

    public GameObject playerChair;

    public List<Level> idlingLevels = new List<Level>();

    public int score = 0;
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
        NewLevel(true);
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
                l.timerCounter -= Time.deltaTime;
                int timeInt = (int)l.timerCounter;
                l.ticket.time.text = timeInt.ToString();
                l.ticket.SetGFX(l.timerCounter / l.timer);

                if (l.timerCounter <= 0)
                {
                    abort = true;
                    break;
                }
            }
            if (abort)
            {
                StartCoroutine(FadeLose());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            NewLevel();
        }
    }

    private void NewLevel(bool haveTutoLevel = false)
    {
        Level newLevel;
        string levelName = gm.playersNames[Random.Range(0, gm.playersNames.Count)];
        if(haveTutoLevel)
        {
            newLevel = new Level(gm.tutoLevel);
        }
        else
        {
            int levelid = Random.Range(0, gm.playableLevels.Count);
            newLevel = new Level(gm.playableLevels[levelid]);
        }
        newLevel.timer = timeForLevelcurve.Evaluate(timeLevel);
        newLevel.timerCounter = newLevel.timer;

        GameObject newObject = Instantiate(defaultTicketPrefab);
        Ticket newTicket = newObject.GetComponent<Ticket>();
        newTicket.nickName.text = levelName;
        newTicket.time.text = newLevel.timer.ToString();

        newTicket.transform.parent = ticketListPanel.transform;

        newLevel.ticket = newTicket;

        idlingLevels.Add(newLevel);
    }

    public static void OpenNextLevel()
    {
        if (instance.idlingLevels.Count > 0)
        {
            GameObject.Find("PlayerChair").GetComponent<Animator>().Play("StartLevel");
            instance.ticketListPanel.transform.GetChild(0).transform.parent = instance.primaryCanvas.transform;
            EnablePanel(false);
            instance.StartCoroutine(instance.FadeThenLoad(instance.idlingLevels[0].name));

        }
    }

    IEnumerator FadeThenLoad(string name)
    {
        FadeInFadeOut.FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
        instance.primaryCanvas.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        instance.primaryCanvas.transform.GetChild(0).GetComponent<Animator>().Play("TicketFocus");
        FadeInFadeOut.FadeOut();
        yield return new WaitForSeconds(1f);
        FadeInFadeOut.Stop();
    }

    IEnumerator FadeLose()
    {
        FadeInFadeOut.FadeIn();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Losing");
        FadeInFadeOut.FadeOut();
        yield return new WaitForSeconds(1f);
        FadeInFadeOut.Stop();
    }

    public static void EnablePanel(bool enable)
    {
        instance.ticketListPanel.SetActive(enable);
    }

    public static void FinishLevel()
    {
        instance.score++;
        instance.idlingLevels.RemoveAt(0);
        Destroy(instance.primaryCanvas.transform.GetChild(0).gameObject);
    }

    public void FinishGame() => Destroy(gameObject);
}