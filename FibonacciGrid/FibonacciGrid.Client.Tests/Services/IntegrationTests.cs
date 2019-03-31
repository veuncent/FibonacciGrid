using System;
using System.Collections.Generic;
using System.Linq;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services
{
    public class IntegrationTests
    {
        [Test]
        public void Given_CellCoordinate_When_HorizontalAndVerticalFibonacciNeighborsAreAboveThreshold_Expect_CorrectNeighborCells()
        {
            // Arrange
            var fibonacciCheckerService = new FibonacciCheckerService();
            var fibonacciNeighborService = new FibonacciNeighborService();

            var grid = new Grid(fibonacciCheckerService);

            var cellsToSetToTrue = GetVerticalCells();
            cellsToSetToTrue.AddRange(GetHorizontalCells());

            foreach (var (rowIndex, columnIndex) in cellsToSetToTrue)
            {
                var gridCell = grid.FibonacciGrid[rowIndex, columnIndex];
                gridCell.IncrementGridCellValue(5);
            }

            var cellsToSetToFalse = new List<Tuple<int, int>> { Tuple.Create(20, 25), Tuple.Create(30, 25), Tuple.Create(25, 20), Tuple.Create(25, 30) };
            foreach (var (rowIndex, columnIndex) in cellsToSetToFalse)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(4);
            }

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25)
            };

            // Act
            var neighbors = fibonacciNeighborService.FindNeighbors(grid, cellToCheck);
            var vertical = neighbors.First();
            var horizontal = neighbors.Skip(1).First();

            // Assert
            Assert.AreEqual(9, horizontal.Count);
            horizontal.ForEach(cell => Assert.AreEqual(5, cell.Value));

            Assert.AreEqual(9, vertical.Count);
            vertical.ForEach(cell => Assert.AreEqual(5, cell.Value));
        }

        private static List<Tuple<int, int>> GetVerticalCells()
        {
            return Enumerable
                .Range(21, 29)
                .Select(i => Tuple.Create(i, 25))
                .ToList();
        }

        private static IEnumerable<Tuple<int, int>> GetHorizontalCells()
        {
            return Enumerable
                .Range(21, 29)
                .Where(i => i != 25)
                .Select(i => Tuple.Create(25, i));
        }

        [Test]
        public void Given_Grid_When_IncrementingWithOne_Expect_ZeroFibonacciSequences()
        {
            // Arrange
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();
            var fibonacciNeighborService = new FibonacciNeighborService();
            var fibonacciSequenceService = new FibonacciSequenceService();

            const int rowIndex = 25;
            const int columnIndex = 25;

            // Act
            var fibonacciCells = gridCellUpdaterService.UpdateCell(grid, 1, rowIndex, columnIndex);
            var fibonacciNeighbors = fibonacciNeighborService.FindNeighbors(grid, fibonacciCells);
            var fibonacciSequences = fibonacciSequenceService.FindFibonacciSequences(fibonacciNeighbors);

            // Assert
            Assert.AreEqual(0, fibonacciSequences.SelectMany(sequence => sequence).Count());
        }

        [Test]
        public void Given_Grid_When_IncrementingCellsToAFibonacciSequence_Expect_FiveGridCellsInList()
        {
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();
            var fibonacciNeighborService = new FibonacciNeighborService();
            var fibonacciSequenceService = new FibonacciSequenceService();

            gridCellUpdaterService.UpdateCell(grid, 1, 25, 25);
            gridCellUpdaterService.UpdateCell(grid, 1, 26, 27);
            gridCellUpdaterService.UpdateCell(grid, 2, 26, 28);
            var fibonacciCells = gridCellUpdaterService.UpdateCell(grid, 4, 26, 29);

            var fibonacciNeighbors = fibonacciNeighborService.FindNeighbors(grid, fibonacciCells);
            var fibonacciSequences = fibonacciSequenceService.FindFibonacciSequences(fibonacciNeighbors);


            Assert.AreEqual(5, fibonacciSequences.SelectMany(sequence => sequence).Count());
        }
    }
}
