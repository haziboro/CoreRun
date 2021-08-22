using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMaker : MonoBehaviour
{
    public GameObject[] pupils;
    [SerializeField] float animationSpeed = 0.4f;//How quickly size will change
    [SerializeField] float animationIntensity = 0.1f;//How much size can change
    [SerializeField] float pupilMovementRadius = 0.15f;
    [SerializeField] bool controlling;

    private float horizontalInput;
    private bool growing = true;
    private float sizeFactor = 0;//holds cumulative size change
    private float sizeDelta = 0;//Holds most recent size change value

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementAnimation();
        if (controlling)
        {
            MoveEyes();
        }
    }

    //Makes the attached object grow and shrink by a set amount. Will look smoother the faster the PC.
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
            transform.localScale = new Vector3(
            transform.localScale.x - sizeDelta,
            transform.localScale.y + sizeDelta,
            transform.localScale.z - sizeDelta);
        }
        else
        {
            sizeFactor -= sizeDelta;
            if (sizeFactor <= 0)
            {
                growing = true;
            }
            transform.localScale = new Vector3(
            transform.localScale.x + sizeDelta,
            transform.localScale.y - sizeDelta,
            transform.localScale.z + sizeDelta);
        }
    }

    //Moves the attached eyes in response to movement
    void MoveEyes()
    {
        horizontalInput = Input.GetAxis("Horizontal");
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

}
