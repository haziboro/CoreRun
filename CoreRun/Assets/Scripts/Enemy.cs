using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    GameObject player;
    CapsuleCollider narrowMissField; //Narrow miss collider around player
    private float playerDistance;
    private bool aggro; //True when player has entered aggro range
    private bool narrowMiss; //True when player narrowly dodges
    public float playerAggroRange; //Distance of aggro range
    public float spawnOffset; //Offset for positioning flat on ground
    public string spawnType; //Defines how this enemy behaves when spawned

    // Start is called before the first frame update
    protected virtual void Start() 
    {
        InitializeStartValues();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DetectPlayerDistance();
    }

    //Initiate variable behavior based on player distance
    protected void DetectPlayerDistance()
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
                player.GetComponent<Player>().ReportEnemyAvoidance(narrowMiss);
                Destroy(gameObject);
            }
        }//endelse
    }

    //Trigger ReportDeath() in player when colliding with them
    protected void OnTriggerEnter(Collider other)
    {
        if(other.name == narrowMissField.name)
        {
            narrowMiss = true;
        }
        else
        {
            player.GetComponent<Player>().ReportImpact();
        }
    }

    //For initializing variables in subclasses
    protected void InitializeStartValues()
    {
        player = GameObject.Find("PlayerCube");
        narrowMissField = player.GetComponentInChildren<CapsuleCollider>();
        aggro = false;
        narrowMiss = false;
    }

    //Returns how far from the level wall an enemy should be when spawned
    public virtual float WallOffset()
    {
        return 0;
    }

}
