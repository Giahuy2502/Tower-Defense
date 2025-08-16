
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GridManager : MonoBehaviour
{
    private PathGenerator pathGenerator;
    [SerializeField] private int gridWidth = 16;
    [SerializeField] private int gridHeight = 8;
    [SerializeField] private int minPathSize = 25;
    [SerializeField] private GameObject pathTile;
    [SerializeField] private GameObject enviromentObject;
    [SerializeField] private  List<GridCellObject> pathCellObjects;
    [SerializeField] private List<GridCellObject> sceneryCellObjects;
    [SerializeField] private WayPointsManager wayPointsManager;
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
        wayPointsManager.SetWayPoints(pathCells,pathGenerator);
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
            var pathCellObj = Instantiate(cellPrefab, new Vector3(pathCell.x, 0f, pathCell.y),yRotation);
            pathCellObj.transform.parent = enviromentObject.transform;
            pathCellObj.AddComponent<BoxCollider>();
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
                    var sceneryCellObj= Instantiate(sceneryCellPrefab, pos, Quaternion.identity);
                    sceneryCellObj.transform.parent = enviromentObject.transform;
                    sceneryCellObj.AddComponent<BoxCollider>();
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }

    [ContextMenu("Save Map To File")]
    public void SaveMapToFile()
    {
#if UNITY_EDITOR
        if (enviromentObject == null)
        {
            Debug.LogWarning("Không có đối tượng environment để lưu.");
            return;
        }
        string folderPath = "Assets/_Asset/Prefabs/Map";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder("Assets/_Asset/Prefabs", "Map");
        }
        string prefabPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/GeneratedMap.prefab"); // GeneratedMap.prefab là tên prefabs , có thể tùy chỉnh trong code sau.
        PrefabUtility.SaveAsPrefabAsset(enviromentObject, prefabPath);

        Debug.Log($"✅ Saved map prefab at: {prefabPath}");
#else
    Debug.LogWarning("SaveMapToFile chỉ hoạt động trong Editor.");
#endif
    }

}
