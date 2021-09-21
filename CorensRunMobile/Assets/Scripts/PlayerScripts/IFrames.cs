using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Triggers iFrames, strongly relies on playerGraphicController
public class IFrames : MonoBehaviour
{
    private PlayerGraphicsController playerGraphicControl;

    [SerializeField] ScriptableBool iFramesObj;
    [SerializeField] float iFrameDuration = 1.5f;
    [SerializeField] float iFrameDeltaTime = 0.10f;


    // Start is called before the first frame update
    void Start()
    {
        playerGraphicControl = GetComponentInChildren<PlayerGraphicsController>();
    }

    //Activates IFrame co-routine
    public void ActivateIFrames()
    {
        StartCoroutine(iFrames());
    }

    //Timer for Invincibility Frames
    IEnumerator iFrames()
    {
        iFramesObj.active = true;
        playerGraphicControl.ToggleSquint();
        for (float i = 0; i < iFrameDuration; i += iFrameDeltaTime)
        {
            playerGraphicControl.ToggleRendererVisibility();
            yield return new WaitForSeconds(iFrameDeltaTime);
        }
        playerGraphicControl.ToggleRendererVisibility(true);
        playerGraphicControl.ToggleOpen();
        iFramesObj.active = false;
    }
}
