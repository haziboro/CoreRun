using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTrue : MonoBehaviour
{
    private Touch touch;

    [SerializeField] ScriptableBool shrinkButtonPressed;

    public void onPress()
    {
        if (Input.touchCount > 0)
        {
            shrinkButtonPressed.active = true;
        }
        else
        {
            shrinkButtonPressed.active = false;
        }
    }

    public void onRelease()
    {
        shrinkButtonPressed.active = false;
    }
   
}
