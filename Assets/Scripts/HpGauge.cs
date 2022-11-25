using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HpGauge : MonoBehaviour
{
    public Image slider;

    public float FillAmount
    {
        set => slider.fillAmount = value;
    }

    public void Shake()
    {
        DOTween.Shake(() => transform.localPosition,
            p => { transform.localPosition = (Vector2)p; },
            0.5f, 25.0f, 50);
    }
}
