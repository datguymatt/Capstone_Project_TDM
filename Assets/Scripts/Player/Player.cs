using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    // health
    public Health playerHealth = new Health(100);

    // audio
    public AudioManager audioManager;
    private UIManager uiManager;

    //events
    public Action DamageTaken;
    public Action HealthLow;
    public Action PlayerDead;


    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
        audioManager = FindObjectOfType<AudioManager>();
        uiManager.UpdateHealthUI();
    }

    public void GetDamage(float damage)
    {
        //play sfx
        int randomFileNumber = UnityEngine.Random.Range(1, 4);
        FindAnyObjectByType<AudioManager>().PlaySFXAudio("player-take-damage-" + randomFileNumber.ToString());
        //
        playerHealth.TakeDamage(damage);
        uiManager.UpdateHealthUI();
        DamageTaken?.Invoke();
        if (playerHealth.GetHealth() <= 0)
        {
            Die();

        } else if (playerHealth.GetHealth() <= (playerHealth.GetMaxHealth() * 0.20f))
        { 
            //health is less than or equal to 20% of maxHealth, signal an event that health is low
            HealthLow?.Invoke();
        }
        
    }

    public void Die()
    {
        PlayerDead?.Invoke();
    }
}
