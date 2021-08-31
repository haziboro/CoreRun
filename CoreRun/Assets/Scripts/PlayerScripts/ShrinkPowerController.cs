using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkPowerController : MonoBehaviour
{
    private bool shrinkPowerOnCooldown = false;
    private float shrinkPower;//amount out of 100
    private UIManager ui;
    private PlayerGraphicsController playerGraphicControl;
    private bool ButtonDown;//Checks if down is held
    public Vector3 playerScale { get; private set; }
    private Vector3 shrinkScale;

    [SerializeField] GameObject playerGraphic;
    [SerializeField] GameObject uiObject;
    [SerializeField] float shrinkPowerCooldownThreshold = 15;//The value that shrinkPower has to reach to go off cooldown
    [SerializeField] float shrinkPowerConsumptionSpeed = 1.0f;
    [SerializeField] float shrinkPowerRegenSpeed = 1.0f;
    [SerializeField] float shrinkSpeed = 1f;//Rate at which shrinking occurs
    [Range(0.0f, 1.0f)] [SerializeField] float minimumSize = 0.5f; //Smallest size allowed

    // Start is called before the first frame update
    void Start()
    {
        //Load Objects
        ui = uiObject.GetComponent<UIManager>();
        playerGraphicControl = playerGraphic.GetComponent<PlayerGraphicsController>();

        //Set Constants
        shrinkPower = 100;
        shrinkScale = new Vector3(-shrinkSpeed, -shrinkSpeed, -shrinkSpeed);
        playerScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonDown = Input.GetButton("Down");
    }

    private void FixedUpdate()
    {
        Shrink();
    }

    //Shrink player when 'Down' is pressed
    void Shrink()
    {
        if (ButtonDown && !shrinkPowerOnCooldown)
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
            if (isBiggerThanMinimum())
            {
                Condense(true);
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
            if (isSmallerThanOriginal())
            {
                Condense(false);
            }//endif
        }//endelse

        //Change UI localScale.x to the same as shrink power
        ui.ModifyShrinkBar(shrinkPower / 100);
    }

    //Change Size
    public void Condense(bool shrinking)
    {
        if (shrinking)
        {
            transform.localScale += shrinkScale * Time.deltaTime;
            transform.Translate(0.0f,
                shrinkScale.x / 2 * Time.deltaTime, 0.0f);
            playerGraphicControl.ToggleSquint();
        }
        else
        {
            transform.localScale -= shrinkScale * Time.deltaTime;
            transform.Translate(0.0f,
                -shrinkScale.x / 2 * Time.deltaTime, 0.0f);
            playerGraphicControl.ToggleOpen();
        }
    }

    //Returns true if the graphic is bigger than its' minimum size, false otherwise
    public bool isBiggerThanMinimum()
    {
        return transform.localScale.x > playerScale.x * minimumSize;
    }

    //Returns true if the graphic is smaller than its' original size, false otherwise
    public bool isSmallerThanOriginal()
    {
        return transform.localScale.x < playerScale.x;
    }
}
