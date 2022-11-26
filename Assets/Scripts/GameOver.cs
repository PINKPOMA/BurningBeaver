using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField]
    Canvas canvas;
    
    public void Create()
    {
        var go = Instantiate(prefab, canvas.transform, false);

        go.GetComponentInChildren<TextMeshProUGUI>().text = "게임 오버!!!";
        
        var btnList = go.GetComponentsInChildren<Button>();

        btnList[0].GetComponentInChildren<TextMeshProUGUI>().text = "다시 시작";
        btnList[0].onClick.AddListener(() =>
        {
            SceneManager.LoadScene("InGame");
        });
        
        btnList[1].GetComponentInChildren<TextMeshProUGUI>().text = "타이틀로";
        btnList[1].onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });
    }
}
