using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    private GameObject planet;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float spawnDegree = 15f;//Enemy spawn position in x-degrees relative to planet
    [SerializeField] float spawnDistance = 50.5f;//Enemy spawn distance from center of planet
    [SerializeField] float baseSpawnRate = 3.0f; //Base rate which enemies spawn in seconds
    [SerializeField] float spawnRateModifier = 10; //How much(in percent) the spawnrate will increase when IncreaseRate() called
    [SerializeField] float trackWidthFromCenter = 3.4f; //Area for enemies to spawn in


    [SerializeField] float currentSpawnRate;//Spawnrate after modifier applied

    private bool readyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Earth");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        readyToSpawn = true;
        currentSpawnRate = baseSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn && gameManager.gameRunning)
        {
            readyToSpawn = false;
            StartCoroutine("SpawnTimer");
        }
    }

    //Spawns enemies on a delay
    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(currentSpawnRate);
        if (gameManager.gameRunning)
        {
            SpawnEnemy();
            readyToSpawn = true;
        }
    }

    //Instantiate an enemy
    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0,enemyPrefabs.Length);
        //Spawn at default position
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex],
            Vector3.zero,
            enemyPrefabs[enemyIndex].transform.rotation);
        //Assign planet earth as enemy parent
        enemy.transform.parent = planet.transform;
        //Reset enemy position to planet center
        enemy.transform.position = planet.transform.position;
        //Rotate enemy relative to planet to position just beyond horizon
        enemy.transform.Rotate(spawnDegree, 0, 0);

        //Move unit to surface, offset by their individual value
        enemy.transform.Translate(0, 0,
            spawnDistance + enemy.GetComponent<Enemy>().spawnOffset);
        enemy.GetComponent<Enemy>().movementZone = trackWidthFromCenter;
    }

    //Increases the spawnrate
    public void IncreaseRate()
    {
        currentSpawnRate -= currentSpawnRate * (spawnRateModifier / 100);
    }
}
