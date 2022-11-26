using DG.Tweening;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private Color c1;
    [SerializeField] private Color c2;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject intro;
    
    void Update()
    {
        cam.backgroundColor = Color.Lerp(c1, c2, Mathf.PingPong(Time.time, 1));
    }

    public void OnStartGame()
    {
    	SoundManager.Instance.Play(SoundManager.Instance.buttonClickSound);
        intro.gameObject.SetActive(true);
    }

    public void OnCredits()
    {
        SoundManager.Instance.Play(SoundManager.Instance.buttonClickSound);
        credits.gameObject.SetActive(true);
        credits.transform.GetChild(0).DOPunchScale(Vector3.one / 5, 0.25f);
    }
}
