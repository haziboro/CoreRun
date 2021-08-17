using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject player;
    private float playerDistance;
    private bool aggro = false; //True when player has entered aggro range
    [SerializeField] float playerAggroRange = 5; //Distance of aggro range

    //Offset for proper positioning when spawned
    [SerializeField] float spawnOffset = 2.40f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCube");
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerDistance();
    }

    //Initiate variable behavior based on player distance
    void DetectPlayerDistance()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if(aggro == false)//Wait for player to enter aggro range
        {
            if (playerDistance < playerAggroRange)//Once they enter aggro, set aggro to true
            {
                aggro = true;
            }
        }//endif
        else//after aggro is entered, if the enemy leaves the player range destroy self
        {
            if (playerDistance > playerAggroRange)
            {
                //Report to the player that they have been passed
                player.GetComponent<Player>().ReportEnemyAvoidance();
                Destroy(gameObject);
            }
        }//endelse
    }

    //Returns spawn Offset
    public float GetOffset()
    {
        return spawnOffset;
    }

}
