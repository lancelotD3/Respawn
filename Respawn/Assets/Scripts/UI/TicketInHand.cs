using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketInHand : MonoBehaviour
{
    Animator animator;

    bool ticketOpen = true;
    PlayerController2D playerController;
    GameManager gm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController2D>();
        gm = FindObjectOfType<GameManager>();

        Image image = GetComponent<Image>();
        image.sprite = gm.idlingLevels[0].map;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ticketOpen = !ticketOpen;

            if(ticketOpen)
            {
                animator.Play("TicketAnimation");
                playerController.EnableController(false, true);
            }
            else
            {
                animator.Play("CloseTicket");
                playerController.EnableController(true, true);
            }
        }
    }
}
