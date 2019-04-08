using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services.DomainServices;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Models
{
    public class TestGridCell
    {
        [Test]
        public void Given_IsFibonacciBoolean_When_IncrementingValue_Expect_True()
        {
            // Arrange
            var fibonacciCheckerService = new FibonacciCheckerService();
            var gridCell = new GridCell(fibonacciCheckerService);

            // Act
            gridCell.IncrementGridCellValue(1);

            // Assert
            Assert.IsTrue(gridCell.IsFibonacci);

            // Act
            gridCell.IncrementGridCellValue(4);

            // Assert
            Assert.IsTrue(gridCell.IsFibonacci);
        }
    }
}
