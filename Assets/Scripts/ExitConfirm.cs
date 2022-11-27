using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitConfirm : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Canvas canvas;

    private GameObject prevSelectedGameObject;

    public void Create()
    {
        prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        
        var go = Instantiate(prefab, canvas.transform, false);

        go.GetComponentInChildren<TextMeshProUGUI>().text = "게임 종료하기";

        var btnList = go.GetComponentsInChildren<Button>();

        btnList[0].GetComponentInChildren<TextMeshProUGUI>().text = "확인";
        btnList[0].onClick.AddListener(Application.Quit);

        btnList[1].GetComponentInChildren<TextMeshProUGUI>().text = "취소";
        btnList[1].onClick.AddListener(() =>
        {
            EventSystem.current.SetSelectedGameObject(prevSelectedGameObject);
            Destroy(go);
        });
        
        EventSystem.current.SetSelectedGameObject(btnList[1].gameObject);
    }
}