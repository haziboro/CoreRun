using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI layerText;
    // Start is called before the first frame update
    private void Start()
    {
        scoreText = transform.Find("ScoreBoard")
            .GetComponentInChildren<TextMeshProUGUI>();
        layerText = transform.Find("LayerBoard")
            .GetComponentInChildren<TextMeshProUGUI>();
    }

    //Display value of score on interface
    public void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }

    //displays value of layer on interface
    public void UpdateLayerUI(int layer)
    {
        layerText.text = layer.ToString();
    }
}
