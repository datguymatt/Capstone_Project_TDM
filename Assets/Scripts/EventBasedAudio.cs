using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventBasedAudio : AudioManager
{
    
    public RoundManager roundManager;

    private void Awake()
    {
        //subscribe to the round events in roundManager
        roundManager.RoundStart += RoundStartSequence;
        roundManager.RoundEnd += RoundEndSequence;
        //events related to player
        //playerHealth low
        //powerup active
    }
    private void RoundEndSequence()
    {
        throw new NotImplementedException();
    }

    private void RoundStartSequence()
    {
        throw new NotImplementedException();
    }
}
