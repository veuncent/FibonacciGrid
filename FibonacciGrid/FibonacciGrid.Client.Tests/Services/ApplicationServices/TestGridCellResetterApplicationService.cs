using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services.ApplicationServices;
using FibonacciGrid.Client.Services.DomainServices;
using Moq;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services.ApplicationServices
{
    public class TestGridCellResetterApplicationService
    {
        [Test]
        public void Given_GridCells_When_ResettingGridCells_Expect_ResetValues()
        {
            // Arrange
            var gridCellResetApplicationService = new GridCellResetterApplicationService();

            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var gridCells = new []
            {
                new GridCell(fibonacciCheckerService), new GridCell(fibonacciCheckerService)
            };

            foreach (var gridCell in gridCells)
            {
                gridCell.IncrementGridCellValue(1);
                gridCell.SetAsSequentialFibonacci();
            }

            // Assert
            foreach (var gridCell in gridCells)
            {
                Assert.AreEqual(1, gridCell.Value);
                Assert.IsTrue(gridCell.IsFibonacci);
                Assert.IsTrue(gridCell.IsSequentialFibonacci);
            }

            // Act
            gridCellResetApplicationService.ResetGridCells(gridCells).Wait();

            // Assert
            foreach (var gridCell in gridCells)
            {
                Assert.AreEqual(0, gridCell.Value);
                Assert.IsFalse(gridCell.IsFibonacci);
                Assert.IsFalse(gridCell.IsSequentialFibonacci);
            }
        }
    }
}
