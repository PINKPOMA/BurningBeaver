using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI userMoney;
   [SerializeField] private TextMeshProUGUI bukketPriseText;
   [SerializeField] private TextMeshProUGUI moveSpeedPriseText;
   [SerializeField] private TextMeshProUGUI waterPriseText;
   [SerializeField] private int bucketPrise;
   [SerializeField] private int maxBukket;
   [SerializeField] private int moveSpeedPrise;
   [SerializeField] private int waterPrise;

   private void Start()
   {
      var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      userMoney.text = "Gold: " + user.GetMoney;
      bukketPriseText.text = bucketPrise.ToString();
      moveSpeedPriseText.text = moveSpeedPrise.ToString();
      waterPriseText.text = waterPrise.ToString();
   }

   public void BuyBucket()
   {
      var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      if (user.GetMoney >= bucketPrise)
      {
         if(5 <=  user.GetwaterCapacity) return;
         
         user.SetMoney(-bucketPrise);
         user.SetwaterCapacity(1);
         bucketPrise += bucketPrise / 10;
         bukketPriseText.text = bucketPrise.ToString();
         userMoney.text = user.GetMoney.ToString();
      }
      else
      {
         Debug.Log("돈 부족");
      }
   }
   public void BuymoveSpeed()
   {
      var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      if (user.GetMoney >= moveSpeedPrise)
      {
         if (user.GetMoveSpeed > 9) return;
         
         user.SetMoney(-moveSpeedPrise);
         user.SetMoveSpeed(1);
         moveSpeedPrise += moveSpeedPrise / 10;
         moveSpeedPriseText.text = moveSpeedPrise.ToString();
         userMoney.text = user.GetMoney.ToString();
      }
      else
      {
         Debug.Log("돈 부족");
      }
   }
   public void BuyWater()
   {
      var user = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
      if (user.GetMoney >= waterPrise)
      {
         if (user.GetFillingSpeed <= 0.2f) return;
         user.SetMoney(-waterPrise);
         user.SetFillingSpeed(0.1f);
         waterPrise += waterPrise / 10;
         waterPriseText.text = waterPrise.ToString();
         userMoney.text = user.GetMoney.ToString();
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
