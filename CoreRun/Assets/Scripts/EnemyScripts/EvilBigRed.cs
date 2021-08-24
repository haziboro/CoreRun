using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilBigRed : Enemy
{

    public bool charging = false;
    public float wallOffset = 0.4f;
    [SerializeField] float speed = 10;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerAggroRange = 10;
        spawnOffset = 1.9f;
        transform.Rotate(90, 0, 0);//Adjust self after spawning
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
        Debug.Log("Wall Offset is: " + wallOffset);
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
            //Find the children (openMouth,ClosedMouth)
            //Set the mouth to active and the other to inactive
            GameObject bigRed = transform.Find("EvilBigRed (1)").gameObject;
            bigRed.transform.Find("OpenMouth").gameObject.SetActive(true);
            bigRed.transform.Find("ClosedMouth").gameObject.SetActive(false);
        }

        //Charging movement
        transform.Translate(new Vector3(0,-0.05f,-1)  * speed * Time.deltaTime);
    }

}
