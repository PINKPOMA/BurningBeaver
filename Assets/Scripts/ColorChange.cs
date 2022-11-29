using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI flameProgressText;
    
    public Color startColor;
    public Color currentColor;
    public Color endColor;

    private void Start()
    {
        currentColor = startColor;
    }

    void Update()
    {
        titleText.color = currentColor;
        flameProgressText.color = currentColor;
        SetColor(playerController.flameProgress);
    }

    public void SetColor(float flameProgress)
    {
        currentColor.r = Mathf.Lerp(startColor.r, endColor.r, flameProgress / 100);
        currentColor.g = Mathf.Lerp(startColor.g, endColor.g, flameProgress / 100);
        currentColor.b = Mathf.Lerp(startColor.b, startColor.b, flameProgress / 100);
    }
}
