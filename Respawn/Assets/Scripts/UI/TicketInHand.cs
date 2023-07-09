using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketInHand : MonoBehaviour
{
    Animator animator;

    public bool ticketOpen = true;
    PlayerController2D playerController;
    LevelManager lm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController2D>();
        lm = FindObjectOfType<LevelManager>();

        if(lm != null)
        {
            Image image = GetComponent<Image>();
            image.sprite = FindObjectOfType<GameManager>().idlingLevels[0].map;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ticketOpen = !ticketOpen;

            if(ticketOpen)
            {
                animator.enabled = true;
                animator.Play("TicketAnimation");
                playerController?.EnableController(false, true);
            }
            else
            {
                animator.Play("CloseTicket");
                playerController?.EnableController(true, true);
            }
        }
    }
}
