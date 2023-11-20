using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    RoundManager roundManager;
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI enemiesLeft;
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI health;

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

    public void UpdateStatusDisplayUI(string _text)
    {
        statusDisplay.text = _text;
    }

    public void UpdateEnemiesLeftUI(int _enemiesLeft)
    {
        enemiesLeft.text = "Enemies left: " + enemiesLeft.ToString();
    }

    public void UpdateRoundNumberUI(int _roundNumber)
    {
        roundNumber.text = "Round: " + _roundNumber.ToString();
    }

    public void UpdateHealthUI(float _health)
    {
        //these are 
        health.text = "Health: " + _health.ToString();
    }
}
