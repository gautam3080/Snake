using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SnakeTail : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SnakeTail childTail;
    Path myPath = new Path();
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myPath = new Path();
    }

    public void AddChildTail(SnakeTail child)
    {
        childTail = child;
        myPath.outPosition = childTail.SetPostion;
    }

    public void SetPostion(Vector3 postion)
    {
        transform.position = postion;
        myPath.AddPostion(postion);
    }
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
    public void SetOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }
}

