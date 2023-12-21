using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private DayNightController dayNightController;

    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI enemiesLeft;
    public TextMeshProUGUI roundCounter;
    public TextMeshProUGUI health;

    //
    public float displayMessageTime = 4f;

    private void Awake()
    {
        dayNightController = FindObjectOfType<DayNightController>();

        //subscribe to the round events in roundManager
        RoundManager.NightStart += NightRoundStartDisplay;
        RoundManager.DayStart += DayStartDisplay;
        //subscribe to the onhealthupdated and ondeath actions from the player's Health script
        RoundManager.EnemyKilled += UpdateEnemiesLeftUI;
        RoundManager.EnemySpawned += UpdateEnemiesLeftUI;
        RoundManager.GameOverEvent += GameOver;

        dayNightController.DuskStarted += Dusk;


        

    }

    //this needs to be cleaned up - more concise design
    public void NightRoundStartDisplay()
    {
        UpdateEnemiesLeftUI();
        UpdateRoundNumberUI();
        StartCoroutine(UpdateStatusDisplayUI("Night " + RoundManager.roundCounter));
    }

    public void DayStartDisplay()
    {
        UpdateEnemiesLeftUI();
        UpdateRoundNumberUI();
        StartCoroutine(UpdateStatusDisplayUI("DAWN"));
    }

    public void Dusk()
    {
        StartCoroutine(UpdateStatusDisplayUI("DUSK"));
    }

    public IEnumerator UpdateStatusDisplayUI(string _text)
    {
        statusDisplay.gameObject.SetActive(true);
        statusDisplay.text = _text;
        yield return new WaitForSeconds(displayMessageTime);
        statusDisplay.gameObject.SetActive(false);
        StopCoroutine(UpdateStatusDisplayUI("null"));
    }

    public void UpdateEnemiesLeftUI()
    {
        enemiesLeft.text = "Vamps: " + RoundManager.enemiesLeft.ToString();
    }

    public void UpdateRoundNumberUI()
    {
        roundCounter.text = "Day: " + RoundManager.roundCounter.ToString();
    }

    public void UpdateHealthUI(float _health)
    {
        //these are 
        health.text = "Health: " + _health.ToString();
    }

    public void GameOver()
    {
        StartCoroutine(UpdateStatusDisplayUI("Game Over"));
    }
}
