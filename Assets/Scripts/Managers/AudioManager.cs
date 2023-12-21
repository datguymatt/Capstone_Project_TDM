using UnityEngine;
 
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
