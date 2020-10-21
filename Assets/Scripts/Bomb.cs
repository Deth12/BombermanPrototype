using DG.Tweening;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private LayerMask explosionMask;
    [SerializeField] private float boomTimer = 2f;
    [SerializeField] private int explosionRadius = 3;
    [SerializeField] private GameObject firePrefab;
    private void Start()
    {
        transform.DOShakeScale(0.3f, 0.2f, 10, 10f).SetLoops(-1);
        Invoke(nameof(Boom), boomTimer);    
    }

    private void Boom()
    {
        DOTween.Kill(transform);
        Vector2[] dirs = new[] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };
        foreach (var dir in dirs)
        {
            var hit = Physics2D.Raycast(transform.position, dir, explosionRadius, explosionMask);
            Debug.DrawRay(transform.position, dir * explosionRadius, Color.yellow, 5f);
            InstantiateFire(dir);
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
        Destroy(gameObject);
    }

    private void InstantiateFire(Vector2 dir)
    {
        for (int i = 0; i < explosionRadius; i++)
        {
            Vector2 spawnPos = (Vector2) transform.position + dir * i;
            Instantiate(firePrefab, spawnPos, Quaternion.identity);
        }
    }
}
