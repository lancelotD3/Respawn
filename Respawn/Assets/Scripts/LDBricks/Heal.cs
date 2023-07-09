using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Heal : LDBrick
{
    private bool canInteract = false;


    private FMOD.Studio.EventInstance instance;

    [SerializeField]
    private float fillSpeed = 0.2f;
    [SerializeField]
    private float cooldown = 1f;
    [SerializeField]
    private Image imageUI;
    [SerializeField]
    private GameObject bigUI;
    [SerializeField]
    private float finishedThreshold = 0.9f;

    private float counter = 0f;
    // between 0f and 1f
    private float value = 0f;

    [SerializeField]
    private GameObject full;
    [SerializeField]
    private GameObject empty;


    private PlayerController2D pc;

    private void Awake()
    {
        pc = FindObjectOfType<PlayerController2D>();
        //MARIUS
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/Mini_Jeu/Liquid_Pouring");
        instance.start();
        //MARIUS

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

    public GameObject bouteille;
    public GameObject verser;
    public GameObject star;

    private void Update()
    {
        bFinished = value >= finishedThreshold && value <= 1f;
        if (bFinished)
        {
            full.SetActive(true);
            empty.SetActive(false);
            star.SetActive(true);
        }
        else
        {
            full.SetActive(false);
            empty.SetActive(true);
            star.SetActive(false);
        }

        if (!canInteract || pc.GetIsCarrying())
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {

            FMODUnity.RuntimeManager.PlayOneShot("event:/Chara/wander");
            bigUI.SetActive(!bigUI.activeSelf);
            pc.EnableController(!bigUI.activeSelf, true);
            pc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (bigUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                //MARIUS
                var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
                emitter.SetParameter("parameter:/Liquid_Lvl", 0);
                instance.release();
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                //MARIUS
                bigUI.SetActive(false);
                pc.EnableController(true);

                

            }

            bouteille.SetActive(true);
            verser.SetActive(false);
            if (Input.GetKey(KeyCode.Space) && Time.time - counter > cooldown)
            {
                instance.setParameterByName("parameter:/Liquid_Lvl", value);
                float effectiveRPM = Mathf.Lerp(0, 1, value);
                var emitter = GetComponent<FMODUnity.StudioEventEmitter>();
                emitter.SetParameter("parameter:/Liquid_Lvl", effectiveRPM);
                value += fillSpeed * Time.deltaTime;
                Debug.Log(value);
                if (value > 1f)
                {
                    value *= 0.5f;
                    counter = Time.time;
                }

                float y = Unity.Mathematics.math.remap(0f, 1f, min, max, value);
                
                imageUI.rectTransform.localPosition = Vector3.up * y;

                bouteille.SetActive(false);
                verser.SetActive(true);
            }
        }
    }
}