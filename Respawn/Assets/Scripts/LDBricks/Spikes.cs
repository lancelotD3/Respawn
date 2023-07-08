using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float repulsionForce = 10f;
    [SerializeField]
    private float height = 3f;
    [SerializeField]
    private float stunTime = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        PlayerController2D pc;
        if (!collision.TryGetComponent<PlayerController2D>(out pc))
            return;

        Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
        dir += Vector2.up * height;
        pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        pc.GetComponent<Rigidbody2D>().AddForce(dir * repulsionForce, ForceMode2D.Impulse);
        pc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);
        StartCoroutine(Stun(pc));
        Debug.Log(collision.name);
    }

    IEnumerator Stun(PlayerController2D pc)
    {
        pc.EnableController(false);
        yield return new WaitForSeconds(stunTime);
        pc.EnableController(true);
    }
}