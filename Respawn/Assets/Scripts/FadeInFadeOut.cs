using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    private static FadeInFadeOut instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
       
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void Stop()
    {
        Animator a = instance.GetComponentInChildren<Animator>();
        a.enabled = false;
    }

    public static void FadeIn()
    {
        Animator a = instance.GetComponentInChildren<Animator>();
        a.enabled = true;
        a.Play("FadeIn");
    }

    public static void FadeOut()
    {
        Animator a = instance.GetComponentInChildren<Animator>();
        a.enabled = true;
        a.Play("FadeOut");
    }

    public static void Destroy()
    {
        Destroy(instance.gameObject);
    }
}