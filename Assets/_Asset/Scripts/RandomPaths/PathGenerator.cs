using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{
    private int width, height;
    private List<Vector2Int> pathCells = new();

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
/// <summary>
/// x là chiều ngang, y là chiều dọc
/// </summary>
/// <returns></returns>
    public List<Vector2Int> GeneratePath()
    {
        pathCells = new();
        int y = (int)(height / 2f);
        int x = 0;
        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));
            bool validMove = false;
            while (!validMove)
            {
                int move = Random.Range(0, 3);
                if (move == 0 || x % 2 == 0|| x>(width-2))
                {
                    x++;   // turn right
                    validMove = true;
                }
                else if (move == 1 && CellIsEmpty(x,y+1) && y<(height-2))
                {
                    y++; // go straight
                    validMove = true;
                }
                else if (move == 2 && CellIsEmpty(x,y-1) && y>2)
                {
                    y--;
                    validMove = true;
                }
            }
        }

        return pathCells;
    }

    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    
    private bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }
    /// <summary>
    ///
    /// Các Cell khi biểu diễn trên ma trận 2 chiều sẽ có các cell (neightbour) tương ứng với 4 hướng
    /// => khi ta set các giá trị cho các neightbour ta có thể tính được tổng các giá trị các neightbour
    ///        8
    ///      2 C 4
    ///        1
    ///  => thông qua tổng các gía trị này, ta sẽ biết được cell hiện tại thuộc loại cell nào ( đi ngang, đi dọc hay là rẽ lên, rẽ xuống)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public int GetCellNeighbourValue(int x, int y)
    {
        int returnValue= 0;
        if (CellIsTaken(x, y - 1))
        {
            returnValue += 1;
        }

        if (CellIsTaken(x, y + 1))
        {
            returnValue += 8;
        }

        if (CellIsTaken(x - 1, y))
        {
            returnValue += 2;
        }

        if (CellIsTaken(x + 1, y))
        {
            returnValue += 4;
        }
        return returnValue;
    }

    public bool GenerateCrossRoads()
    {
        for (int i = 0; i < pathCells.Count; i++)
        {
            Vector2Int pathCell = pathCells[i];
            if (!(pathCell.x > 3) ||!(pathCell.x < width - 4)|| !(pathCell.y > 3) || !(pathCell.y < height - 3))
            {
                continue;
            }
            if (CellIsEmpty(pathCell.x, pathCell.y + 3) && CellIsEmpty(pathCell.x + 1, pathCell.y + 3) && CellIsEmpty(pathCell.x + 2, pathCell.y + 3)
                &&CellIsEmpty(pathCell.x-1, pathCell.y + 2) && CellIsEmpty(pathCell.x, pathCell.y + 2) && CellIsEmpty(pathCell.x + 1, pathCell.y + 2)&&CellIsEmpty(pathCell.x + 2, pathCell.y + 2)&& CellIsEmpty(pathCell.x + 3, pathCell.y + 2)
                &&CellIsEmpty(pathCell.x-1, pathCell.y + 1) && CellIsEmpty(pathCell.x, pathCell.y + 1) && CellIsEmpty(pathCell.x + 1, pathCell.y + 1)&&CellIsEmpty(pathCell.x + 2, pathCell.y + 1)&& CellIsEmpty(pathCell.x + 3, pathCell.y + 1)
                &&CellIsEmpty(pathCell.x+1, pathCell.y) && CellIsEmpty(pathCell.x+2, pathCell.y) && CellIsEmpty(pathCell.x+3, pathCell.y)
                &&CellIsEmpty(pathCell.x+1, pathCell.y -1) && CellIsEmpty(pathCell.x+2, pathCell.y -1))
            {
                pathCells.InsertRange(i+1, new List<Vector2Int>{new Vector2Int(pathCell.x+1,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y+1),new Vector2Int(pathCell.x+2,pathCell.y+2),new Vector2Int(pathCell.x+1,pathCell.y+2),new Vector2Int(pathCell.x,pathCell.y+2),new Vector2Int(pathCell.x,pathCell.y+1)});
                return true;
            }
            if (CellIsEmpty(pathCell.x+1, pathCell.y + 1) && CellIsEmpty(pathCell.x + 2, pathCell.y + 1)
                &&CellIsEmpty(pathCell.x+1, pathCell.y) && CellIsEmpty(pathCell.x+2, pathCell.y) && CellIsEmpty(pathCell.x + 3, pathCell.y)
                &&CellIsEmpty(pathCell.x-1, pathCell.y - 1) && CellIsEmpty(pathCell.x, pathCell.y-1) && CellIsEmpty(pathCell.x +1, pathCell.y - 1)&&CellIsEmpty(pathCell.x +2, pathCell.y -1)&& CellIsEmpty(pathCell.x +3, pathCell.y -1)
                &&CellIsEmpty(pathCell.x-1, pathCell.y - 2) && CellIsEmpty(pathCell.x, pathCell.y-2) && CellIsEmpty(pathCell.x +1, pathCell.y - 2)&&CellIsEmpty(pathCell.x +2, pathCell.y -2)&& CellIsEmpty(pathCell.x +3, pathCell.y -2)
                &&CellIsEmpty(pathCell.x, pathCell.y-3) && CellIsEmpty(pathCell.x+1, pathCell.y-3) && CellIsEmpty(pathCell.x+2, pathCell.y-3))
            {
                pathCells.InsertRange(i+1, new List<Vector2Int>{new Vector2Int(pathCell.x+1,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y),new Vector2Int(pathCell.x+2,pathCell.y-1),new Vector2Int(pathCell.x+2,pathCell.y-2),new Vector2Int(pathCell.x+1,pathCell.y-2),new Vector2Int(pathCell.x,pathCell.y-2),new Vector2Int(pathCell.x,pathCell.y-1)});
                return true;
            }
        }
        return false;
    }
}
