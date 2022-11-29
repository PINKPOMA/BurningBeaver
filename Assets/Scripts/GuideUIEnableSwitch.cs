using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUIEnableSwitch : MonoBehaviour
{
    private bool _active = true;
    public GameObject ui;
    public void SetUI()
    {
        _active = !_active;
        ui.SetActive(_active);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetUI();
        }
    }
}
