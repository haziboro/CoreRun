using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] GameEvent loadEarth;

    //Restart Game
    public void Restart()
    {
        gameRunning.active = false;
        loadEarth.Raise();
    }
}
