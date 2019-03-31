using System;
using System.Collections.Generic;
using System.Diagnostics;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services;
using Moq;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Benchmarks
{
    public class BenchmarkAddToTableCoordinateList
    {
        [Test, Category("Benchmark")]
        public void BenchmarkListTuplePerformance()
        {
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid(fibonacciCheckerService, 50);

            var listTuple = new List<Tuple<int, int>>();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var x = 0; x < 100; x++)
            {
                for (var i = 0; i < grid.FibonacciGrid.GetLength(0); i++)
                {
                    for (var j = 0; j < grid.FibonacciGrid.GetLength(1); j++)
                    {
                        listTuple.Add(Tuple.Create(i, j));
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [Test, Category("Benchmark")]
        public void BenchmarkListListPerformance()
        {
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid(fibonacciCheckerService, 50);

            var listList = new List<List<int>>();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var x = 0; x < 100; x++)
            {
                for (var i = 0; i < grid.FibonacciGrid.GetLength(0); i++)
                {
                    for (var j = 0; j < grid.FibonacciGrid.GetLength(1); j++)
                    {
                        listList.Add(new List<int> { i, j });
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }

        [Test, Category("Benchmark")]
        public void BenchmarkListArrayPerformance()
        {
            var fibonacciCheckerService = Mock.Of<IFibonacciCheckerService>();
            Mock.Get(fibonacciCheckerService).Setup(service => service.IsFibonacci(It.IsAny<int>())).Returns(true);
            var grid = new Grid(fibonacciCheckerService, 50);

            var listArray = new List<int[]>();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var x = 0; x < 100; x++)
            {
                for (var i = 0; i < grid.FibonacciGrid.GetLength(0); i++)
                {
                    for (var j = 0; j < grid.FibonacciGrid.GetLength(1); j++)
                    {
                        listArray.Add(new[] { i, j });
                    }
                }
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}
