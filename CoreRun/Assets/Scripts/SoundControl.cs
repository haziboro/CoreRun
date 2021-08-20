using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    private AudioSource backgroundMusic;
    private List<GameObject> SFXsources;
    [Range(0.0f, 1.0f)] [SerializeField] float startingVolume = 0.25f;
    [Range(0.0f, 1.0f)] [SerializeField] float backgroundVolume;
    [Range(0.0f, 1.0f)] [SerializeField] float soundEffectsVolume;

    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        StartMusic();
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeBackgroundMusicVolume();
        ChangeEffectVolume(soundEffectsVolume);
    }

    //Set's all volume to startingVolume and starts background music
    public void StartMusic()
    {
        backgroundMusic.volume = startingVolume;
        ChangeEffectVolume(startingVolume);

        backgroundVolume = backgroundMusic.volume;
        soundEffectsVolume = startingVolume;

        backgroundMusic.Play();
    }

    //Pauses the background music
    public void PauseMusic(bool pausing = true)
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

    //Stop background music and sets all stored audiosources volumes to zero
    public void StopMusic()
    {
        backgroundMusic.Stop();
        ChangeEffectVolume(0);
    }

    //Background Music Volume
    private void ChangeBackgroundMusicVolume()
    {
        backgroundMusic.volume = backgroundVolume;
    }

    //Changes volume of all AudioSources in scene
    private void ChangeEffectVolume(float volumeLevel)
    {
        if (SFXsources != null)
        {
            foreach (GameObject obj in SFXsources)
            {
                obj.GetComponent<AudioSource>().volume = volumeLevel;
            }
        }
    }

}
