using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public GameObject[] pupils;
    [SerializeField] float pupilMovementRadius = 0.15f;
    [SerializeField] bool controlling;
    [SerializeField] float maxStrafeStretchSize = 0.2f;
    [SerializeField] float StrafeStretchGrowthSpeed = 1;//How quickly the attached changes size
    [SerializeField] float StrafeStretchBounceBack = 2;//How quickly the attached restores its' original size

    private float horizontalInput;

    
    private float cumulativeGrowth;
    private float growthIncrease;//Represents the current amount of growth strafing has caused relative to normal size

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controlling)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            MoveEyes();
            StrafeStretch();
        }
    }

    //Moves the attached eyes in response to movement
    void MoveEyes()
    {
        
        foreach (GameObject pupil in pupils)
        {
            pupil.transform.Translate(Vector3.right * horizontalInput * Time.deltaTime);

            // Check for left and right bounds, on the Z axis here
            if (pupil.transform.localPosition.x < -pupilMovementRadius)
            {
                pupil.transform.localPosition = new Vector3(-pupilMovementRadius, transform.localPosition.y, transform.localPosition.z);
            }
            if (pupil.transform.localPosition.x > pupilMovementRadius)
            {
                pupil.transform.localPosition = new Vector3(pupilMovementRadius, transform.localPosition.y, transform.localPosition.z);
            }//endif
        }//endforeach
    }

    //Makes the character stretch a little when he moves left or right
    void StrafeStretch()
    {
        //If moving horizontally 
        if (Input.GetButton("Horizontal"))
        {
            //If the shape is smaller than its' max size
            if (cumulativeGrowth < maxStrafeStretchSize)
            {
                //Grow it in size proportional to horizontal input, capped at its' max size
                //Generate a growth value
                growthIncrease = Mathf.Abs(horizontalInput) * Time.deltaTime * StrafeStretchGrowthSpeed;
                //add to cumulative growth, capped at max growth
                //If the growth increase goes beyond the maximum value, only increase growth up to the max value
                if(cumulativeGrowth + growthIncrease > maxStrafeStretchSize)
                {
                    growthIncrease = maxStrafeStretchSize - cumulativeGrowth;
                    cumulativeGrowth = maxStrafeStretchSize;
                }
                else
                {
                    cumulativeGrowth += growthIncrease;
                }
                //Add growthIncrease to x.localscale, capped at max growth
                transform.localScale = new Vector3(
                    transform.localScale.x + growthIncrease,
                    transform.localScale.y,transform.localScale.z);
            }
        }
        //When not moving shrink until all accumulated increase is lost.
        else
        {
            if (cumulativeGrowth > 0)
            {
                //Generate a growth value, 
                growthIncrease = Time.deltaTime * StrafeStretchGrowthSpeed * StrafeStretchBounceBack;
                //Subtract from cumulative growth, with a floor of 0

                if(cumulativeGrowth - growthIncrease < 0)
                {
                    growthIncrease = cumulativeGrowth;
                    cumulativeGrowth = 0;
                }
                else
                {
                    cumulativeGrowth -= growthIncrease;
                }
                //Subtract growthIncrease from x.localScale, with a floor of 1 (1 being the assumed normal scale)
                transform.localScale = new Vector3(
                    transform.localScale.x - growthIncrease,//Assuming normal scale is 1 here, max doesn't matter
                    transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
