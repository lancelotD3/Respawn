using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerController2D playerController;
    public Animator animator;

    private void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerController.GetVelocity().x));
        animator.SetFloat("VelocityY", playerController.GetVelocity().y);
        animator.SetBool("Stun", playerController.IsStun());
        animator.SetBool("Carry", playerController.GetIsCarrying());
        animator.SetBool("Grounded", playerController.GetIsGrounded());
        animator.SetBool("Swiping", playerController.GetIsSwiping());
        animator.SetBool("RightSwipe", playerController.rightSwipe);
    }

    private void FixedUpdate()
    {
        Vector3 scale = gameObject.transform.localScale;

        if(playerController.GetVelocity().x > 0)
            gameObject.transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);

        if (playerController.GetVelocity().x < 0)
            gameObject.transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);

    }
}
