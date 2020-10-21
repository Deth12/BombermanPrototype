using DG.Tweening;
using UnityEngine;

public enum MovementDirection
{
    Vertical, Horizontal
}

public class Enemy : MonoBehaviour
{
    public MovementDirection MovementDirection;
    public LayerMask obstaclesLayer;
    public Transform endPoint;
    
    private bool isInMovement = false;
    
    private Vector2 currentDir;
    
    private void Start()
    {
        currentDir = MovementDirection == MovementDirection.Horizontal ? Vector2.right : Vector2.up;
    }
    
    private void Update()
    {
        if (!isInMovement)
        {
            Move();
        }
    }

    private void Move()
    {
        isInMovement = true;
        var hit = Physics2D.Raycast(transform.position, currentDir, 1f, obstaclesLayer);
        Debug.DrawRay(transform.position, currentDir, Color.blue, 1f);
        if (hit)
        {
            currentDir = MovementDirection == MovementDirection.Horizontal ? 
                (currentDir == Vector2.left ? Vector2.right : Vector2.left) :
                (currentDir == Vector2.down ? Vector2.up : Vector2.down);
        }
        var pos = (Vector2)transform.position + currentDir;
        transform.DOMove(pos, .5f).OnComplete(() => { isInMovement = false;});
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
