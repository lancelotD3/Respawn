using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BonusBlock : LDBrick
{
    BoxCollider2D boxCollider;

    public Image SelectedSprite;
    public Sprite WantedSprite;

    public GameObject mainWheel;

    bool canInteract = false;
    private void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canInteract = false;
            mainWheel.GetComponent<MainWheel>().CloseAllWheel();
            mainWheel.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {

            FMODUnity.RuntimeManager.PlayOneShot("event:/clic_off"); //MARIUS
            mainWheel.GetComponent<MainWheel>().CloseAllWheel();
            mainWheel.SetActive(true);
        }
    }
}
