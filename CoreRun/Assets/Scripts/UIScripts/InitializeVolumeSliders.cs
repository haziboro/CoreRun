using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class InitializeVolumeSliders : MonoBehaviour
{
    [SerializeField] VolumeSettings setting;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        InitializeSliders();
    }

    //Set's sliders initial values to saved values
    private void InitializeSliders()
    {
        musicVolumeSlider.value = setting.backgroundMusicVolume;
        SFXVolumeSlider.value = setting.SFXVolume;
    }
}
