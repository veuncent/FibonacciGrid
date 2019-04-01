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

            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var cellsToSetToTrue = Enumerable.Range(1, 9)
                .Select(i => Tuple.Create(25, 20 + i))
                .ToList();

            foreach (var (rowIndex, columnIndex) in cellsToSetToTrue)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(5);
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

            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var cellsToSetToTrue = Enumerable.Range(1, 9)
                .Select(i => Tuple.Create(20 + i, 25))
                .ToList();

            foreach (var (rowIndex, columnIndex) in cellsToSetToTrue)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(5);
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

            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var cellsToSetToTrue = new List<Tuple<int, int>>();
            foreach (var i in Enumerable.Range(1, 9))
            {
                cellsToSetToTrue.Add(Tuple.Create(20 + i, 25));
                cellsToSetToTrue.Add(Tuple.Create(25, 20 + i));
            }

            foreach (var (rowIndex, columnIndex) in cellsToSetToTrue)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(5);
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
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid(fibonacciCheckerService);

            var cellsToSetToTrue = new List<Tuple<int, int>>();
            foreach (var i in Enumerable.Range(1, 9))
            {
                cellsToSetToTrue.Add(Tuple.Create(20 + i, 25));
                cellsToSetToTrue.Add(Tuple.Create(25, 20 + i));
            }
            foreach (var i in Enumerable.Range(5, 11))
            {
                cellsToSetToTrue.Add(Tuple.Create(10 + i, 10));
                cellsToSetToTrue.Add(Tuple.Create(20, 1 + i));
            }
            foreach (var i in Enumerable.Range(1, 18))
            {
                cellsToSetToTrue.Add(Tuple.Create(30 + i, 48));
                cellsToSetToTrue.Add(Tuple.Create(40, 30 + i));
            }

            foreach (var (rowIndex, columnIndex) in cellsToSetToTrue)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(5);
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
            Assert.AreEqual(76, neighbors.SelectMany(neighborList => neighborList).Count());
        }

        [Test]
        public void Given_CellCoordinate_When_CheckingSequence_Expect_NoNeighbors()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var grid = new Grid( fibonacciCheckerService);

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
        public void Given_ThreeCellCoordinates_When_CheckingSequence_Expect_SixListOfNeighbors()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciNeighborService();
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid(fibonacciCheckerService);

            var cellsToSetToTrue = new List<Tuple<int, int>>();
            foreach (var i in Enumerable.Range(4, 6))
            {
                cellsToSetToTrue.Add(Tuple.Create(20 + i, 25));
                cellsToSetToTrue.Add(Tuple.Create(25, 20 + i));
            }
            foreach (var i in Enumerable.Range(9, 11))
            {
                cellsToSetToTrue.Add(Tuple.Create(20 + i, 30));
                cellsToSetToTrue.Add(Tuple.Create(30, 20 + i));
            }
            foreach (var i in Enumerable.Range(9, 11))
            {
                cellsToSetToTrue.Add(Tuple.Create(30 + i, 40));
                cellsToSetToTrue.Add(Tuple.Create(40, 30 + i));
            }

            foreach (var (rowIndex, columnIndex) in cellsToSetToTrue)
            {
                grid.FibonacciGrid[rowIndex, columnIndex].IncrementGridCellValue(5);
            }

            var cellToCheck = new List<Tuple<int, int>>
            {
                Tuple.Create(25, 25),
                Tuple.Create(30, 30),
                Tuple.Create(40, 40),
            };

            // Act
            var neighbors = fibonacciSequenceService.FindNeighbors(grid, cellToCheck);

            // Assert
            Assert.AreEqual(6, neighbors.Count);
        }
    }
}
