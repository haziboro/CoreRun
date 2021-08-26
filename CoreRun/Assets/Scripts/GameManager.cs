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
    [SerializeField] int dodgeBonus = 2;//Multiplier for narrow dodges
    [SerializeField] int layerInterval = 20;//Number of seconds between layer transitions

    public bool gamePaused;
    public bool gameRunning;
    public bool transitioningLayer; //true when the layer is increasing

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

        if (transitioningLayer)
        {
            if(spawner.spawningLayerEnd == false)
            {
                IncreaseLayer();
                StartCoroutine(LayerCountdown());
            }
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
        GameObject.Find("SceneDataTransfer").
            GetComponent<SceneDataTransfer>().CheckHighScore(score);
        SceneManager.LoadScene(0);
    }

    //Starts the game
    void StartGame()
    {
        gameRunning = true;
        transitioningLayer = false;
        score = 0;
        layer = 1;

        //Load in Menu data
        SceneDataTransfer sceneData = GameObject.Find("SceneDataTransfer").
            GetComponent<SceneDataTransfer>();
        //Update sound volume from loaded data
        soundControl.ChangeBackgroundMusicVolume(sceneData.backgroundMusicVolume);
        soundControl.ChangeEffectVolume(sceneData.soundEffectsVolume);

        StartCoroutine(LayerCountdown());
    }

    //Toggles pause feature during gameplay
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
        if (narrowDodge) { score += 1 * layer * dodgeBonus; }
        else { score += 1 * layer; }
        ui.UpdateScoreUI(score);
    }

    //Increases the current layer, updating other scripts
    public void IncreaseLayer()
    {
        layer++;
        planet.IncreaseSpeed();
        spawner.IncreaseRate();
        //Layer increase notification
        //animation
        //animation
        //Layer increase notification
        ui.UpdateLayerUI(layer);
        transitioningLayer = false;
    }

    //Initiate layer end after layerInterval seconds
    IEnumerator LayerCountdown()
    {
        yield return new WaitForSeconds(layerInterval);
        //Tell Spawner to spawn the end of layer object
        transitioningLayer = true;
        spawner.spawningLayerEnd = true;
    }
}
