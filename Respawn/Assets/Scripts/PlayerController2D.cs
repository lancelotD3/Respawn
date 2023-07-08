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
    protected float decay = 20f;
    [SerializeField]
    protected float jumpForce = 25f;


    [SerializeField]
    protected float groundCheckDistance = 0.2f;

    [SerializeField]
    protected float airDragCoefficient = 0.2f;
    [SerializeField]
    protected float airResistance = 0.9f;
    [SerializeField]
    protected float falloffAcceleration = 1.1f;
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
    }

    protected virtual void Update()
    {
        CheckIsGrounded();

        JumpHandle();
        HorizontalMovementHandle();
    }

    protected virtual void FixedUpdate()
    {
        float yVelocity = rb.velocity.y;
        float drag = bIsGrounded ? 1f : airDragCoefficient;

        // horizontal velocity
        if (inputVelocity.sqrMagnitude > 0f)
            rb.velocity += inputVelocity * acceleration * drag * Time.fixedDeltaTime;
        else
            rb.velocity -= rb.velocity * decay * drag * Time.fixedDeltaTime;
        rb.velocity = Vector2.ClampMagnitude(new Vector2(rb.velocity.x, 0f), maxMovementSpeed);

        // vertical velocity
        yVelocity -= yVelocity >= 0f ? airResistance : falloffAcceleration;
        yVelocity = Mathf.Clamp(yVelocity, -maxFalloffSpeed, Mathf.Infinity);
        rb.velocity = new Vector2(rb.velocity.x, yVelocity);
    }

    private void CheckIsGrounded()
    {
        bIsGrounded = Physics2D.CircleCast(transform.position, playerCollider.size.x, Vector2.down, groundCheckDistance);
    }

    private void JumpHandle()
    {
        if (Input.GetButtonUp(jumpButton) && rb.velocity.y > 0f)
        {
            rb.AddForce(Vector2.up * Physics2D.gravity.y * gravityMultiplier, ForceMode2D.Impulse);
        }

        if (!bIsGrounded)
            return;

        if (Input.GetButtonDown(jumpButton))
        {
            rb.velocity -= Vector2.Scale(Vector2.up, rb.velocity);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
}