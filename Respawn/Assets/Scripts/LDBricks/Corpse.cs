using UnityEngine;

public class Corpse : MonoBehaviour
{
    public BloodStain bloodStain;

    private Portable portable;
    private void Awake()
    {
        portable = GetComponent<Portable>();
    }
    private void Update()
    {
        portable.enabled = bloodStain.bFinished;
        FMODUnity.RuntimeManager.PlayOneShot("event/Chara/happy");
    }
}