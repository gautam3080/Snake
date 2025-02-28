using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] FruitPrefabs;

    [SerializeField] float SpawningRateInSec = 3;
    
    // Start is called before the first frame update
    void Start()
    {
    }


    public void StartSpawning()
    {
        StartCoroutine(FruitSpawnwer());
    }

    IEnumerator FruitSpawnwer()
    {

        while(GameManager.Instance.gameState == GameState.GamePlay)
        {
            SpawnRandomFruit();
            yield return new WaitForSeconds(1/SpawningRateInSec);

        }
    }

   
    public Vector3 GetRandomPosition()
    {

        Vector3 min = new Vector3(-8.65f, -4.75f);
        Vector3 max = new Vector3(8.66f, 4.75f);

        var x = Random.Range(min.x, max.x);
        var y = Random.Range(min.y, max.y);

        return new Vector3 (x, y, 0);
    }

    /// <summary>
    /// It will spawn Random Fruits
    /// </summary>
    public void SpawnRandomFruit()
    {
        var randomIndex = Random.Range(0, FruitPrefabs.Length);
        var fruitObj = Instantiate(FruitPrefabs[randomIndex],GetRandomPosition(), Quaternion.identity);
        fruitObj.transform.SetParent(GameManager.Instance.fruitsContainer);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
