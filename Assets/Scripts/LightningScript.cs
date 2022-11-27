using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LightningScript : MonoBehaviour
{
   [SerializeField] private GameObject lightning;
   [SerializeField] private Tilemap _flameTilemap;
   [SerializeField] private int nowFlameTile;
   [SerializeField] private TileBase[] flameTileBase;
   private void Start()
   {
      _flameTilemap = GetComponent<Tilemap>();
      if (nowFlameTile <= 1)
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
   }

   private IEnumerator Stoplightning()
   {
      yield return new WaitForSeconds(1.11f);
      Destroy(lightning);
   }

   public void enhanceFlameTile()
   {
      nowFlameTile++;
      if (nowFlameTile < 3 && _flameTilemap)
         _flameTilemap.SwapTile(flameTileBase[nowFlameTile - 1],flameTileBase[nowFlameTile]);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Water"))
      {
         Destroy(gameObject);
      }
   }
}
