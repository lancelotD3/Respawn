using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController2D : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected CapsuleCollider2D playerCollider;


    [SerializeField]
    protected string horizontalAxis = "Horizontal";
    [SerializeField]
    protected string jumpButton = "Jump";


    [SerializeField]
    protected float maxMovementSpeed = 10f;
    [SerializeField]
    protected float acceleration = 99f;
    [SerializeField]
    protected float decay = 8f;
    [SerializeField]
    protected float jumpForce = 25f;


    [SerializeField]
    protected float groundCheckDistance = 0.1f;

    [SerializeField]
    protected float airDragCoefficient = 0.2f;
    [SerializeField]
    protected float airResistance = 49f;
    [SerializeField]
    protected float falloffAcceleration = 51f;
    [SerializeField]
    protected float maxFalloffSpeed = 50f;
    public float gravityMultiplier = 1.1f;

    protected Vector2 inputVelocity;
    protected bool bIsGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        // raycasts will ignore already colliding objects
        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;
    }

    [HideInInspector]
    public Vector2 lastInputVelocity = Vector2.zero;
    protected virtual void Update()
    {
        CheckIsGrounded();

        JumpHandle();
        HorizontalMovementHandle();

        if (inputVelocity.sqrMagnitude > 0f)
            lastInputVelocity = inputVelocity.normalized;
    }

    private Vector2 lastVelocity;
    protected virtual void FixedUpdate()
    {
        float yVelocity = rb.velocity.y;
        float drag = bIsGrounded ? 1f : airDragCoefficient;

        // horizontal velocity
        Vector2 v = rb.velocity;
        if (inputVelocity.sqrMagnitude > 0f)
            v += inputVelocity * acceleration * drag * Time.fixedDeltaTime;
        else
            v -= v * decay * drag * Time.fixedDeltaTime;
        float vx = Mathf.Clamp(v.x, -maxMovementSpeed, maxMovementSpeed);

        // vertical velocity
        yVelocity -= (yVelocity >= 0f ? airResistance : falloffAcceleration) * Time.fixedDeltaTime;
        yVelocity = Mathf.Clamp(yVelocity, -maxFalloffSpeed, Mathf.Infinity);


        rb.velocity = new Vector2(vx, yVelocity);
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float yVelocity = rb.velocity.y;
        rb.velocity = new Vector2(-collision.relativeVelocity.x, 0f).normalized * Mathf.Abs(lastVelocity.x);
        rb.velocity += Vector2.up * yVelocity;
    }

    [SerializeField]
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter = 0f;
    private void CheckIsGrounded()
    {
        bIsGrounded = Physics2D.CircleCast(transform.position, playerCollider.size.x, Vector2.down, groundCheckDistance);
        if (bIsGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    [SerializeField]
    private float jumpMercy = 0.1f;
    private bool jumpQuerry = false;
    IEnumerator JumpMercy()
    {
        yield return new WaitForSeconds(jumpMercy);
        jumpQuerry = false;
    }
    [SerializeField]
    private float adaptativeJumpSpeedThreshold = 15f;
    private void JumpHandle()
    {
        if (Input.GetButtonUp(jumpButton))
        {
            coyoteTimeCounter = 0f;
            if (rb.velocity.y > adaptativeJumpSpeedThreshold)
                rb.AddForce(Vector2.up * Physics2D.gravity.y * gravityMultiplier, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown(jumpButton))
        {
            jumpQuerry = true;
            StartCoroutine(JumpMercy());
        }

        if (jumpQuerry && coyoteTimeCounter > 0f)
        {
            rb.velocity -= Vector2.Scale(Vector2.up, rb.velocity);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpQuerry = false;
        }
    }

    private void HorizontalMovementHandle()
    {
        inputVelocity = Vector2.right * Input.GetAxisRaw(horizontalAxis);
        inputVelocity.Normalize();
    }

    public void EnableController(bool enable)
    {
        enabled = enable;
    }

    public void Stun(float stunTime)
    {
        StartCoroutine(StunCoroutine(stunTime));
    }
    private IEnumerator StunCoroutine(float stunTime)
    {
        EnableController(false);
        yield return new WaitForSeconds(stunTime);
        EnableController(true);
    }
}