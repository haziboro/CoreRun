using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject earth;
    public float earthRadius;
    public GameObject[] enemyPrefabs;

    float earthSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        earthRadius = earth.GetComponent<SphereCollider>().radius *
            earth.transform.localScale.x;
        InvokeRepeating("SpawnRandomEnemy", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        earth.transform.Rotate(Vector3.left * Time.deltaTime * earthSpeed);
    }

    void SpawnRandomEnemy()
    {

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject newEnemy = Instantiate(enemyPrefabs[enemyIndex], setSpawn(),
            enemyPrefabs[enemyIndex].transform.rotation);



        Vector3 enemySize = newEnemy.transform.localScale;
        newEnemy.transform.parent = earth.transform;
        newEnemy.transform.localScale = new Vector3(enemySize.x/earth.transform.localScale.x,
            enemySize.y / earth.transform.localScale.y,
            enemySize.z / earth.transform.localScale.z);

        int randNum = Random.Range(-5,5);

        newEnemy.transform.RotateAround(Vector3.zero,
            newEnemy.transform.parent.up, randNum);

    }

    Vector3 setSpawn()
    {
        Vector3 enemyPos = new Vector3(earth.transform.position.x,
            earth.transform.position.y + earthRadius + 0.5f,
            earth.transform.position.z);
        return enemyPos;
    }

}
