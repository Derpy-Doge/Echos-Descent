using UnityEngine;

public class ColliderFlip : MonoBehaviour
{
    public BoxCollider2D leftCollider;
    public BoxCollider2D rightCollider;
    private SpriteRenderer spriteRenderer;

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

    // Update is called once per frame
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
    }
}
