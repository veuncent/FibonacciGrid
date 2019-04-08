using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.DomainServices
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
        /// <returns>Returns a list of all cells that have a Fibonacci value after updating. Coordinates of each cell are stored in a Tuple</returns>
        public List<Tuple<int, int>> UpdateCell(IGrid grid, int increment, int rowIndex, int columnIndex)
        {
            var fibonacciCells = new List<Tuple<int, int>>();
            for (var row = 0; row < grid.FibonacciGrid.GetLength(0); row++)
            {
                var gridCell = grid.FibonacciGrid[row, columnIndex];
                IncrementCellValue(increment, gridCell);
                AddFibonacciCell(gridCell, row, columnIndex, fibonacciCells);
            }

            for (var column = 0; column < grid.FibonacciGrid.GetLength(1); column++)
            {
                if (IsAlreadyProcessed(column, columnIndex))
                {
                    continue;
                }

                var gridCell = grid.FibonacciGrid[rowIndex, column];
                IncrementCellValue(increment, gridCell);
                AddFibonacciCell(gridCell, rowIndex, column, fibonacciCells);
            }

            return fibonacciCells;
        }

        private static void IncrementCellValue(int increment, GridCell gridCell)
        {
            gridCell.IncrementGridCellValue(increment);
        }

        private static void AddFibonacciCell(GridCell gridCell, int rowIndex, int columnIndex, ICollection<Tuple<int, int>> fibonacciCells)
        {
            if (gridCell.IsFibonacci)
            {
                fibonacciCells.Add(Tuple.Create(rowIndex, columnIndex));
            }
        }

        private static bool IsAlreadyProcessed(int column, int columnIndex)
        {
            return column == columnIndex;
        }
    }
}
