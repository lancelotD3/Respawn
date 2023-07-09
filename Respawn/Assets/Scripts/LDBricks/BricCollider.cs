using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BricCollider : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private PlayerController2D pc;
    private bool bPlayerInTrigger = false;

    private bool bColliderQuerry = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        pc = FindObjectOfType<PlayerController2D>();
    }

    public void SetTrigger(bool trigger)
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider2D>();

        if (trigger)
        {
            boxCollider.isTrigger = true;
            bColliderQuerry = false;
        }
        else
        {
            if (!bPlayerInTrigger)
                boxCollider.isTrigger = false;
            else
                bColliderQuerry = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bPlayerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bPlayerInTrigger = false;

        if (bColliderQuerry)
        {
            boxCollider.isTrigger = false;
            bColliderQuerry = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != pc.gameObject)
            return;

        Vector2 dir = (collision.transform.position - transform.position).normalized;
        float dot = Vector3.Dot(Vector3.down, dir);
        if (dot >= 0.7f)
            transform.parent.gameObject.GetComponent<Bric>().HitByBelow();
    }
}