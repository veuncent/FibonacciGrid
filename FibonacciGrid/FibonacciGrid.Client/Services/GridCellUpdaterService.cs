using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public class GridCellUpdaterService : IGridCellUpdaterService
    {
        /// <summary>
        /// Updates a selected cell, as well as all cells in the same row and column
        /// </summary>
        /// <param name="grid">The cell grid on which the cell is located</param>
        /// <param name="increment">The amount with which to increment all affected cells</param>
        /// <param name="rowIndex">The row index of the selected cell</param>
        /// <param name="columnIndex">The column index of the selected cell</param>
        /// <returns>Returns a list of all cells that have a Fibonacci value after updating</returns>
        public List<GridCell> UpdateCell(Grid grid, int increment, int rowIndex, int columnIndex)
        {
            var affectedCells = new List<GridCell>();
            for (var row = 0; row < grid.FibonacciGrid.GetLength(0); row++)
            {
                affectedCells.Add(grid.FibonacciGrid[row, columnIndex]);
            }

            for (var column = 0; column < grid.FibonacciGrid.GetLength(1); column++)
            {
                if (column != columnIndex)
                {
                    affectedCells.Add(grid.FibonacciGrid[rowIndex, column]);
                }
            }

            var fibonacciCells = new List<GridCell>();
            foreach (var gridCell in affectedCells)
            {
                gridCell.IncrementGridCellValue(increment);

                if (gridCell.IsFibonacci)
                {
                    fibonacciCells.Add(gridCell);
                }
            }

            return fibonacciCells;
        }


    }
}
