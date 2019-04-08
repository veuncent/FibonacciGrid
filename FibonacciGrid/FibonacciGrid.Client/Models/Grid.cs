using System.Collections.Generic;
using FibonacciGrid.Client.Services.DomainServices;

namespace FibonacciGrid.Client.Models
{
    public class Grid
    {
        public GridCell[,] FibonacciGrid { get; }

        public Grid(IFibonacciCheckerService fibonacciCheckerService, int gridSize = 50)
        {
            FibonacciGrid = new GridCell[gridSize, gridSize];

            for (var i = 0; i < gridSize; i++)
            {
                for (var j = 0; j < gridSize; j++)
                {
                    FibonacciGrid[i, j] = new GridCell(fibonacciCheckerService);
                }
            }
        }
    }

    public class GridList
    {
        public List<List<GridCell>> FibonacciGrid { get; }

        public GridList(IFibonacciCheckerService fibonacciCheckerService, int gridSize = 50)
        {
            FibonacciGrid = new List<List<GridCell>>(50);

            for (var i = 0; i < gridSize; i++)
            {
                FibonacciGrid.Add(new List<GridCell>(50));

                for (var j = 0; j < gridSize; j++)
                {
                    FibonacciGrid[i].Add(new GridCell(fibonacciCheckerService));
                }
            }
        }
    }
}
