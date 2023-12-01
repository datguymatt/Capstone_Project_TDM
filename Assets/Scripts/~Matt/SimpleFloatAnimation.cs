using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleFloatAnimation : MonoBehaviour
{
    public float distance;


    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        //change the sequence by changing the Start Point
        FloatUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //BOB
    public void BobUp() 
    {
        transform.DOMoveY(transform.position.y - distance, speed).OnComplete(BobDown);
    }
    public void BobDown() 
    {
        transform.DOMoveY(transform.position.y + distance, speed).OnComplete(BobUp);
    }
    //END

    //FLOAT UP SLOWLY
    //START
    public void FloatUp()
    {
        transform.DOMoveY(distance, speed).OnComplete(BobUp);
    }
    //END

}
