using DG.Tweening;
using TMPro;
using UnityEngine;

public class SystemMessage : MonoBehaviour
{
    public Canvas canvas;
    public GameObject prefab;

    public void Create(string text)
    {
        var go = Instantiate(prefab, canvas.transform, false);
        go.GetComponentInChildren<TextMeshProUGUI>().text = text;
        //go.transform.DOLocalMoveY(5.0f, 1.0f).onComplete += () => Destroy(go);
        go.transform.DOBlendableLocalMoveBy(50.0f * Vector2.up, 1.0f).onComplete += () => Destroy(go);
    }
}
