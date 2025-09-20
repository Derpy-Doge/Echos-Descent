using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;

    public GameObject leftBoxrefToggle;
    public GameObject rightBoxrefToggle;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.flipX)
        {
            leftCollider.enabled = true;
            rightCollider.enabled = false;
        }
        else
        {
            leftCollider.enabled = false;
            rightCollider.enabled = true;
        }

        leftBoxrefToggle.SetActive(true);
        rightBoxrefToggle.SetActive(false);
    }

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private float horizontalInput;
    private bool facingLeft = true;

    void Update()
    {
        if (spriteRenderer.flipX)
        {
            leftCollider.enabled = true;
            rightCollider.enabled = false;
        }
        else
        {
            leftCollider.enabled = false;
            rightCollider.enabled = true;
        }

        

        horizontalInput = Input.GetAxis("Horizontal");

        SetupDirectionByComponent();
        FlipColliderWhenTurn();
    }

    private void SetupDirectionByScale() // breaks with wall jump
    {
        if(horizontalInput < 0 && facingLeft || horizontalInput > 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    private void FlipColliderWhenTurn()
    {
        if (horizontalInput < 0)
        {
            leftBoxrefToggle.SetActive(true);
            rightBoxrefToggle.SetActive(false);
        }
        else if (horizontalInput > 0)
        {
            leftBoxrefToggle.SetActive(false);
            rightBoxrefToggle.SetActive(true);
        }
    }

    

    private void SetupDirectionByComponent()
    {
        if(horizontalInput < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(horizontalInput > 0)
        {
            spriteRenderer.flipX = true;
        }

    }

    private void SetupDirectionByRotation() // flips whole screen :(
    {
        if (horizontalInput < 0 && facingLeft || horizontalInput > 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            transform.Rotate(new Vector3(0,180,0));
        }
    }
}
