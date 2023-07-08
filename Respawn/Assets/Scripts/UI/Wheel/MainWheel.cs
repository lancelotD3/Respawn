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
        SwordWheel.SetActive(true);
    }
    public void CloseSwordWheel() => SwordWheel.SetActive(false);

    public void OpenShieldWheel()
    {
        CloseAllWheel();
        ShieldWheel.SetActive(true);
    }
    public void CloseShieldWheel() => ShieldWheel.SetActive(false);

    public void OpenBootWheel()
    {
        CloseAllWheel();
        BootWheel.SetActive(true);
    }
    public void CloseBootWheel() => BootWheel.SetActive(false);

    public void OpenStarWheel()
    {
        CloseAllWheel();
        StarWheel.SetActive(true);
    }
    public void CloseStarWheel() => StarWheel.SetActive(false);

    public void OpenHearthWheel()
    {
        CloseAllWheel();
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
