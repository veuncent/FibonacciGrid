using System;
using System.Collections.Generic;
using System.Linq;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.DomainServices
{
    public class FibonacciSequenceService : IFibonacciSequenceService
    {
        private const int FibonacciSequenceMinimum = 5;
        private readonly double _phi = (1 + Math.Sqrt(5)) / 2;

        /// <summary>
        /// Finds Fibonacci sequences in given list of neighboring Fibonacci cells
        /// </summary>
        /// <param name="groupedFibonacciCells">Each list in this list contains a group of neighboring Fibonacci cells</param>
        /// <returns>Returns a list with all Fibonacci cells that are part of a sequence</returns>
        public List<GridCell> FindFibonacciSequences(List<List<GridCell>> groupedFibonacciCells)
        {
            var sequentialFibonacciCells = new List<GridCell>();
            foreach (var fibonacciCellGroup in groupedFibonacciCells)
            {
                var sequentialCells = FindFibonacciSequence(fibonacciCellGroup);
                if (sequentialCells.Count > 0)
                {
                    sequentialFibonacciCells.AddRange(sequentialCells);
                }
            }

            return sequentialFibonacciCells;
        }

        private List<GridCell> FindFibonacciSequence(IReadOnlyCollection<GridCell> fibonacciCellGroup)
        {
            var fibonacciSequenceCells = new List<GridCell>();

            if (!fibonacciCellGroup.Any())
            {
                return fibonacciSequenceCells;
            }

            HandleNonFibonacciCells(fibonacciCellGroup);
            DetectFibonacciSequences(fibonacciCellGroup, fibonacciSequenceCells);

            return fibonacciSequenceCells;
        }

        private static void HandleNonFibonacciCells(IReadOnlyCollection<GridCell> fibonacciCellGroup)
        {
            if (fibonacciCellGroup.Any(cel => !cel.IsFibonacci))
            {
                throw new ArgumentException("Only GridCells that have a Fibonacci value are allowed");
            }
        }

        private void DetectFibonacciSequences(IReadOnlyCollection<GridCell> fibonacciCellGroup, List<GridCell> fibonacciSequenceCells)
        {
            var fibonacciSequenceTempList = new List<GridCell>();

            for (var currentIndex = 0; currentIndex < fibonacciCellGroup.Count; currentIndex++)
            {
                var previousGridCell = fibonacciCellGroup.ElementAtOrDefault(currentIndex - 1);
                var currentGridCell = fibonacciCellGroup.ElementAt(currentIndex);
                var currentValue = currentGridCell.Value;

                if (HasSequenceRestarted(currentValue, previousGridCell))
                {
                    CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequenceCells);
                }

                if (currentValue == 0 || currentValue == 1)
                {
                    var nextValue = fibonacciCellGroup.ElementAtOrDefault(currentIndex + 1)?.Value;

                    if (ContinuingZeroOrOneIsFutile(fibonacciCellGroup, currentIndex, nextValue))
                    {
                        CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequenceCells);
                        break;
                    }

                    EvaluateZeroOrOneFibonacci(fibonacciCellGroup, currentIndex, currentValue, nextValue, fibonacciSequenceTempList, fibonacciSequenceCells);
                }
                else
                {
                    if (previousGridCell != null)
                    {
                        var expectedValue = GetExpectedValueFromPreviousValue(previousGridCell);
                        if (expectedValue == currentValue)
                        {
                            fibonacciSequenceTempList.Add(currentGridCell);
                        }
                        else
                        {
                            CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequenceCells);
                            if (ContinuingIsFutile(fibonacciCellGroup, currentIndex))
                                break;
                        }
                    }
                    else
                    {
                        fibonacciSequenceTempList.Add(currentGridCell);
                    }
                }
            }

            CollectResultsInSequenceList(fibonacciSequenceTempList, fibonacciSequenceCells);
        }

        private static bool HasSequenceRestarted(int valueA, GridCell previous)
        {
            return valueA < previous?.Value;
        }

        private static bool ContinuingZeroOrOneIsFutile(IReadOnlyCollection<GridCell> fibonacciCellGroup, int currentIndex, int? nextValue)
        {
            return fibonacciCellGroup.Count - currentIndex < 3 || nextValue == null;
        }

        private static void EvaluateZeroOrOneFibonacci(IReadOnlyCollection<GridCell> fibonacciCellGroup, int i, int valueA,
            int? valueB, List<GridCell> fibonacciSequenceTempList, List<GridCell> fibonacciSequence)
        {
            var valueC = fibonacciCellGroup.ElementAt(i + 2).Value;

            if (ValuesAreSequential(valueA, valueB, valueC))
            {
                fibonacciSequenceTempList.Add(fibonacciCellGroup.ElementAt(i));
            }
            else
            {
                CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequence);
            }
        }

        private static bool ValuesAreSequential(int valueA, int? valueB, int valueC)
        {
            return valueA + valueB == valueC && valueC != 0;
        }

        private int GetExpectedValueFromPreviousValue(GridCell previousGridCell)
        {
            return (int) Math.Round(previousGridCell.Value * _phi);
        }

        private static void CollectIntermediateResults(List<GridCell> fibonacciSequenceTempList, List<GridCell> fibonacciSequence)
        {
            CollectResultsInSequenceList(fibonacciSequenceTempList, fibonacciSequence);
            fibonacciSequenceTempList.Clear();
        }

        private static bool ContinuingIsFutile(IReadOnlyCollection<GridCell> fibonacciCellGroup, int currentIndex)
        {
            return fibonacciCellGroup.Count - currentIndex < FibonacciSequenceMinimum;
        }

        private static void CollectResultsInSequenceList(IReadOnlyCollection<GridCell> fibonacciSequenceTempList, List<GridCell> fibonacciSequence)
        {
            if (fibonacciSequenceTempList.Count >= FibonacciSequenceMinimum)
            {
                fibonacciSequence.AddRange(fibonacciSequenceTempList);
            }
        }
    }
}
