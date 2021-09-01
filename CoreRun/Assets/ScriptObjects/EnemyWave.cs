using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class EnemyWave : ScriptableObject, ISerializationCallbackReceiver
{
    private int totalEnemies_ = 0;

    public List<EnemySubWave> enemySubWaves;

    [System.NonSerialized]
    public int totalEnemies;

    //Count and store total number of individual enemies in all waves
    public void OnAfterDeserialize()
    {
        totalEnemies = totalEnemies_;
        foreach (EnemySubWave s in enemySubWaves)
        {
            totalEnemies += s.numEnemy;
        }
    }

    public void OnBeforeSerialize() { }

    [System.Serializable]
    public class EnemySubWave
    {
        public int numEnemy = 1;
        public Enemy enemy;
    }
}
