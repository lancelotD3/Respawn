using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Portable : MonoBehaviour
{
    private bool bCanInteract = false;
    private bool bPickedUp = false;

    private void Awake()
    {
        //PhysicsMaterial2D m = new PhysicsMaterial2D();
        //m.friction = 999f;
        //GetComponent<Rigidbody2D>().sharedMaterial = m;
        //GetComponent<BoxCollider2D>().sharedMaterial = m;
        //GetComponent<CircleCollider2D>().sharedMaterial = m;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController2D>(out _))
            bCanInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController2D>(out _))
            bCanInteract = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!bPickedUp)
            {
                if (bCanInteract)
                    PickUp();
            }
            else
            {
                Launch();
            }
        }
    }

    private void PickUp()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().isTrigger = true;

        PlayerController2D pc = FindObjectOfType<PlayerController2D>();
        transform.position = pc.transform.position;
        transform.position += Vector3.up * (pc.transform.localScale.y + transform.localScale.y * 0.5f);
        transform.parent = pc.transform;

        bPickedUp = true;
    }

    [SerializeField]
    private float launchForce = 5f;
    [SerializeField]
    private float verticalForce = 0.5f;
    private void Launch()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;

        transform.parent = null;
        bPickedUp = false;
        bCanInteract = false;

        PlayerController2D pc = FindObjectOfType<PlayerController2D>();
        GetComponent<Rigidbody2D>().AddForce((pc.lastInputVelocity.normalized +
            Vector2.up * verticalForce) * launchForce + pc.GetComponent<Rigidbody2D>().velocity,
            ForceMode2D.Impulse);
    }
}