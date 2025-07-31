using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static GameUltis;
public class HealthMonster : MonoBehaviour
{
    [SerializeField] private float healthMax = 100f;
    [SerializeField] private float health = 100f;
    [SerializeField] private UIHeathBar healthBar;
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
        Show(healthBar.gameObject);
        healthBar.SetMaxHeath(healthMax);
    }

    public void TakeDamage(float damage)
    {
        if (health <= 0 || damage <= 0) return;
        Health -= damage;
        healthBar.SetCurrentHeath(Health);
        if(health<=0) Hide(healthBar.gameObject);
    }

    public void Heal(float heal)
    {
        if(health <=0 || heal <= 0) return;
        Health += heal;
        healthBar.SetCurrentHeath(Health);
    }

    public bool IsDead()
    {
        return Health <= 0;
    }
}
