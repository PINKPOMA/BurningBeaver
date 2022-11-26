using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkGauge : MonoBehaviour
{
    [SerializeField] private Image slider;
    [SerializeField] private TextMeshProUGUI title;

    public float FillAmount
    {
        get => slider.fillAmount;
        set => slider.fillAmount = value;
    }

    public string Title
    {
        set => title.text = value;
    }

    public void StartWork(float duration, Action action)
    {
        StopAllCoroutines();
        StartCoroutine(StartWorkCoro(duration, action));
    }

    private IEnumerator StartWorkCoro(float duration, Action action)
    {
        FillAmount = 0;
        while (FillAmount < 1)
        {
            FillAmount += 1.0f / duration * Time.deltaTime;
            yield return null;
        }

        action.Invoke();
    }
}
