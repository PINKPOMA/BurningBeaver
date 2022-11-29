using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private SystemMessage sysMsg;
    [SerializeField] private TextMeshProUGUI userMoneyText;
    [SerializeField] private TextMeshProUGUI bucketPriceText;
    [SerializeField] private TextMeshProUGUI moveSpeedPriceText;
    [SerializeField] private TextMeshProUGUI waterPriceText;
    [SerializeField] private int bucketPrice;
    [SerializeField] private int moveSpeedPrice;
    [SerializeField] private int waterPrice;
    [SerializeField] private PlayerController player;
    private float _minFillingSpeed = 0.2f;
    private int _maxMoveSpeed = 9;
    private int _maxWaterCapacity = 5;

    private void OnEnable()
    {
        userMoneyText.text = $"Gold:{player.money}";
        bucketPriceText.text = $"{bucketPrice}G";
        moveSpeedPriceText.text = $"{moveSpeedPrice}G";
        waterPriceText.text = $"{waterPrice}G";
        
        Time.timeScale = 0;
    }
    
    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void UpgradeBucket()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player.money >= bucketPrice)
        {
            if (user.GetWaterCapacity >= _maxWaterCapacity) return;

            player.AddMoney(-bucketPrice);
            user.SetWaterCapacity(1);
            bucketPrice += bucketPrice / 10;
            bucketPriceText.text = $"{bucketPrice}G";
            userMoneyText.text =$"Gold:{player.money}";
        }
        else
        {
            sysMsg.Create("돈이 부족합니다.");
        }
    }

    public void UpgradeMoveSpeed()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player.money >= moveSpeedPrice)
        {
            if (user.GetMoveSpeed >= _maxMoveSpeed) return;

            player.AddMoney(-moveSpeedPrice);
            user.SetMoveSpeed(1);
            moveSpeedPrice += moveSpeedPrice / 10;
            moveSpeedPriceText.text = $"{moveSpeedPrice}G";
            userMoneyText.text = $"Gold:{player.money}";
        }
        else
        {
            sysMsg.Create("돈이 부족합니다.");
        }
    }

    public void UpgradeWaterFillingSpeed()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player.money >= waterPrice)
        {
            if (user.GetWaterFillingSpeed <= _minFillingSpeed) return;
            player.AddMoney(-waterPrice);
            user.SetFillingSpeed(0.1f);
            waterPrice += waterPrice / 10;
            waterPriceText.text = $"{waterPrice}G";
            userMoneyText.text = $"Gold:{player.money}";
        }
        else
        {
            sysMsg.Create("돈이 부족합니다.");
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Exit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpgradeWaterFillingSpeed();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpgradeMoveSpeed();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpgradeBucket();
        }
    }
}