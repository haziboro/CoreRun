using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioSource;
    private GameObject playerGraphic;//The player graphic, which contains a box collider and rigidbody
    private Vector3 playerScale;
    private Vector3 shrinkScale; //all three values defined by shrinkspeed
    private float currentScale;//scale the player currently has out of 100
    private float shrinkGroundAdjustment;
    private float horizontalInput;
    private bool running = false;
    private float health;//Must be float to lerp colors properly
    private bool invincibilityFramesOn = false;

    [SerializeField] float speed;
    [SerializeField] AudioClip[] narrowDodgeClips;
    [SerializeField] float baseSpeed = 10;//left/right movement speed
    [SerializeField] float shrinkSpeed = 1f;//Rate at which shrinking occurs
    [SerializeField] float levelBoundaries = 3.4f;//distance from center
    [SerializeField] int maxHealth = 3;
    [SerializeField] float iFrameDuration = 0.2f;
    [SerializeField] Color lerpColorMaxHealth;
    [SerializeField] Color lerpColorNoHealth;
    [Range(0.0f, 1.0f)] [SerializeField] float minimumSize = 0.5f; //Smallest size allowed

    // Start is called before the first frame update
    void Start()
    {
        //Load gameobjects
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        playerGraphic = transform.Find("PlayerGraphic").gameObject;

        //Set base scale values
        playerScale = playerGraphic.transform.localScale;

        //Calculate variables
        shrinkScale = new Vector3(-shrinkSpeed, -shrinkSpeed, -shrinkSpeed);
        shrinkGroundAdjustment = shrinkSpeed / 2;
        speed = baseSpeed;
        health = maxHealth;

        StartCoroutine(ShortPause(0.2f));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameRunning && !gameManager.gamePaused && running)
        {
            SideMovement();
            Shrink();
        }
    }

    //Move player left/right based on input
    void SideMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);

        CheckBoundaries();
    }

    //Shrink player when 'Down' is pressed
    void Shrink()
    {
        //Shrink player down to half size when down is held
        if (Input.GetButton("Down")){
            //if the player's current scale is higher than their original scale/minimunSize
            if (playerGraphic.transform.localScale.x > playerScale.x * minimumSize)
            {
                playerGraphic.transform.localScale += shrinkScale * Time.deltaTime;
                playerGraphic.transform.Translate(0.0f,
                    -shrinkGroundAdjustment * Time.deltaTime, 0.0f);
            }
        }//endif
        //Automatically grow player up to original size when down is not held
        else
        {
            if(playerGraphic.transform.localScale.x < playerScale.x)
            {
                playerGraphic.transform.localScale -= shrinkScale * Time.deltaTime;
                playerGraphic.transform.Translate(0.0f,
                    shrinkGroundAdjustment * Time.deltaTime, 0.0f);
            }
        }//endelse
        currentScale = playerGraphic.transform.localScale.x / playerScale.x;

        //Change speed proportionate to size change
        speed = baseSpeed * currentScale;
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

    //Report to GameManager that an enemy has been sucessfully passed
    public void ReportEnemyAvoidance(bool isNarrowDodge)
    {
        gameManager.EnemyAvoided(isNarrowDodge);
        if (isNarrowDodge)
        {
            //Play random dodge audio clip on succesful narrow dodge
            int clipNum = Random.Range(0, narrowDodgeClips.Length);
            audioSource.clip = narrowDodgeClips[clipNum];
            audioSource.Play();
            //Do a happy animation
        }
    }

    //Reduced player health, changing their color and causing a sound.
    //Reports to GameManager when the player dies
    public void ReportImpact()
    {
        if (!invincibilityFramesOn)
        {
            //Lose a life
            health--;
            //if health is zero, report death
            if (health == 0)
            {
                //Shift to last color
                playerGraphic.GetComponent<Renderer>().material.SetColor(
                    "_Color", Color.Lerp(lerpColorNoHealth, lerpColorMaxHealth, 0));

                //Make death SFX

                gameManager.PlayerDied();
            }
            else
            {
                StartCoroutine(iFrames());
                //Go down a color
                playerGraphic.GetComponent<Renderer>().material.SetColor(
                    "_Color", Color.Lerp(lerpColorNoHealth, lerpColorMaxHealth, (health/maxHealth)));
                //Make a hurt sound
            }//endelse
        }//endif
    }

    //Timer for Invincibility Frames
    IEnumerator iFrames()
    {
        //Visual indicator for iframes should go here

        invincibilityFramesOn = true;
        yield return new WaitForSeconds(iFrameDuration);
        invincibilityFramesOn = false;

        //Turn off indicator
    }

    //For delaying when needed
    IEnumerator ShortPause(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        running = true;
    }
}
