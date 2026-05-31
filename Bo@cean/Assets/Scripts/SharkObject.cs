using UnityEngine;

public class SharkObject : FloatingObject
{
    public Vector3 movementDirection = Vector3.left;
    public float distanceToMove = 1f;
    public float horizontalSpeed = 1f;

    private float leftX;
    private float rightX;
    private bool movingToRight;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialMovingToRight;

    protected void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        if (movementDirection != Vector3.left && movementDirection != Vector3.right)
        {
            movementDirection = Vector3.left;
        }

        float startX = transform.position.x;
        if (movementDirection == Vector3.left)
        {
            leftX = startX - distanceToMove;
            rightX = startX;
            movingToRight = false;
        }
        else
        {
            leftX = startX;
            rightX = startX + distanceToMove;
            movingToRight = true;
        }
        initialMovingToRight = movingToRight;
    }

    public void ResetShark()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        movingToRight = initialMovingToRight;
    }

    protected override void Update()
    {
        base.Update();

        if (WaterManager.Instance == null)
        {
            return;
        }

        float targetX = movingToRight ? rightX : leftX;
        Vector3 position = transform.position;
        position.x = Mathf.MoveTowards(position.x, targetX, horizontalSpeed * Time.deltaTime);
        transform.position = position;

        if (Mathf.Abs(position.x - targetX) < 0.01f)
        {
            movingToRight = !movingToRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}