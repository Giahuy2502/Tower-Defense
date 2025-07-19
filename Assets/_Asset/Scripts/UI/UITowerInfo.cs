using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameUltis;
public class UITowerInfo : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI towerNameText;
    [SerializeField] private TextMeshProUGUI towerDamageText;
    [SerializeField] private TextMeshProUGUI towerRangeText;
    [SerializeField] private Button updateButton;
    private TowerInfo currentTowerInfo;

    public TowerInfo CurrentTowerInfo
    {
        get => currentTowerInfo;
        set => currentTowerInfo = value;
    }

    private void Awake()
    {
        exitButton.onClick.AddListener(OnExit);
        updateButton.onClick.AddListener(OnUpdateTower);
    }

    private void OnEnable()
    {
        UpdateInfoTower();
    }

    private void UpdateInfoTower()
    {
        towerNameText.text = currentTowerInfo.towerName+"( Level "+currentTowerInfo.level+")";
        towerDamageText.text = "Damage: " + currentTowerInfo.damage;
        towerRangeText.text = "Range: " + currentTowerInfo.range;
    }

    public void OnExit()
    {
        Hide(gameObject);
    }

    public void OnUpdateTower()
    {
        
    }
}
