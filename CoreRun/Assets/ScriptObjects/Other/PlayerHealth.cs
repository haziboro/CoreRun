using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Health")]
public class PlayerHealth : ScriptableObject, ISerializationCallbackReceiver
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
