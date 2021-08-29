using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(1000)]
public class UIManager : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject player;
    private GameObject pauseMenu;
    private GameObject shrinkBar;
    private Image shrinkBarImage;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI layerText;
    private bool onCooldown;

    [SerializeField] int dodgePopupDuration = 2;
    [SerializeField] int randomPopupRange = 30;//Bounds of popups random location spawn
    [SerializeField] int downOffset = 50;//How far down from player to spawn popup
    [SerializeField] Color PopupColorOne = Color.yellow;
    [SerializeField] Color PopupColorTwo = Color.red;
    [SerializeField] GameObject dodgePopup;
    [SerializeField] Color shrinkPowerBarHigh = Color.green;
    [SerializeField] Color shrinkPowerBarMedium = Color.yellow;
    [SerializeField] Color shrinkPowerBarLow = Color.red;
    [SerializeField] Color shrinkPowerBarOnCooldown = Color.black;


    private bool dodgePopupActive = false;
    private Color lerpedColor;
    private Vector3 shrinkBarScale;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("PlayerCube");
        shrinkBar = GameObject.Find("ShrinkBar");
        shrinkBarImage = shrinkBar.GetComponent<Image>();
        pauseMenu = transform.Find("PauseMenu").gameObject;
        scoreText = transform.Find("ScoreBoard")
            .GetComponentInChildren<TextMeshProUGUI>();
        layerText = transform.Find("LayerBoard")
            .GetComponentInChildren<TextMeshProUGUI>();

        //For flashing dodge popup
        lerpedColor = PopupColorOne;

        //Initialize shrinkBar
        shrinkBarScale = new Vector3(shrinkBar.transform.localScale.x,
            shrinkBar.transform.localScale.y,
            shrinkBar.transform.localScale.z);
        shrinkBar.GetComponent<Image>().color = shrinkPowerBarHigh;
    }

    // Update is called once per frame
    private void Update()
    {
        PopupAnimation();
    }

    //Display value of score on interface
    public void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }

    //displays value of layer on interface
    public void UpdateLayerUI(int layer)
    {
        layerText.text = layer.ToString();
    }

    //Makes a popup appear at playerPos
    public void NarrowDodgePopup(Vector3 playerPos)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(playerPos);
        //Randomize the popup position in an area around the player
        screenPos.x += Random.Range(-randomPopupRange, randomPopupRange);
        screenPos.y += Random.Range(-randomPopupRange, 0) - downOffset;
        dodgePopup.transform.position = screenPos;
        StartCoroutine(PopUpTimer(screenPos));
    }

    //Changes scale of shrink bar and its' color according to parameter
    public void ModifyShrinkBar(float shrinkBarXScale)
    {
        shrinkBarScale.x = shrinkBarXScale;
        shrinkBar.transform.localScale = shrinkBarScale;

        ChangePowerBarColor(shrinkBarXScale);
    }

    //Changes shrink power bar's color based on given Input
    public void ChangePowerBarColor(float barValue)
    {
        if (!onCooldown)
        {
            if (barValue < 0.3f)
            {
                shrinkBarImage.color = shrinkPowerBarLow;
            }
            else if (barValue < 0.67f)
            {
                shrinkBarImage.color = shrinkPowerBarMedium;
            }
            else
            {
                shrinkBarImage.color = shrinkPowerBarHigh;
            }//endelse
        }//endif
        else
        {
            shrinkBarImage.color = shrinkPowerBarOnCooldown;
        }//endelse
    }

    //Changes shrink bars color to its' cooldown color
    public void ShrinkBarOnCooldown(bool isOnCooldown)
    {
        onCooldown = isOnCooldown;
        if (isOnCooldown)
        {
            shrinkBarImage.color = shrinkPowerBarOnCooldown;
        }
    }

    //Displays the pause menu
    public void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    //Turns the popup on and initiates color changing
    IEnumerator PopUpTimer(Vector3 screenPos)
    {
        dodgePopup.SetActive(true);
        dodgePopupActive = true;
        yield return new WaitForSeconds(dodgePopupDuration);
        dodgePopup.SetActive(false);
        dodgePopupActive = false;
    }

    //Makes the popup flash between two colors
    void PopupAnimation()
    {
        if (dodgePopupActive)
        {
            lerpedColor = Color.Lerp(PopupColorOne, PopupColorTwo, Mathf.PingPong(Time.time, 1));
            dodgePopup.GetComponent<TextMeshProUGUI>().color = lerpedColor;
        }
    }
}
