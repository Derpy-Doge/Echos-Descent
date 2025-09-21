using UnityEngine;

public class WallCheckFlip : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float horizontalInput;
    private bool flipped;

    public Vector3 position = new Vector3(.77f,0f, 0f);
    public GameObject playerObject;
    public bool isWallCheck;

    void Start()
    {
        transform.position = playerObject.transform.position;
        transform.position -= position;
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
            transform.position = playerObject.transform.position;
            transform.position -= position;
            flipped = false;
        }
        else if (!directionFlip && !flipped)
        {
            Vector3 xFlip = transform.position;
            xFlip.x -= -0.3058f*2;
            transform.position = playerObject.transform.position;
            transform.position = xFlip;
            flipped = true;
        }
    }
    
    public void Flip(bool flipDirection)
    {
       if(flipDirection)
        {
            transform.position = playerObject.transform.position;
            transform.position -= position;
        }
       else if(!flipDirection)
        {
            transform.position = playerObject.transform.position;
            transform.position += position;
        }
    }
}
