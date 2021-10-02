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
    [SerializeField] GameEvent loadMainMenu;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && gamePaused.active == false)
        {
            PauseToggle();
        }
    }

    //Unpause and return to menu
    public void ReturnToMenu()
    {
        if (gamePaused.active)//unpause to prevent the game from starting paused
        {
            PauseToggle();
        }
        loadMainMenu.Raise();
    }

    //Stops running the game
    public void GameOver()
    {
        gameRunning.active = false;
    }

    //Starts the game
    public void StartGame()
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
            FreezeTime(true);
        }
        else
        {
            unpause.Raise();
            FreezeTime(false);
        }//endelse
    }//end PauseToggle

    public void FreezeTime(bool freeze)
    {
        if (freeze)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
