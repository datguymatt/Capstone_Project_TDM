using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public RoundManager roundManager;
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI enemiesLeft;
    public TextMeshProUGUI roundCounter;
    public TextMeshProUGUI health;

    //
    public float displayMessageTime = 4f;

    private void Awake()
    {
        //subscribe to the round events in roundManager
        roundManager.RoundStart += RoundStartDisplay;
        roundManager.RoundEnd += RoundEndDisplay;
        //subscribe to the onhealthupdated and ondeath actions from the player's Health script
        roundManager.EnemyKilled += UpdateEnemiesLeftUI;
        roundManager.EnemySpawned += UpdateEnemiesLeftUI;

    }

    //this needs to be cleaned up - more concise design
    public void RoundStartDisplay()
    {
        UpdateEnemiesLeftUI();
        UpdateRoundNumberUI();
        StartCoroutine(UpdateStatusDisplayUI("Round Start"));
    }

    public void RoundEndDisplay()
    {
        UpdateEnemiesLeftUI();
        UpdateRoundNumberUI();
        StartCoroutine(UpdateStatusDisplayUI("Round End"));
    }

    public IEnumerator UpdateStatusDisplayUI(string _text)
    {
        statusDisplay.gameObject.SetActive(true);
        statusDisplay.text = _text;
        yield return new WaitForSeconds(displayMessageTime);
        statusDisplay.gameObject.SetActive(false);
    }

    public void UpdateEnemiesLeftUI()
    {
        enemiesLeft.text = "Vamps: " + roundManager.enemiesLeft.ToString();
    }

    public void UpdateRoundNumberUI()
    {
        roundCounter.text = "Round: " + roundManager.roundCounter.ToString();
    }

    public void UpdateHealthUI(float _health)
    {
        //these are 
        health.text = "Health: " + _health.ToString();
    }
}
