using UnityEngine;
using DG.Tweening;
using System;

public class DayNightController : MonoBehaviour
{
    public float switchToDuskDuration;
    public float switchToNightDuration;
    public float switchToDayDuration;

    public Material moonMaterial;
    public Color moonfadedColor;
    public ParticleSystem stars;
    public NM_Wind windZone;

    public bool stormActive = false;
    public bool isFirstRound = true;

    public Action DuskStarted;

    // Start is called before the first frame update
    void Start()
    {
        //set listener for round start event
        RoundManager.TransitionToNightStart += SwitchToNightTime;
        RoundManager.TransitionToDayStart += SwitchToDayTime;
        RoundManager.TransitionToDuskStart += SwitchToDuskTime;
        RoundManager.NightStart += SwitchToNightTime;
        moonMaterial.color = Color.white;
        isFirstRound = true;

        //
        isFirstRound = false;
        DOTween.To(() => windZone.WindSpeed, x => windZone.WindSpeed = x, 70f, 20);
        DOTween.To(() => windZone.Turbulence, x => windZone.Turbulence = x, 0.9f, 20);
        stormActive = true;
        GetComponent<AudioSource>().DOFade(0.497f, 4);
        GetComponent<AudioSource>().Play();
    }

    private void OnDestroy()
    {
        RoundManager.TransitionToNightStart -= SwitchToNightTime;
        RoundManager.NightStart -= SwitchToNightTime;
        RoundManager.TransitionToDayStart -= SwitchToDayTime;
        RoundManager.TransitionToDuskStart -= SwitchToDuskTime;
    }

    public void SwitchToDayTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.558965564f, -0.424670637f, 0.604867339f, -0.375962704f), RoundManager.transitionToDayDuration);
        moonMaterial.DOColor(moonfadedColor, 7f);
        stars.startColor = moonfadedColor;
        //every once in a while, and evening will have a storm
        if(stormActive)
        {
            DOTween.To(() => windZone.WindSpeed, x => windZone.WindSpeed = x, 15f, 15);
            DOTween.To(() => windZone.Turbulence, x => windZone.Turbulence = x, 0.20f, 15);
            stormActive = false;
            GetComponent<AudioSource>().DOFade(0, 4);
            
        }
        
    }

    public void SwitchToDuskTime()
    {
        //Quaternion(-0.06835825,0.534569681,0.00846953783,0.842312753)
        //transform.DORotateQuaternion(new Quaternion(0.434261024f, 0.555342317f, 0.487651318f, 0.514983892f), time).SetEase(Ease.InOutSine);
        transform.DORotateQuaternion(new Quaternion(-0.06835825f, 0.534569681f, 0.00846953783f, 0.842312753f), RoundManager.transitionToDuskDuration).SetEase(Ease.InOutSine);
        
    }

    public void SwitchToNightTime()
    {
        
        transform.DORotateQuaternion(new Quaternion(0.0632761344f, -0.883777916f, -0.219275698f, 0.408473969f), RoundManager.transitionToNightDuration).SetEase(Ease.InOutSine);
        moonMaterial.DOColor(Color.white, switchToNightDuration);
        if (UnityEngine.Random.Range(0, 4) == 3 || isFirstRound)
        {
            isFirstRound = false;
            DOTween.To(() => windZone.WindSpeed, x => windZone.WindSpeed = x, 70f, 20);
            DOTween.To(() => windZone.Turbulence, x => windZone.Turbulence = x, 0.9f, 20);
            stormActive = true;
            GetComponent<AudioSource>().DOFade(0.497f, 4);
            GetComponent<AudioSource>().Play();
        }
        
    }



}
