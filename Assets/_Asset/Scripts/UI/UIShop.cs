using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUltis;
public class UIShop : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private List<GameObject> products;
    [SerializeField] private TowerData data;
    [SerializeField] private GameObject shopPopup;
    [Header("UI Confirm Popup")]
    [SerializeField] private GameObject confirmPopup;
    [SerializeField] private TextMeshProUGUI confirmText;
    [SerializeField] private Button no;
    [SerializeField] private Button yes;
    private int selectedTowerIndex;
    private void Awake()
    {
        closeButton.onClick.AddListener(OnClose);
        no.onClick.AddListener(OnNo);
        yes.onClick.AddListener(OnYes);
        foreach (var product in products)
        {
            var button = product.GetComponent<Button>();
            var uiProduct = product.GetComponent<UIProduct>();
            var towerIndex = uiProduct.TowerIndex;
            button.onClick.AddListener(() =>
            {
                OnConfirm(towerIndex);
            });
        }
        UnlockDefaultTower();
    }

    private void UnlockDefaultTower()
    {
        var uiDefaultTower = products[0].GetComponent<UIProduct>();
        uiDefaultTower.Unlock();
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(OnClose);
    }

    void OnClose()
    {
        SceneManager.UnloadSceneAsync(3);
    }

    void OnConfirm(int index)
    {
        selectedTowerIndex = index;
        Show(confirmPopup);
        Hide(shopPopup);
        UpdateConfirmText();
    }

    void OnNo()
    {
        Hide(confirmPopup);
        Show(shopPopup);
    }

    void OnYes()
    {
        var uiProduct = products[selectedTowerIndex].GetComponent<UIProduct>();
        uiProduct.Unlock();
        Hide(confirmPopup);
        Show(shopPopup);
    }

    void UpdateConfirmText()
    {
        var tower = data.Towers[selectedTowerIndex];
        var towerName = tower.towerType.ToString();
        confirmText.text = $"Are you sure you want to buy the tower {towerName} for 0 gems?";
    }
}
