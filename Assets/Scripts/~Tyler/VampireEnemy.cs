using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    }
}
