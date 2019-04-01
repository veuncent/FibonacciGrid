using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public class FibonacciNeighborService : IFibonacciNeighborService
    {
        private const int FibonacciSequenceMinimum = 5;

        /// <summary>
        /// Finds neighboring Fibonacci cells for each given cell
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="cellsToCheck"></param>
        /// <returns>List of neighboring Fibonacci cells grouped by cell from input</returns>
        public List<List<GridCell>> FindNeighbors(Grid grid, List<Tuple<int, int>> cellsToCheck)
        {
            var groupedFibonacciCells = new List<List<GridCell>>();
            foreach (var (rowIndex, columnIndex) in cellsToCheck)
            {
                GetNeighboringFibonacciCells(grid, rowIndex, columnIndex, groupedFibonacciCells);
            }

            return groupedFibonacciCells;
        }

        private static void GetNeighboringFibonacciCells(Grid grid, int initialRow, int initialColumn, ICollection<List<GridCell>> groupedFibonacciCells)
        {
            var verticalNeighbors = new List<GridCell>();
            for (var iteratedRow = initialRow -1; iteratedRow >= 0; iteratedRow--)
            {
                if (AddCellOrBreak(grid, iteratedRow, initialColumn, verticalNeighbors)) break;
            }
            verticalNeighbors.Reverse();

            for (var iteratedRow = initialRow; iteratedRow < grid.FibonacciGrid.GetLength(0); iteratedRow++)
            {
                if (AddCellOrBreak(grid, iteratedRow, initialColumn, verticalNeighbors)) break;
            }

            AddGroupedNeighbors(groupedFibonacciCells, verticalNeighbors);


            var horizontalNeighbors = new List<GridCell>();
            for (var iteratedColumn = initialColumn - 1; iteratedColumn >= 0; iteratedColumn--)
            {
                if (AddCellOrBreak(grid, initialRow, iteratedColumn, horizontalNeighbors)) break;
            }
            horizontalNeighbors.Reverse();

            for (var iteratedColumn = initialColumn; iteratedColumn < grid.FibonacciGrid.GetLength(1); iteratedColumn++)
            {
                if (AddCellOrBreak(grid, initialRow, iteratedColumn, horizontalNeighbors)) break;
            }

            AddGroupedNeighbors(groupedFibonacciCells, horizontalNeighbors);
        }

        private static bool AddCellOrBreak(Grid grid, int rowIndex, int columnIndex, ICollection<GridCell> fibonacciCells)
        {
            var gridCell = grid.FibonacciGrid[rowIndex, columnIndex];

            if (!gridCell.IsFibonacci)
            {
                return true;
            }

            fibonacciCells.Add(gridCell);
            return false;
        }

        private static void AddGroupedNeighbors(ICollection<List<GridCell>> groupedFibonacciCells, List<GridCell> neighborCells)
        {
            if (neighborCells.Count >= FibonacciSequenceMinimum)
            {
                groupedFibonacciCells.Add(neighborCells);
            }
        }
    }
}
