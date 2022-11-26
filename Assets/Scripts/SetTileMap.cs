using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class SetTileMap : MonoBehaviour
{
  [SerializeField]private Tilemap fireTileMap;
  [SerializeField]private Vector3Int tilePos;
  [SerializeField]private float spreadDelay;
  [Header("World edge block")]
  [SerializeField]private float xMin;
  [SerializeField]private float yMin;
  [SerializeField]private float xMax;
  [SerializeField]private float yMax;

  [SerializeField]
  LayerMask noFlame;

  private void Start()
  {
    tilePos.x = (int)transform.position.x;
    tilePos.y = (int)transform.position.y;
    StartCoroutine(SpreadFlame());
    StartCoroutine(WeightedValue());
  } 

  float ReturnAddPlayerPos()
  {
    return Random.Range(0, 2) == 0 ? Random.Range(-3f, -5f) : Random.Range(3f, 5f);
  }

  private IEnumerator WeightedValue()
  {
    for (int i = 0; i < 6; i++)
    {
      yield return new WaitForSeconds(20f);
      spreadDelay -= 0.2f;
    }
  }

  private IEnumerator SpreadFlame()
  {
    yield return new WaitForSeconds(spreadDelay);
    if(Random.Range(0,2) == 0)
      tilePos.x += ReturnAddPosX();
    else
      tilePos.y += ReturnAddPosY();

    if (!Physics2D.OverlapCircle((Vector3)tilePos, 0.2f, noFlame))
    {
      Instantiate(fireTileMap, tilePos, Quaternion.identity);
    }

    StartCoroutine(SpreadFlame());
  }

  private int ReturnAddPosX()
  {
    if (Random.Range(0, 2) == 0)
    {
      if(tilePos.x - 1 > xMin)
        return -1;
      else
      {
        return 1;
      }
    }
    else
    {
      if(tilePos.x + 1 < xMax)
        return 1;
      else
      {
        return -1;
      } 
    }
  }
  private int ReturnAddPosY()
  {
    if (Random.Range(0, 2) == 0)
    {
      if(tilePos.y - 1 > yMin)
        return -1;
      else
      {
        return 1;
      }
    }
    else
    {
      if (tilePos.y + 1 < yMax)
        return 1;
      else
        return -1;
    }
  }
}
