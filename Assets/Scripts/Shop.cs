using System;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI userMoney;
    [SerializeField] private TextMeshProUGUI bucketPriceText;
    [SerializeField] private TextMeshProUGUI moveSpeedPriceText;
    [SerializeField] private TextMeshProUGUI waterPriceText;
    [SerializeField] private int bucketPrice;
    [SerializeField] private int moveSpeedPrice;
    [SerializeField] private int waterPrice;
    [SerializeField] private SystemMessage sysMsg;

    private void OnEnable()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        userMoney.text = "Gold:" + user.GetMoney;
        bucketPriceText.text = bucketPrice + "G";
        moveSpeedPriceText.text = moveSpeedPrice + "G";
        waterPriceText.text = waterPrice + "G";
        
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void BuyBucket()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (user.GetMoney >= bucketPrice)
        {
            if (5 <= user.GetwaterCapacity) return;

            user.SetMoney(-bucketPrice);
            user.SetWaterCapacity(1);
            bucketPrice += bucketPrice / 10;
            bucketPriceText.text = bucketPrice + "G";
            userMoney.text = "Gold:" + user.GetMoney;
        }
        else
        {
            sysMsg.Create("돈이 부족합니다.");
        }
    }

    public void BuyMoveSpeed()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (user.GetMoney >= moveSpeedPrice)
        {
            if (user.GetMoveSpeed > 9) return;

            user.SetMoney(-moveSpeedPrice);
            user.SetMoveSpeed(1);
            moveSpeedPrice += moveSpeedPrice / 10;
            moveSpeedPriceText.text = moveSpeedPrice + "G";
            userMoney.text = "Gold:" + user.GetMoney;
        }
        else
        {
            sysMsg.Create("돈이 부족합니다.");
        }
    }

    public void BuyWater()
    {
        var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (user.GetMoney >= waterPrice)
        {
            if (user.GetFillingSpeed <= 0.2f) return;
            user.SetMoney(-waterPrice);
            user.SetFillingSpeed(0.1f);
            waterPrice += waterPrice / 10;
            waterPriceText.text = waterPrice + "G";
            userMoney.text = "Gold:" + user.GetMoney;
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
            BuyWater();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BuyMoveSpeed();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BuyBucket();
        }
    }
}