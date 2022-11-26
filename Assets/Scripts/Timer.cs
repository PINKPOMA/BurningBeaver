using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    
    Slider _slTimer;
    void Start()
    {
        _slTimer = GetComponent<Slider>();
    }
 
    void Update()
    {
        if (playerController.IsDead)
        {
            return;
        }
        
        if (_slTimer.value < _slTimer.maxValue)
        {
            _slTimer.value += Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
