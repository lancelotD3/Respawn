using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketInHand : MonoBehaviour
{
    Animator animator;

    bool ticketOpen = false;
    PlayerController2D playerController;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController2D>();
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
