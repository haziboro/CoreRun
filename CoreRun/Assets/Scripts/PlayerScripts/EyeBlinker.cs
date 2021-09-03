using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinker : MonoBehaviour
{
    private bool eyesClosed = false;

    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] GameObject eyes;
    [SerializeField] int blinkFrequency = 10;//1 or 0 guarantees a blink every blinkCheckDelay, higher numbers make chance 1/blinkFrequency
    private float blinkCheckDelay = 0.2f;//Duration/Time delay between blink checks

    // Start is called before the first frame update
    void Start()
    {
        //Makes the character occasionally blink
        InvokeRepeating("randomBlink", 0.1f, blinkCheckDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Toggles the characters eye scale
    public void Blink()
    {
        if (!eyesClosed)
        {
            eyes.transform.localScale = new Vector3(
            eyes.transform.localScale.x,
            eyes.transform.localScale.y, 0);
            eyesClosed = true;
        }
        else
        {
            eyes.transform.localScale = new Vector3(
            eyes.transform.localScale.x,
            eyes.transform.localScale.y, 1);
            eyesClosed = false;
        }//endelse 
    }//endBlink

    //Makes the character randomly blink their eyes occasionally
    void randomBlink()
    {
        if (gameRunning.active)
        {
            if (Random.Range(0, blinkFrequency) == 0)
            {
                Blink();
                StartCoroutine(BlinkPause());
            }
        }
    }

    //The pause between blinks
    IEnumerator BlinkPause()
    {
        yield return new WaitForSeconds(blinkCheckDelay);
        Blink();
    }
}
