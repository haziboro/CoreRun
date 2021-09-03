using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorizeText : MonoBehaviour
{
    private TextMeshProUGUI textObject;
    private string word;
    private string end = "</color>";
    private string newLetter = "";
    private string finalWord = "";
    private List<string> letterColors;
    private int counter = 0;

    [SerializeField] GameObject titleTextObj;
    [SerializeField] float flickerDuration;
    [SerializeField] int numFlickers;

    // Start is called before the first frame update
    void Start()
    {
        textObject = titleTextObj.GetComponent<TextMeshProUGUI>();
        word = textObject.text.ToString();
        letterColors = new List<string>();
        AddColorsToList();
    }

    //Adds colors to list to change letters to
    void AddColorsToList()
    {
        letterColors.Add("<color=red>");
        letterColors.Add("<color=blue>");
        letterColors.Add("<color=yellow>");
        letterColors.Add("<color=green>");
        letterColors.Add("<color=orange>");
        letterColors.Add("<color=purple>");
    }

    //Colors a word
    void AddColor()
    {
        for (int i = 0; i < word.Length; i++)
        {
            newLetter = letterColors[Random.Range(0, letterColors.Count)] + word[i] + end;
            finalWord += newLetter;
        }
    }

    //Removes word color
    void RemoveColor()
    {
        for (int i = 0; i < word.Length; i++)
        {
            newLetter = "<color=white>" + word[i] + end;
            finalWord += newLetter;
        }
        textObject.text = textObject.text.Replace(textObject.text.ToString(),
            finalWord);
        finalWord = "";
    }

    //Colors Text Randomly
    public void ColorText()
    {
        AddColor();
        textObject.text = textObject.text.Replace(textObject.text.ToString(),
            finalWord);
        finalWord = "";
    }

    //Change letter color multiple times, then return to normal
    public void FlashColorText()
    {
        StartCoroutine("ColorChangeTimer");
    }

    //Timer for FlashColorText
    IEnumerator ColorChangeTimer()
    {

        AddColor();
        yield return new WaitForSeconds(flickerDuration);
        textObject.text = textObject.text.Replace(textObject.text.ToString(),
            finalWord);

        counter++;
        finalWord = "";

        if (counter <= numFlickers)
        { FlashColorText(); }
        else
        {
            counter = 0;
            RemoveColor();
        }

    }
}
