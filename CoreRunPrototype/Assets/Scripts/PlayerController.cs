using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20.0f;  //set character side movement speed
    private float horizontalInput;

    private float zRotationRange = 5;  //set range of horizontal boundaries in radians

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.back * horizontalInput * Time.deltaTime * speed);

    }



}
