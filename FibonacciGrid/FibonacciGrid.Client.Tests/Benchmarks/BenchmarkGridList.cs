using System;
using System.Diagnostics;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using Moq;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Benchmarks
{
    public class BenchmarkGridList
    {
        [Test, Category("Benchmark")]
        public void BenchmarkMultiDimensionalArray()
        {
            // Arrange
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            var grid = new Grid(fibonacciCheckerService, 50);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [Test, Category("Benchmark")]
        public void BenchmarkListInList()
        {
            // Arrange
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            
            // Act
            var stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var gridList = new GridList(fibonacciCheckerService, 50);
            stopwatch2.Stop();
            Console.WriteLine(stopwatch2.Elapsed);
        }
    }
}
