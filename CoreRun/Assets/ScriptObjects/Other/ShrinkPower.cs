using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShrinkPower Obj")]
public class ShrinkPower : ScriptableObject, ISerializationCallbackReceiver
{
    private bool onCooldown_ = false;
    private float value_ = 100;

    [System.NonSerialized]
    public bool onCooldown;
    [System.NonSerialized]
    public float value;

    public void OnAfterDeserialize()
    {
        value = value_;
        onCooldown = onCooldown_;
    }

    public void OnBeforeSerialize() { }
}
