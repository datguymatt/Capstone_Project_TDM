using UnityEngine;
using DG.Tweening;

public class RoundEventAudio : MonoBehaviour
{
    private DayNightController dayNightController;

    //fade stuff
    private float maxDayAmbienceVolume;
    private float maxNightAmbienceVolume;

    public void Awake()
    {
        dayNightController = FindObjectOfType<DayNightController>();
        RoundManager.NightStart += FadeToNight;
        RoundManager.TransitionToDayStart += TransitionToDay;
        dayNightController.DuskStarted += DuskStart;
        maxNightAmbienceVolume = AudioManager._instance.nightAmbience.volume;
    }
    //ROUND RELATED TRIGGERS
    public void FadeToNight()
    {
        Debug.Log("sound here");
        if (AudioManager._instance.nightAmbience.playOnAwake)
        {
            //no need to fade, it is start of game
            AudioManager._instance.nightAmbience.playOnAwake = false;
            NightStart();
            //set the current max volume as the default for fading back in
        }
        else
        {
            AudioManager._instance.musicDusk.DOFade(0, 5f).OnComplete(AudioManager._instance.musicNight.Play);
            //fade in volume of ambience, and music
            AudioManager._instance.dayAmbience.DOFade(0, 5).OnComplete(AudioManager._instance.dayAmbience.Stop);
            AudioManager._instance.nightAmbience.DOFade(maxNightAmbienceVolume, 14).OnComplete(NightStart);
            //music

        }

    }

    public void NightStart()
    {
        AudioManager._instance.PlaySFXAudio("night-start");
    }

    public void TransitionToDay()
    {
        AudioManager._instance.musicNight.DOFade(0, 10);
        AudioManager._instance.dayAmbience.Play();
        AudioManager._instance.musicDay.Play();
        //fade in volume
        AudioManager._instance.musicDay.DOFade(0.6f, 6);
        AudioManager._instance.dayAmbience.DOFade(0.5f, 8);
        AudioManager._instance.nightAmbience.DOFade(0, 8).OnComplete(AudioManager._instance.nightAmbience.Stop);
    }

    public void DuskStart()
    {
        //fade out day quickly, then dusk countdown starts
        AudioManager._instance.musicDay.DOFade(0, 4).OnComplete(AudioManager._instance.musicDusk.Play);

    }
}
