using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class Fire : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spr;
    private Color sprColor;
    
    private void Reset()
    {
        spr = GetComponent<SpriteRenderer>();
        sprColor = spr.color;
    }

    private void Start()
    {
        spr.DOColor(new Color(sprColor.r, sprColor.g, sprColor.b, 0), 1f).OnComplete(() =>
        {
            DOTween.Kill(spr);
            Destroy(gameObject);
        });
    }

}
