using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBigRed : Enemy
{
    private AnimatorController anim;
    private bool charging = false;

    public float wallOffset = 0.4f;
    [SerializeField] float speed = 10;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        transform.Rotate(90, 0, 0);//Adjust self after spawning
        anim = GetComponent<AnimatorController>();
        Move();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (aggro)
        {
            Charge();
        }
    }

    //Moves the attached enemy to the center or side of road
    protected override void Move()
    {
        int position = Random.Range(0,3);
        //left
        if (position == 0)
        {
            transform.Translate(Vector3.right * (-movementZone + wallOffset));
        }
        //right
        else if(position == 1)
        {
            transform.Translate(Vector3.right * (movementZone - wallOffset));
        }
        else{}
    }

    //Changes attached appearance when initiating charge and moves it continuously afterwards
    void Charge()
    {
        if (!charging)//runs the first time function called
        {
            charging = true;
            anim.enemyAnimator.SetBool("Awake", true);
        }

        //Charging movement
        transform.Translate(new Vector3(0,-0.05f,-1)  * speed * Time.deltaTime);
    }

    protected override void AggroTrigger()
    {

    }
}
