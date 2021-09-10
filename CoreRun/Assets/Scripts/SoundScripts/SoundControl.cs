using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private AudioSource backgroundMusic;

    [SerializeField] VolumeSettings setting;
    [SerializeField] GameEvent updateSFXVolume;

    // Start is called before the first frame update
    void Start()
    {
        StartMusic();
    }

    //Intializes sounds in scene using volume from SceneDataTransfer
    public void StartMusic()
    {
        backgroundMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        ChangeBackgroundMusicVolume(setting.backgroundMusicVolume);
        ChangeEffectVolume(setting.SFXVolume);
        backgroundMusic.Play();
    }

    //Pauses the background music
    public void PauseMusic(bool pausing)
    {
        if(pausing)
        { backgroundMusic.Pause(); }
        else
        { backgroundMusic.Play(); }
    }

    //Stop background music
    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    //Background Music Volume
    public void ChangeBackgroundMusicVolume(float newVolume)
    {
        backgroundMusic.volume = newVolume;
        setting.backgroundMusicVolume = newVolume;
    }

    //Changes volume of all AudioSources in scene
    public void ChangeEffectVolume(float newVolume)
    {
        setting.SFXVolume = newVolume;
        updateSFXVolume.Raise();
    }

    

}
