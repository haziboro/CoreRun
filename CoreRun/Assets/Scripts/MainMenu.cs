using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI scoreText;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject soundControl;
    [SerializeField] GameObject settings;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundEffectsVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("GameTitle").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        AssignSliders();

        ColorTitle();
        UpdateHighScoreDisplay();
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
        scoreText.text = GameObject.Find("SceneDataTransfer").
            GetComponent<SceneDataTransfer>().highScore.ToString();
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
        //Save scene data
        SceneDataTransfer sceneData = GameObject.Find("SceneDataTransfer")
            .GetComponent<SceneDataTransfer>();
        sceneData.backgroundMusicVolume = soundControl.
            GetComponent<SoundControl>().backgroundMusicVolume;
        sceneData.soundEffectsVolume = soundControl.
            GetComponent<SoundControl>().soundEffectsVolume;

        SceneManager.LoadScene(1);
    }

    //Assigns sliders
    void AssignSliders()
    {
        SoundControl controller = soundControl.GetComponent<SoundControl>();
        musicVolumeSlider.onValueChanged.AddListener(
            delegate { controller.ChangeBackgroundMusicVolume(musicVolumeSlider.value); });
        soundEffectsVolumeSlider.onValueChanged.AddListener(
            delegate { controller.ChangeEffectVolume(soundEffectsVolumeSlider.value); });
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
