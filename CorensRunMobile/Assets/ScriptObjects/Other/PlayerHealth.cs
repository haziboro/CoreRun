using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Health")]
public class PlayerHealth : ScriptableObject, ISerializationCallbackReceiver
{
    private bool lostHealth_ = false;

    [SerializeField] int maxHealth_;

    [System.NonSerialized]
    public int maxHealth;
    [System.NonSerialized]
    public bool lostHealth;
    public int health;

    public void OnAfterDeserialize()
    {
        health = maxHealth_;
        maxHealth = maxHealth_;
        lostHealth = lostHealth_;
    }

    public void OnBeforeSerialize() { }
}
