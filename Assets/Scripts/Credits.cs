using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private GameObject prevSelectedGameObject;
    
    private void OnEnable()
    {
        prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);
    }

    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(prevSelectedGameObject);
    }
}
