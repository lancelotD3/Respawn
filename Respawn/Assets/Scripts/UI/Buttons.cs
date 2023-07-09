using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void Play()
    {
        GameMode.PlayButton();
    }
    public void NextLevel()
    {
        GameManager.OpenNextLevel();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Leaving game");
    }
}