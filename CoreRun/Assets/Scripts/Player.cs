using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    private float horizontalInput;
    [SerializeField] float speed = 10;//left/right movement speed
    [SerializeField] float levelBoundaries = 3.4f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SideMovement();
    }

    //Move player left/right based on input
    void SideMovement()
    {
        if(gameManager.gameRunning == true)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            CheckBoundaries();
        }
    }

    //Establishes level boundaries
    private void CheckBoundaries()
    {
        // Check for left and right bounds, on the Z axis here
        if (transform.position.x < -levelBoundaries)
        {
            transform.position = new Vector3(-levelBoundaries, transform.position.y, transform.position.z);
        }
        if (transform.position.x > levelBoundaries)
        {
            transform.position = new Vector3(levelBoundaries, transform.position.y, transform.position.z);
        }
    }

    //Report to GameManager that an enemy has been sucessfully passed
    public void ReportEnemyAvoidance(bool isNarrowDodge)
    {
        gameManager.EnemyAvoided(isNarrowDodge);
    }

    //Reports to GameManager that the player has been hit
    public void ReportImpact()
    {
        gameManager.PlayerDied();
    }
}
