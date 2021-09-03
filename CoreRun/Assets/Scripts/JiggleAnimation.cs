using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adds idle animation to the attached object, scaling them repeatadly by within a constant margin
public class JiggleAnimation : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] float animationSpeed = 0.4f;//How quickly size will change
    [SerializeField] float animationIntensity = 0.1f;//How much size can change

    //Set to true for jiggling on that axis to be active
    [SerializeField] bool xScaleOn;
    [SerializeField] bool yScaleOn;
    [SerializeField] bool zScaleOn;

    //set to true to scale up, false to scale down
    [SerializeField] bool xScaleUp;
    [SerializeField] bool yScaleUp;
    [SerializeField] bool zScaleUp;

    private bool growing = true;
    private float sizeFactor = 0;//holds cumulative size change
    private float sizeDelta = 0;//Holds most recent size change value

    public bool animationOn = true;//true when animation is on

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameRunning.active)
        {
            MovementAnimation();
        }
    }

    //Makes the attached change scale repeatadly, dependent on the bools in animation type
    void MovementAnimation()
    {
        sizeDelta = Time.deltaTime * animationSpeed;
        if (growing)
        {
            sizeFactor += sizeDelta;
            if (sizeFactor >= animationIntensity)
            {
                growing = false;
            }
            UpdateScale(xScaleUp, yScaleUp, zScaleUp);
        }
        else
        {
            sizeFactor -= sizeDelta;
            if (sizeFactor <= 0)
            {
                growing = true;
            }
            UpdateScale(!xScaleUp, !yScaleUp, !zScaleUp);
        }
    }
 
    //Increments or decrement scale based on presets
    void UpdateScale(bool x, bool y, bool z)
    {
        //Debug.Log("X: " + x + " " + "Y: " + y + " " + "Z: " + z);
        transform.localScale = new Vector3(
            transform.localScale.x + CalculateSizeChange(xScaleOn, x),
            transform.localScale.y + CalculateSizeChange(yScaleOn, y),
            transform.localScale.z + CalculateSizeChange(zScaleOn, z)
            /*transform.localScale.x + (x ? 1 : -1) * sizeDelta,
            transform.localScale.y + (y ? 1 : -1) * sizeDelta,
            transform.localScale.z + (z ? 1 : -1) * sizeDelta*/);
    }

    //Calculate how the scale is modified for a single axis
    float CalculateSizeChange(bool isJiggling, bool jiggleType)
    {
        if (isJiggling)
        { return (jiggleType ? 1 : -1) * sizeDelta; }
        else
        { return 0; }
    }


    //Toggles animation
    public void ToggleAnimation()
    {
        animationOn = !animationOn;
    }

}
