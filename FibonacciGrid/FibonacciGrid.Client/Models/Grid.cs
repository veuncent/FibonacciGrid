using System.Collections.Generic;

namespace FibonacciGrid.Client.Models
{
    public class Grid
    {
        public GridCell[,] FibonacciGrid { get; }

        public Grid(int gridSize = 50)
        {
            FibonacciGrid = new GridCell[gridSize, gridSize];

            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    FibonacciGrid[i, j] = new GridCell();
                }
            }
        }
    }

    public class GridList
    {
        public List<List<GridCell>> FibonacciGrid { get; }

        public GridList(int gridSize = 50)
        {
            FibonacciGrid = new List<List<GridCell>>(50);

            for (var i = 0; i < gridSize; i++)
            {
                FibonacciGrid.Add(new List<GridCell>(50));

                for (var j = 0; j < gridSize; j++)
                {
                    FibonacciGrid[i].Add(new GridCell());
                }
            }
        }
    }
}
