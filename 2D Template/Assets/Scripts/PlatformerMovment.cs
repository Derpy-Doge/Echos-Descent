using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    public float dashForce;
    public float dashDuration;
    public float dashCooldown;
    public float moveSpeed;
    public float JumpHeight;
    public WallCheckFlip wcf;

    public Rigidbody2D rb2d;
    private float _movement;

    public bool isGrounded;

    private bool canDash = true;
    private bool isDashing = false;
    private bool canJump;
    private bool isFacingRight = true;
    private float move;

    public GameObject boxRef;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpPower = new Vector2(10f, 16f);

    private bool isCrawling;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] public Animator animator;
    [SerializeField] public SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

    }

    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");

        if(move > 0f)
        {
            isFacingRight = false;
        }
        else if(move < 0f)
        {
            isFacingRight = true;
        }

        if (isDashing)
        {
            isDashing = rb2d.linearVelocityX != _movement;
        }
        else if (!isDashing)
        {
            rb2d.linearVelocityX = _movement;
        }

       

            Vector2 boxsize = new Vector2(0.502f, 0.05f);
        bool overlap = Physics2D.OverlapBox(boxRef.transform.position, boxsize, 0f, LayerMask.GetMask("Grounded"));
        boxRef.transform.localScale = boxsize;
        if (overlap)
        {
            Debug.Log("Grounded");
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
        else
        {
            Debug.Log("Not Grounded");
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }

        if (isGrounded && !isCrawling)
        {
            canJump = true;
        }

        if (isCrawling)
        {
            moveSpeed = 3f;
        }
        else if (!isCrawling )
        {
            moveSpeed = 6f;
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

        if (Input.GetKey(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        if(Input.GetKey(KeyCode.LeftShift) && canDash && isCrawling)
        {
            StartCoroutine(Dash());
            GetComponent<BoxCollider2D>().size = new Vector2(1.8f, 0.65f);
            GetComponent<BoxCollider2D>().offset = new Vector2(0.24f, -0.13f);
        }

        if (isDashing)
        {
            return;
        }

        if(isDashing && isCrawling)
        {
            animator.SetBool("isSliding", true);
        }
        else
        {
            animator.SetBool("isSliding", false);
        }

        if (canDash)
        {
            animator.SetBool("canDash", true);
        }
        else if(!canDash)
        {
            animator.SetBool("canDash", false);
        }

        if (!isGrounded && !canJump)
        {
            animator.SetBool("isDoubleJumping", true);
        }
        else
        {
            animator.SetBool("isDoubleJumping", false);
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
            animator.SetBool("isWallSliding", true);
            if(isFacingRight)
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(-.18f, .26f);
            }
            else if (!isFacingRight)
            {
                GetComponent<BoxCollider2D>().offset = new Vector2(.18f, .26f);
            }

                rb2d.linearVelocityY = Mathf.Clamp(rb2d.linearVelocityY, -wallSlidingSpeed, float.MaxValue);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("isWallSliding", false);
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
        if (ctx.ReadValue<float>() == 1)
        {
            isCrawling = true;
            canJump = false;
            animator.SetBool("isCrawling", true);
            Vector2 boxsize = new Vector2(1.16f, 0.046f);
            GetComponent<BoxCollider2D>().size = new Vector2(2f, 1f);
        }
        else
        {
            isCrawling = false;
            canJump = true;
            animator.SetBool("isCrawling", false);
            Vector2 boxsize = new Vector2(0.502f, 0.05f);
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

            isFacingRight = !isFacingRight;

            if (transform.localScale.x != wallJumpDirection)
            {
                
                spriteRenderer.flipX = true;

                if (isFacingRight)
                {
                    wcf.Flip(true);
                    wcf.BoxFlip(true);
                }
                else if (!isFacingRight)
                {
                    wcf.Flip(false);
                    wcf.BoxFlip(false);
                }

            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    IEnumerator Dash()
    {
        canDash = false;
        canJump = false;
        isDashing = true;

        float dashDirection;
        float originalGravity = 4f;

        if (isFacingRight)
        {
            dashDirection = -1f;
        }
        else
        {
            dashDirection = 1f;
        }

        originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.linearVelocity = new Vector2(dashForce * dashDirection, 0f);
        animator.SetBool("isDashing", true);

        yield return new WaitForSeconds(dashDuration);
        
        rb2d.gravityScale = originalGravity;
        rb2d.linearVelocity = Vector2.zero;
        animator.SetBool("isDashing", false);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}