using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//gives 50% chance for animation to reverse
public class ReverseAnimation : MonoBehaviour
{
    [SerializeField] bool reverseAnimation;
    Animator enemyAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        if (reverseAnimation)
        {
            CheckForReversal();
        }
    }

    //Randomly determine if the animation will be reversed
    private void CheckForReversal()
    {
        if (Random.Range(0, 2) == 0)
        {
            enemyAnimator.SetTrigger("ReverseAnim");
        }
    }
}
