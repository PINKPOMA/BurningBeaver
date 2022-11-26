using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void OnAnimFinished()
    {
        SceneManager.LoadScene("InGame");
    }
}
