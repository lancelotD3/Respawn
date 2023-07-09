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

        //MARIUS
        if ((Time.time - shootTime + 1.4 >= rate) && (Time.time - shootTime + 1.25 <= rate))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/tower_preshoot");
        }

            if (Time.time - shootTime >= rate)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/enemies/tower_shoot");

            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, 45);
            //bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Cos(Vector2.Dot(gameObject.transform.position, target.transform.position)));

            bullet.GetComponent<Bullet>().GetComponent<Rigidbody2D>().AddForce((target.transform.position -
                transform.position).normalized * bulletSpeed);
            shootTime = Time.time;
        }
    }
}