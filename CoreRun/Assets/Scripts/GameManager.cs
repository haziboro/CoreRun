using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlanetControl planet;
    private UIManager ui;
    private SpawnManager spawner;
    private SoundControl soundControl;
    private GameObject player;

    [SerializeField] int layer;
    [SerializeField] int score;
    [SerializeField] int multiplier;
    [SerializeField] int maxMultiplier = 3;
    [SerializeField] int layerInterval = 20;//Number of seconds between layer transitions

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
        
    }

    //Ends the game
    private void GameOver()
    {
        gameRunning = false;
        soundControl.StopMusic();
        Debug.Log("The Game is Over");
    }

    //Starts the game
    void StartGame()
    {
        gameRunning = true;
        score = 0;
        layer = 1;
        multiplier = 1;
        soundControl.StartMusic();
        StartCoroutine(LayerCountdown());
    }

    //Called when an enemy is avoided. Modifies multiplier based on narrow dodge
    public void EnemyAvoided(bool narrowDodge)
    {
        if (narrowDodge)
        {
            if(multiplier < maxMultiplier)
            {
                multiplier++;
            }
            ui.NarrowDodgePopup(player.transform.position);
        }
        else
        {
            if(multiplier > 1)
            {
                multiplier--;
            }
        }
        UpdateScore();
    }

    //Called by player, ends the game when player has died
    public void PlayerDied()
    {
        Debug.Log("The player has died");
        GameOver();
    }

    //Updates score and request the UI to change display
    void UpdateScore()
    {
        score += 1 * layer;
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
