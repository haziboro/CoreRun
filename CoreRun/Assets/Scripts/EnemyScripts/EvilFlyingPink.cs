using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlyingPink : Enemy
{
    private AnimatorController anim;
    private float lerpValue;//Holds lerp value for this enemy, letting them move back and forth
    private bool moving;

    [SerializeField] float speed = 0.5f;//Distance above the ground the enemy can fly
    [SerializeField] float pauseDuration = 0.1f;//Duration of random pauses
    [SerializeField] int pauseChance = 1000;//Chance of random pause occuring per frame is 1/pauseChance
    [SerializeField] int turnChance = 50;//Chance of turning around after pausing as a percent


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        lerpValue = Random.Range(0.0f,1.0f);
        moving = true;
        anim = GetComponent<AnimatorController>();

        CheckForReversal();

        transform.Rotate(180,0,0);//Adjust self after spawning
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (gameRunning.active)
        {
            Move();
        }
    }

    //Move
    protected override void Move()
    {
        transform.position = new Vector3(
            Mathf.Lerp(-movementZone, movementZone, lerpValue),
            transform.position.y,
            transform.position.z);
        lerpValue += speed * Time.deltaTime *(moving ? 1 : 0);
        if (lerpValue > 1.0f)
        {
            movementZone = -movementZone;
            lerpValue = 0.0f;
        }

        CheckForRandomPause();
    }

    //Rolls to see if the enemy should randomly stop moving
    private void CheckForRandomPause()
    {
        if (Random.Range(0,pauseChance) == 0)
        {
            StartCoroutine(RandomPause());
        }
    }

    //Randomly determine if the animation will be reversed
    private void CheckForReversal()
    {
        if (Random.Range(0, 2) == 0)
        {
            anim.enemyAnimator.SetTrigger("ReverseAnim");
        }
    }

    //Timer for random pause, with chance of turning around
    IEnumerator RandomPause()
    {

        //Eye animation goes here

        moving = false;
        if (Random.Range(0,100) < turnChance)
        {
            lerpValue = 1 - lerpValue;
            movementZone = -movementZone;
        }
        yield return new WaitForSeconds(pauseDuration);
        moving = true;
    }

    protected override void AggroTrigger()
    {

    }
}
