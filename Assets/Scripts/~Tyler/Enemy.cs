using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public Health health = new Health();
    public Transform player;
    public RoundManager roundManager;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        roundManager = FindObjectOfType<RoundManager>();
    }

    public virtual void GetDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Die(string name)
    {
        roundManager.KillEnemy();
        Debug.Log($"A {name} has died");
    }
}
