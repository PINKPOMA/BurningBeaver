using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    [SerializeField] Image slider;
    [SerializeField] TextMeshProUGUI title;
    
    bool isShaking;
    
    public float FillAmount
    {
        get => slider.fillAmount;
        set
        {
            slider.fillAmount = value;
            title.color = FillAmount < 0.25f ? Color.red : FillAmount < 0.50f ? Color.yellow : Color.white;
        }
    }

    public void Shake()
    {
        if (isShaking)
        {
            return;
        }

        isShaking = true;
        DOTween.Shake(() => transform.localPosition,
            p => { transform.localPosition = (Vector2)p; },
            0.5f, 25.0f, 50).onComplete += () => isShaking = false;
    }
}
