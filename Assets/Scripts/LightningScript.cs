using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class LightningScript : MonoBehaviour
{
   [SerializeField] private GameObject lightning;
   [SerializeField] private Tilemap flameTilemap;
   [SerializeField] private int nowFlameTile;
   [SerializeField] private TileBase[] flameTileBase;

   private void Start()
   {
      flameTilemap = GetComponent<Tilemap>();
      if (nowFlameTile > 1)
      {
         return;
      }

      if (Random.Range(0, 10) != 9)
      {
         lightning.gameObject.SetActive(false);
      }
      else
      {
         StartCoroutine(StopLightning());
      }
   }

   private IEnumerator StopLightning()
   {
      yield return new WaitForSeconds(1.11f);
      Destroy(lightning);
   }

   public void EnhanceFlameTile()
   {
      nowFlameTile++;
      if (nowFlameTile < 3 && flameTilemap)
         flameTilemap.SwapTile(flameTileBase[nowFlameTile - 1],flameTileBase[nowFlameTile]);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Water"))
      {
         Destroy(gameObject);
      }
   }
}
