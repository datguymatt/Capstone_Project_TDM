using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DayNightController : MonoBehaviour
{

    private RoundManager roundManager;
    public float switchToDuskDuration;
    public float switchToNightDuration;
    public float switchToDayDuration;

    public Material moonMaterial;
    public Color moonfadedColor;

    // Start is called before the first frame update
    void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
        //set listener for round start event
        roundManager.RoundStart += SwitchToNightTime;
        roundManager.RoundEnd += SwitchToDayTime;

        moonMaterial.color = Color.white;
    }

    public void SwitchToDayTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.558965564f, -0.424670637f, 0.604867339f, -0.375962704f), switchToDayDuration).OnComplete(SwitchToDuskTime);
        moonMaterial.DOColor(moonfadedColor, 7f);
    }

    public void SwitchToDuskTime()
    {
        //Quaternion(-0.06835825,0.534569681,0.00846953783,0.842312753)
        //transform.DORotateQuaternion(new Quaternion(0.434261024f, 0.555342317f, 0.487651318f, 0.514983892f), time).SetEase(Ease.InOutSine);
        transform.DORotateQuaternion(new Quaternion(-0.06835825f, 0.534569681f, 0.00846953783f, 0.842312753f), switchToDuskDuration).SetEase(Ease.InOutSine);
    }

    public void SwitchToNightTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.0632761344f, -0.883777916f, -0.219275698f, 0.408473969f), switchToNightDuration).SetEase(Ease.InOutSine);
        moonMaterial.DOColor(Color.white, switchToNightDuration);
    }



}
