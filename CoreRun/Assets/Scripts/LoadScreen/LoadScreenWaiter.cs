using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreenWaiter : MonoBehaviour
{
    [SerializeField] ScriptableBool loadScreen;


    public void LoadToScene(int sceneNum)
    {
        StartCoroutine(WaitForLoading(sceneNum));
    }

    //Waits for loading screen before changing scenes
    IEnumerator WaitForLoading(int sceneNum)
    {
        while (!loadScreen.active)
        {
            yield return null;
        }
        SceneManager.LoadScene(sceneNum);
    }
}
