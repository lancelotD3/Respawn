using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Chest : LDBrick
{
    private bool bFull = false;
    private bool bCanInteract = false;

    [SerializeField]
    private string horizontalAxis = "Horizontal";
    [SerializeField]
    private string verticalAxis = "Vertical";

    [SerializeField]
    private GameObject ui;

    private int digit0 = 0;
    private int digit1 = 0;
    private int digit2 = 0;
    private int digit3 = 0;

    [SerializeField]
    private TextMeshProUGUI text0;
    [SerializeField]
    private TextMeshProUGUI text1;
    [SerializeField]
    private TextMeshProUGUI text2;
    [SerializeField]
    private TextMeshProUGUI text3;

    private PlayerController2D pc;


    private void Awake()
    {
        digit0 = Random.Range(0, 10);
        digit1 = Random.Range(0, 10);
        digit2 = Random.Range(0, 10);
        digit3 = Random.Range(0, 10);

        pc = FindObjectOfType<PlayerController2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Treasure>(out _))
        {
            bFull = true;
            Destroy(collision.gameObject);
        }

        if (!bFull)
            return;

        if (collision.gameObject == pc.gameObject)
            bCanInteract = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == pc.gameObject)
            bCanInteract = false;
    }

    public int select = 0;
    private void Update()
    {
        bFinished = digit0 == 0 && digit1 == 0 && digit2 == 0 && digit3 == 0;
        if (!bCanInteract || pc.GetIsCarrying())
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            ui.SetActive(!ui.activeSelf);
            pc.EnableController(!ui.activeSelf, true);
            pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (ui.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ui.SetActive(false);
                pc.EnableController(true);
            }

            if (Input.GetButtonDown(horizontalAxis))
            {
                int d = select + (int)Input.GetAxisRaw(horizontalAxis);
                select = (d < 0 ? 3 : d) % 4;
            }

            if (Input.GetButtonDown(verticalAxis))
            {
                switch (select)
                {
                    case 0:
                        {
                            int d = digit0 + (int)Input.GetAxisRaw(verticalAxis);
                            digit0 = (d < 0 ? 9 : d) % 10;
                            break;
                        }
                    case 1:
                        {
                            int d = digit1 + (int)Input.GetAxisRaw(verticalAxis);
                            digit1 = (d < 0 ? 9 : d) % 10;
                            break;
                        }
                    case 2:
                        {
                            int d = digit2 + (int)Input.GetAxisRaw(verticalAxis);
                            digit2 = (d < 0 ? 9 : d) % 10;
                            break;
                        }
                    case 3:
                        {
                            int d = digit3 + (int)Input.GetAxisRaw(verticalAxis);
                            digit3 = (d < 0 ? 9 : d) % 10;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        text0.text = digit0.ToString();
        text1.text = digit1.ToString();
        text2.text = digit2.ToString();
        text3.text = digit3.ToString();
    }

    public void AddDigit0()
    {
        digit0 += 1;
        digit0 = digit0 % 10;
    }
    public void RemoveDigit0()
    {
        digit0 -= 1;
        if (digit0 < 0) digit0 = 9;
        digit0 = digit0 % 10;
    }
    public void AddDigit1()
    {
        digit1 += 1;
        digit1 = digit1 % 10;
    }
    public void RemoveDigit1()
    {
        digit1 -= 1;
        if (digit1 < 0) digit1 = 9;
        digit1 = digit1 % 10;
    }
    public void AddDigit2()
    {
        digit2 += 1;
        digit2 = digit2 % 10;
    }
    public void RemoveDigit2()
    {
        digit2 -= 1;
        if (digit2 < 0) digit2 = 9;
        digit2 = digit2 % 10;
    }
    public void AddDigit3()
    {
        digit3 += 1;
        digit3 = digit3 % 10;
    }
    public void RemoveDigit3()
    {
        digit3 -= 1;
        if (digit3 < 0) digit3 = 9;
        digit3 = digit3 % 10;
    }
}