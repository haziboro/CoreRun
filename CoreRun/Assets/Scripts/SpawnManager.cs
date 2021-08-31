using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class SpawnManager : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] GameObject planet;
    [SerializeField] GameObject layerEnd;//Spawns the layer end object
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float spawnDegree = 15f;//Enemy spawn position in x-degrees relative to planet
    [SerializeField] float spawnDistance = 50.5f;//Enemy spawn distance from center of planet
    [SerializeField] float baseSpawnRate = 3.0f; //Base rate which enemies spawn in seconds
    [SerializeField] float spawnRateModifier = 10; //How much(in percent) the spawnrate will increase when IncreaseRate() called
    [SerializeField] float trackWidthFromCenter = 3.4f; //Area for enemies to spawn in
    [SerializeField] float currentSpawnRate;//Spawnrate after modifier applied

    private bool readyToSpawn;
    public bool spawningLayerEnd;//true when transitioning layers.

    // Start is called before the first frame update
    void Start()
    {
        currentSpawnRate = baseSpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToSpawn && gameRunning.active)
        {
            readyToSpawn = false;
            StartCoroutine("SpawnTimer");
        }
    }

    //Spawns enemies on a delay. When layer is ending the next enemy is replaced with layer end object.
    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(currentSpawnRate);
        if (gameRunning.active)
        {
            if (!spawningLayerEnd)
            {
                SpawnEnemy(ChooseRandomEnemy());
                readyToSpawn = true;
            }//endif
            else
            {
                SpawnEnemy(layerEnd);
            }//endelse
        }//endif
    }

    //Instantiate an enemy
    void SpawnEnemy(GameObject prefab)
    {
        //Spawn at default position
        GameObject enemy = Instantiate(prefab, Vector3.zero,
            prefab.transform.rotation);
        //Assign planet earth as enemy parent
        enemy.transform.parent = planet.transform;
        //Reset enemy position to planet center
        enemy.transform.position = planet.transform.position;
        //Rotate enemy relative to planet to position just beyond horizon
        enemy.transform.Rotate(spawnDegree, 0, 0);

        //Move unit to surface, offset by their spawnOffset parameter
        enemy.transform.Translate(0, 0,
            spawnDistance + enemy.GetComponent<Enemy>().spawnOffset);
        enemy.GetComponent<Enemy>().movementZone = trackWidthFromCenter;
    }

    //Returns a random enemy from enemyPrefabs
    private GameObject ChooseRandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[enemyIndex];
    }

    //Initiates enemy spawning
    public void StartSpawning()
    {
        spawningLayerEnd = false;
        readyToSpawn = true;
    }

    //Spawns the end of the layer
    public void SpawnLayerEnd()
    {
        spawningLayerEnd = true;
    }

    //Increases the spawnrate
    public void IncreaseRate()
    {
        currentSpawnRate -= currentSpawnRate * (spawnRateModifier / 100);
    }
}
