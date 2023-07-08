using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float repulsionForce = 10f;
    [SerializeField]
    private float stunTime = 2f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ignore self collision 
        if (collision.TryGetComponent<Shooter>(out _))
            return;

        if (collision.TryGetComponent<PlayerController2D>(out _))
        {
            Debug.Log(collision.name);
            PlayerController2D pc;
            if (!collision.TryGetComponent<PlayerController2D>(out pc))
                return;

            Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
            pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            pc.GetComponent<Rigidbody2D>().AddForce(dir * repulsionForce, ForceMode2D.Impulse);
            pc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);
            pc.Stun(stunTime);
            Debug.Log(collision.name);
        }
        Destroy(gameObject);
    }
}