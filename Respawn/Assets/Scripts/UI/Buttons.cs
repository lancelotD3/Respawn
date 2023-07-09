using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void Play()
    {
        GameMode.PlayButton();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
    }
    public void NextLevel()
    {
        GameManager.OpenNextLevel();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Leaving game");
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
    }
}