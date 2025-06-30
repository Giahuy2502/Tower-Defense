using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementSystem : MonoBehaviour
{
    public static TowerPlacementSystem instance;
    [SerializeField] private GameObject towerSpawned;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void Update()
    {
        MoveTowerObj(towerSpawned);
        if(Input.GetMouseButtonDown(0)) PlaceTowerObj(towerSpawned);
    }

    public GameObject SpawnTowerObj(GameObject tower)
    {
        var mousePos = GetMouseWorldPosition();
        var newTower = Instantiate(tower, mousePos, Quaternion.identity);
        towerSpawned = newTower;
        return newTower;
    }

    public void MoveTowerObj(GameObject towerObj)
    {
        if (towerObj == null) return;
        var mousePos = GetMouseWorldPosition();
        towerObj.transform.position = mousePos;
    }

    public void PlaceTowerObj(GameObject towerObj)
    {
        if (towerObj == null) return;
        towerObj.transform.position = GetMouseWorldPosition();
        towerSpawned = null;
        return;
    }
    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    
        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
    
        return Vector3.zero; // Fallback nếu không intersect
    }
}
