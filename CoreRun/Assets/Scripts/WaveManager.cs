using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unpacks a wave and requests individual spawns
public class WaveManager : MonoBehaviour
{
    private bool readyToSpawn = false;
    private float timeBetweenGroups = 0;
    private float timeBetweenSpawns = 0;
    private int count;
    private Queue<SpawnInstruction> enemyQueue;

    [SerializeField] GameEvent SpawnNextEnemy;
    [SerializeField] GameEvent startLayer;
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScriptableInt layerInterval;
    [SerializeField] float waveStartBufferTime;//Time after the layer transition before the next wave starts
    [SerializeField] NextEnemy nextEnemy;
    //Contains a list of enemies and how much each should be spawned respectively
    [SerializeField] List<EnemyWave> waves;
    

    // Start is called before the first frame update
    void Awake()
    {
        enemyQueue = new Queue<SpawnInstruction>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (readyToSpawn && gameRunning.active)
        {
            readyToSpawn = false;
            if (enemyQueue.Count != 0)
            {
                StartCoroutine("RequestNewEnemy");
            }//endif
        }//endif
    }

    //Requests spawns of enemies on intervals extracted from SpawnInstructions
    IEnumerator RequestNewEnemy()
    {
        SpawnInstruction s = enemyQueue.Dequeue();
        nextEnemy.enemy = s.enemy;
        SpawnNextEnemy.Raise();//Event notifies spawn manager to check Next Enemy
        yield return new WaitForSeconds(s.timer);
        readyToSpawn = true;
    }

    //Pauses for buffer time before starting the next wave
    public void WaveBuffer()
    {
        StartCoroutine("WaveBufferTimer");
    }
    
    private IEnumerator WaveBufferTimer()
    {
        yield return new WaitForSeconds(waveStartBufferTime);
        startLayer.Raise();
    }

    //Overload for events to call
    public void LoadWave()
    {
        LoadWave(ChooseRandomWave());
        readyToSpawn = true;
    }

    //ChooseRandomWave
    public EnemyWave ChooseRandomWave()
    {
        return waves[Random.Range(0, waves.Count)];
    }

    //Loads the next wave
    private void LoadWave(EnemyWave w)
    {
        //calculates how much time should be between each group of spawns
        timeBetweenGroups = (float)layerInterval.value / w.numGroups;
        foreach (EnemyWave.EnemyGroup group in w.enemyGroups)
        {
            UnwrapGroup(group);
        }
        count = enemyQueue.Count;
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
                enemyQueue.Enqueue(new SpawnInstruction(enemy, timeBetweenSpawns));
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
    }//end SpawnInstruction

}
