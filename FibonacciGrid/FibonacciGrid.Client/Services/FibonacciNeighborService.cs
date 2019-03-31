using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public class FibonacciNeighborService : IFibonacciNeighborService
    {
        private const int FibonacciSequenceMinimum = 5;

        public List<List<Tuple<int, int>>> FindNeighbors(Grid grid, List<Tuple<int, int>> cellsToCheck)
        {
            var groupedFibonacciCells = new List<List<Tuple<int, int>>>();
            foreach (var (rowIndex, columnIndex) in cellsToCheck)
            {
                var neighboringFibonacciCells = GetNeighboringFibonacciCells(grid, rowIndex, columnIndex);
                if (neighboringFibonacciCells.Count >= FibonacciSequenceMinimum)
                {
                    groupedFibonacciCells.Add(neighboringFibonacciCells);
                }
            }

            return groupedFibonacciCells;
        }

        private static List<Tuple<int, int>> GetNeighboringFibonacciCells(Grid grid, int rowIndex, int columnIndex)
        {
            var fibonacciCells = new List<Tuple<int, int>>();
            for (var row = rowIndex; row < grid.FibonacciGrid.GetLength(0); row++)
            {
                if (!grid.FibonacciGrid[row, columnIndex].IsFibonacci)
                {
                    break;
                }

                fibonacciCells.Add(Tuple.Create(row, columnIndex));
            }

            for (var row = rowIndex -1; row >= 0; row--)
            {
                if (!grid.FibonacciGrid[row, columnIndex].IsFibonacci)
                {
                    break;
                }

                fibonacciCells.Add(Tuple.Create(row, columnIndex));
            }

            for (var column = columnIndex + 1; column < grid.FibonacciGrid.GetLength(1); column++)
            {
                if (!grid.FibonacciGrid[rowIndex, column].IsFibonacci)
                {
                    break;
                }

                fibonacciCells.Add(Tuple.Create(column, columnIndex));
            }

            for (var column = columnIndex - 1; column >= 0; column--)
            {
                if (!grid.FibonacciGrid[rowIndex, column].IsFibonacci)
                {
                    break;
                }

                fibonacciCells.Add(Tuple.Create(rowIndex, column));
            }

            return fibonacciCells;
        }
    }
}
