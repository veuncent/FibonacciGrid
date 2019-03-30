using System;
using System.Diagnostics;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services
{
    public class TestGridCellUpdaterService
    {
        [Test]
        public void Given_Grid_When_UpdatingValue_Expect_UpdatedValue()
        {
            // Arrange
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();

            const int increment = 5;
            const int rowIndex = 25;
            const int columnIndex = 25;

            // Act
            gridCellUpdaterService.UpdateCell(grid, increment, columnIndex, rowIndex);

            // Assert
            var updatedCell = grid.FibonacciGrid[rowIndex, columnIndex];
            Assert.AreEqual(increment, updatedCell.Value);
            Assert.True(updatedCell.IsFibonacci);
        }

        [Test]
        public void Given_Grid_When_UpdatingValue_Expect_UpdatedCorrectIsFibonacci()
        {
            // Arrange
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();

            const int increment = 4;
            const int rowIndex = 25;
            const int columnIndex = 25;

            // Act
            gridCellUpdaterService.UpdateCell(grid, increment, columnIndex, rowIndex);

            // Assert
            var updatedCell = grid.FibonacciGrid[rowIndex, columnIndex];
            Assert.False(updatedCell.IsFibonacci);
        }

        [Test]
        public void Given_Grid_When_UpdatingValue_Expect_AllAffectedCellsUpdated()
        {
            // Arrange
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();

            const int increment = 5;
            const int rowIndex = 25;
            const int columnIndex = 25;

            // Act
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            gridCellUpdaterService.UpdateCell(grid, increment, columnIndex, rowIndex);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            // Assert
            stopwatch.Start();
            for (var row = 0; row < grid.FibonacciGrid.GetLength(0); row++)
            {
                var updatedCell = grid.FibonacciGrid[row, columnIndex];
                Assert.AreEqual(increment, updatedCell.Value);
                Assert.True(updatedCell.IsFibonacci);
            }

            for (var column = 0; column < grid.FibonacciGrid.GetLength(1); column++)
            {
                if (column == columnIndex) continue;
                var updatedCell = grid.FibonacciGrid[rowIndex, column];
                Assert.AreEqual(increment, updatedCell.Value);
                Assert.True(updatedCell.IsFibonacci);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
