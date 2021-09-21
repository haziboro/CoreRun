using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Controls player movement and responds to damage taken
public class Player : MonoBehaviour
{
    private float horizontalInput;

    [SerializeField] GameEvent playerDied;
    [SerializeField] GameEvent iFramesOn;
    [SerializeField] GameEvent tookDamage;
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScriptableBool gamePaused;
    [SerializeField] ScriptableBool iFramesObj;
    [SerializeField] PlayerHealth health;
    [SerializeField] float speed = 10;//left-right movement speed
    [SerializeField] float levelBoundaries = 3.4f;//distance from center

    // Start is called before the first frame update
    private void Start()
    {
        health.health = health.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
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
