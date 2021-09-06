using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScenario : MonoBehaviour
{

    [SerializeField] GameObject playerGraphic;

    // Start is called before the first frame update
    void Awake()
    {
        playerGraphic.GetComponent<Animator>().Play("playerIdle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
