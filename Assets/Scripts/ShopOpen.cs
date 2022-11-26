using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
   [SerializeField] private GameObject shopUI;

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.E))
      {
         shopUI.SetActive(true);
         Time.timeScale = 0;
      }
   }

   public void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Player"))
      {
         shopUI.SetActive(true);
         Time.timeScale = 0;
      }
   }
}
