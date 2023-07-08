using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Heal : LDBrick
{
    private bool canInteract = false;

    [SerializeField]
    private float fillSpeed = 0.2f;
    [SerializeField]
    private float cooldown = 1f;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Image image2;
    [SerializeField]
    private GameObject bigUI;
    [SerializeField]
    private float finishedThreshold = 0.9f;

    private float counter = 0f;
    // between 0f and 1f
    private float value = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController2D>(out _))
            canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController2D>(out _))
        {
            canInteract = false;
            bigUI.SetActive(false);
        }
    }

    private void Update()
    {
        bFinished = value >= finishedThreshold && value <= 1f;

        if (!canInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            bigUI.SetActive(!bigUI.activeSelf);
            FindObjectOfType<PlayerController2D>().EnableController(!bigUI.activeSelf);
            FindObjectOfType<PlayerController2D>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (bigUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bigUI.SetActive(false);
                FindObjectOfType<PlayerController2D>().EnableController(true);
            }

            if (Input.GetKey(KeyCode.Space) && Time.time - counter > cooldown)
            {
                value += fillSpeed * Time.deltaTime;
                if (value > 1f)
                {
                    value *= 0.5f;
                    counter = Time.time;
                }
                image.rectTransform.localPosition = Vector3.up * (-1f + value);
                image2.rectTransform.localPosition = Vector3.up * (-1f + value);
            }
        }
    }
}