using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToTitle : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Canvas canvas;
    
    private GameObject prevSelectedGameObject;
    
    public void Create()
    {
        prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        
        var go = Instantiate(prefab, canvas.transform, false);

        go.GetComponentInChildren<TextMeshProUGUI>().text = "타이틀로 돌아가기";
        
        var btnList = go.GetComponentsInChildren<Button>();

        btnList[0].GetComponentInChildren<TextMeshProUGUI>().text = "확인";
        btnList[0].onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });
        
        btnList[1].GetComponentInChildren<TextMeshProUGUI>().text = "취소";
        btnList[1].onClick.AddListener(() =>
        {
            Destroy(go);
            EventSystem.current.SetSelectedGameObject(prevSelectedGameObject);
        });
        
        EventSystem.current.SetSelectedGameObject(btnList[1].gameObject);
    }
}
