using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private Slider _slTimer;

    private void Start()
    {
        _slTimer = GetComponent<Slider>();
    }

    private void Update()
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
