using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A particular type of enemy
public class EvilRectangle : Enemy
{
    private bool falling = false;
    private AnimatorController anim;
    private AggroDetection detector;

    [Range(0, 100)] [SerializeField] int fallChancePercent;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<AnimatorController>();
        detector = GetComponent<AggroDetection>();
        if (Random.Range(0, 100) < fallChancePercent)
        {
            if((Random.value > 0.5f))//50/50 split between left or right fall
            {
                anim.enemyAnimator.SetBool("FallRight", false);
            }
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
        if (detector.aggro)
        {
            AggroTrigger();
        }
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
    public void AggroTrigger()
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
