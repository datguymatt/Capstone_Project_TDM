using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatTweener : MonoBehaviour
{
    public DayNightController DayNightController;
    public float switchToNightDuration;

    // Start is called before the first frame update
    void Start()
    {
        DayNightController = FindAnyObjectByType<DayNightController>();
        SwitchToNightTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToNightTime()
    {
        transform.DORotateQuaternion(new Quaternion(0.484917283f, 0.384504378f, 0.7563802f, 0.211897805f), switchToNightDuration);
    }
}
