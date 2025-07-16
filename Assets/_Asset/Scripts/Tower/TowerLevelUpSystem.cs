using System;
using System.Collections;
using System.Collections.Generic;
using _Asset.Scripts.MyAsset;
using UnityEngine;

public class TowerLevelUpSystem : MonoBehaviour
{
    [SerializeField] private TowerData towerData;
    [SerializeField] private GameObject towerToUpdate;
    
    private Dictionary<TowerType,List<TowerInfo>> towers = new ();

    private void Start()
    {
        towers = towerData.towerLevelUps;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            towerToUpdate = GetTowerToUpdate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelUpTower(towerToUpdate);
        }
    }
    public void LevelUpTower(GameObject tower)
    {
        if(tower == null) return;
        var baseTower = tower.GetComponent<BaseTower>();
        var towerType = baseTower.TowerType;
        var listTower = towers[towerType]; 
        var towerPos = tower.transform.position;
        var towerLevel = baseTower.TowerLevel;
        if (towerLevel == listTower.Count) return;
        var newTowerData = listTower[towerLevel];
        var newTowerPrefabs = newTowerData.towerPrefab;
        Destroy(tower);
        var newTower = Instantiate(newTowerPrefabs, towerPos, Quaternion.identity);
        var towerBase = newTower.GetComponent<BaseTower>();
        towerBase.InitializeTower(newTowerData);
    }

    public GameObject GetTowerToUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Collider[] colliders = Physics.OverlapSphere(hitPoint, 0.25f);
            foreach (var col in colliders)
            {
                if (col.CompareTag("Tower"))
                {
                    Debug.Log($"Click On Tower: {col.gameObject.name}");
                    return col.gameObject;
                }
            }
        }

        return null;
    }
}
