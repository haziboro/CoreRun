using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1000)]
public class DisplayHighScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    [SerializeField] GameObject scoreTextObj;
    [SerializeField] HighScore savedScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();

        UpdateHighScoreDisplay();
    }

    //Updates the High Score Display
    void UpdateHighScoreDisplay()
    {
        scoreText.text = savedScore.highScore.ToString();
    }

}
