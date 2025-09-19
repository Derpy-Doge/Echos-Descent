using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float JumpHeight;

    private Rigidbody2D rb2d;
    private float _movement;

    public bool isGrounded;

    private bool canJump;
    private bool isFacingRight = true;

    public GameObject boxRef;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpPower = new Vector2(8f, 16f);

    private bool isCrawling;


    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] public Animator animator;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.linearVelocityX = _movement;

        RaycastHit2D hitInfo;
        Vector2 boxsize = new Vector2(0.25f, 0.1f);
        hitInfo = Physics2D.BoxCast(boxRef.transform.position, boxsize, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Grounded"));

        boxRef.transform.localScale = boxsize;
        if (hitInfo)
        {
            Debug.Log("Grounded" + hitInfo.transform.gameObject.name);
            isGrounded = true;
        }
        else
        {
            Debug.Log("Not Grounded");
            isGrounded = false;
        }

        if (isGrounded)
        {
            canJump = true;
        }

        WallSlide();
        WallJump();

        float input = Input.GetAxis("Horizontal");
        if (input != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }


    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !isGrounded && rb2d.linearVelocityY < 0)
        {
            isWallSliding = true;
            rb2d.linearVelocityY = Mathf.Clamp(rb2d.linearVelocityY, -wallSlidingSpeed, float.MaxValue);
        }
        else
        {
            isWallSliding = false;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        _movement = ctx.ReadValue<Vector2>().x * moveSpeed;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<float>() == 1 && canJump)
        {
            rb2d.linearVelocityY = JumpHeight;

            canJump = false;
        }
    }

    public void Crawl(InputAction.CallbackContext ctx)
    { 
        if(ctx.ReadValue<float>() == 1)
        {
            isCrawling = true;
            animator.SetBool("isCrawling", true);
            GetComponent<BoxCollider2D>().size = new Vector2(2f, 1f);
        }
        else
        {
            isCrawling = false;
            animator.SetBool("isCrawling", false);
            GetComponent<BoxCollider2D>().size = new Vector2(0.7f, 1.829f);
        }
    }
    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpCounter > 0f)
        {
            isWallJumping = true;
            rb2d.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpCounter = 0f;

            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);

        }

    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

}