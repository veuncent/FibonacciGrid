using System;
using System.Collections.Generic;
using System.Threading;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services.ApplicationServices;
using FibonacciGrid.Client.Services.DomainServices;
using Moq;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services.ApplicationServices
{
    public class TestGridCellIncrementerApplicationService
    {
        [Test]
        public void Given_ListOfGridCells_When_HighlightingCells_Expect_IsSequentialFibonacciPropertyEqualsTrue()
        {
            // Arrange
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            var gridCellUpdaterService = Mock.Of<IGridCellUpdaterService>();
            var fibonacciNeighborService = Mock.Of<IFibonacciNeighborService>();
            var fibonacciSequenceService = Mock.Of<IFibonacciSequenceService>();
            var gridCellResetApplicationService = Mock.Of<IGridCellResetterApplicationService>();
            var grid = Mock.Of<IGrid>();

            var sequenceGridCells = new List<GridCell>{ new GridCell(fibonacciCheckerService), new GridCell(fibonacciCheckerService) };
            Mock.Get(fibonacciSequenceService).Setup(sequenceChecker => sequenceChecker
                    .FindFibonacciSequences(It.IsAny<List<List<GridCell>>>()))
                .Returns(sequenceGridCells);

            var gridCellIncrementerApplicationService = new GridCellIncrementerApplicationService(gridCellUpdaterService, fibonacciNeighborService,
                fibonacciSequenceService, gridCellResetApplicationService);

            // Assert
            foreach (var sequenceGridCell in sequenceGridCells)
            {
                Assert.IsFalse(sequenceGridCell.IsSequentialFibonacci);
            }

            // Act
            gridCellIncrementerApplicationService.IncrementGridCell(1, 1, grid).Wait();

            // Assert
            foreach (var sequenceGridCell in sequenceGridCells)
            {
                Assert.IsTrue(sequenceGridCell.IsSequentialFibonacci);
            }
        }

        [Test]
        public void Given_EventHandler_When_HighlightingCells_Expect_EventHandlerIsInvoked()
        {
            // Arrange
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            var gridCellUpdaterService = Mock.Of<IGridCellUpdaterService>();
            var fibonacciNeighborService = Mock.Of<IFibonacciNeighborService>();
            var grid = Mock.Of<IGrid>();

            var fibonacciSequenceService = Mock.Of<IFibonacciSequenceService>();
            var sequenceGridCells = new List<GridCell> { new GridCell(fibonacciCheckerService), new GridCell(fibonacciCheckerService) };
            Mock.Get(fibonacciSequenceService).Setup(sequenceChecker => sequenceChecker
                    .FindFibonacciSequences(It.IsAny<List<List<GridCell>>>()))
                .Returns(sequenceGridCells);

            var gridCellResetApplicationService = new GridCellResetterApplicationService();
            var gridCellIncrementerApplicationService = new GridCellIncrementerApplicationService(gridCellUpdaterService, fibonacciNeighborService,
                fibonacciSequenceService, gridCellResetApplicationService);

            var eventWasRaised = false;
            gridCellIncrementerApplicationService.OnGridCellsChange += () => eventWasRaised = true;

            // Act
            gridCellIncrementerApplicationService.IncrementGridCell(1, 1, grid).Wait();

            // Assert
            Assert.IsTrue(eventWasRaised);
        }
    }
}
