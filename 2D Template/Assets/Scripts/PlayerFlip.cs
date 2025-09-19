using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    private float horizontalInput;
    private bool facingLeft = true;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

       
        SetupDirectionByComponent();
    }

    private void SetupDirectionByScale()
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

    private void SetupDirectionByRotation()
    {
        if (horizontalInput < 0 && facingLeft || horizontalInput > 0 && !facingLeft) ; 
    }
}
