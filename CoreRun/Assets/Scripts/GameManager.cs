using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScriptableBool gamePaused;
    [SerializeField] GameEvent startLayer;
    [SerializeField] GameEvent pause;
    [SerializeField] GameEvent unpause;

    //Start is called before the first frame update
    void Start()
    {
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
        if (gamePaused.active)//unpause to prevent the game from starting paused
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
        startLayer.Raise();
    }

    //Toggles pause feature during gameplay
    public void PauseToggle()
    {
        gamePaused.active = !gamePaused.active;
        if (gamePaused.active)
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

    //Ends the game
    public void PlayerDied()
    {
        Debug.Log("The player has died");
        GameOver();
    }
}
