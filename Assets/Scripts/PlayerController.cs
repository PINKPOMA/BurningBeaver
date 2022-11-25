using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Transform movePoint;

    public LayerMask whatStopsMovement;

    public LayerMask water;

    public SpriteRenderer spriteRenderer;

    public int waterCollected;

    public int waterCapacity = 1;

    public TextMeshProUGUI guideText;
    
    void Start()
    {
        movePoint.parent = null;
    }

    static readonly Vector2[] dirs = new Vector2[]
    {
        Vector2.right,
        Vector2.left,
        Vector2.up,
        Vector2.down
    };

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            UpdateMovement(Input.GetAxisRaw("Horizontal"), Vector2.right);
            UpdateMovement(Input.GetAxisRaw("Vertical"), Vector2.up);
        }

        var isNearWater = false;
        foreach (var dir in dirs)
        {
            if (Physics2D.OverlapCircle((Vector2)movePoint.position + dir, 0.2f, water))
            {
                isNearWater = true;
                break;
            }
        }
        
        spriteRenderer.color = waterCollected > 0 ? Color.blue : isNearWater ? Color.cyan : Color.white;

        guideText.text = waterCollected == 0 && isNearWater ? "스페이스를 눌러 물을 담으세요." : "";

        if (isNearWater && Input.GetKeyDown(KeyCode.Space))
        {
            if (waterCollected < waterCapacity)
            {
                waterCollected++;
            }
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
        if (!Physics2D.OverlapCircle(movePoint.position + delta, 0.2f, whatStopsMovement))
        {
            movePoint.position += delta;
        }
    }
}