using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour
{
    [SerializeField] GameObject[] pupils;
    [SerializeField] float pupilMovementRadius = 0.15f;

    private float horizontalInput;

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEyes();
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
    }//end MoveEyes

}
