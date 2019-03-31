﻿using System;
using System.Collections.Generic;
using System.Linq;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public class FibonacciSequenceService : IFibonacciSequenceService
    {
        private const int FibonacciSequenceMinimum = 5;
        private readonly double _phi = (1 + Math.Sqrt(5)) / 2;

        public List<List<GridCell>> FindFibonacciSequences(List<List<GridCell>> groupedFibonacciCells)
        {
            var sequentialFibonacciCells = new List<List<GridCell>>();
            foreach (var fibonacciCellGroup in groupedFibonacciCells)
            {
                var sequentialCells = CheckFibonacciSequence(fibonacciCellGroup);
                if (sequentialCells.Count > 0)
                {
                    sequentialFibonacciCells.Add(sequentialCells);
                }
            }

            return sequentialFibonacciCells;
        }

        private List<GridCell> CheckFibonacciSequence(IReadOnlyCollection<GridCell> fibonacciCellGroup)
        {
            var fibonacciSequence = new List<GridCell>();

            if (!fibonacciCellGroup.Any())
            {
                return fibonacciSequence;
            }

            if (fibonacciCellGroup.Any(cel => !cel.IsFibonacci))
            {
                throw new ArgumentException("Only GridCells that have a Fibonacci value are allowed");
            }

            var fibonacciSequenceTempList = new List<GridCell>();

            for (var i = 0; i < fibonacciCellGroup.Count; i++)
            {
                var previous = fibonacciCellGroup.ElementAtOrDefault(i - 1);
                var gridCell = fibonacciCellGroup.ElementAt(i);
                var valueA = gridCell.Value;
                var nextValue = fibonacciCellGroup.ElementAtOrDefault(i + 1)?.Value;

                if (valueA < previous?.Value)
                {
                    CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequence);
                }

                if (valueA == 0 || valueA == 1)
                {
                    if (fibonacciCellGroup.Count - i < 3 || nextValue == null)
                    {
                        CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequence);
                        break;
                    }

                    var valueC = fibonacciCellGroup.ElementAt(i + 2).Value;
                    
                    if (valueA + nextValue == valueC && nextValue != 0 && valueC != 0)
                    {
                        fibonacciSequenceTempList.Add(fibonacciCellGroup.ElementAt(i));
                    }
                    else
                    {
                        CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequence);
                    }

                    continue;
                }

                if (previous != null)
                {
                    var expected = (int)Math.Round(previous.Value * _phi);
                    if (expected == valueA)
                    {
                        fibonacciSequenceTempList.Add(gridCell);
                    }
                    else
                    {
                        CollectIntermediateResults(fibonacciSequenceTempList, fibonacciSequence);

                        if (ContinuingIsFutile(fibonacciCellGroup, i)) break;
                    }
                }
                else
                {
                    fibonacciSequenceTempList.Add(gridCell);
                }
            }

            CollectResultsInSequenceList(fibonacciSequenceTempList, fibonacciSequence);

            return fibonacciSequence;
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
