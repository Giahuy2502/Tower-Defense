using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointsManager : MonoBehaviour
{ 
    [SerializeField] private GameObject wayPointPrefab;
    [SerializeField] private List<GameObject> wayPoints;
    
    public void SetWayPoints(List<Vector2Int> pathCells,PathGenerator pathGenerator)
    {
        wayPoints.Clear();
        foreach (var pathCell in pathCells)
        {
            Debug.Log($"PathCel: {pathCell.x}, {pathCell.y}"+$" neightbour value : {pathGenerator.GetCellNeighbourValue(pathCell.x, pathCell.y)}");
            if (pathGenerator.IsRoadNode(pathCell))
            {
                Vector3 position = new Vector3(pathCell.x, 0, pathCell.y);
                var roadNode = Instantiate(wayPointPrefab, position, Quaternion.identity);
                roadNode.transform.parent = transform;
                wayPoints.Add(roadNode);
            }
        }
        
    }
    public List<GameObject> GetWayPoints()
    {
        return wayPoints;
    }
}
