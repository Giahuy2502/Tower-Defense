using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMonster : MonoBehaviour
{
    [SerializeField] private float healthMax = 100f;
    [SerializeField] private float health = 100f;

    public float Health
    {
        get => health;
        set => health = value;
    }

    public float HealthMax
    {
        get => healthMax;
        set => healthMax = value;
    }

    private void OnEnable()
    {
        health = healthMax;
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0 || damage <= 0) return;
        Health -= damage;
    }

    public void Heal(float heal)
    {
        if(health <=0 || heal <= 0) return;
        Health += heal;
    }

    public bool IsDead()
    {
        return Health <= 0;
    }
}
