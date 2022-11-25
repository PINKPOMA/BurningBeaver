using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SetTileMap : MonoBehaviour
{
  [SerializeField]private Tilemap fireTileMap;
  [SerializeField]private TileBase fireTileBase;
  [SerializeField]private Vector3Int tilePos;
  [SerializeField]private float spreadDelay;
  
  [Header("World edge block")]
  [SerializeField]private float xMin;
  [SerializeField]private float yMin;
  [SerializeField]private float xMax;
  [SerializeField]private float yMax;
  
  [Header("World edge block")]
  [SerializeField]private float waterXMin;
  [SerializeField]private float waterYMin;
  [SerializeField]private float waterXMax;
  [SerializeField]private float waterYMax;


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
      tilePos.x += ReturnAddPosX();
    else
      tilePos.y += ReturnAddPosY();
    Instantiate(fireTileMap, tilePos, Quaternion.identity);
    StartCoroutine(SpreadFrame());
  }

  private int ReturnAddPosX()
  {
    if (Random.Range(0, 2) == 0)
    {
      return tilePos.x - 1 < xMin ? 1 : -1;
    }
    else
    {
      return tilePos.x + 1 > xMax ? -1 : 1;
    }
  }
  private int ReturnAddPosY()
  {
    if (Random.Range(0, 2) == 0)
    {
      return tilePos.y - 1 < yMin ? 1 : -1;
    }
    else
    {
      return tilePos.y + 1 > yMax ?  -1 : 1;
    }
  }
}
