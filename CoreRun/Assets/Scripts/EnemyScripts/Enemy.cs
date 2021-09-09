using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    CapsuleCollider narrowMissField; //Narrow miss collider around player
    private bool narrowMiss; //True when player narrowly dodges
    private bool hitPlayer;//true when an enemy has already hit the player

    [SerializeField] protected ScriptableBool gameRunning;
    [SerializeField] GameEvent playerHit;
    [SerializeField] GameEvent normalDodge;
    [SerializeField] GameEvent narrowDodge;

    public float spawnOffset; //Offset for positioning flat on ground
    public float movementZone; //Defines how far from spawn an enemy can move

    // Start is called before the first frame update
    protected virtual void Start() 
    {
        narrowMissField = GameObject.Find("PlayerCube")
            .GetComponentInChildren<CapsuleCollider>();

        narrowMiss = false;
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

    //Returns how far from the level wall an enemy should be when spawned
    public virtual float WallOffset()
    {
        return 0;
    }

}
