using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChange : MonoBehaviour
{
    public AudioMixer mainMixer;

    public void SetMasterVolume(float volume)
    {
        if(volume == -30)
        {
            mainMixer.SetFloat("mastervol", -80f);
        }
        else
        {
            mainMixer.SetFloat("mastervol", volume);
        }
    }
    public void SetMusicVolume(float volume)
    {
        if (volume == -30)
        {
            mainMixer.SetFloat("musicvol", -80f);
        }
        else
        {
            mainMixer.SetFloat("musicvol", volume);
        }
        
    }

    public void SetSFXVolume(float volume)
    {
        if (volume == -30)
        {
            mainMixer.SetFloat("sfxvol", -80f);
        }
        else
        {
            mainMixer.SetFloat("sfxvol", volume);
        }
        
    }
}
