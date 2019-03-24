using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services
{
    public class TestGridCellUpdaterService
    {
        [Test]
        public void Given_GridCellList_When_UpdatingValue_Expect_UpdatedValues()
        {
            // Arrange
            var gridCell = new GridCell();
            var gridCellUpdaterService = new GridCellUpdaterService(new FibonacciCheckerService());

            const int newValue = 5;

            // Act
            gridCellUpdaterService.UpdateCell(gridCell, newValue);

            // Assert
            Assert.AreEqual(newValue, gridCell.Value);
            Assert.True(gridCell.IsFibonacci);
        }

        [Test]
        public void Given_GridCellList_When_UpdatingValue_Expect_UpdatedCorrectIsFibonacci()
        {
            // Arrange
            var gridCell = new GridCell();
            var gridCellUpdaterService = new GridCellUpdaterService(new FibonacciCheckerService());

            const int newValue = 4;

            // Act
            gridCellUpdaterService.UpdateCell(gridCell, newValue);

            // Assert
            Assert.False(gridCell.IsFibonacci);
        }
    }
}
