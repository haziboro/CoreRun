using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private AudioSource backgroundMusic;
    public List<GameObject> SFXsources;

    public float backgroundMusicVolume = 0.25f;
    public float soundEffectsVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        StartMusic();
    }

    //Intializes sounds in scene using volume from SceneDataTransfer
    public void StartMusic()
    {
        backgroundMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        ChangeBackgroundMusicVolume(backgroundMusicVolume);
        ChangeEffectVolume(soundEffectsVolume);
        backgroundMusic.Play();
    }

    //Pauses the background music
    public void PauseMusic(bool pausing)
    {
        if(pausing)
        {
            backgroundMusic.Pause();
        }
        else
        {
            backgroundMusic.Play();
        }
    }

    //Stop background music
    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    //Background Music Volume
    public void ChangeBackgroundMusicVolume(float backgroundVolume)
    {
        backgroundMusic.volume = backgroundVolume;
        backgroundMusicVolume = backgroundVolume;
    }

    //Changes volume of all AudioSources in scene
    public void ChangeEffectVolume(float volumeLevel)
    {
        if (SFXsources != null)
        {
            foreach (GameObject obj in SFXsources)
            {
                obj.GetComponent<AudioSource>().volume = volumeLevel;
            }//endforeach
        }//endif
        soundEffectsVolume = volumeLevel;
    }

    

}
