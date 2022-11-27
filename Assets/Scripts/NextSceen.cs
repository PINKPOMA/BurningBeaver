using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceen : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Title");
    }
}
