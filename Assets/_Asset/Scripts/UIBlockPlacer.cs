using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIBlockPlacer : MonoBehaviour
{
    [SerializeField] private List<Button> towerUI;
    [SerializeField] private TowerData towers;
    private TowerPlacementSystem towerPlacementSystem=>TowerPlacementSystem.instance;

    private void Start()
    {
        towerUI[0].onClick.AddListener(()=>towerPlacementSystem.SpawnTowerObj(towers.Towers[0].towerPrefab));
    }
    
}
