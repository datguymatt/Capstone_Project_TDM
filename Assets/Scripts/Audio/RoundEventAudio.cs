using UnityEngine;
using DG.Tweening;

public class RoundEventAudio : MonoBehaviour
{
    private DayNightController dayNightController;
    private AudioManager audioManager;

    //fade stuff
    private float maxDayAmbienceVolume;
    private float maxNightAmbienceVolume;

    public void Awake()
    {
        dayNightController = FindObjectOfType<DayNightController>();
        audioManager = FindFirstObjectByType<AudioManager>();
        RoundManager.NightStart += NightStart;
        RoundManager.TransitionToDayStart += TransitionToDay;
        RoundManager.TransitionToDuskStart += TransitionToDusk;
        RoundManager.DuskStart += Dusk;
        maxNightAmbienceVolume = audioManager.nightAmbience.volume;
    }
    //ROUND RELATED TRIGGERS

    public void NightStart()
    {
        audioManager.PlaySFXAudio("night-start");
        audioManager.musicNight.volume = 0.455f;
        audioManager.musicNight.Play();
    }

    public void TransitionToDay()
    {
        audioManager.musicNight.DOFade(0, 10);
        audioManager.dayAmbience.Play();
        audioManager.musicDay.Play();
        //fade in volume
        audioManager.musicDay.DOFade(0.6f, 6);
        audioManager.dayAmbience.DOFade(0.5f, 8);
        audioManager.nightAmbience.DOFade(0, 8).OnComplete(audioManager.nightAmbience.Stop);
    }

    public void TransitionToDusk()
    {
        //fade out day quickly, then dusk countdown starts
        audioManager.musicDusk.volume = 0.455f;
        audioManager.musicDay.DOFade(0, 4).OnComplete(audioManager.musicDusk.Play);

    }

    public void Dusk()
    {
        audioManager.musicDusk.DOFade(0, RoundManager.duskDuration + 3);
        audioManager.nightAmbience.Play();
        audioManager.dayAmbience.DOFade(0, RoundManager.duskDuration);
        audioManager.nightAmbience.DOFade(maxNightAmbienceVolume, RoundManager.duskDuration);
    }
}
