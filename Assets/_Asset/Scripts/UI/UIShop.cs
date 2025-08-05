using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameUltis;
public class UIShop : MonoBehaviour,ITowerUnlockHandler
{
    [SerializeField] private Button closeButton;
    [SerializeField] private List<GameObject> products;
    [SerializeField] private TowerData data;
    [SerializeField] private TowerCost towerCost;
    [SerializeField] private GameObject shopPopup;
    [Header("UI Confirm Popup")]
    [SerializeField] private GameObject confirmPopup;
    [SerializeField] private TextMeshProUGUI confirmText;
    [SerializeField] private Button no;
    [SerializeField] private Button yes;
    [Header("UI Warning Popup")]
    [SerializeField] private GameObject warningPopup;
    private int selectedTowerIndex;
    private UnlockTowerService unlockService;
    private void Awake()
    {
        unlockService = new UnlockTowerService(EconomySystem.instance, towerCost, this);
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
    public void OnYes()
    {
        var tower = data.Towers[selectedTowerIndex];
        unlockService.TryUnlockTower(tower.towerType, selectedTowerIndex);
    }

    void UpdateConfirmText()
    {
        var tower = data.Towers[selectedTowerIndex];
        var towerName = tower.towerType.ToString();
        int cost = towerCost.GetCost(tower.towerType);
        confirmText.text = $"Are you sure you want to buy the tower {towerName} for {cost} gems?";
    }
    public void OnTowerUnlocked(TowerType type, int index)
    {
        products[index].GetComponent<UIProduct>().Unlock();
        Hide(confirmPopup);
        Show(shopPopup);
    }

    public void OnUnlockFailed(TowerType type)
    {
        Debug.LogWarning($"Failed to unlock {type}");
        Hide(confirmPopup);
        Show(warningPopup);
        Show(shopPopup);
    }
}