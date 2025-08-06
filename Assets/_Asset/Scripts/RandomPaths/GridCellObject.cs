using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GridCellObject", menuName = "MapGenerator/GridCellObject", order = 1)]
public class GridCellObject : ScriptableObject
{
    enum CellType {Path,Ground}
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int yRotation;
    [SerializeField] private CellType cellType;

    public GameObject CellPrefab
    {
        get => cellPrefab;
        set => cellPrefab = value;
    }

    public int YRotation
    {
        get => yRotation;
        set => yRotation = value;
    }
}

