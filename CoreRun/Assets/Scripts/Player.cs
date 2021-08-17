using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    private float horizontalInput;
    [SerializeField] float speed = 10;//left/right movement speed

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
        }
    }

    //Report to GameManager that an enemy has been sucessfully passed
    public void ReportEnemyAvoidance()
    {
        gameManager.EnemyAvoided();
    }
}
