using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightningScript : MonoBehaviour
{
   [SerializeField] private GameObject lightning;

   private void Start()
   {
      if (Random.Range(0, 10) != 9)
      {
         lightning.gameObject.SetActive(false);
     
      }
      else
      {
         StartCoroutine(Stoplightning());
      }
   }
   IEnumerator Stoplightning()
   {
      yield return new WaitForSeconds(1.11f);
      Destroy(lightning);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Water"))
      {
         Destroy(gameObject);
      }
   }
}
