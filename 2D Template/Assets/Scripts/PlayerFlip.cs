using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    private float horizontalInput;
    private bool faceingLeft = true;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        SetupDirectionByScale();
    }

    private void SetupDirectionByScale()
    {
        if(horizontalInput < 0 && faceingLeft || horizontalInput > 0 && !faceingLeft)
        {
            faceingLeft = !faceingLeft;
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }
}
