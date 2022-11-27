using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject afterButton;
    [SerializeField] private string afterSceneName = "InGame";
    [SerializeField] private Button defaultButton;

    private void OnEnable()
    {
        if (defaultButton)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }
    }

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