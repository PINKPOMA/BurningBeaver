using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Slider _slTimer;
    void Start()
    {
        _slTimer = GetComponent<Slider>();
    }
 
    void Update()
    {
        if (_slTimer.value < 300f)
        {
            _slTimer.value += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
