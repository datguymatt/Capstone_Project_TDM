using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DayNightController : MonoBehaviour
{

    public RoundManager roundManager;
    public float switchToDuskDuration;
    public float switchToNightDuration;
    public float switchToDayDuration;
    
    // Start is called before the first frame update
    void Awake()
    {
        //set listener for round start event
        roundManager.RoundStart += StartNightCycle;
        roundManager.RoundEnd += StartDayCycle;
    }

    // Update is called once per frame
    void Update()
    {
        //in it's simplest form, this will get it to daytime
        
        
    }

    public void StartNightCycle()
    {
        //on RoundStart event, do this:
        SwitchToDuskTime();

    }
    public void StartDayCycle()
    {
        SwitchToDayTime();
    }

    public void SwitchToDayTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.558965564f, -0.424670637f, 0.604867339f, -0.375962704f), switchToDayDuration);
    }

    public void SwitchToDuskTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.434261024f, 0.555342317f, 0.487651318f, 0.514983892f), switchToDuskDuration).OnComplete(SwitchToNightTime); 
    }

    public void SwitchToNightTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.484917283f, 0.384504378f, 0.7563802f, 0.211897805f), switchToNightDuration);
    }



}
