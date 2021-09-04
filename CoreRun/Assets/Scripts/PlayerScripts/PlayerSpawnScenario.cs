using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScenario : MonoBehaviour
{
    private Animator playerAnimator;

    [SerializeField] GameEvent startGame;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        float t = playerAnimator.GetCurrentAnimatorClipInfo(0).Length;
        StartCoroutine(WaitUntilClipFinished(t));
    }

    IEnumerator WaitUntilClipFinished(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        startGame.Raise();
    }
}
