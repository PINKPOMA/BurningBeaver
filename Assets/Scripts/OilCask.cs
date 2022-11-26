using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OilCask : MonoBehaviour
{
   [SerializeField] private Tilemap maxFlameTilemap;

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Q))
         Bomb();
   }

   public void Bomb()
   {
      for (int i = 1; i <= 3; i++)
      {
         for (int j = 1; j <= 3; j++)
         {
            Instantiate(maxFlameTilemap, new Vector3Int((int)transform.position.x + (j - 2), (int)transform.position.y + (i-2)),Quaternion.identity);
         }
      }
      gameObject.SetActive(false);
   }
}
