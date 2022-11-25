using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Transform movePoint;

    public LayerMask whatStopsMovement;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            UpdateMovement(Input.GetAxisRaw("Horizontal"), Vector2.right);
            UpdateMovement(Input.GetAxisRaw("Vertical"), Vector2.up);
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