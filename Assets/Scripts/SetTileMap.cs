using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

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

  [SerializeField] private LayerMask noFlame;
  [SerializeField] private LayerMask enhanceFlame;
  [SerializeField] private LayerMask oilCaskLayer;

  [SerializeField] private PlayerController playerController;

  private void Start()
  {
    playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    tilePos.x = (int)transform.position.x;
    tilePos.y = (int)transform.position.y;
    StartCoroutine(SpreadFlame());
    StartCoroutine(WeightedValue());
    StartCoroutine(Dead());
  }

  private IEnumerator Dead()
  {
    yield return new WaitForSeconds(35f);
    Destroy(gameObject.GetComponent<SetTileMap>());
  }
  private IEnumerator WeightedValue()
  {
    for (var i = 0; i < 6; i++)
    {
      yield return new WaitForSeconds(5f);
      spreadDelay -= i / 20f;
    }
  }

  private IEnumerator SpreadFlame()
  {
    if (playerController.IsDead)
    {
      yield break;
    }

    yield return new WaitForSeconds(spreadDelay);
    if (Random.Range(0, 2) == 0)
    {
      tilePos.x += ReturnAddPosX();
    }
    else
    {
      tilePos.y += ReturnAddPosY();
    }

    if (Physics2D.OverlapCircle((Vector3)tilePos, 0.2f, oilCaskLayer))
    {
      var flameCollision = Physics2D.OverlapCircle((Vector3)tilePos, 0.2f, oilCaskLayer);
      flameCollision.GetComponent<OilCask>().Bomb();
    }

    if (Physics2D.OverlapCircle((Vector3)tilePos, 0.2f, enhanceFlame))
    {
      var flameCollision = Physics2D.OverlapCircle((Vector3)tilePos, 0.2f, enhanceFlame);
      flameCollision.GetComponent<LightningScript>().EnhanceFlameTile();
    }
    
    else if (!Physics2D.OverlapCircle((Vector3)tilePos, 0.2f, noFlame))
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
      {
        return 1;
      }
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
      {
        return 1;
      }
      else
      {
        return -1;
      }
    }
  }
}
