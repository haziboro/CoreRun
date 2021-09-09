using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCountdown : MonoBehaviour
{
    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScriptableBool doneSpawning;
    [SerializeField] ScoreAndLayer score;
    [SerializeField] GameEvent EndLayer;
    [SerializeField] ScriptableInt layerInterval;//Number of seconds between layer transitions

    //Starts a new Layer
    public void StartNewLayer()
    {
        StartCoroutine(LayerTimer());
    }

    //Increments the current layer
    public void IncreaseLayerLevel()
    {
        score.layer++;
    }

    //Initiate layer end after layerInterval seconds
    IEnumerator LayerTimer()
    {
        yield return new WaitForSeconds(layerInterval.value);
        //Wait for wave manager to finish
        StartCoroutine(WaitForWaveEnd());
    }

    //Waits for spawncycle to complete, then spawns the layer end
    IEnumerator WaitForWaveEnd()
    {
        while (!doneSpawning.active)
        {
            yield return null;
        }

        if (gameRunning.active)
        {
            EndLayer.Raise();
        }
    }
}
