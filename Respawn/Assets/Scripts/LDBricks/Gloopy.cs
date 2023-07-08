using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Gloopy : LDBrick
{
    public GameObject leftDetector;
    public GameObject rightDetector;
    public GameObject leftDetectorAir;
    public GameObject rightDetectorAir;

    public LayerMask layerMask;

    public float speed = .5f;
    Rigidbody2D rb;

    bool canSwitch = true;

    Vector2 direction = Vector2.right;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0);

        RaycastHit2D leftHit = Physics2D.Raycast(leftDetector.transform.position, -Vector2.left);
        if(leftHit.collider != null && leftHit.distance < 1 && canSwitch)
        {
            speed = -speed;
            canSwitch = false;
            StartCoroutine(DelaySwitch());

        }

        RaycastHit2D righttHit = Physics2D.Raycast(leftDetector.transform.position, -Vector2.right);
        if (righttHit.collider != null && righttHit.distance < 1 && canSwitch)
        {
            speed = -speed;
            canSwitch = false;
            StartCoroutine(DelaySwitch());
        }

        RaycastHit2D rightAirtHit = Physics2D.Raycast(rightDetectorAir.transform.position, -Vector2.up);
        if (rightAirtHit.collider == null && canSwitch)
        {
            speed = -speed;
            canSwitch = false;
            StartCoroutine(DelaySwitch());
        }
        RaycastHit2D leftAirtHit = Physics2D.Raycast(leftDetectorAir.transform.position, -Vector2.up);
        if (leftAirtHit.collider == null && canSwitch)
        {
            speed = -speed;
            canSwitch = false;
            StartCoroutine(DelaySwitch());
        }


    }

    IEnumerator DelaySwitch()
    {
        yield return new WaitForSeconds(0.2f);
        canSwitch = true;
    }
}
