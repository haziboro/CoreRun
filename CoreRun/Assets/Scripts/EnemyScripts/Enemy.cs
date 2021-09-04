using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    GameObject player;
    CapsuleCollider narrowMissField; //Narrow miss collider around player
    private float playerDistance;
    protected bool aggro; //True when player has entered aggro range
    private bool narrowMiss; //True when player narrowly dodges
    private bool hitPlayer;//true when an enemy has already hit the player

    [SerializeField] protected ScriptableBool gameRunning;
    [SerializeField] GameEvent playerHit;
    [SerializeField] GameEvent normalDodge;
    [SerializeField] GameEvent narrowDodge;

    public float playerAggroRange; //Distance of aggro range
    public float spawnOffset; //Offset for positioning flat on ground
    public float movementZone; //Defines how far from spawn an enemy can move

    // Start is called before the first frame update
    protected virtual void Start() 
    {
        //For player tracking
        player = GameObject.Find("PlayerCube");
        narrowMissField = player.GetComponentInChildren<CapsuleCollider>();

        //variables
        aggro = false;
        narrowMiss = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (gameRunning.active)
        {
            DetectPlayerDistance();
        }
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
                AggroTrigger();
            }
        }//endif
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        OnTriggerFire(other);
    }

    //Trigger ReportDeath() in player when colliding with them
    public virtual void OnTriggerFire(Collider other)
    {
        if(other.name == narrowMissField.name)
        {
            narrowMiss = true;
        }
        else if (other.CompareTag("Sensor"))
        {
            if (!hitPlayer)
            {
                if (narrowMiss) { narrowDodge.Raise(); }
                else { normalDodge.Raise();  }
            }
            Destroy(gameObject);
        }
        else
        {
            playerHit.Raise();
            hitPlayer = true;
        }
    }

    //Performs an enemy movement, if applicable
    protected virtual void Move()
    {
        //Do nothing, implement in subclass
    }

    //Action to trigger when the enemy first enter aggro range
    protected abstract void AggroTrigger();

    //Returns how far from the level wall an enemy should be when spawned
    public virtual float WallOffset()
    {
        return 0;
    }

}
