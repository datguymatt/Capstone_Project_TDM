using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private DayNightController dayNightController;

    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI enemiesLeft;
    public TextMeshProUGUI roundCounter;
    public Slider health;
    public Image bloodFlashImage;
    public Color bloodFlashColor;

    //
    public float displayMessageTime = 4f;

    private void Start()
    {
        dayNightController = FindObjectOfType<DayNightController>();

        //subscribe to the round events in roundManager
        RoundManager.NightStart += NightStart;
        RoundManager.TransitionToDayStart += DayStartDisplay;
        RoundManager.TransitionToDuskStart += Dusk;
        //subscribe to the onhealthupdated and ondeath actions from the player's Health script
        RoundManager.EnemyKilled += UpdateEnemiesLeftUI;
        RoundManager.EnemySpawned += UpdateEnemiesLeftUI;
        RoundManager.GameOverEvent += GameOver;
        Player.Instance.HealthLow += 

        dayNightController.DuskStarted += Dusk;

        bloodFlashColor = bloodFlashImage.color;
        

    }

    private void OnDestroy()
    {
        RoundManager.NightStart -= NightStart;
        RoundManager.TransitionToDayStart -= DayStartDisplay;
        RoundManager.TransitionToDuskStart -= Dusk;
        //subscribe to the onhealthupdated and ondeath actions from the player's Health script
        RoundManager.EnemyKilled -= UpdateEnemiesLeftUI;
        RoundManager.EnemySpawned -= UpdateEnemiesLeftUI;
        RoundManager.GameOverEvent -= GameOver;

        dayNightController.DuskStarted -= Dusk;
    }
    //this needs to be cleaned up - more concise design
    public void NightStart()
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
        enemiesLeft.text = "Vamps " + RoundManager.enemiesLeft.ToString();
    }

    public void UpdateRoundNumberUI()
    {
        roundCounter.text = "Day " + RoundManager.roundCounter.ToString();
    }

    public void UpdateHealthUI()
    {
        float healthPercent = Player.Instance.playerHealth.GetHealth() / Player.Instance.playerHealth.GetMaxHealth();
        health.value = healthPercent;
        bloodFlashColor.a = healthPercent;
    }

    public void HealthLow()
    {
        //tint the screen red flashing, add a heartbeat sound
        bloodFlashImage.DOColor(bloodFlashColor, 2).SetLoops(-1);

    }

    public void GameOver()
    {
        StartCoroutine(UpdateStatusDisplayUI("Game Over"));
    }
}
