using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementSystem : MonoBehaviour
{
    public static TowerPlacementSystem instance;
    [Header("Tower Placement Settings")]
    [SerializeField] private GameObject towerSpawned;
    [SerializeField] private PlacementIndicator placementIndicator;
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Grid grid;
    [Header("Tower Data")]
    [SerializeField] private TowerData towerData;
    [SerializeField] private List<TowerInfo> towerInfos;

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

    public void Update()
    {
        MoveTowerObj(towerSpawned);
        MovePlacementIndicator();
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTowerObj(towerSpawned);
        }
    }

    public GameObject SpawnTowerObj(TowerInfo tower)
    {
        var mousePos = GetMouseWorldPosition();
        var newTower = Instantiate(tower.towerPrefab, mousePos, Quaternion.identity);
        towerSpawned = newTower;
        placementIndicator.CurrentTower = towerSpawned;
        placementIndicator.Show(true);
        return newTower;
    }

    public void MoveTowerObj(GameObject towerObj)
    {
        if (towerObj == null) return;
        var mousePos = GetMouseWorldPosition();
        var gridPos = grid.WorldToCell(mousePos);
        towerObj.transform.position = grid.CellToWorld(gridPos);
    }

    private void MovePlacementIndicator()
    {
        var mousePos = GetMouseWorldPosition();
        var gridPos = grid.WorldToCell(mousePos);
        placementIndicator.SetPosition(grid.CellToWorld(gridPos));
    }

    public void PlaceTowerObj(GameObject towerObj)
    {
        if (towerObj == null) return;
        if (!placementIndicator.IsValidPosition()) return;
        towerObj.transform.position = GetMouseWorldPosition();
        towerSpawned = null;
        placementIndicator.CurrentTower = towerSpawned;
        placementIndicator.Show(false);
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
}
