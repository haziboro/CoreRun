using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

//Controls the game's main menu interface
[DefaultExecutionOrder(1000)]
public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI scoreText;

    [SerializeField] HighScore savedScore;
    [SerializeField] VolumeSettings setting;
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject settings;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("GameTitle").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();

        ColorTitle();
        UpdateHighScoreDisplay();
        InitializeSliders();
        gameRunning.active = true;
    }

    //Changes the title's individual letter colors
    void ColorTitle()
    {
        titleText.text = titleText.text.Replace(titleText.text.ToString(),
            "<color=red>" + 'C' + "</color>" +
            "<color=blue>" + 'o' + "</color>" +
            "<color=yellow>" + 'r' + "</color>" +
            "<color=green>" + 'e' + "</color>" +
            "<color=orange>" + 'n' + "</color>" +
            "<color=purple>" + '\'' + "</color>" +
            "<color=red>" + 's' + "</color>" +
            "<color=blue>" + ' ' + "</color>" +
            "<color=yellow>" + 'R' + "</color>" +
            "<color=green>" + 'u' + "</color>" +
            "<color=orange>" + 'n' + "</color>"
            );
    }

    //Updates the High Score Display
    void UpdateHighScoreDisplay()
    {
        scoreText.text = savedScore.highScore.ToString();
    }

    //Set's sliders initial values to saved values
    void InitializeSliders()
    {
        musicVolumeSlider.value = setting.backgroundMusicVolume;
        SFXVolumeSlider.value = setting.SFXVolume;
    }

    //Opens the Settings and closes the menu
    public void ToggleSettings()
    {
        menu.SetActive(!menu.activeSelf);
        settings.SetActive(!settings.activeSelf);
    }

    //Starts the game, loading saved values into scene data
    public void StartNewGame()
    {
        gameRunning.active = false;
        SceneManager.LoadScene(1);
    }

    //Quits the application
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
