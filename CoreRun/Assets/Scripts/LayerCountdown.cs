using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCountdown : MonoBehaviour
{
    [SerializeField] ScoreAndLayer score;
    [SerializeField] GameEvent EndLayer;
    [SerializeField] ScriptableInt layerInterval;//Number of seconds between layer transitions

    //Starts a new Layer
    public void StartNewLayer()
    {
        //Debug.Log("Layer Countdown Starting a new layer");
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
        //Debug.Log("Layer Countdown ending the layer");
        EndLayer.Raise();
    }
}
