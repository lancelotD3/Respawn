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

    [SerializeField]
    private float repulsionForce = 10f;
    [SerializeField]
    private float height = 1f;
    [SerializeField]
    private float stunTime = 2f;

    Vector2 direction = Vector2.right;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftDetector.transform.position, -Vector2.left);
        if(leftHit.collider != null && leftHit.distance < 1 && canSwitch)
        {
            if (leftHit.collider.gameObject.tag != "Player")
            {
                speed = -speed;
                canSwitch = false;
                StartCoroutine(DelaySwitch());
            }
        }

        RaycastHit2D righttHit = Physics2D.Raycast(leftDetector.transform.position, -Vector2.right);
        if (righttHit.collider != null && righttHit.distance < 1 && canSwitch)
        {
            if (righttHit.collider.gameObject.tag != "Player")
            {
                speed = -speed;
                canSwitch = false;
                StartCoroutine(DelaySwitch());
            }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController2D pc;
        if (!collision.gameObject.TryGetComponent<PlayerController2D>(out pc))
            return;

        Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
        dir += Vector2.up * height;
        pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pc.GetComponent<Rigidbody2D>().AddForce(dir * repulsionForce, ForceMode2D.Impulse);
        pc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);
        pc.Stun(stunTime);
    }
    IEnumerator DelaySwitch()
    {
        yield return new WaitForSeconds(0.2f);
        canSwitch = true;
    }
}
