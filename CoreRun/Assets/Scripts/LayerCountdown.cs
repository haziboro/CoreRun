using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCountdown : MonoBehaviour
{
    [SerializeField] ScoreAndLayer score;
    [SerializeField] GameEvent TransitionCurrentLayer;
    [SerializeField] int layerInterval = 20;//Number of seconds between layer transitions

    // Start is called before the first frame update
    void Start()
    {
        StartNewLayer();
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
        yield return new WaitForSeconds(layerInterval);
        TransitionCurrentLayer.Raise();
    }
}
