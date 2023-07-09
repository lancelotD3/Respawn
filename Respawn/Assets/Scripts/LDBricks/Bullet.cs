using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float repulsionForce = 10f;
    [SerializeField]
    private float stunTime = 2f;

    private void Awake()
    {
        Physics2D.queriesHitTriggers = false;
    }

    private void Update()
    {
        //transform.rotation = Quaternion.Euler(0, 0, Mathf.Cos(Vector2.Dot(Vector2.up, gameObject.GetComponent<Rigidbody2D>().velocity)));
        //transform.rotation = Quaternion.LookRotation(gameObject.GetComponent<Rigidbody2D>().velocity);
        //transform.rotation = Quaternion.Euler(0, 0, 45);
        //Debug.Log(transform.rotation.eulerAngles);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ignore self collision 
        if (collision.TryGetComponent<Shooter>(out _))
            return;

        PlayerController2D pc;
        if (collision.TryGetComponent<PlayerController2D>(out pc))
        {
            Rigidbody2D rb = pc.GetComponent<Rigidbody2D>();

            FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/tower_damage");

            Vector2 dir = (Vector3.right * (collision.transform.position.x - transform.position.x)).normalized;
            rb.velocity = Vector2.zero;
            transform.Rotate(new Vector3(rb.velocity.x, rb.velocity.x, rb.velocity.x));
            Debug.Log(transform.rotation);
            rb.AddForce(dir * repulsionForce, ForceMode2D.Impulse);
            rb.AddForce(Vector2.up * Physics2D.gravity.y * pc.gravityMultiplier, ForceMode2D.Impulse);

            if (pc.GetIsCarrying())
                pc.GetComponentInChildren<Portable>().Launch();

            pc.Stun(stunTime);
        }
        
        if (!collision.isTrigger)
            FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/bullet_touch");
            Destroy(gameObject);
    }
}