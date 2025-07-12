using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData : MonoBehaviour
{
    private Dictionary<Vector3Int, PlacemntData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, GameObject placedObject)
    {
        List<Vector3Int> occupiedPositions = CalculatePositions(gridPosition, objectSize);
        var placementData = new PlacemntData(occupiedPositions, placedObject);
        foreach (var pos in occupiedPositions)
        {
            if (placedObjects.ContainsKey(pos))
            {
                Debug.LogError($"Conflict at {pos} by {placedObject.name}, already occupied by {placedObjects[pos].placementObject.name}");
                throw new Exception("Dictionary already contains a placement at " + pos);
            }
            placedObjects[pos] = placementData;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();

        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }
        return returnVal;
    }
    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
    
        foreach (var pos in positionToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }

        return true;
    }
}

public class PlacemntData
{
    public List<Vector3Int> occupiedPositions = new();
    public GameObject placementObject { get; private set; }

    public PlacemntData(List<Vector3Int> occupiedPositions, GameObject placementObject)
    {
        this.occupiedPositions = occupiedPositions;
        this.placementObject = placementObject;
    }
}
