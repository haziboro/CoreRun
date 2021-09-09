using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlyingText : MonoBehaviour
{
    private TextMeshProUGUI flyingText;
    private float textWidth;
    private Vector3 textStartingPos;

    [SerializeField] List<string> words;
    [SerializeField] float timeOnScreen; //Number of seconds to move text across screen

    // Start is called before the first frame update
    void Awake()
    {
        flyingText = GetComponent<TextMeshProUGUI>();
        textStartingPos = transform.position;
    }

    //Sends the text flying across the screen
    public void FlyTextAcrossScreen()
    {
        transform.position = textStartingPos;
        flyingText.text = words[Random.Range(0,words.Count)];

        //Move to points just beyond the screen
        Vector3 endPos = textStartingPos;
        endPos.x += Screen.width +
            flyingText.GetComponent<RectTransform>().sizeDelta.x;
        StartCoroutine(FlyTimer(endPos));
    }

    //timer for flying text
    IEnumerator FlyTimer(Vector3 endPosition)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime / timeOnScreen;
            transform.position = Vector3.Lerp(textStartingPos, endPosition, t);
            yield return null; 
        }
    }

}
