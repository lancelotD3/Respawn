using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BloodStain : LDBrick
{
    bool bCanInteract = false;

    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private const int stain = 10;
    private int actualStain = 0;

    private void Awake()
    {
        actualStain = stain;
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
        if (!bCanInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E) && !bFinished)
        {
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