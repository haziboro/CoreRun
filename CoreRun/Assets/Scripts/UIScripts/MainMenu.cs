using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

//Starts the menu
[DefaultExecutionOrder(1000)]
public class MainMenu : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] GameEvent colorTitle;

    // Start is called before the first frame update
    void Start()
    {
        gameRunning.active = true;
        colorTitle.Raise();
    }

}
