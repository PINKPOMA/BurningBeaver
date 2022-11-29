using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private BucketCount bucketCount;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Transform movePoint;
    [SerializeField] private LayerMask whatStopsMovement;
    [SerializeField] private LayerMask water;
    [SerializeField] private LayerMask flame;
    [SerializeField] private LayerMask item;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int waterCollected;
    [SerializeField] private int waterCapacity = 3;
    [SerializeField] private TextMeshProUGUI guideText;
    [SerializeField] private SystemMessage sysMsg;
    [SerializeField] private GameObject waterSpillPrefab;
    [SerializeField] private Sprite sideBeaverSprite;
    [SerializeField] private Sprite sideFullBeaverSprite;
    [SerializeField] private Sprite topBeaverSprite;
    [SerializeField] private Sprite topFullBeaverSprite;
    [SerializeField] private HpGauge hpGauge;
    [SerializeField] private bool isDead;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private float damagePerSec = 0.2f; 
    [SerializeField] private float waterFillingSpeed = 1f;
    [SerializeField] private Tilemap itemTilemap;
    [SerializeField] private WorkGauge workGauge;
    [SerializeField] private KeyCode spreadWater;
    [SerializeField] private KeyCode fillWater;
    [SerializeField] private TextMeshProUGUI flameProgressText;
    [SerializeField] private TextMeshProUGUI userMoneyText;
    
    public int money;

    public int flameCount;
    public float flameProgress;
    
    private const float CheckOverlapRadius = 0.2f;

    public bool IsDead => isDead;


    public void SetWaterCapacity(int num)
    {
        waterCapacity += num;
        bucketCount.Init(waterCapacity);
    }

    public int GetWaterCapacity => waterCapacity;
    public void SetMoveSpeed(float num) => moveSpeed += num;
    public float GetMoveSpeed => moveSpeed;

    public void SetFillingSpeed(float num)
    {
        waterFillingSpeed -= num;
        if (waterFillingSpeed <= 0.2)
            waterFillingSpeed = 0.2f;
    }

    public float GetWaterFillingSpeed => waterFillingSpeed;


    private IEnumerator Start()
    {
        movePoint.parent = null;
        hpGauge.FillAmount = 1f;
        bucketCount.Init(waterCapacity);

        for (var i = 10; i > 0; i--)
        {
            sysMsg.Create($"{i}초 뒤 산불이 시작됩니다.");
            yield return new WaitForSeconds(1.0f);
        }

        sysMsg.Create("산불이 났습니다~~~");
    }

    private static readonly Vector2[] Dirs =
    {
        Vector2.right,
        Vector2.left,
        Vector2.up,
        Vector2.down
    };

    private void Update()
    {
        if (isDead)
        {
            SoundManager.Instance.Play(SoundManager.Instance.dieSound);
            guideText.text = "";
            return;
        }

        flameCount = FindObjectsOfType<LightningScript>().Length;
        flameProgress = flameCount / 1.5f;

        flameProgressText.text = $"{flameProgress:f0}%";
        if (flameProgress >= 100.0f)
        {
            flameProgress = 100.0f;
            CommitGameOver(GameOverReason.FlameProgress);
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        var moved = false;
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            moved |= UpdateMovement(Input.GetAxisRaw("Horizontal"), Vector2.right);
            moved |= UpdateMovement(Input.GetAxisRaw("Vertical"), Vector2.up);
        }

        // 이동하면 하던 작업 취소
        if (moved)
        {
            if (workGauge.gameObject.activeSelf)
            {
                sysMsg.Create("작업이 취소됐습니다.");
            }

            workGauge.gameObject.SetActive(false);
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

        guideText.text = IsDead
            ? ""
            : waterCollected != 0 && isNearWater
                ? $"{fillWater}키를 눌러 물을 담으세요."
                : isNearFlame && waterCollected > 0
                    ? $"{spreadWater}키를 눌러 물을 뿌리세요."
                    : isNearFlame && waterCollected == 0
                        ? "물을 담아 오세요!!!"
                        : "";

        if (Input.GetKeyDown(spreadWater) && waterCollected > 0)
        {
            waterCollected--;
            bucketCount.SpendWaterBucket();
            Instantiate(waterSpillPrefab, transform.position, Quaternion.identity);
        }
        else if (isNearWater && Input.GetKeyDown(fillWater))
        {
            if (waterCollected < waterCapacity)
            {
                if (!workGauge.gameObject.activeSelf)
                {
                    workGauge.gameObject.SetActive(true);
                    workGauge.StartWork(waterFillingSpeed, () =>
                    {
                        waterCollected++;
                        bucketCount.FillWaterBucket();
                        SoundManager.Instance.Play(SoundManager.Instance.scoopOutWaterSound);
                        sysMsg.Create("물을 담았습니다.");
                        workGauge.gameObject.SetActive(false);
                    });
                }
                else
                {
                    sysMsg.Create("작업 중입니다.");
                }
            }
            else
            {
                SoundManager.Instance.Play(SoundManager.Instance.scoopOutWaterSound);
                sysMsg.Create("더 담을 수 없습니다.");
            }
        }

        // 불에 겹쳐지면 데미지를 받는다.
        if (Physics2D.OverlapCircle(movePoint.position, CheckOverlapRadius, flame))
        {
            hpGauge.FillAmount -= damagePerSec * Time.deltaTime;
            if (hpGauge.FillAmount <= 0)
            {
                CommitGameOver(GameOverReason.PlayerDead);
            }

            hpGauge.Shake();
        }
    }
    
    private bool UpdateMovement(float axisValue, Vector3 axis)
    {
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        if (Mathf.Abs(axisValue) != 1.0f)
        {
            return false;
        }

        var delta = axisValue * axis;
        if (Physics2D.OverlapCircle(movePoint.position + delta, CheckOverlapRadius, whatStopsMovement))
        {
            return false;
        }
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

        return true;

    }
    
    public void AddMoney(int won)
    {
        money += won;
        userMoneyText.text = $"Gold:{money}";
    }

    public enum GameOverReason
    {
        PlayerDead,
        FlameProgress,
    }

    private void CommitGameOver(GameOverReason reason)
    {
        isDead = true;
        spriteRenderer.sprite = sideBeaverSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        spriteRenderer.transform.DOLocalRotate(new Vector3(0, 0, 90), 0.25f);
        spriteRenderer.transform.DOBlendableLocalMoveBy(Vector2.down / 4, 0.25f);
        gameOver.Create(reason);
        workGauge.gameObject.SetActive(false);
    }
}