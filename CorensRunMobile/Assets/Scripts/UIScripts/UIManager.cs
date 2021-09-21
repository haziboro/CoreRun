using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1000)]
public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI layerText;
    private TextMeshProUGUI highScoreText;

    [SerializeField] GameObject scoreTextObj;
    [SerializeField] GameObject layerTextObj;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] ScoreAndLayer score;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
        layerText = layerTextObj.GetComponent<TextMeshProUGUI>();
    }

    //Display value of score on interface
    public void UpdateScoreUI()
    {
        scoreText.text = score.score.ToString();
    }

    //displays value of layer on interface
    public void UpdateLayerUI()
    {
        layerText.text = score.layer.ToString();
    }

    //Displays the pause menu
    public void TogglePauseMenu(bool paused)
    {
        pauseMenu.SetActive(paused);
    }

    //ActivateGameOverMenu
    public void ActivateGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
}
