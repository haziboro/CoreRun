using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Controls player movement and responds to damage taken
public class Player : MonoBehaviour
{
    private float horizontalInput;
    private Touch touch;

    [SerializeField] Camera cam;
    [SerializeField] GameEvent playerDied;
    [SerializeField] GameEvent iFramesOn;
    [SerializeField] GameEvent tookDamage;
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScriptableBool gamePaused;
    [SerializeField] ScriptableBool iFramesObj;
    [SerializeField] PlayerHealth health;
    [SerializeField] float speed = 10;//left-right movement speed
    [SerializeField] float levelBoundaries = 3.4f;//distance from center

    [SerializeField] int touchDeadzone;//width around character touch isn't read
    [SerializeField] float inputAcceleration;
    [SerializeField] float inputDecceleration;

    // Start is called before the first frame update
    private void Start()
    {
        health.health = health.maxHealth;
        horizontalInput = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            //if touch to the left, decrease axes
            if (touch.position.x < cam.WorldToScreenPoint(transform.position).x - touchDeadzone)
            {
                horizontalInput -= inputAcceleration;
                horizontalInput = horizontalInput < -1 ? -1 : horizontalInput;
            }
            //if to the right, increase axis
            else if (touch.position.x > cam.WorldToScreenPoint(transform.position).x + touchDeadzone)
            {
                horizontalInput += inputAcceleration;
                horizontalInput = horizontalInput > 1 ? 1 : horizontalInput;
            }
            else
            {
                NeutralizeMovement();
            }
        }
        //When not touching, return to 0
        else if (Input.touchCount == 0)
        {
            NeutralizeMovement();
        }
        else { }
    }

    //Return movement to zero
    private void NeutralizeMovement()
    {
        float adjustedInput;
        if (horizontalInput > 0)
        {
            adjustedInput = horizontalInput - inputDecceleration;
            horizontalInput = adjustedInput < 0 ? 0 : adjustedInput;
        }
        else if (horizontalInput < 0)
        {
            adjustedInput = horizontalInput + inputDecceleration;
            horizontalInput = adjustedInput > 0 ? 0 : adjustedInput;
        }
        else { }
    }

    private void FixedUpdate()
    {
        if (gameRunning.active && !gamePaused.active)
        {
            SideMovement();
        }
    }

    //Move player left/right based on input
    void SideMovement()
    {
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        CheckBoundaries();
    }

    //Establishes level boundaries
    private void CheckBoundaries()
    {
        // Check for left and right bounds, on the Z axis here
        if (transform.position.x < -levelBoundaries)
        {
            transform.position = new Vector3(-levelBoundaries, transform.position.y, transform.position.z);
        }
        if (transform.position.x > levelBoundaries)
        {
            transform.position = new Vector3(levelBoundaries, transform.position.y, transform.position.z);
        }
    }

    //Reduces player health and changes their color when damaged
    //Raise death flag when health is reduced to zero
    public void TakeDamage()
    {
        health.lostHealth = true;
        if (!iFramesObj.active && gameRunning.active)
        {
            health.health--;
            tookDamage.Raise();
            if (health.health == 0)
            {
                playerDied.Raise();
            }
            else
            {
                iFramesOn.Raise();
            }//endelse
        }//endif
    }//end TakeDamage

    //If the player completed a layer without getting hit, this will give them another life
    public void GainHealth()
    {
        if (!health.lostHealth)
        {
            if (health.health < health.maxHealth)
            {
                health.health += 1;
            }//endif
        }//endif
    }

    //Resets the tracker for lost player health
    public void ResetLostHealthBool()
    {
        health.lostHealth = false;
    }

}//End Player
