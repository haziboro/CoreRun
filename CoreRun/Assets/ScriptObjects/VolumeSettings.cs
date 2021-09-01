using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Volume Settings")]
public class VolumeSettings : ScriptableObject, ISerializationCallbackReceiver
{
    [Range(0.0f, 1.0f)] [SerializeField] float savedBackgroundVolume;
    [Range(0.0f, 1.0f)] [SerializeField] float savedSFXVolume;

    [System.NonSerialized]
    [Range(0.0f, 1.0f)] public float backgroundMusicVolume;
    [System.NonSerialized]
    [Range(0.0f, 1.0f)] public float SFXVolume;

    public void OnAfterDeserialize()
    {
        backgroundMusicVolume = savedBackgroundVolume;
        SFXVolume = savedSFXVolume;
    }

    public void OnBeforeSerialize()
    {
        savedBackgroundVolume = backgroundMusicVolume;
        savedSFXVolume = SFXVolume;
    }
}
