using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Score and Layer Tracker")]
public class ScoreAndLayer : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] int initialScore;
    [SerializeField] int initialLayer;

    [System.NonSerialized]
    public int score;
    [System.NonSerialized]
    public int layer;

    public void OnAfterDeserialize()
    {
        score = initialScore;
        layer = initialLayer;
    }

    public void OnBeforeSerialize() { }
    
}
