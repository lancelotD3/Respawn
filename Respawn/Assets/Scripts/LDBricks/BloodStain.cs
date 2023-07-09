using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BloodStain : LDBrick
{
    private bool bCanInteract = false;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private const int stain = 10;
    private int actualStain = 0;

    private PlayerController2D pc;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController2D>();
        actualStain = stain;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bCanInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bCanInteract = false;
    }

    private void Update()
    {
        if (!bCanInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E) && !bFinished && !pc.GetIsCarrying())
        {
            pc.StartSwipe();
            pc.rightSwipe = !pc.rightSwipe;
            //MARIUS
            FMODUnity.RuntimeManager.PlayOneShot("event:/Action/wipe");
            //MARIUS

            --actualStain;
            sprite.color = new Vector4(sprite.color.r,
                sprite.color.g,
                sprite.color.b,
                sprite.color.a * actualStain / stain);

            if (actualStain <= 0)
                bFinished = true;
        }
    }
}