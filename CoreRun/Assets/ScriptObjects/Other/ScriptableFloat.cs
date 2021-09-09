using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableFloat")]
public class ScriptableFloat : ScriptableObject, ISerializationCallbackReceiver
{
    private float value_ = 1;

    [System.NonSerialized]
    public float value;


    public void OnAfterDeserialize()
    {
        value = value_;
    }

    public void OnBeforeSerialize() { }
}
