using System;
using System.Collections.Generic;
using System.Linq;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public class FibonacciSequenceService : IFibonacciSequenceService
    {
        private readonly double _phi = (1 + Math.Sqrt(5)) / 2;

        public List<List<Tuple<int, int>>> FindFibonacciSequences(Grid grid, List<List<Tuple<int, int>>> groupedFibonacciCells)
        {
            var sequentialFibonacciCells = new List<List<Tuple<int, int>>>();
            foreach (var fibonacciCellGroup in groupedFibonacciCells)
            {
                sequentialFibonacciCells.Add(CheckFibonacciSequence(grid, fibonacciCellGroup));
            }

            return sequentialFibonacciCells;
        }

        private List<Tuple<int, int>> CheckFibonacciSequence(Grid grid, IReadOnlyCollection<Tuple<int, int>> fibonacciCellGroup)
        {
            var fibonacciSequence = new List<Tuple<int, int>>();

            if (!fibonacciCellGroup.Any())
            {
                return fibonacciSequence;
            }

            var fibonacciSequenceTempList = new List<Tuple<int, int>>();

            var (firstRowIndex, firstColumnIndex) = fibonacciCellGroup.First();
            var valueA = grid.FibonacciGrid[firstRowIndex, firstColumnIndex].Value;
            fibonacciSequenceTempList.Add(Tuple.Create(firstRowIndex, firstColumnIndex));

            foreach (var (row, column) in fibonacciCellGroup.Skip(1))
            {
                var expected = (int)Math.Round(valueA * _phi);
                var valueB = grid.FibonacciGrid[row, column].Value;
                valueA = valueB;

                if (expected == valueB)
                {
                    fibonacciSequenceTempList.Add(Tuple.Create(row, column));
                }
                else
                {
                    if (fibonacciSequenceTempList.Count >= 5)
                    {
                        fibonacciSequence.AddRange(fibonacciSequenceTempList);
                    }
                    fibonacciSequenceTempList.Clear();
                }
            }

            return fibonacciSequence;
        }
    }
}
