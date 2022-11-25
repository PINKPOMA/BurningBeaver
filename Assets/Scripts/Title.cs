using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Color c1;

    public Color c2;

    public Camera cam;
    
    void Update()
    {
        cam.backgroundColor = Color.Lerp(c1, c2, Mathf.PingPong(Time.time, 1));
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("InGame");
    }
}
