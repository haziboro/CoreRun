using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManager : MonoBehaviour
{

    [SerializeField] ScriptableBool gameRunning;

    //Contains a list of enemies and how much each should be spawned respectively
    [SerializeField] List<EnemyWave> waves;
    Queue<SpawnInstruction> enemysWaiting;

    private int layerInterval = 20;
    private float timeBetweenGroups = 0;
    private float timeBetweenSpawns = 0;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        enemysWaiting = new Queue<SpawnInstruction>();
        LoadWave();
        for (int i = 0; i < count; i++)
        {
            SpawnInstruction f = enemysWaiting.Dequeue();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Loads the next wave
    public void LoadWave()
    {
        //calculates how much time should be between each group of spawns
        timeBetweenGroups = (float)layerInterval / waves[0].numGroups;
        foreach (EnemyWave.EnemyGroup group in waves[0].enemyGroups)
        {
            UnwrapGroup(group);
        }
        count = enemysWaiting.Count;
    }

    //Determines how much time should pass between spawning individual enemies in a group
    void UnwrapGroup(EnemyWave.EnemyGroup group)
    {
        timeBetweenSpawns = timeBetweenGroups / (group.repeatAmount * group.seriesOfEnemies.Count);

        //Repeats the entire group by repeatAmount
        for (int i = 0; i < group.repeatAmount; i++)
        {
            foreach (Enemy enemy in group.seriesOfEnemies)//Adds the group to the queue
            {
                enemysWaiting.Enqueue(new SpawnInstruction(enemy, timeBetweenSpawns));
            }//endforeach
        }//endfor
    }//endGenerateCommands


    //Holds an enemy to spawn and its spawn buffer time
    public class SpawnInstruction
    {
        public Enemy enemy;
        public float timer;
        public SpawnInstruction(Enemy e, float spawnTimer)
        {
            enemy = e;
            timer = spawnTimer;
        }
    }//end SpawnCommand

}
