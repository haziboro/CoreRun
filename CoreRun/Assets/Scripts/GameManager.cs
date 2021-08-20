using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private PlanetControl planet;
    private UIManager ui;
    private SpawnManager spawner;
    private SoundControl soundControl;
    private GameObject player;

    [SerializeField] int layer;
    [SerializeField] int score;
    [SerializeField] int dodgeBonus;//Multiplier for narrow dodges
    [SerializeField] int layerInterval = 20;//Number of seconds between layer transitions

    private bool gamePaused;
    public bool gameRunning;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Earth").GetComponent<PlanetControl>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        soundControl = GameObject.Find("SoundControl").GetComponent<SoundControl>();
        player = GameObject.Find("PlayerCube");

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameRunning)
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
        gameRunning = false;
        planet.keepSpinning = false;
        soundControl.StopMusic();
        SceneManager.LoadScene(0);
    }

    //Starts the game
    void StartGame()
    {
        gameRunning = true;
        score = 0;
        layer = 1;
        soundControl.StartMusic();
        StartCoroutine(LayerCountdown());
    }

    //Pauses the game
    //Allows for pausing the game
    public void PauseToggle()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            Time.timeScale = 0.0f;
            soundControl.PauseMusic();
            planet.keepSpinning = false;
            ui.TogglePauseMenu(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            soundControl.PauseMusic(false);
            planet.keepSpinning = true;
            ui.TogglePauseMenu(false);
        }//endelse
    }//end PauseToggle

    //Called by player when an enemy is avoided
    public void EnemyAvoided(bool narrowDodge)
    {
        if (narrowDodge)
        {
            ui.NarrowDodgePopup(player.transform.position);
            UpdateScore(true);
        }
        else
        {
            UpdateScore(false);
        }
    }

    //Called by player, ends the game when player has died
    public void PlayerDied()
    {
        Debug.Log("The player has died");
        GameOver();
    }

    //Updates score and request the UI to change display
    void UpdateScore(bool narrowDodge)
    {
        score += 1 * layer;
        if (narrowDodge) { score *= dodgeBonus; }
        ui.UpdateScoreUI(score);
    }

    //Initaite layer increase after layerInterval seconds
    IEnumerator LayerCountdown()
    {
        while (gameRunning)
        {
            yield return new WaitForSeconds(layerInterval);
            layer++;
            planet.IncreaseSpeed();
            spawner.IncreaseRate();
            //Layer increase notification
            //animation
            //animation
            //Layer increase notification
            ui.UpdateLayerUI(layer);
        }
    }
}
