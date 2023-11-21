using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSFXSource;
    [SerializeField] private AudioSource audioMusicSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip[] musicClips;
    AudioClip searchResult;

    public AudioClip SearchForClip(string clipName, string audioSourceType)
    {
        //declare the variable to hold the search result once found
        if (audioSourceType == "sfx")
        {
            
            foreach (AudioClip clip in audioClips)
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
                
                    audioSFXSource.clip = clipFound;
                    audioSFXSource.PlayOneShot(clipFound);
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
        audioSFXSource.Stop();
    }

    public void PlayMusicAudio(string clip)
    {
        AudioClip clipFound = SearchForClip(clip, "music");

        if (clipFound != null)
        {
            if (clipFound.name == clip)
            {
                audioMusicSource.clip = clipFound;
                audioMusicSource.PlayOneShot(clipFound);
            }
        }
        else
        {
            Debug.LogError("Cannot find clip by name " + clip);
        }
        
    }

}
