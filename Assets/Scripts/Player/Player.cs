using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance;

    // health
    public Health playerHealth = new Health(2);

    // audio
    public AudioManager audioManager;
    

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void GetDamage(float damage)
    {
        playerHealth.TakeDamage(damage);
    }
}
