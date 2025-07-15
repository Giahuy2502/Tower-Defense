using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUltis;

public class TowerPlacementSystem : MonoBehaviour
{
    public static TowerPlacementSystem instance;
    [Header("Tower Placement Settings")]
    [SerializeField] private GameObject towerSpawned;
    [SerializeField] private PlacementIndicator placementIndicator;
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private PreviewSystem previewSystem;
    [Header("Tower Data")]
    [SerializeField] private TowerData towerData;
    [SerializeField] private List<TowerInfo> towerInfos;

    private GridData gridData => MapManager.instance.GridData;
    public List<TowerInfo> TowerInfos
    {
        get => towerInfos;
        set => towerInfos = value;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        GetData();
    }

    private void Start()
    {
        Hide(gridVisualization);
        Hide(placementIndicator.gameObject);
    }

    public void Update()
    {
        MoveTowerObj();
        MovePlacementIndicator();
        if (Input.GetMouseButtonDown(0))
        {
            ExitPlacementTower(towerSpawned);
        }
    }

    public void FixedUpdate()
    {
        var isValid = CheckValidPos();
        previewSystem.UpdatePreview(isValid);
    }

    public void StartPlacementTower(TowerInfo tower)
    {
        towerSpawned = tower.towerPrefab;
        placementIndicator.CurrentTower = towerSpawned;
        placementIndicator.Show(true);
        Show(gridVisualization);
        previewSystem.StartPreview(towerSpawned);
    }

    private void MoveTowerObj()
    {
        var pos = GetCellPosition(GetMouseWorldPosition());
        previewSystem.MovePreview(pos);
    }

    private void MovePlacementIndicator()
    {
        var pos = GetCellPosition(GetMouseWorldPosition());
        placementIndicator.SetPosition(pos);
    }

    private void ExitPlacementTower(GameObject towerObj)
    {
        if (towerObj == null) return;
        if (!CheckValidPos()) return;
        AddToGridData();
        var pos = GetCellPosition(GetMouseWorldPosition());
        var newTowerObj = Instantiate(towerObj, pos, Quaternion.identity);
        towerSpawned = null;
        placementIndicator.CurrentTower = towerSpawned;
        placementIndicator.Show(false);
        Hide(gridVisualization);
        previewSystem.StopPreview();
        return;
    }
    private Vector3 GetMouseWorldPosition()
    {
        var mousePos = Input.mousePosition;
        Vector3 lastPos = new();
        mousePos.z = sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            lastPos = hit.point;
        }

        return lastPos;
    }

    private void GetData()
    {
        towerInfos = new List<TowerInfo>();
        foreach (var tower in towerData.Towers)
        {
            var towerList = tower.towers;
            towerInfos.Add(towerList[0]);
        }
    }

    private Vector3 GetCellPosition(Vector3 mousePos)
    {
        var gridPos = grid.WorldToCell(mousePos);
        return grid.CellToWorld(gridPos);
    }

    private bool CheckValidPos()
    {
        if (!placementIndicator.gameObject.activeSelf) return false;
        var gridPosInt = GetCellPositionInt(grid,GetMouseWorldPosition());
        var isValid = gridData.CanPlaceObjectAt(gridPosInt, Vector2Int.one);
        placementIndicator.SetValid(isValid);
        return isValid;
    }

    void AddToGridData()
    {
        var gridPosInt = GetCellPositionInt(grid,GetMouseWorldPosition());
        gridData.AddObjectAt(gridPosInt,Vector2Int.one,towerSpawned);
    }
}
