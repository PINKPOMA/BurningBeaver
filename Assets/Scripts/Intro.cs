using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject afterButton;
    [SerializeField] private string afterSceneName = "InGame";

    public void OnAnimFinished()
    {
        if (afterButton)
        {
            afterButton.gameObject.SetActive(true);
            afterButton = null;
        }
        else
        {
            SceneManager.LoadScene(afterSceneName);
        }
    }
}