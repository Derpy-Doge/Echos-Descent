using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;
    public GameObject leftWallToggle;
    public GameObject rightWallToggle;
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
    }

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private float horizontalInput;
    private bool facingLeft = true;

    void Update()
    {
        if (leftCollider.enabled = true)
        {
            leftBoxrefToggle.SetActive(true);
            leftWallToggle.SetActive(true);
            rightBoxrefToggle.SetActive(false);
            rightWallToggle.SetActive(false);
        }
        else
        {
            leftBoxrefToggle.SetActive(false);
            leftWallToggle.SetActive(false);
            rightBoxrefToggle.SetActive(true);
            rightWallToggle.SetActive(true);
        }

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
