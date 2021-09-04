using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroDetection : MonoBehaviour
{
    GameObject player;
    private float playerDistance;

    public ScriptableBool gameRunning;
    public bool aggro; //True when player has entered aggro range
    public float playerAggroRange; //Distance of aggro range

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerCube");
        aggro = false;
    }

    // Update is called once per frame
    void Update()
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
        if (aggro == false)//Wait for player to enter aggro range
        {
            if (playerDistance < playerAggroRange)//Once they enter aggro, set aggro to true
            {
                aggro = true;
            }
        }//endif
    }


}
