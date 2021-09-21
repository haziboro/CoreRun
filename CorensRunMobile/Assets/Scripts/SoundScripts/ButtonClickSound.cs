using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    private AudioSource clickAudio;

    [SerializeField] VolumeSettings setting;

    // Start is called before the first frame update
    void Awake()
    {
        clickAudio = GetComponent<AudioSource>();
    }

    //Changes player audio volume to match volume settings
    public void ChangeClickVolume()
    {
        clickAudio.volume = setting.SFXVolume;
    }
}
