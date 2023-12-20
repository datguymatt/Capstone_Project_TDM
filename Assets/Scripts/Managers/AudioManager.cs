using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
 
public class AudioManager : MonoBehaviour
{
    [Header("Music")]
    //channels
    public AudioSource musicNight;
    public AudioSource musicDusk;
    public AudioSource musicDay;

    [SerializeField] private AudioClip[] musicClips;

    [Header("SFX Oneshots")]
    //clips
    [SerializeField] private AudioSource[] sfxOneShotChannels;
    [SerializeField] private AudioClip[] sfxOneShotClips;

    [Header("Ambience")]
    //Ambience
    public AudioSource dayAmbience;
    public AudioSource nightAmbience;
    //search
    AudioClip searchResult;

    //subscribing to events
    private RoundManager roundManager;
    private DayNightController dayNightController;

    //fade stuff
    
    private float maxDayAmbienceVolume;
    private float maxNightAmbienceVolume; 


    public static AudioManager _instance;

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new AudioManager();
        }
        roundManager = FindObjectOfType<RoundManager>();
        dayNightController = FindObjectOfType<DayNightController>();
        roundManager.RoundStart += FadeToNight;
        roundManager.RoundEnd += FadeToDay;
        dayNightController.DuskStarted += DuskStart;
        maxNightAmbienceVolume = nightAmbience.volume;
    }

    
    public void FadeToNight()
    {
        if(nightAmbience.playOnAwake)
        {
            //no need to fade, it is start of game
            nightAmbience.playOnAwake = false;
            NightStart();
            //set the current max volume as the default for fading back in
        }
        else
        {
            musicDusk.DOFade(0, 5f).OnComplete(musicNight.Play);
            //fade in volume of ambience, and music
            dayAmbience.DOFade(0, 5).OnComplete(dayAmbience.Stop);
            nightAmbience.DOFade(maxNightAmbienceVolume, 14).OnComplete(NightStart);
            //music
            
        }
        
    }

    public void NightStart()
    {
        PlaySFXAudio("night-start");
    }
    public void FadeToDay()
    {
        dayAmbience.Play();
        musicDay.Play();
        //fade in volume
        musicDay.DOFade(0.6f, 6);
        dayAmbience.DOFade(0.5f, 8);
        nightAmbience.DOFade(0, 8).OnComplete(nightAmbience.Stop);

            

    }

    public void DuskStart()
    {
        //fade out day quickly, then dusk countdown starts
        musicDay.DOFade(0, 4).OnComplete(musicDusk.Play);
        
    }
    public int SelectChannel()
    {
        //will boot out the first channel if it can't find anything
        int channelNumber = 0;
        for (int i = 0; i < sfxOneShotChannels.Length; i++)
        {
            if (!sfxOneShotChannels[i].isPlaying)
            {
                channelNumber = i;
                break;
            }
        }
        return channelNumber;
    }

    public AudioClip SearchForClip(string clipName, string audioSourceType)
    {
        //declare the variable to hold the search result once found
        if (audioSourceType == "sfx")
        {
            
            foreach (AudioClip clip in sfxOneShotClips)
            {
                if (clip.name == clipName)
                {
                    searchResult = clip;
                    break;
                }
                else if (clip.name != clipName)
                {
                    searchResult = null;
                }
            }
        } 
        else if (audioSourceType == "music")
        {
            foreach (AudioClip clip in musicClips)
            {
                if (clip.name == clipName)
                {
                    searchResult = clip;
                }
                else if (clip.name != clipName)
                {
                    searchResult = null;
                }
            }
        }
        
        return searchResult;
    }

    public void PlaySFXAudio(string clip)
    {
        AudioClip clipFound = SearchForClip(clip, "sfx");
        if (clipFound != null)
        {
            if (clipFound.name == clip)
            {

                sfxOneShotChannels[SelectChannel()].clip = clipFound;
                    sfxOneShotChannels[SelectChannel()].PlayOneShot(clipFound);
            }
        }
        else
        {
            Debug.LogError("Cannot find clip by name " + clip);
        }
    }

    public void StopSFX()
    {
        Debug.Log("Stopping");
        sfxOneShotChannels[SelectChannel()].Stop();
    }

    public void PlayMusicAudio(string clip)
    {
        AudioClip clipFound = SearchForClip(clip, "music");

        if (clipFound != null)
        {
            if (clipFound.name == clip)
            {
                if (musicNight.isPlaying)
                {
                    musicDusk.clip = clipFound;
                } else if (musicDusk.isPlaying)
                {
                    musicNight.clip = clipFound;
                }
            }
        }
        else
        {
            Debug.LogError("Cannot find clip by name " + clip);
        }
        
    }

}
