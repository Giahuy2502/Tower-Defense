using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] private string towerName;
    [SerializeField] private int towerLevel;
    [SerializeField] protected float baseTowerHealth = 100f;
    [SerializeField] protected float baseTowerDamage = 20f;
    [SerializeField] protected float rotateSpeed = 5f;
    [Header("Base Tower")]
    [SerializeField] protected GameObject barrel;
    [SerializeField] protected TowerAttack towerAttack;

    [SerializeField] protected List<GameObject> activeMonsters = new List<GameObject>();
    
    private MapManager mapManager => MapManager.instance;
    public string TowerName
    {
        get => towerName;
        set => towerName = value;
    }
    public int TowerLevel
    {
        get => towerLevel;
        set => towerLevel = value;
    }

    private Coroutine attackCoroutine;
    protected void Start()
    {
        activeMonsters = mapManager.ActiveMonsters;
        towerAttack.Damage = baseTowerDamage;
    }
    protected void Update()
    {
        RotateBarrelToMonster();
        DoAttack();
    }
    private void DoAttack()
    {
        if (towerAttack.attackCoroutine != null) return;

        if (activeMonsters.Count > 0)
        {
            towerAttack.StartAttack(activeMonsters[0]);
        }
    }
    private void RotateBarrelToMonster()
    {
        if (activeMonsters.Count <= 0 || activeMonsters[0]==null) return;
        var targetMonster = activeMonsters[0];
        var direction = targetMonster.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }
}
