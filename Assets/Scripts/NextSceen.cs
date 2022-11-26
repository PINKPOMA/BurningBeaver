using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceen : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Title");
    }
}
