using System;
using System.Collections.Generic;
using System.Linq;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using Moq;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services
{
    public class TestFibonacciSequenceService
    {
        [Test]
        public void Given_SequentialFibonacciSequenceOfFiveCells_WhenFindingFibonacciSequence_Expect_AllCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();

            foreach (var number in new[]{0, 1, 1, 2, 3})
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(1, sequences.Count);

            var sequence = sequences.First();
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        public void Given_SequentialFibonacciSequenceOfFiveOtherCells_WhenFindingFibonacciSequence_Expect_AllCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();

            foreach (var number in new[] { 1, 1, 2, 3, 5 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(1, sequences.Count);

            var sequence = sequences.First();
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        public void Given_SequentialFibonacciSequenceOfEightCells_WhenFindingFibonacciSequence_Expect_AllCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();

            foreach (var number in new[] { 0, 1, 1, 2, 3, 5, 8, 13 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(1, sequences.Count);

            var sequence = sequences.First();
            Assert.AreEqual(8, sequence.Count);
        }

        [Test]
        public void Given_InterruptedSequentialFibonacciSequence_WhenFindingFibonacciSequence_Expect_FiveCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();

            foreach (var number in new[] { 0, 1, 2, 3, 5, 8, 10, 13 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(1, sequences.Count);

            var sequence = sequences.First();
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        public void Given_SequentialFibonacciSequencePrecededByOnes_WhenFindingFibonacciSequence_Expect_AllCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();

            foreach (var number in new[] { 1, 1, 1, 1, 1, 2, 3, 5, 1 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(1, sequences.Count);

            var sequence = sequences.First();
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        public void Given_SequentialFibonacciSequencePrecededByZeroAndOnes_WhenFindingFibonacciSequence_Expect_AllCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();

            foreach (var number in new[] { 0, 1, 1, 1, 1, 2, 3, 5, 10, 13 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(1, sequences.Count);

            var sequence = sequences.First();
            Assert.AreEqual(5, sequence.Count);
        }

        [Test]
        public void Given_NonSequentialFibonacciSequence_WhenFindingFibonacciSequence_Expect_NoCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();
            foreach (var number in new[] { 1, 2, 1, 3, 5 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(0, sequences.Count);
        }

        [Test]
        public void Given_NonSequentialFibonacciSequence2_WhenFindingFibonacciSequence_Expect_NoCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();
            foreach (var number in new[] { 1, 1, 2, 1, 2, 3, 5 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(0, sequences.Count);
        }

        [Test]
        public void Given_ListOfOnes_WhenFindingFibonacciSequence_Expect_NoCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();
            foreach (var number in new[] { 1, 1, 1, 1, 1 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(0, sequences.Count);
        }

        [Test]
        public void Given_ListOfZeroes_WhenFindingFibonacciSequence_Expect_NoCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();
            foreach (var number in new[] { 0, 0, 0, 0, 0 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(0, sequences.Count);
        }

        [Test]
        public void Given_ListOfZeroesAndOnes_WhenFindingFibonacciSequence_Expect_NoCellsReturned()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var fibonacciCellList = new List<GridCell>();
            foreach (var number in new[] { 0, 1, 1, 0, 0, 0, 1, 1, 0, 1, 1 })
            {
                var gridCell = new GridCell(fibonacciCheckerService);
                gridCell.IncrementGridCellValue(number);
                fibonacciCellList.Add(gridCell);
            }

            // Act
            var sequences = fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { fibonacciCellList });

            // Assert
            Assert.AreEqual(0, sequences.Count);
        }

        [Test]
        public void Given_GridCellWithNonFibonacciValue_WhenFindingFibonacciSequence_Expect_ArgumentException()
        {
            // Arrange
            var fibonacciSequenceService = new FibonacciSequenceService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(false);
            var gridCell = new GridCell(fibonacciCheckerService);
            gridCell.IncrementGridCellValue(4);

            // Act && Assert
            Assert.Throws<ArgumentException>( () => fibonacciSequenceService.FindFibonacciSequences(new List<List<GridCell>> { new List<GridCell> { gridCell } })) ;
        }
    }
}
