using System.Collections.Generic;
using System.Threading.Tasks;
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
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            var gridCells = new List<GridCell>
            {
                new GridCell(fibonacciCheckerService), new GridCell(fibonacciCheckerService)
            };

            gridCells.ForEach(gridCell => gridCell.IncrementGridCellValue(1));
            gridCells.ForEach(gridCell => gridCell.SetAsSequentialFibonacci());

            var gridCellResetApplicationService = new GridCellResetterApplicationService();

            // Assert
            gridCells.ForEach(gridCell => Assert.AreEqual(1 , gridCell.Value));
            gridCells.ForEach(gridCell => Assert.IsTrue(gridCell.IsFibonacci));
            gridCells.ForEach(gridCell => Assert.IsTrue(gridCell.IsSequentialFibonacci));

            // Act
            gridCellResetApplicationService.ResetGridCells(gridCells).Wait();
        
            // Assert
            gridCells.ForEach(gridCell => Assert.AreEqual(0 , gridCell.Value));
            gridCells.ForEach(gridCell => Assert.IsFalse(gridCell.IsFibonacci));
            gridCells.ForEach(gridCell => Assert.IsFalse(gridCell.IsSequentialFibonacci));
        }
    }
}
