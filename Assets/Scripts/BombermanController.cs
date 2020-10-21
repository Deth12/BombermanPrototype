using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class BombermanController : MonoBehaviour
{
    [SerializeField] private LayerMask obstaclesMask = default;
    [SerializeField] private Bomb bombPrefab = default;

    private bool isInMovement;

    private void Update()
    {
        if (isInMovement)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayerTo(Vector2.left);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayerTo(Vector2.right);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayerTo(Vector2.up);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayerTo(Vector2.down);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
    
    private void MovePlayerTo(Vector2 dir)
    {
        if (Raycast(dir))
        {
            return;
        }

        isInMovement = true;
        var pos = (Vector2)transform.position + dir;
        transform.DOMove(pos, 0.2f).OnComplete(() => { isInMovement = false; });
    }

    private bool Raycast(Vector2 dir)
    {
        var hit = Physics2D.Raycast(transform.position, dir, 1f, obstaclesMask);
        Debug.DrawRay(transform.position, dir * 1f, Color.red, 0.5f);
        return hit.collider != null;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy met");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        SceneManager.LoadScene(0);
    }
}
