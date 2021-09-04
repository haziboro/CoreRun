using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A particular type of enemy
public class EvilRectangle : Enemy
{
    private bool falling = false;
    private AnimatorController anim;

    [Range(0, 100)] [SerializeField] int fallChancePercent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<AnimatorController>();
        if (Random.Range(0, 100) < fallChancePercent)
        {
            anim.enemyAnimator.SetBool("Falling", true);
            anim.enemyAnimator.SetBool("Idle", false);
            falling = true;
        }
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

    //Makes the rectangle animate an angry face when near the player, or fall if falling
    protected override void AggroTrigger()
    {
        if(falling)
        {
            anim.enemyAnimator.SetBool("Landing", true);
        }
        else
        {
            anim.enemyAnimator.SetBool("Idle", false);
        }
    }
}
