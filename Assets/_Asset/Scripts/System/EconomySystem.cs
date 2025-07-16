using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EconomySystem : MonoBehaviour
{
    [SerializeField] private int gold;
    [SerializeField] private int gem;
    [SerializeField] private int exp;
    public static EconomySystem instance;

    public int Gold
    {
        get => gold;
        set => gold = value;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseGold(int amount)
    {
        gold += amount;
    }

    public void BuyTower(TowerInfo towerInfo)
    {
        var cost = towerInfo.upgradeCost;
        if (gold < cost)
        {
            Debug.LogError("Not enough Economy");
            return;
        }
        gold -= cost;
    }

    public void IncreaseGem(int amount)
    {
        gem += amount;
    }

    public void IncreaseExp(int amount)
    {
        exp += amount;
    }

    public void LevelUp(LevelRewardConfig reward)
    {
        IncreaseGold(reward.rewardGold);
        IncreaseGem(reward.rewardGems);
        IncreaseExp(reward.rewardExp);
    }
}
