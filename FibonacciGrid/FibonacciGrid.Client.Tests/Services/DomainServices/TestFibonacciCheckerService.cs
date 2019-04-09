using FibonacciGrid.Client.Services.DomainServices;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Services.DomainServices
{
    public class TestFibonacciCheckerService
    {
        [Test]
        public void Given_FibonacciNumber_When_CheckingIfIsFibonacciNumber_Expect_True()
        {
            // Arrange
            var fibonacciCheckerService = new FibonacciCheckerService();
            const int fibonacciNumber = 3;

            // Act
            var isFibonacci = fibonacciCheckerService.IsFibonacci(fibonacciNumber);

            // Assert
            Assert.True(isFibonacci);
        }

        [Test]
        public void Given_NonFibonacciNumber_When_CheckingIfIsFibonacciNumber_Expect_False()
        {
            // Arrange
            var fibonacciCheckerService = new FibonacciCheckerService();
            const int fibonacciNumber = 4;

            // Act
            var isFibonacci = fibonacciCheckerService.IsFibonacci(fibonacciNumber);

            // Assert
            Assert.False(isFibonacci);
        }
    }
}
