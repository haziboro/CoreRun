using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrowDodgePopup : MonoBehaviour
{
    private Vector3 dodgePopupStartPos;
    private Color lerpedColor;

    [SerializeField] GameObject dodgePopup;
    [SerializeField] int dodgePopupDuration = 2;
    [SerializeField] int randomPopupRange = 30;//Bounds of popups random location spawn
    [SerializeField] int upOffset = 50;//How far above bar to spawn popup
    [SerializeField] Color PopupColorOne = Color.yellow;
    [SerializeField] Color PopupColorTwo = Color.red;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize starting values
        lerpedColor = PopupColorOne;
        dodgePopupStartPos = dodgePopup.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        PopupAnimation();
    }

    //Makes a popup appear above the shrink bar
    public void ActivatePopup()
    {
        Vector3 screenPos = dodgePopupStartPos;
        //Randomize the popup position in an area
        screenPos.x += Random.Range(-randomPopupRange, randomPopupRange);
        screenPos.y += Random.Range(0, randomPopupRange) + upOffset;
        dodgePopup.transform.position = screenPos;
        StartCoroutine(PopUpTimer());
    }

    //Turns the popup on and initiates color changing
    IEnumerator PopUpTimer()
    {
        dodgePopup.SetActive(true);
        yield return new WaitForSeconds(dodgePopupDuration);
        dodgePopup.SetActive(false);
    }

    //Makes the popup flash between two colors
    void PopupAnimation()
    {
        if (dodgePopup.activeSelf)
        {
            lerpedColor = Color.Lerp(PopupColorOne, PopupColorTwo, Mathf.PingPong(Time.time, 1));
            dodgePopup.GetComponent<TextMeshProUGUI>().color = lerpedColor;
        }//endif
    }//end PopupAnimation
}
