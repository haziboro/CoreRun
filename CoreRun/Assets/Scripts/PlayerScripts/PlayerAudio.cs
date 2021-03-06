using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource playerAudio;

    [SerializeField] VolumeSettings setting;
    [SerializeField] AudioClip[] narrowDodgeClips;
    [SerializeField] AudioClip[] healthLossClips;
    [SerializeField] AudioClip[] playerDeathClips;
    [SerializeField] AudioClip[] layerCompleteClips;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        ChangePlayerSFX();
    }

    //Plays a random dodge SFX
    public void PlayNarrowDodgeAudio()
    {
        //Play random dodge audio clip on succesful narrow dodge
        int clipNum = Random.Range(0, narrowDodgeClips.Length);
        playerAudio.clip = narrowDodgeClips[clipNum];
        playerAudio.Play();
    }

    //Plays a random damage SFX
    public void PlayPlayerHitAudio()
    {
        //Play random dodge audio clip on succesful narrow dodge
        int clipNum = Random.Range(0, healthLossClips.Length);
        playerAudio.clip = healthLossClips[clipNum];
        playerAudio.Play();
    }

    //Plays a random death SFX
    public void PlayPlayerDeathAudio()
    {
        int clipNum = Random.Range(0, playerDeathClips.Length);
        playerAudio.clip = playerDeathClips[clipNum];
        playerAudio.Play();
    }

    //Plays a random layer complete SFX
    public void PlayLayerCompleteAudio()
    {
        int clipNum = Random.Range(0, layerCompleteClips.Length);
        playerAudio.clip = layerCompleteClips[clipNum];
        playerAudio.Play();
    }

    //Changes player audio volume to match volume settings
    public void ChangePlayerSFX()
    {
        playerAudio.volume = setting.SFXVolume;
    }

}
