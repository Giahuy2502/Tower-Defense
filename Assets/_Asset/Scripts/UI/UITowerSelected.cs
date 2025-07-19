using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using _Asset.Scripts.MyAsset;
using static GameUltis;

public class UITowerSelected : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI towerNameTxt;
    [SerializeField] private Button towerInfoBtn;
    [SerializeField] private Button towerUpdatebtn;
    [SerializeField] private UITowerInfo uiTowerInfo;

    [Header("UI Detection")]
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private UnityEngine.EventSystems.EventSystem eventSystem;

    private TowerInfo currentTowerInfo;
    private GameObject towerSelected;
    private bool shouldHide = false;
    private bool canHide = false;

    private TowerLevelUpSystem towerLevelUpSystem => TowerLevelUpSystem.Instance;

    private void Awake()
    {
        towerInfoBtn.onClick.AddListener(OnTowerInfoBtn);
        towerUpdatebtn.onClick.AddListener(OnTowerUpdateBtn);
        Hide(this.gameObject);
        EventSystem.Subscribe<BaseTower>(EventName.SelectTower, OnSelectedTower);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && canHide && gameObject.activeSelf)
        {
            Vector2 pos = Input.mousePosition;
            Debug.Log("OnExit (Editor)");

            if (!IsPointerOverUI(pos))
            {
                Exit();
            }
        }
#else
        if (Input.touchCount > 0 && canHide && gameObject.activeSelf)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!IsPointerOverUI(touch.position, touch.fingerId))
                {
                    Exit();
                }
            }
        }
#endif
    }

    private bool IsPointerOverUI(Vector2 screenPos, int fingerId = -1)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = screenPos;
        if (fingerId >= 0)
            pointerData.pointerId = fingerId;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        foreach (var result in results)
        {
            Debug.Log("Bạn vừa click vào UI: " + result.gameObject.name);
        }

        return results.Count > 0;
    }

    private void OnSelectedTower(BaseTower tower)
    {
        if (tower.Info.towerName.Trim() == "") return;

        currentTowerInfo = tower.Info;
        towerSelected = tower.gameObject;

        Show(gameObject);
        UpdateTowerNameTxt();
        StartCoroutine(EnableCanHideOnNextFrame());
    }

    IEnumerator EnableCanHideOnNextFrame()
    {
        yield return null;
        canHide = true;
    }

    private void UpdateTowerNameTxt()
    {
        var towerName = currentTowerInfo.towerName;
        var towerLevel = currentTowerInfo.level.ToString();
        towerNameTxt.text = towerName + " (level: " + towerLevel + ")";
    }

    private void OnTowerInfoBtn()
    {
        uiTowerInfo.CurrentTowerInfo = currentTowerInfo;
        Show(uiTowerInfo.gameObject);
        Exit();
    }

    private void OnTowerUpdateBtn()
    {
        towerLevelUpSystem.LevelUpTower(towerSelected);
        Exit();
    }

    private void Exit()
    {
        canHide = false;
        shouldHide = false;
        Hide(gameObject);
        Debug.Log("Hide");
    }
}
