using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceen : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Title");
    }
}
