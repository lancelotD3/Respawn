using UnityEngine;

public class Corpse : MonoBehaviour
{
    public BloodStain bloodStain;

    private Portable portable;
    private void Awake()
    {
        portable = GetComponentInChildren<Portable>();
    }
    private void Update()
    {
        portable.enabled = bloodStain.bFinished;
    }
}