using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "High Score")]
public class HighScore : ScriptableObject, ISerializationCallbackReceiver
{
    int highScore_;

    [System.NonSerialized]
    public int highScore;

    public void OnAfterDeserialize()
    {
        highScore = highScore_;
    }

    public void OnBeforeSerialize() { }

}
