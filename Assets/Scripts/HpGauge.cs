using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    public Image slider;
    bool isShaking;

    public float FillAmount
    {
        get => slider.fillAmount;
        set => slider.fillAmount = value;
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
