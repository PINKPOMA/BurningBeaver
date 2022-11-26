using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI userMoney;
   [SerializeField] private TextMeshProUGUI bucketPriceText;
   [SerializeField] private TextMeshProUGUI moveSpeedPriceText;
   [SerializeField] private TextMeshProUGUI waterPriceText;
   [SerializeField] private int bucketPrice;
   [SerializeField] private int maxBucket;
   [SerializeField] private int moveSpeedPrice;
   [SerializeField] private int waterPrice;

   private void Start()
   {
      var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      userMoney.text = "Gold:"+ user.GetMoney;
      bucketPriceText.text = bucketPrice+"G";
      moveSpeedPriceText.text = moveSpeedPrice+"G";
      waterPriceText.text = waterPrice+"G";
   }

   public void BuyBucket()
   {
      var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      if (user.GetMoney >= bucketPrice)
      {
         if(5 <=  user.GetwaterCapacity) return;
         
         user.SetMoney(-bucketPrice);
         user.SetwaterCapacity(1);
         bucketPrice += bucketPrice / 10;
         bucketPriceText.text = bucketPrice+"G";
         userMoney.text = "Gold:" + user.GetMoney;
      }
      else
      {
         Debug.Log("돈 부족");
      }
   }
   public void BuymoveSpeed()
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
         Debug.Log("돈 부족");
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
         Debug.Log("돈 부족");
      }
   }

   public void Exit()
   {
      Time.timeScale = 1;
      gameObject.SetActive(false);
   }
}
