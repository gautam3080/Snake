using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]  
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2.5f;

    private Vector3 moveDirection;

    // Take Controls to move or not move gameobject in both directions according to boundary touch
    private int LeftDir = -1, RightDir = 1, UpDir = 1, DownDir = -1;

    SpriteRenderer spriteRenderer;

    public SnakeTail snakeTailPrefab;
    List<SnakeTail> tails = new();
    public Path myPath = new();
    [Range(2, 200)]
    public int tailSperationLevel;
    SnakeTail myTailChild;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveDirection = new Vector3(1, 0, 0);
        if (tails.Count > 0)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            LeftDir = -1; // left move permission allowed
            moveDirection = new Vector3(RightDir, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            RightDir = 1; // right move permission allowed
            moveDirection = new Vector3(LeftDir, 0, 0);
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            DownDir = -1; // down move permission allowed
            moveDirection = new Vector3(0, UpDir, 0);
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            UpDir = 1; // up move permission allowed
            moveDirection = new Vector3(0, DownDir, 0);
        }
        var prevPos = transform.position;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(prevPos != transform.position)
        {

            myPath.AddPostion(transform.position);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "RightBoundary")
        {
            RightDir = 0; // more right move permission denied
            moveDirection = new Vector3(0, 0, 0);
        }
        else if (collision.gameObject.name == "LeftBoundary")
        {
            LeftDir = 0; // more left move permission denied
            moveDirection = new Vector3(0, 0, 0);
        }
        else if (collision.gameObject.name == "UpperBoundary")
        {
            UpDir = 0; // more up move permission denied
            moveDirection = new Vector3(0, 0, 0);
        }
        else if (collision.gameObject.name == "LowerBoundary")
        {
            DownDir = 0; // more down move permission denied
            moveDirection = new Vector3(0, 0, 0);
        }
        
    }

    public Color prevColor;
    public Color myColor;
    public void AddTail()
    {
        var tail = Instantiate(snakeTailPrefab);
        tail.transform.SetParent(GameManager.Instance.tailsContainer);
        tails.Add(tail);
        tail.SetOrder(-tails.Count);
        if(tails.Count > 1)
        {
            tails[tails.Count -2].AddChildTail(tail);

        }
        else
        {
            myTailChild = tails[0];
            myPath.outPosition = myTailChild.SetPostion;
        }
            
    }

    public void ChangeTailsColor(Color color)
    {
        prevColor = myColor;
        myColor = color;
        spriteRenderer.color = color;   
        foreach(var tail in tails)
        {
            tail.SetColor(myColor);
        }
    }
}


public class Path
{

    Queue<Vector3> positionsQueue = new();
    public int MaxQueueLength => GameManager.Instance.tailsSeprationLevel;

    public Action<Vector3> outPosition;
    public void AddPostion(Vector3 position)
    {

        // Dynamically adjust queue length to new spacing or speration value
        if (positionsQueue.Count > MaxQueueLength)
        {
            var difference = positionsQueue.Count - MaxQueueLength;
            for (int i = 0; i < difference; i++)
            {
                positionsQueue.Dequeue();
            }
        }

        // Actual logic
        if (positionsQueue.Count == MaxQueueLength)
        {
            var pos = positionsQueue.Dequeue();
            positionsQueue.Enqueue(position);
            if (outPosition != null)
            {
                outPosition(pos);
            }
        }
        else
        {
            positionsQueue.Enqueue(position);
        }
    }

}