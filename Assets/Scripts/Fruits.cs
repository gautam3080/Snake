using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Fruits : MonoBehaviour
{
    public float lifeTimeInSec = 5;

    private SpriteRenderer mySpriteRenderer;
    public PlayerController playerController;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        Destroy(gameObject, lifeTimeInSec);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlayerController = collision.TryGetComponent(out playerController);// it will check if collided gameObject have component of type playerController, if true it will out that component
        if (!isPlayerController) return;
        
        playerController.AddTail();
        playerController.ChangeTailsColor(mySpriteRenderer.color);
        Destroy(gameObject);
    }
}
