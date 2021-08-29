using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioSource;
    private UIManager ui;
    private playerGraphicController playerGraphic;//Defined here
    private float currentScalePercent;//scale the player currently has out of 100
    private float horizontalInput;
    private bool running = false;
    private float health;//Must be float to lerp colors properly
    private bool invincibilityFramesOn = false;
    [SerializeField]  private float shrinkPower = 100;//as a percent
    private bool shrinkPowerOnCooldown = false;

    [SerializeField] float shrinkPowerCooldownThreshold = 15;//The value that shrinkPower has to reach to go off cooldown
    [SerializeField] float shrinkPowerConsumptionSpeed = 1.0f;
    [SerializeField] float shrinkPowerRegenSpeed = 1.0f;
    [SerializeField] float currentSpeed;
    [SerializeField] float baseSpeed = 10;//left/right movement speed
    [SerializeField] float shrinkSpeed = 1f;//Rate at which shrinking occurs
    [SerializeField] float levelBoundaries = 3.4f;//distance from center
    [SerializeField] int maxHealth = 3;
    [SerializeField] float iFrameDuration = 1.5f;
    [SerializeField] float iFrameDeltaTime = 0.10f; //For gradual loss of iframes
    [SerializeField] Color lerpColorMaxHealth;
    [SerializeField] Color lerpColorNoHealth;
    [Range(0.0f, 1.0f)] [SerializeField] float minimumSize = 0.5f; //Smallest size allowed

    //SFX
    [SerializeField] AudioClip[] narrowDodgeClips;
    [SerializeField] AudioClip[] healthLossClips;

    // Start is called before the first frame update
    void Start()
    {
        //Load gameobjects
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        playerGraphic = new playerGraphicController(transform.Find("PlayerGraphic").gameObject);
        playerGraphic.setShrinkValues(shrinkSpeed, minimumSize);

        currentSpeed = baseSpeed;
        health = maxHealth;

        StartCoroutine(ShortPause(0.1f));
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
        transform.Translate(Vector3.right * horizontalInput * currentSpeed * Time.deltaTime);
        CheckBoundaries();
    }

    //Shrink player when 'Down' is pressed
    void Shrink()
    {
        if (Input.GetButton("Down") && !shrinkPowerOnCooldown)
        {
            //Reduces Shrink Power While holding Down
            shrinkPower -= shrinkPowerConsumptionSpeed * Time.deltaTime;
            if (shrinkPower <= 0)
            {
                shrinkPower = 0;
                ui.ShrinkBarOnCooldown(true);
                shrinkPowerOnCooldown = true;
            }
            //Redcues player graphics size if shrink is held, down to minimum
            if (playerGraphic.isBiggerThanMinimum())
            {
                playerGraphic.Condense(true);
            }//endif
        }//endif
        else
        {
            //Increase Shrink Power while down is not held
            if (shrinkPower < 100)
            {
                shrinkPower += shrinkPowerRegenSpeed * Time.deltaTime;
                shrinkPower = shrinkPower > 100 ? 100 : shrinkPower;
                if (shrinkPowerOnCooldown && shrinkPower >= shrinkPowerCooldownThreshold)
                {
                    shrinkPowerOnCooldown = false;
                    ui.ShrinkBarOnCooldown(false);
                }//endif
            }//endif
            //Increases the player graphics size if it needs to grow
            if (playerGraphic.isSmallerThanOriginal())
            {
                playerGraphic.Condense(false);
            }//endif
        }//endelse

        //Change UI localScale.x to the same as shrink power
        ui.ModifyShrinkBar(shrinkPower / 100);

        //Preserve current scale as percentage of maximum
        currentScalePercent = playerGraphic.playerGraphic.transform.localScale.x / playerGraphic.playerScale.x;
        //Change speed proportionate to size change
        currentSpeed = baseSpeed * currentScalePercent;
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
                playerGraphic.SetGraphicColor(Color.Lerp(
                    lerpColorNoHealth, lerpColorMaxHealth, 0));

                //Make death SFX

                gameManager.PlayerDied();
            }
            else
            {
                //Play random dodge audio clip on succesful narrow dodge
                int clipNum = Random.Range(0, narrowDodgeClips.Length);
                audioSource.clip = healthLossClips[clipNum];
                audioSource.Play();

                StartCoroutine(iFrames());
                //Go down a color
                playerGraphic.SetGraphicColor(Color.Lerp(
                    lerpColorNoHealth, lerpColorMaxHealth, (health / maxHealth)));

                //Make a hurt sound

            }//endelse
        }//endif
    }

    //Timer for Invincibility Frames
    IEnumerator iFrames()
    {
        invincibilityFramesOn = true;
        playerGraphic.ToggleSquint();
        for (float i = 0; i < iFrameDuration; i += iFrameDeltaTime)
        {
            playerGraphic.ToggleRendererVisibility();
            yield return new WaitForSeconds(iFrameDeltaTime);
        }
        playerGraphic.ToggleRendererVisibility(true);
        playerGraphic.ToggleOpen();
        invincibilityFramesOn = false;
    }

    //To delay player Start
    IEnumerator ShortPause(float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        running = true;
    }
}//End Player



//Holds reference to player graphics object and allows for its' manipulation
public class playerGraphicController
{
    public GameObject playerGraphic { get; private set; }
    private Renderer[] playerGraphicRenderers;
    private GameObject openEyes;
    private GameObject happyEyes;
    private GameObject squintedEyes;

    public Vector3 playerScale { get; private set; }
    private Vector3 shrinkScale;
    private float minimumSize;

    public playerGraphicController(GameObject graphic)
    {
        playerGraphic = graphic;
        playerGraphicRenderers = playerGraphic.GetComponentsInChildren<Renderer>();
        playerScale = playerGraphic.transform.localScale;

        GameObject eyes = playerGraphic.transform.Find("Eyes").gameObject;
        openEyes = eyes.transform.Find("OpenEyes").gameObject;
        happyEyes = eyes.transform.Find("ClosedEyesHappy").gameObject;
        squintedEyes = eyes.transform.Find("SquintEyes").gameObject;
    }

    //Takes shrinkspeed parameter and stores as a vector and defines minimum shrink size
    public void setShrinkValues(float shrinkSpeed, float minSize)
    {
        shrinkScale = new Vector3(-shrinkSpeed, -shrinkSpeed, -shrinkSpeed);
        minimumSize = minSize;
    }

    //Changes the graphic's color
    public void SetGraphicColor(Color color)
    {
        playerGraphic.GetComponent<Renderer>().material.SetColor(
                    "_Color", color);
    }

    //Toggles renderer visibility from visible to non visible and vice versa
    public void ToggleRendererVisibility()
    {
        foreach (Renderer renderer in playerGraphicRenderers)
        {
            bool vis = !renderer.isVisible;
            renderer.enabled = vis;
            if (vis){ ToggleAll(); }
            else{ ToggleSquint(); }
        }
    }

    //ToggleRendererVisibility overload, sets visibility to the given parameter
    public void ToggleRendererVisibility(bool givenVisibility)
    {
        foreach (Renderer renderer in playerGraphicRenderers)
        {
            renderer.enabled = givenVisibility;
            //currentEyes.SetActive(givenVisibility);
        }
    }

    //Change Size
    public void Condense(bool shrinking)
    {
        if (shrinking)
        {
            playerGraphic.transform.localScale += shrinkScale * Time.deltaTime;
            playerGraphic.transform.Translate(0.0f,
                shrinkScale.x / 2 * Time.deltaTime, 0.0f);
            ToggleSquint();
        }
        else
        {
            playerGraphic.transform.localScale -= shrinkScale * Time.deltaTime;
                playerGraphic.transform.Translate(0.0f,
                    -shrinkScale.x / 2 * Time.deltaTime, 0.0f);
            ToggleOpen();
        }
    }

    //Returns true if the graphic is bigger than its' minimum size, false otherwise
    public bool isBiggerThanMinimum()
    {
        return playerGraphic.transform.localScale.x > playerScale.x * minimumSize;
    }

    //Returns true if the graphic is smaller than its' original size, false otherwise
    public bool isSmallerThanOriginal()
    {
        return playerGraphic.transform.localScale.x < playerScale.x;
    }

    //Toggles which set of eyes are visible
    private void ToggleAll()
    {
        squintedEyes.SetActive(false);
        openEyes.SetActive(false);
        happyEyes.SetActive(false);
    }
    public void ToggleSquint(bool visible = true)
    {
        squintedEyes.SetActive(visible);
        openEyes.SetActive(false);
        happyEyes.SetActive(false);
    }
    public void ToggleHappy(bool visible = true)
    {
        squintedEyes.SetActive(false);
        openEyes.SetActive(false);
        happyEyes.SetActive(visible);
    }
    public void ToggleOpen(bool visible = true)
    {
        squintedEyes.SetActive(false);
        openEyes.SetActive(visible);
        happyEyes.SetActive(false);
    }


}//End playerGraphicController
