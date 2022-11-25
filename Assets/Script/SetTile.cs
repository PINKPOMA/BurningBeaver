using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTile : MonoBehaviour
{
  [SerializeField]private Tilemap fireTileMap;
  [SerializeField]private TileBase fireTileBase;
  [SerializeField]private Vector3Int tilePos;

  private void Start()
  {
    StartCoroutine(SpreadFrame());
  }

  private IEnumerator SpreadFrame()
  {
    fireTileMap.SetTile(tilePos, fireTileBase);
    yield return new WaitForSeconds(2f);
    if(Random.Range(0,2) == 0)
      tilePos.x += ReturnAddPos();
    else
      tilePos.y += ReturnAddPos();
    StartCoroutine(SpreadFrame());
  }

  private int ReturnAddPos()
  {
    return Random.Range(0, 2) == 0 ? -1 : 1;
  }
}
