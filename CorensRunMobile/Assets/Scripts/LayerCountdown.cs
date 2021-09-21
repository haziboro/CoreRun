using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCountdown : MonoBehaviour
{
    private int layerTimeReduction = 1;
    private int countdown = 0;

    [SerializeField] ScriptableBool gameRunning;
    [SerializeField] ScriptableBool doneSpawning;
    [SerializeField] ScoreAndLayer score;
    [SerializeField] GameEvent EndLayer;
    [SerializeField] ScriptableInt layerInterval;//Number of seconds between layer transitions
    [SerializeField] int layerStartTime;
    [SerializeField] int timeDecreaseWaitTime;//Number of waves before the layer time is reduced

    private void Awake()
    {
        layerInterval.value = layerStartTime;
    }

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
        countdown++;
        //Reduce time of wave after a specified number of rounds
        if (countdown == timeDecreaseWaitTime)
        {
            countdown = 0;
            if(layerInterval.value > 1)
            {
                layerInterval.value -= layerTimeReduction;
            }
        }
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
