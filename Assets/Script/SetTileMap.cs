using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTileMap : MonoBehaviour
{
  [SerializeField]private Tilemap fireTileMap;
  [SerializeField]private TileBase fireTileBase;
  [SerializeField]private Vector3Int tilePos;

  private void Start()
  {
    var newTileMap = Instantiate(fireTileMap, Vector3.zero, Quaternion.identity);
    StartCoroutine(SpreadFrame());
  }

  private IEnumerator SpreadFrame()
  {
    yield return new WaitForSeconds(2f);
    if(Random.Range(0,2) == 0)
      tilePos.x += ReturnAddPos();
    else
      tilePos.y += ReturnAddPos();
    Instantiate(fireTileMap, tilePos, Quaternion.identity);
    StartCoroutine(SpreadFrame());
  }

  private int ReturnAddPos()
  {
    return Random.Range(0, 2) == 0 ? -1 : 1;
  }
}
