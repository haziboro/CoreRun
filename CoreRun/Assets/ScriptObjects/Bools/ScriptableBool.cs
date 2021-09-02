using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableBools")]
public class ScriptableBool : ScriptableObject, ISerializationCallbackReceiver
{
    private bool active_ = false;

    [System.NonSerialized]
    public bool active;


    public void OnAfterDeserialize()
    {
        active = active_;
    }

    public void OnBeforeSerialize() { }

}
