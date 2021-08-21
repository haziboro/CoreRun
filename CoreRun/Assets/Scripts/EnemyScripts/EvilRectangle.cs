using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A particular type of enemy
public class EvilRectangle : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerAggroRange = 5;
        spawnOffset = 2.40f;

        Move();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    //Evil Rectangle will move itself randomly after its' spawned
    protected override void Move()
    {
        Vector3 newPos = transform.position;
        newPos.x += Random.Range(-movementZone + WallOffset(),
            movementZone - WallOffset());
        transform.position = newPos;
    }
}
