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
    
    public void Create(PlayerController.GameOverReason gameOverReason)
    {
        var go = Instantiate(prefab, canvas.transform, false);

        go.GetComponentInChildren<TextMeshProUGUI>().text = gameOverReason == PlayerController.GameOverReason.PlayerDead ? "게임 오버!!!\n김비버 R.I.P." : "게임 오버!!!\n산 R.I.P.";
        
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
