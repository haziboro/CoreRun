using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Must attach to object that contains the animator
public class AnimatorController : MonoBehaviour
{
    [System.NonSerialized]
    public Animator enemyAnimator;
    [System.NonSerialized]
    public AnimatorStateInfo state;

    [SerializeField] bool randomizeAnimation;
    [SerializeField] bool reverseAnimation;

    // Start is called before the first frame update
    void Awake()
    {
        enemyAnimator = GetComponent<Animator>();
        state = enemyAnimator.GetCurrentAnimatorStateInfo(0);

        if (randomizeAnimation)
        {
            RandomizeAnimation();
        }
    }

    //randomize animation to prevent synchronization with duplicates
    void RandomizeAnimation()
    {
        enemyAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
