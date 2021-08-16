using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private PlanetControl planet;
    private UIManager ui;
    private int layer = 1;
    private int score = 0;
    private int multiplier = 0;

    // Start is called before the first frame update
    void Start()
    {
        planet = GameObject.Find("Earth").GetComponent<PlanetControl>();
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Ends the game
    public void GameOver()
    {
        planet.StopMoving();
    }

    //Increases the planet's speed
    public void IncreasePlanetSpeed()
    {
        planet.IncreaseSpeed();
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
