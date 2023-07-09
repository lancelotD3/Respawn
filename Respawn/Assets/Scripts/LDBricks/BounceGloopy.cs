using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BounceGloopy : MonoBehaviour
{
    [SerializeField]
    private float repulsionForce = 10f;
    [SerializeField]
    private float height = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController2D pc;
        if (!collision.TryGetComponent<PlayerController2D>(out pc))
            return;

        Debug.Log("ouaisouaisouais");
        Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
        dir += Vector2.up * height;
        pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pc.GetComponent<Rigidbody2D>().AddForce(dir * repulsionForce, ForceMode2D.Impulse);
        pc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController2D pc;
        if (!collision.collider.TryGetComponent<PlayerController2D>(out pc))
            return;

        Debug.Log("ouaisouaisouais");
        Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
        dir += Vector2.up * height;
        pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pc.GetComponent<Rigidbody2D>().AddForce(dir * repulsionForce, ForceMode2D.Impulse);
        pc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);
    }
}
