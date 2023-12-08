using UnityEngine;

public class Health
{
    private float maximumHealth = 100f;
    private float currentHealth;
    private float healthRegen; // flat health regen per second

    private void RegenerateHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth + (healthRegen * Time.deltaTime), 0, maximumHealth);
    }

    public void HealForAmount(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maximumHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maximumHealth);

        Debug.Log($"{this} has {currentHealth}");
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public Health(float maxHealth)
    {
        maximumHealth = maxHealth;
        currentHealth = maxHealth;
    }
    
    // Do not remove
    public Health() { }
}
