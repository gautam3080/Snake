using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform tailsContainer;
    public Transform fruitsContainer;
    public SpriteRenderer playerSprite;
    public FruitSpawner spawner;



    [Range(5f, 100f)]
    public int tailsSeprationLevel = 20;

    public GameState gameState = GameState.GamePlay;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Play()
    {
        gameState = GameState.GamePlay;
        spawner.StartSpawning();
    }

    private void Start()
    {
        Play();
    }


}

public enum GameState
{
    GameOver,
    GamePlay,
    Menu,
}