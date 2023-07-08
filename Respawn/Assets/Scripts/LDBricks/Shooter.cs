using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Shooter : MonoBehaviour
{
    [SerializeField]
    private float rate = 0.2f;
    [SerializeField]
    private float bulletSpeed = 1f;

    [SerializeField]
    private GameObject bulletPrefab;

    private GameObject target = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController2D>(out _))
            target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
            target = null;
    }

    private float shootTime = 0f;
    private void Update()
    {
        if (target == null)
            return;

        if (Time.time - shootTime >= rate)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().GetComponent<Rigidbody2D>().AddForce((target.transform.position -
                transform.position).normalized * bulletSpeed);
            shootTime = Time.time;
        }
    }
}