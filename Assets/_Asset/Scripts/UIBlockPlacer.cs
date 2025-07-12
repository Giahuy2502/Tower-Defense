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
        for (int i = 0; i < towerUI.Count; i++)
        {
            var index = i;
            towerUI[i].onClick.AddListener(()=>towerPlacementSystem.StartPlacementTower(towerPlacementSystem.TowerInfos[index]));
        }
    }
    
}
