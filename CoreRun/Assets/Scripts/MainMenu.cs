using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MainMenu : MonoBehaviour
{
    private TextMeshProUGUI titleText;

    // Start is called before the first frame update
    void Start()
    {
        titleText = GameObject.Find("GameTitle").GetComponent<TextMeshProUGUI>();
        ColorTitle();
        
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

    //Opens the Settings
    public void OpenSettings()
    {
        Debug.Log("Setting Opened");
    }

    //Starts the game
    public void StartNewGame()
    {
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
