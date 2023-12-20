using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private RoundManager roundManager;
    private DayNightController dayNightController;

    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI enemiesLeft;
    public TextMeshProUGUI roundCounter;
    public TextMeshProUGUI health;

    //
    public float displayMessageTime = 4f;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
        dayNightController = FindObjectOfType<DayNightController>();

        //subscribe to the round events in roundManager
        roundManager.RoundStart += RoundStartDisplay;
        roundManager.RoundEnd += RoundEndDisplay;
        //subscribe to the onhealthupdated and ondeath actions from the player's Health script
        roundManager.EnemyKilled += UpdateEnemiesLeftUI;
        roundManager.EnemySpawned += UpdateEnemiesLeftUI;
        roundManager.GameOverEvent += GameOver;

        dayNightController.DuskStarted += Dusk;


        

    }

    //this needs to be cleaned up - more concise design
    public void RoundStartDisplay()
    {
        UpdateEnemiesLeftUI();
        UpdateRoundNumberUI();
        StartCoroutine(UpdateStatusDisplayUI("Night " + roundManager.roundCounter));
    }

    public void RoundEndDisplay()
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
        enemiesLeft.text = "Vamps: " + roundManager.enemiesLeft.ToString();
    }

    public void UpdateRoundNumberUI()
    {
        roundCounter.text = "Day: " + roundManager.roundCounter.ToString();
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
