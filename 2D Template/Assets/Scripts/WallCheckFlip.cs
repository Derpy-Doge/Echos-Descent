using UnityEngine;

public class WallCheckFlip : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float horizontalInput;
    private bool flipped;
    private bool flopped;

    public Vector3 position = new Vector3(.77f,0f, 0f);
    public GameObject playerObject;
    public bool isWallCheck;

    void Start()
    {
        transform.localPosition = new(-position.x, position.y);
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (isWallCheck)
        {
            if (horizontalInput < 0)
            {
                Flip(true);
            }
            else if (horizontalInput > 0)
            {
                Flip(false);
            }
        }

        if (!isWallCheck)
        {
            if (horizontalInput < 0)
            {
                BoxFlip(true);
            }
            else if (horizontalInput > 0)
            {
                BoxFlip(false);
            }
        }
    }

    public void BoxFlip(bool directionFlip)
    {
        if (directionFlip)
        {
            transform.localPosition = new(-position.x, position.y);
            flipped = false;
        }
        else if (!directionFlip && !flipped)
        {
            transform.localPosition = position;
            flipped = true;
        }
    }

    public void CeilingCheckFlip(bool directionFlop)
    {
        if (directionFlop)
        {
            transform.localPosition = new(-position.x, position.y);
            flopped = false;
        }
        else if (!directionFlop && !flopped)
        {
            transform.localPosition = position;
            flopped = true;
        }
    }
    
    public void Flip(bool flipDirection)
    {
        if (flipDirection)
        {
            transform.localPosition = new(-position.x, position.y);
        }
        else
        {
            transform.localPosition = position;
        }
    }
}
