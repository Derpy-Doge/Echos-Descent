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

    public GameObject boxRef;

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


}

