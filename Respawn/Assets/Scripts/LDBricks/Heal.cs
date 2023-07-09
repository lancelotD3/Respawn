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


    private PlayerController2D pc;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
        {
            canInteract = false;
            bigUI.SetActive(false);
        }
    }

    public float min = 0f;
    public float max = 1f;

    private void Update()
    {
        bFinished = value >= finishedThreshold && value <= 1f;

        if (!canInteract || pc.GetIsCarrying())
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            bigUI.SetActive(!bigUI.activeSelf);
            pc.EnableController(!bigUI.activeSelf, true);
            pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (bigUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bigUI.SetActive(false);
                pc.EnableController(true);
            }

            if (Input.GetKey(KeyCode.Space) && Time.time - counter > cooldown)
            {
                value += fillSpeed * Time.deltaTime;
                if (value > 1f)
                {
                    value *= 0.5f;
                    counter = Time.time;
                }

                float y = Unity.Mathematics.math.remap(0f, 1f, min, max, value);

                image.rectTransform.localPosition = Vector3.up * y;
                image2.rectTransform.localPosition = Vector3.up * y;
            }
        }
    }
}