using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunAnimation : MonoBehaviour
{
    Animator sunAnimator;

    void Awake()
    {
        sunAnimator = GetComponent<Animator>();   
    }

    public void ActivateCelebrateAnimation()
    {
        sunAnimator.SetTrigger("Celebrate");
    }
}
