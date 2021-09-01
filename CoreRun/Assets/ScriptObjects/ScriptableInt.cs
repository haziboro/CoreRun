using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableInt")]
public class ScriptableInt : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] int maxHealth_;

    [System.NonSerialized]
    public int maxHealth;
    public int health;

    public void OnAfterDeserialize()
    {
        health = maxHealth_;
        maxHealth = maxHealth_;
    }

    public void OnBeforeSerialize() { }
}
