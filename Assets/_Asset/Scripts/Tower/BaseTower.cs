using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseTower : MonoBehaviour
{
    [Header("Tower Stats")]
    [SerializeField] private TowerType towerType;
    [SerializeField] private int towerLevel;
    [SerializeField] protected float baseTowerDamage = 20f;
    [SerializeField] protected float rotateSpeed = 5f;
    [SerializeField] private float baseTowerRange = 15f;
    [SerializeField] private float upgradeCost;
    [Header("Base Tower")]
    [SerializeField] protected GameObject barrel;
    [SerializeField] protected TowerAttack towerAttack;
    
    private TowerInfo towerInfo;
    private bool isInitialized = false;
    private MapManager mapManager => MapManager.instance;
    private EconomySystem economySystem => EconomySystem.instance;
    public int TowerLevel
    {
        get => towerLevel;
        set => towerLevel = value;
    }

    public TowerType TowerType
    {
        get => towerType;
        set => towerType = value;
    }

    public float BaseTowerRange
    {
        get => baseTowerRange;
        set => baseTowerRange = value;
    }

    public float RotateSpeed
    {
        get => rotateSpeed;
        set => rotateSpeed = value;
    }

    public float BaseTowerDamage
    {
        get => baseTowerDamage;
        set => baseTowerDamage = value;
    }

    public TowerInfo Info
    {
        get => towerInfo;
        set => towerInfo = value;
    }


    protected void Start()
    {
        
    }
    protected void OnMouseDown()
    {
        if (!isInitialized) return;
        SelectedTower();
    }


    public void InitializeTower(TowerInfo towerInfo)
    {
        this.towerInfo = towerInfo;
        towerLevel = towerInfo.level;
        baseTowerRange = towerInfo.range;
        baseTowerDamage = towerInfo.damage;
        upgradeCost = towerInfo.upgradeCost;
        economySystem.BuyTower(towerInfo);
        EventSystem.Invoke(EventName.UpdateGoldTxt);
        isInitialized = true;
    }

    public void SelectedTower()
    {
        EventSystem.Invoke<BaseTower>(EventName.SelectTower,this);
    }
    
}
