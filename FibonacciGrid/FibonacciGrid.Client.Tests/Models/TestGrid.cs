using FibonacciGrid.Client.Models;
using NUnit.Framework;
using FibonacciGrid.Client.Services.DomainServices;
using Moq;

namespace FibonacciGrid.Client.Tests.Models
{
    public class TestGrid
    {
        [Test]
        public void Given_Grid_When_ConstructingObject_Expect_InitializedTwoDimensionalArray()
        {
            // Arrange
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);

            // Act
            var grid = new Grid(fibonacciCheckerService);

            // Assert
            Assert.AreEqual(50, grid.FibonacciGrid.GetLength(0));
            Assert.AreEqual(50, grid.FibonacciGrid.GetLength(1));

            var item = grid.FibonacciGrid[49, 49];
            Assert.IsNotNull(item);
            Assert.AreEqual(typeof(GridCell), item.GetType());
        }
    }
}
