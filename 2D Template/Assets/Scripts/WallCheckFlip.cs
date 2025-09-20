using UnityEngine;

public class WallCheckFlip : MonoBehaviour
{
    private float horizontalInput;
    private bool facingLeft = true;

    public Vector3 position = new Vector3(.77f,0f, 0f);
    public GameObject playerObject;

    void Start()
    {
        transform.position = playerObject.transform.position;
        transform.position -= position;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //Flip();

        if (horizontalInput < 0)
        {
            Flip(true);   
        }
        else if (horizontalInput > 0)
        {
            Flip(false);
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
