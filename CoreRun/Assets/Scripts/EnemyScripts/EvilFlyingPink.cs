using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlyingPink : Enemy
{
    private float lerpValue;//Holds lerp value for this enemy, letting them move back and forth

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerAggroRange = 5;
        spawnOffset = 2;
        lerpValue = Random.Range(0.0f,1.0f);
        transform.Rotate(90,0,0);//Adjust self after spawning
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Move();
    }

    //Move
    protected override void Move()
    {
        transform.position = new Vector3(
            Mathf.Lerp(-movementZone, movementZone, lerpValue),
            transform.position.y,
            transform.position.z);
        lerpValue += 0.5f * Time.deltaTime;
        if (lerpValue > 1.0f)
        {
            movementZone = -movementZone;
            lerpValue = 0.0f;
        }
    }
}
