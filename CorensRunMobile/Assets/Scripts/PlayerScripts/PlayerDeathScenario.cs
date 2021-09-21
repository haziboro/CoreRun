using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathScenario : MonoBehaviour
{

    private Animator playerAnimator;
    private float delayBeforeScenario;

    [SerializeField] GameObject playerHatCopy;
    [SerializeField] GameEvent animationComplete;
    [SerializeField] float gameOverDelay;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame

    //shrink to nothing
    public void AnimatePlayerDeath()
    {
        transform.localScale = Vector3.one;
        playerAnimator.SetTrigger("playerDeath");
        StartCoroutine(WaitAFrame());    
    }

    //Waits a frame so the death animation can trigger
    IEnumerator WaitAFrame()
    {
        yield return 0;
        float t = playerAnimator.GetCurrentAnimatorClipInfo(0).Length;
        StartCoroutine(WaitUntilClipFinished(t));
    }

    IEnumerator WaitUntilClipFinished(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        InstantiateHat();
    }

    //Instantiates the Game over hat at the play location right before they disappear
    private void InstantiateHat()
    {
        Instantiate(playerHatCopy, transform.position,transform.rotation);
        StartCoroutine(GameOverScreenDelay());
    }

    //Short delay before the game over screen is raised
    IEnumerator GameOverScreenDelay()
    {
        yield return new WaitForSeconds(gameOverDelay);
        gameObject.SetActive(false);
        animationComplete.Raise();
    }
}
