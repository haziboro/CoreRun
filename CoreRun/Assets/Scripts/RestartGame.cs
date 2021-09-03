using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;

    //Restart Game
    public void Restart()
    {
        gameRunning.active = false;
        SceneManager.LoadScene(1);
    }
}
