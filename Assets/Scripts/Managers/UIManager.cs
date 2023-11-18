using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    RoundManager roundManager;

    private void Awake()
    {
        //subscribe to the roundStart event in roundManager
        roundManager.RoundStart += RoundStartDisplay;
        //subscribe to the onhealthupdated and ondeath actyions from the player's Health script
        
    }

    public void RoundStartDisplay()
    {
        //SHOW:
        //number of enemies
        //Round # - cool haunting animation to text
        //maybe have the round be a full night?? if we can

        //start timer on screen before enemies appear
    }

    public void UpdateHealthUI(float health)
    {
        //these are 
    }

    public void UpdateEnemyCountUI()
    {

    }
}
