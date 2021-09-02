using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCave : Enemy
{
    [SerializeField] GameEvent increaseLayer;
    bool isColliding = true;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        spawnOffset = 5;
        playerAggroRange = 1;
        transform.Rotate(90, 0, 0);//Adjust self after spawning
    }

    // Update is called once per frame
    protected override void Update()
    {
        isColliding = false;
    }

    //Once the cave hits the player, it starts spawning
    protected override void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        //Transition the layer once the player hits the cave
        increaseLayer.Raise();
        Destroy(gameObject);
    }
}
