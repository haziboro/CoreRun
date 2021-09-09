using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedModifier : MonoBehaviour
{

    [SerializeField] float baseAnimationSpeed;
    [SerializeField] ScriptableFloat animationSpeedMultiplier;
    [SerializeField] float percentIncreaseOnTransition;

    private void Awake()
    {
        animationSpeedMultiplier.value = baseAnimationSpeed;
    }

    //Increases animation speed multiplier
    public void IncreaseAnimationSpeed()
    {
        animationSpeedMultiplier.value += 
            baseAnimationSpeed * (percentIncreaseOnTransition / 100);
    }
}
