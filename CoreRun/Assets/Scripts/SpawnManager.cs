using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public bool spawningLayerEnd;//true when transitioning layers.

    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] NextEnemy nextEnemy;
    [SerializeField] GameObject planet;
    [SerializeField] GameObject layerEnd;//Spawns the layer end object
    [SerializeField] float spawnDegree = 15f;//Enemy spawn position in x-degrees relative to planet
    [SerializeField] float spawnDistance = 50.5f;//Enemy spawn distance from center of planet
    [SerializeField] float trackWidthFromCenter = 3.4f; //Area for enemies to spawn in

    //Instantiate an enemy
    public void SpawnEnemy(GameObject prefab)
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

    //SpawnNextEnemy
    public void SpawnNextEnemy()
    {
        if (gameRunning.active)
        {
            SpawnEnemy(nextEnemy.enemy.gameObject);
        }
    }

    //Spawns the end of the layer
    public void SpawnLayerEnd()
    {
        if (gameRunning.active)
        {
            SpawnEnemy(layerEnd);
        }
    }
}
