using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioSource;
    private PlayerGraphicsController playerGraphicControl;//Defined here
    private float horizontalInput;
    private float health;//Must be float to lerp colors properly
    private bool invincibilityFramesOn = false;

    //Events
    [SerializeField] GameEvent playerDied;

    //ScriptObj
    [SerializeField] ScriptableBool gameRunning;

    [SerializeField] GameObject playerGraphic;
    [SerializeField] float speed = 10;//left/right movement speed
    [SerializeField] float levelBoundaries = 3.4f;//distance from center
    [SerializeField] int maxHealth = 3;
    [SerializeField] float iFrameDuration = 1.5f;
    [SerializeField] float iFrameDeltaTime = 0.10f; //For gradual loss of iframes
    [SerializeField] Color lerpColorMaxHealth;
    [SerializeField] Color lerpColorNoHealth;

    //SFX
    [SerializeField] AudioClip[] narrowDodgeClips;
    [SerializeField] AudioClip[] healthLossClips;

    // Start is called before the first frame update
    void Start()
    {
        //Load gameobjects
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();

        playerGraphicControl = playerGraphic.GetComponent<PlayerGraphicsController>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (gameRunning.active && !gameManager.gamePaused)
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

    //Report to GameManager that an enemy has been sucessfully passed
    public void ReportEnemyAvoidance(bool isNarrowDodge)
    {
        //gameManager.EnemyAvoided(isNarrowDodge);
        if (isNarrowDodge)
        {
            //Play random dodge audio clip on succesful narrow dodge
            int clipNum = Random.Range(0, narrowDodgeClips.Length);
            audioSource.clip = narrowDodgeClips[clipNum];
            audioSource.Play();
        }
    }

    //Plays a sound clip when dodging
    public void NarrowDodge()
    {
        int clipNum = Random.Range(0, narrowDodgeClips.Length);
        audioSource.clip = narrowDodgeClips[clipNum];
        audioSource.Play();
    }

    //Reduces player health, changing their color and causing a sound.
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
                playerGraphicControl.SetGraphicColor(Color.Lerp(
                    lerpColorNoHealth, lerpColorMaxHealth, 0));

                //Make death SFX
                playerDied.Raise();
            }
            else
            {
                //Play random dodge audio clip on succesful narrow dodge
                int clipNum = Random.Range(0, narrowDodgeClips.Length);
                audioSource.clip = healthLossClips[clipNum];
                audioSource.Play();

                StartCoroutine(iFrames());
                //Go down a color
                playerGraphicControl.SetGraphicColor(Color.Lerp(
                    lerpColorNoHealth, lerpColorMaxHealth, (health / maxHealth)));

                //Make a hurt sound

            }//endelse
        }//endif
    }

    //Timer for Invincibility Frames
    IEnumerator iFrames()
    {
        invincibilityFramesOn = true;
        playerGraphicControl.ToggleSquint();
        for (float i = 0; i < iFrameDuration; i += iFrameDeltaTime)
        {
            playerGraphicControl.ToggleRendererVisibility();
            yield return new WaitForSeconds(iFrameDeltaTime);
        }
        playerGraphicControl.ToggleRendererVisibility(true);
        playerGraphicControl.ToggleOpen();
        invincibilityFramesOn = false;
    }

}//End Player
