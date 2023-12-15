using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuDayNightController : MonoBehaviour
{
    MenuDayNightController _instance;
    public float switchToDuskDuration;
    public float switchToNightDuration;
    public float switchToDayDuration;
    
    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new MenuDayNightController();
        }
        
    }

    void Update()
    {
        
        
    }

    public void SwitchToDayTime(float time)
    {
        transform.DORotateQuaternion(new Quaternion(0.558965564f, -0.424670637f, 0.604867339f, -0.375962704f), time);
    }

    public void SwitchToDuskTime(float time)
    {
        //Quaternion(-0.06835825,0.534569681,0.00846953783,0.842312753)
        //transform.DORotateQuaternion(new Quaternion(0.434261024f, 0.555342317f, 0.487651318f, 0.514983892f), time).SetEase(Ease.InOutSine);
        transform.DORotateQuaternion(new Quaternion(-0.06835825f, 0.534569681f, 0.00846953783f, 0.842312753f), time).SetEase(Ease.InOutSine);
    }

    public void SwitchToNightTime(float time)
    {
        transform.DORotateQuaternion(new Quaternion(0.0632761344f, -0.883777916f, -0.219275698f, 0.408473969f), time).SetEase(Ease.InOutSine);
    }



}
