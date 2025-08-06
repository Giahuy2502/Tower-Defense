
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class GridManager : MonoBehaviour
{
    private PathGenerator pathGenerator;
    [SerializeField] private int gridWidth = 16;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private int minPathSize = 25;
    [SerializeField] private GameObject pathTile;

    [SerializeField] private  List<GridCellObject> pathCellObjects;
    [SerializeField] private List<GridCellObject> sceneryCellObjects;
    /// <summary>
    /// random path cho đến khi pathCell.Count >= min path Cell.
    /// </summary>
    private void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        List<Vector2Int> pathCells = pathGenerator.GeneratePath();
        var pathSize = pathCells.Count;
        while (pathSize < minPathSize)
        {
            pathCells = pathGenerator.GeneratePath();
            while(pathGenerator.GenerateCrossRoads()); // tạo nhiều ngã 4 cho đến khi không tạo thêm được.
            pathSize = pathCells.Count;
        }
        StartCoroutine(LayGridCells(pathCells));
    }

    private IEnumerator LayGridCells(List<Vector2Int> pathCells)
    {
        yield return StartCoroutine(LayPathCells(pathCells));
        yield return StartCoroutine(LaySceneryCells());
        yield return null;
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (var pathCell in pathCells)
        {
            Debug.Log($"PathCel: {pathCell.x}, {pathCell.y}"+$" neightbour value : {pathGenerator.GetCellNeighbourValue(pathCell.x, pathCell.y)}");
            var neighbourValue = pathGenerator.GetCellNeighbourValue(pathCell.x, pathCell.y);
            var cellPrefab = pathCellObjects[neighbourValue].CellPrefab;
            var yRotate = pathCellObjects[neighbourValue].YRotation;
            Quaternion yRotation = Quaternion.Euler(0, yRotate, 0);
            Instantiate(cellPrefab, new Vector3(pathCell.x, 0f, pathCell.y),yRotation);
           
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    IEnumerator LaySceneryCells()
    {
        yield return null;
        for (int y = gridHeight-1; y>=0; y--)
        {
            for (int x = 0 ; x< gridWidth; x++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    var randomScenearyCellIndex = Random.Range(0, sceneryCellObjects.Count);
                    var sceneryCellPrefab = sceneryCellObjects[randomScenearyCellIndex].CellPrefab;
                    var pos = new Vector3(x, 0, y);
                    Instantiate(sceneryCellPrefab, pos, Quaternion.identity);
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}
