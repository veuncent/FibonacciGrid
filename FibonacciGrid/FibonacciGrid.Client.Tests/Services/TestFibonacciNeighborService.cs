using System;
using System.Collections.Generic;
using System.Linq;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using Moq;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services
{
    public class TestFibonacciNeighborService
    {
        [Test]
        public void Given_CellCoordinate_When_NoNeighborsAreFibonacci_Expect_EmptyList()
        {
            // Arrange
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid(fibonacciCheckerService);
            foreach (var gridCell in grid.FibonacciGrid)
            {
                gridCell.IncrementGridCellValue(4);
            }

            var fibonacciSequenceService = new FibonacciNeighborService();

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25)
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(0, neighbors.Count);
        }

        [Test]
        public void Given_CellCoordinate_When_FibonacciNeighborsAreBelowThreshold_Expect_EmptyList()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid(fibonacciCheckerService);

            var cellsToSetToFalse = new List<Tuple<int, int>> { Tuple.Create(25, 23), Tuple.Create(25, 27), Tuple.Create(24, 25), Tuple.Create(26, 25) };
            foreach (var (rowIndex, columnIndex) in cellsToSetToFalse)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(4);
            }

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25)
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(0, neighbors.Count);
        }

        [Test]
        public void Given_CellCoordinate_When_HorizontalFibonacciNeighborsAreAboveThreshold_Expect_CorrectListCount()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid(fibonacciCheckerService);

            var cellsToSetToFalse = new List<Tuple<int, int>> { Tuple.Create(25, 20), Tuple.Create(25, 30), Tuple.Create(24, 25), Tuple.Create(26, 25) };
            foreach (var (rowIndex, columnIndex) in cellsToSetToFalse)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(4);
            }

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25)
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(1, neighbors.Count);
            var horizontal = neighbors.First();
            Assert.AreEqual(9, horizontal.Count);
        }

        [Test]
        public void Given_CellCoordinate_When_VerticalFibonacciNeighborsAreAboveThreshold_Expect_SingleListWithCorrectNeighborCount()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid(fibonacciCheckerService);

            var cellsToSetToFalse = new List<Tuple<int, int>> { Tuple.Create(20, 25), Tuple.Create(30, 25), Tuple.Create(25, 24), Tuple.Create(25, 26) };
            foreach (var (rowIndex, columnIndex) in cellsToSetToFalse)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(4);
            }

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25)
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(1, neighbors.Count);
            var vertical = neighbors.First();
            Assert.AreEqual(9, vertical.Count);
        }

        [Test]
        public void Given_CellCoordinate_When_HorizontalAndVerticalFibonacciNeighborsAreAboveThreshold_Expect_CorrectNeighborCount()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid(fibonacciCheckerService);

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
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);
            var horizontal = neighbors.First();
            var vertical = neighbors.Skip(1).First();

            // Assert
            Assert.AreEqual(9, horizontal.Count);
            Assert.AreEqual(9, vertical.Count);
        }

        [Test]
        public void Given_MultipleCellCoordinates_When_HorizontalAndVerticalFibonacciNeighborsAreAboveThreshold_Expect_CorrectNeighborCount()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid(fibonacciCheckerService);

            var neighbors1 = new List<Tuple<int, int>> { Tuple.Create(20, 25), Tuple.Create(30, 25), Tuple.Create(25, 20), Tuple.Create(25, 30) };  // 17 neighbors
            var neighbors2 = new List<Tuple<int, int>> { Tuple.Create(14, 10), Tuple.Create(22, 10), Tuple.Create(20, 5), Tuple.Create(20, 13) };    // 13 neighbors
            var neighbors3 = new List<Tuple<int, int>> { Tuple.Create(30, 48), Tuple.Create(49, 48), Tuple.Create(40, 42), Tuple.Create(40, 49) };  // 23 neighbors
            var cellsToSetToFalse = new List<List<Tuple<int, int>>> { neighbors1, neighbors2, neighbors3};

            foreach (var (rowIndex, columnIndex) in cellsToSetToFalse.SelectMany(neighborList => neighborList))
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(4);
            }

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25),
                Tuple.Create(20, 10),
                Tuple.Create(40, 48),
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(56, neighbors.SelectMany(neighborList => neighborList).Count());
        }

        [Test]
        public void Given_CellCoordinate_When_CheckingSequence_Expect_TwoListsOfNeighbors()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid( fibonacciCheckerService);


            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25)
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(2, neighbors.Count);
        }

        [Test]
        public void Given_ThreeCellCoordinates_When_CheckingSequence_Expect_SixListOfNeighbors()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid(fibonacciCheckerService);


            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25),
                Tuple.Create(30, 25),
                Tuple.Create(40, 10),
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(6, neighbors.Count);
        }
    }
}
