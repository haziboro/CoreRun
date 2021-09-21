using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class EnemyWave : ScriptableObject, ISerializationCallbackReceiver
{
    private int numGroups_ = 0;
    private int numEnemies_ = 0;

    public List<EnemyGroup> enemyGroups;

    [System.NonSerialized]
    public int numGroups;
    [System.NonSerialized]
    public int totalNumEnemies;

    public void OnAfterDeserialize()
    {
        numGroups = numGroups_;
        totalNumEnemies = numEnemies_;
        foreach (EnemyGroup s in enemyGroups)
        {
            totalNumEnemies += s.repeatAmount * s.seriesOfEnemies.Count;
        }
        numGroups = enemyGroups.Count;
    }

    public void OnBeforeSerialize() { }

    //Represents a group of enemies
    [System.Serializable]
    public class EnemyGroup
    {
        //Represents amount this group is repeated
        public int repeatAmount = 1;
        public List<Enemy> seriesOfEnemies;
    }
}
