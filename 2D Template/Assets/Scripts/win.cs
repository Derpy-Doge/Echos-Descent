using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour
{
    public GameObject winScreen;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        winScreen.SetActive(true);
    }

    private void Start()
    {
        winScreen.SetActive(false);
    }

}
