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
        spawnType = "RandomStill"; //Can be placed anywhere randomly and doesn't move.
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
