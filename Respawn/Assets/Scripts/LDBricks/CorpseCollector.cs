using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class CorpseCollector : LDBrick
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Corpse>(out _))
            bFinished = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Corpse>(out _))
            bFinished = false;
    }
}