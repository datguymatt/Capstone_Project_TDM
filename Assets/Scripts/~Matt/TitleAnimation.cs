using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    public float distance;

    public float waitTime;
    public float speed;

    public RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        //change the sequence by changing the Start Point
        StartCoroutine(Wait());
        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        FloatDown();
    }

    private void FloatDown()
    {
        transform.DOMoveY(transform.position.y - distance, speed);
    }
}


