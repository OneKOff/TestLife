using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private float cellDistance;
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;

    private Cell[,] _cellMatrix;

    private void Start()
    {
        _cellMatrix = new Cell[gridSizeX, gridSizeY];

        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                _cellMatrix[i, j] = Instantiate(cellPrefab, transform.position + 
                    new Vector3(i * cellDistance, j * cellDistance, 0), Quaternion.identity, transform);
                _cellMatrix[i, j].SetCoords(i, j);
            }
        }
    }

    public void GameCycle()
    {
        var nextGenerationChanges = new List<Cell>();

        foreach (var cell in _cellMatrix)
        {
            var aliveNeighbors = 0;

            if (_cellMatrix[(cell.X + 1 + gridSizeX) % gridSizeX, (cell.Y + 1 + gridSizeY) % gridSizeY].Alive) aliveNeighbors++;
            if (_cellMatrix[(cell.X + 1 + gridSizeX) % gridSizeX, cell.Y].Alive) aliveNeighbors++;
            if (_cellMatrix[(cell.X + 1 + gridSizeX) % gridSizeX, (cell.Y - 1 + gridSizeY) % gridSizeY].Alive) aliveNeighbors++;

            if (_cellMatrix[(cell.X - 1 + gridSizeX) % gridSizeX, (cell.Y + 1 + gridSizeY) % gridSizeY].Alive) aliveNeighbors++;
            if (_cellMatrix[(cell.X - 1 + gridSizeX) % gridSizeX, cell.Y].Alive) aliveNeighbors++;
            if (_cellMatrix[(cell.X - 1 + gridSizeX) % gridSizeX, (cell.Y - 1 + gridSizeY) % gridSizeY].Alive) aliveNeighbors++;

            if (_cellMatrix[cell.X, (cell.Y + 1 + gridSizeY) % gridSizeY].Alive) aliveNeighbors++;
            if (_cellMatrix[cell.X, (cell.Y - 1 + gridSizeY) % gridSizeY].Alive) aliveNeighbors++;

            if (cell.Alive && (aliveNeighbors < 2 || aliveNeighbors > 3) ) { nextGenerationChanges.Add(cell); }
            if (!cell.Alive && aliveNeighbors == 3) { nextGenerationChanges.Add(cell); }
        }

        if (!nextGenerationChanges.Any()) // Rule of game over - if no changes were made
        {
            GameController.Instance.GameStartedTrigger();
        } 

        foreach (var cell in nextGenerationChanges)
        {
            cell.ChangeState();
        }
    }
}
