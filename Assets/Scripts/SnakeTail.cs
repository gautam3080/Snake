using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SnakeTail : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer SpriteRenderer { 
        get { 
        
            if(_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return _spriteRenderer;
        } }
    private SnakeTail _childTail;
    private SnakeTail ChildTail;
    Path myPath = new Path();
    

    public void AddChildTail(SnakeTail child)
    {
        _childTail = child;
        myPath.outPosition = _childTail.SetPostion;
    }

    public void SetPostion(Vector3 postion)
    {
        transform.position = postion;
        myPath.AddPostion(postion);
    }
    public void SetColor(Color color)
    {
        SpriteRenderer.color = color;
    }
    public void SetOrder(int order)
    {
        SpriteRenderer.sortingOrder = order;
    }
}

