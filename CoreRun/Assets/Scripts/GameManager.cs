using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlanetControl planet;
    private UIManager ui;
    private SpawnManager spawner;
    private GameObject player;

    private int layer;
    private int score;
    private int multiplier;
    [SerializeField] int maxMultiplier = 3;

    public bool gameRunning;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Earth").GetComponent<PlanetControl>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
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
        Debug.Log("The Game is Over");
    }

    //Starts the game
    void StartGame()
    {
        gameRunning = true;
        score = 0;
        layer = 1;
        multiplier = 1;
    }

    //Increases the planet's speed
    public void IncreasePlanetSpeed()
    {
        planet.IncreaseSpeed();
    }

    //Called when an enemy is avoided. Modifies multiplier based on narrow dodge
    public void EnemyAvoided(bool narrowDodge)
    {
        if (narrowDodge)
        {
            /*Narrow Dodge notification animation
             */
            if(multiplier < maxMultiplier)
            {
                multiplier++;
            }
        }
        else
        {
            if(multiplier > 1)
            {
                multiplier--;
            }
        }
        CalculatePoints();
        UpdateScore();
    }

    //Called by player, ends the game when player has died
    public void PlayerDied()
    {
        Debug.Log("The player has died");
        GameOver();
    }

    //Calculate point rewards
    void CalculatePoints()
    {
        score += 1*layer;
    }

    //Requests an update for the current score on the interface
    void UpdateScore()
    {
        ui.UpdateScoreUI(score);
    }

    //Requests an update for the current layer on the interface
    void UpdateLayer()
    {
        ui.UpdateLayerUI(layer);
    }
}
