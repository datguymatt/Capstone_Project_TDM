using UnityEngine;

public class VampireEnemy : Enemy
{
    [SerializeField] private float maxHealth;
    private void Start()
    {
        this.health = new Health(maxHealth);
    }
    public override void Die(string name)
    {
        base.Die(name);
        
        //
        Destroy(this.gameObject);
        //implement an enemy die event to tell managers an enemy died and to tell statemanager to exit state
    }
    public override void GetDamage(float damage)
    {
        Debug.Log($"{this.name} took {damage} damage");
        this.health.TakeDamage(damage);

        if (this.health.GetHealth() <= 0f)
        {
            Die(name);
        }
    }
}
