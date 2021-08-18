using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControl : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] int baseRotationSpeed = 10;
    [SerializeField] float currentRotationSpeed;
    [SerializeField] float rotationSpeedPercentIncrease;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentRotationSpeed = baseRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameRunning)
        {
            SpinSelf();
        }
    }

    //Rotates the Planet along the x-axis
    public void SpinSelf()
    {
        transform.Rotate(Vector3.left * currentRotationSpeed * Time.deltaTime);
    }

    //Increase the planets rotation speed by 'percent' its' base speed
    public void IncreaseSpeed()
    {
        currentRotationSpeed += baseRotationSpeed * (rotationSpeedPercentIncrease / 100);
    }

}
