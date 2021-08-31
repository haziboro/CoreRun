using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] SoundControl soundControlObj;
    [SerializeField] GameObject player;
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScoreAndLayer score;
    [SerializeField] GameEvent startLayer;

    //private UIManager ui;
    private SoundControl soundControl;

    [SerializeField] GameEvent pause;
    [SerializeField] GameEvent unpause;

    public bool gamePaused;

    //Start is called before the first frame update
    void Start()
    {
        soundControl = soundControlObj.GetComponent<SoundControl>();

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameRunning.active)
        {
            PauseToggle();
        }
    }

    //Ends the game
    public void GameOver()
    {
        if (gamePaused)//unpause to prevent the game from starting paused
        {
            PauseToggle();
        }
        gameRunning.active = false;
        SceneManager.LoadScene(0);
    }

    //Starts the game
    void StartGame()
    {
        gameRunning.active = true;

        //Load in Menu data
        SceneDataTransfer sceneData = GameObject.Find("SceneDataTransfer").
            GetComponent<SceneDataTransfer>();

        //Update sound volume from loaded data
        soundControl.ChangeBackgroundMusicVolume(sceneData.backgroundMusicVolume);
        soundControl.ChangeEffectVolume(sceneData.soundEffectsVolume);

        //StartCoroutine(LayerCountdown());
        startLayer.Raise();

    }

    //Toggles pause feature during gameplay
    public void PauseToggle()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            pause.Raise();
            Time.timeScale = 0.0f;
        }
        else
        {
            unpause.Raise();
            Time.timeScale = 1.0f;
        }//endelse
    }//end PauseToggle

    //Called by player, ends the game when player has died
    public void PlayerDied()
    {
        Debug.Log("The player has died");
        GameOver();
    }
}
