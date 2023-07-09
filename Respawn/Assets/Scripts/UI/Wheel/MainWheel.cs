using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWheel : MonoBehaviour
{
    public GameObject SwordWheel;
    public GameObject ShieldWheel;
    public GameObject BootWheel;
    public GameObject StarWheel;
    public GameObject HearthWheel;

    public void OpenSwordWheel()
    {
        Debug.Log("ouais");
        CloseAllWheel();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
        SwordWheel.SetActive(true);
    }
    public void CloseSwordWheel() => SwordWheel.SetActive(false);

    public void OpenShieldWheel()
    {
        CloseAllWheel();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
        ShieldWheel.SetActive(true);
    }
    public void CloseShieldWheel() => ShieldWheel.SetActive(false);

    public void OpenBootWheel()
    {
        CloseAllWheel();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
        BootWheel.SetActive(true);
    }
    public void CloseBootWheel() => BootWheel.SetActive(false);

    public void OpenStarWheel()
    {
        CloseAllWheel();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
        StarWheel.SetActive(true);
    }
    public void CloseStarWheel() => StarWheel.SetActive(false);

    public void OpenHearthWheel()
    {
        CloseAllWheel();
        FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
        HearthWheel.SetActive(true);
    }
    public void CloseHearthWheel() => HearthWheel.SetActive(false);

    public void CloseAllWheel()
    {


       

        gameObject.SetActive(false);
        CloseBootWheel();
        CloseHearthWheel();
        CloseShieldWheel();
        CloseStarWheel();
        CloseSwordWheel();
    }
}
