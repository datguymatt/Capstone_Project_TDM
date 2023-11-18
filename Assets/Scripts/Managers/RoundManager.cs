using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
    public static RoundManager _instance;

    [Header("Global Round Variables")]
    //global round related
    public int roundCounter = 1;
    public int totalRounds = 5;
    public int enemiesRemaining;
    public int roundCoolDownTime = 5;

    //events
    public Action RoundStart;
    public Action RoundEnd;

    //diffulty scaling management
    [Header("Difficulty Scaling")]
    public int difficultyModifier;

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new RoundManager();
        }
    }

    public void InitializeRound()
    {
        if (roundCounter < totalRounds)
        {
            // new round starts, set values back to normal, count round,
            roundCounter++;

        }
    }

    public IEnumerator BeginRoundLoop()
    {
        //start a new round loop of enemy spawns after a cooldown buffer which will get smaller 
        yield return new WaitForSeconds(roundCoolDownTime);
        //UI queues the start
        RoundStart?.Invoke();

    }
}
