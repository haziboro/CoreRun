using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    GameManager gameManager;
    private AudioSource backgroundMusic;
    private AudioSource playerAudio;
    [Range(0.0f, 1.0f)] [SerializeField] float startingVolume = 0.25f;
    [Range(0.0f, 1.0f)] [SerializeField] float backgroundVolume;
    [Range(0.0f, 1.0f)] [SerializeField] float soundEffectsVolume;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        backgroundMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        playerAudio = GameObject.Find("PlayerCube").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.gameRunning)
        {
            ChangeBackgroundMusicVolume();
            ChangePlayerVolume();
        }
    }

    //Set's all volume to startingVolume and starts background music
    public void StartMusic()
    {
        backgroundMusic.volume = startingVolume;
        playerAudio.volume = startingVolume;
        backgroundVolume = backgroundMusic.volume;
        soundEffectsVolume = playerAudio.volume;

        backgroundMusic.Play();
    }

    public void PauseMusic()
    {
        backgroundMusic.Pause();
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
        playerAudio.Stop();
    }

    //Background Music Volume
    private void ChangeBackgroundMusicVolume()
    {
        backgroundMusic.volume = backgroundVolume;
    }

    //Background Music Volume
    private void ChangePlayerVolume()
    {
        playerAudio.volume = soundEffectsVolume;
    }

}
