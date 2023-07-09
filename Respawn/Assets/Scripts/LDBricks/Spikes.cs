using UnityEngine;

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
        PlayerController2D pc;
        if (!collision.TryGetComponent<PlayerController2D>(out pc))
            return;

        Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
        dir += Vector2.up * height;

        Rigidbody2D rb = pc.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(dir * repulsionForce, ForceMode2D.Impulse);
        rb.AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);

        if (pc.GetIsCarrying())
            pc.GetComponentInChildren<Portable>().Launch();

        pc.Stun(stunTime);
    }
}