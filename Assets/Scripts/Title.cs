using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField] private Color c1;
    [SerializeField] private Color c2;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject credits;
    
    void Update()
    {
        cam.backgroundColor = Color.Lerp(c1, c2, Mathf.PingPong(Time.time, 1));
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    public void OnCredits()
    {
        credits.gameObject.SetActive(true);
        credits.transform.GetChild(0).DOPunchScale(Vector3.one / 5, 0.25f);
    }
}
