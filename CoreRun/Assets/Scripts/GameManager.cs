using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlanetControl planet;
    private UIManager ui;
    private SpawnManager spawner;

    private int layer = 1;
    private int score = 0;
    private int multiplier = 0;

    public bool gameRunning;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Earth").GetComponent<PlanetControl>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Ends the game
    private void GameOver()
    {
        planet.StopMoving();
        spawner.StopSpawns();
    }

    //Increases the planet's speed
    public void IncreasePlanetSpeed()
    {
        planet.IncreaseSpeed();
    }

    //Call when an enemy is avoided
    public void EnemyAvoided()
    {
        Debug.Log("GameManager receiving avoidance report");
    }

    //Updates the current score
    void UpdateScore()
    {
        ui.UpdateScoreUI(score);
    }

    //Updates the current layer
    void UpdateLayer()
    {
        ui.UpdateLayerUI(layer);
    }
}
