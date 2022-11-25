using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTileMap : MonoBehaviour
{
  [SerializeField]private Tilemap fireTileMap;
  [SerializeField]private TileBase fireTileBase;
  [SerializeField]private Vector3Int tilePos;
  [SerializeField]private float spreadDelay;

  private void Start()
  {
    var newTileMap = Instantiate(fireTileMap, Vector3.zero, Quaternion.identity);
    StartCoroutine(SpreadFrame());
    StartCoroutine(WeightedValue());
  }

  private IEnumerator WeightedValue()
  {
    for (int i = 0; i < 6; i++)
    {
      yield return new WaitForSeconds(20f);
      spreadDelay -= 0.2f;
    }
  }

  private IEnumerator SpreadFrame()
  {
    yield return new WaitForSeconds(spreadDelay);
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
