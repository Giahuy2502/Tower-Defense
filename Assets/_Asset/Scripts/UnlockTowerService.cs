using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;

public class UnlockTowerService : MonoBehaviour
{
    private readonly EconomySystem economySystem = new EconomySystem();
    private readonly TowerCost towerCost = new TowerCost();
    private readonly ITowerUnlockHandler handler;
    public UnlockTowerService(EconomySystem economySystem, TowerCost towerCost,ITowerUnlockHandler handler)
    {
        this.economySystem = economySystem;
        this.towerCost = towerCost;
        this.handler = handler;
    }

    public void TryUnlockTower(TowerType type, int index)
    {
        int cost = towerCost.GetCost(type);
        if (cost <= 0 || economySystem.Gem < cost)
        {
            handler.OnUnlockFailed(type);
            return;
        }

        economySystem.IncreaseGem(-cost);
        handler.OnTowerUnlocked(type, index);
    }
}
