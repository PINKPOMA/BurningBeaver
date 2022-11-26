using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] BucketCount bucketCount;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] Transform movePoint;
    [SerializeField] LayerMask whatStopsMovement;
    [SerializeField] LayerMask water;
    [SerializeField] LayerMask flame;
    [SerializeField] LayerMask item;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int waterCollected;
    [SerializeField] int waterCapacity = 1;
    [SerializeField] TextMeshProUGUI guideText;
    [SerializeField] SystemMessage sysMsg;
    [SerializeField] GameObject waterSpillPrefab;
    [SerializeField] Sprite sideBeaverSprite;
    [SerializeField] Sprite sideFullBeaverSprite;
    [SerializeField] Sprite topBeaverSprite;
    [SerializeField] Sprite topFullBeaverSprite;
    [SerializeField] HpGauge hpGauge;
    [SerializeField] bool isDead;
    [SerializeField] GameOver gameOver;
    [SerializeField] float damagePerSec = 0.2f;
    [SerializeField] Tilemap itemTilemap;

    const float CheckOverlapRadius = 0.2f;

    public bool IsDead => isDead;

    void Start()
    {
        movePoint.parent = null;
        hpGauge.FillAmount = 0.5f;
        bucketCount.Init(waterCapacity);
    }

    static readonly Vector2[] Dirs =
    {
        Vector2.right,
        Vector2.left,
        Vector2.up,
        Vector2.down
    };

    void Update()
    {
        if (isDead)
        {
            guideText.text = "";
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            UpdateMovement(Input.GetAxisRaw("Horizontal"), Vector2.right);
            UpdateMovement(Input.GetAxisRaw("Vertical"), Vector2.up);
        }

        var isNearWater = false;
        foreach (var dir in Dirs)
        {
            if (Physics2D.OverlapCircle((Vector2)movePoint.position + dir, CheckOverlapRadius, water))
            {
                isNearWater = true;
                break;
            }
        }

        var isNearFlame = false;
        foreach (var dir in Dirs)
        {
            if (Physics2D.OverlapCircle((Vector2)movePoint.position + dir, CheckOverlapRadius, flame))
            {
                isNearFlame = true;
                break;
            }
        }


        if (Physics2D.OverlapCircle(movePoint.position, CheckOverlapRadius, item))
        {
            var itemCell = itemTilemap.WorldToCell(movePoint.position);
            var itemTile = itemTilemap.GetTile(itemCell);
            if (itemTile)
            {
                Debug.Log($"item at {itemCell}");
                itemTilemap.SetTile(itemCell, null);
                hpGauge.FillAmount += 0.2f;
            }
        }

        if (waterCollected > 0)
        {
            if (spriteRenderer.sprite == sideBeaverSprite)
            {
                spriteRenderer.sprite = sideFullBeaverSprite;
            }
            else if (spriteRenderer.sprite == topBeaverSprite)
            {
                spriteRenderer.sprite = topFullBeaverSprite;
            }
        }
        else
        {
            if (spriteRenderer.sprite == sideFullBeaverSprite)
            {
                spriteRenderer.sprite = sideBeaverSprite;
            }
            else if (spriteRenderer.sprite == topFullBeaverSprite)
            {
                spriteRenderer.sprite = topBeaverSprite;
            }
        }

        //spriteRenderer.color = waterCollected > 0 ? Color.blue : isNearWater ? Color.cyan : Color.white;

        // 물을 하나도 안모은 상태에서 물 근처에 있다면 물을 담는 선택지 밖에 없다.
        guideText.text = IsDead
            ? ""
            : waterCollected == 0 && isNearWater
                ? "Z 키를 눌러 물을 담으세요."
                : isNearFlame && waterCollected > 0
                    ? "X 키를 눌러 물을 뿌리세요."
                    : isNearFlame && waterCollected == 0
                        ? "물을 담아 오세요!!!"
                        : "";

        // 모은 물이 있으면 먼저 쓴다.
        if (Input.GetKeyDown(KeyCode.X) && waterCollected > 0)
        {
            waterCollected--;
            bucketCount.SpendWaterBucket();
            Instantiate(waterSpillPrefab, transform.position, Quaternion.identity);
        }
        // 쓸 물이 없다면 물 근처에서는 물을 담는다.
        else if (isNearWater && Input.GetKeyDown(KeyCode.Z))
        {
            if (waterCollected < waterCapacity)
            {
                waterCollected++;
                bucketCount.FillWaterBucket();
                sysMsg.Create("물을 담았습니다.");
            }
            else
            {
                sysMsg.Create("더 담을 수 없습니다.");
            }
        }

        // 불에 겹쳐지면 데미지를 받는다.
        if (Physics2D.OverlapCircle(movePoint.position, CheckOverlapRadius, flame))
        {
            hpGauge.FillAmount -= damagePerSec * Time.deltaTime;
            if (hpGauge.FillAmount <= 0)
            {
                isDead = true;
                spriteRenderer.sprite = sideBeaverSprite;
                spriteRenderer.flipX = false;
                spriteRenderer.flipY = false;
                spriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.25f);
                spriteRenderer.transform.DOBlendableLocalMoveBy(Vector2.down / 4, 0.25f);
                gameOver.Create();
            }

            hpGauge.Shake();
        }
    }

    void UpdateMovement(float axisValue, Vector3 axis)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (Mathf.Abs(axisValue) != 1.0f)
        {
            return;
        }

        var delta = axisValue * axis;
        if (!Physics2D.OverlapCircle(movePoint.position + delta, CheckOverlapRadius, whatStopsMovement))
        {
            movePoint.position += delta;


            if (delta.x != 0)
            {
                spriteRenderer.sprite = sideBeaverSprite;

                spriteRenderer.flipX = delta.x switch
                {
                    < 0 => true,
                    > 0 => false,
                    _ => spriteRenderer.flipX
                };
                spriteRenderer.flipY = false;
            }
            else if (delta.y != 0)
            {
                spriteRenderer.sprite = topBeaverSprite;

                spriteRenderer.flipX = false;
                spriteRenderer.flipY = delta.y switch
                {
                    < 0 => true,
                    > 0 => false,
                    _ => spriteRenderer.flipY
                };
            }
        }
    }
}