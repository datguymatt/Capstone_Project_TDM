using UnityEngine;
using DG.Tweening;

public class RoundEventAudio : AudioManager
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
        maxNightAmbienceVolume = nightAmbience.volume;
    }
    // ROUND RELATED TRIGGERS
    public void FadeToNight()
    {
        Debug.Log("sound here");
        if (nightAmbience.playOnAwake)
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

    public void TransitionToDay()
    {
        musicNight.DOFade(0, 10);
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
}
