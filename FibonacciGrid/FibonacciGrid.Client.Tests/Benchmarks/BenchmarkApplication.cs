using System;
using System.Diagnostics;
using System.Linq;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services.ApplicationServices;
using FibonacciGrid.Client.Services.DomainServices;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Benchmarks
{
    public class BenchmarkApplication
    {
        private GridCellIncrementerApplicationService _gridCellIncrementerApplicationService;
        private Grid _fibonacciGrid;

        [SetUp]
        public void SetUp()
        {
            _fibonacciGrid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();
            var fibonacciNeighborService = new FibonacciNeighborService();
            var fibonacciSequenceService = new FibonacciSequenceService();
            var gridCellResetterApplicationService = new GridCellResetterApplicationService();

            _gridCellIncrementerApplicationService = new GridCellIncrementerApplicationService(
                gridCellUpdaterService, fibonacciNeighborService,
                fibonacciSequenceService, gridCellResetterApplicationService);
        }

        [Test, Category("Benchmark")]
        public void BenchmarkDomainServices()
        {
            // Arrange
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();
            var fibonacciNeighborService = new FibonacciNeighborService();
            var fibonacciSequenceService = new FibonacciSequenceService();

            var totalTime = new TimeSpan();

            foreach (var _ in Enumerable.Range(0, 10))
            {
                // Act
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                gridCellUpdaterService.UpdateCell(grid, 1, 25, 25);
                gridCellUpdaterService.UpdateCell(grid, 1, 26, 27);
                gridCellUpdaterService.UpdateCell(grid, 2, 26, 28);
                gridCellUpdaterService.UpdateCell(grid, 4, 26, 29);
                gridCellUpdaterService.UpdateCell(grid, 7, 26, 30);
                gridCellUpdaterService.UpdateCell(grid, 12, 26, 31);
                var fibonacciCells = gridCellUpdaterService.UpdateCell(grid, 20, 26, 32);

                var fibonacciNeighbors = fibonacciNeighborService.FindNeighbors(grid, fibonacciCells);
                fibonacciSequenceService.FindFibonacciSequences(fibonacciNeighbors);
                stopwatch.Stop();

                // Report
                Console.WriteLine(stopwatch.Elapsed);
                totalTime = totalTime.Add(stopwatch.Elapsed);
            }

            Console.WriteLine($"Average: \n{totalTime.Divide(10)}");
        }

        [Test, Category("Benchmark")]
        public void BenchmarkApplicationService()
        {
            // Arrange
            var totalTime = new TimeSpan();

            // Act
            foreach (var _ in Enumerable.Range(0, 10))
            {
                // Act
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                IncrementGridCell(25, 25, 1);
                IncrementGridCell(26, 26, 1);
                IncrementGridCell(27, 27, 2);
                IncrementGridCell(28, 28, 4);

                IncrementGridCell(29, 29, 7);
                IncrementGridCell(30, 30, 12);
                IncrementGridCell(31, 31, 20);
                IncrementGridCell(32, 32, 33);
                IncrementGridCell(33, 33, 54);

                stopwatch.Stop();

                // Report
                Console.WriteLine(stopwatch.Elapsed);
                totalTime = totalTime.Add(stopwatch.Elapsed);
            }
            Console.WriteLine($"Average: \n{totalTime.Divide(10)}");
        }

        private void IncrementGridCell(int row, int column, int times)
        {
            for (var i = 0; i < times; i++)
            {
                _gridCellIncrementerApplicationService.IncrementGridCell(row, column, _fibonacciGrid).Wait();
            }
        }
    }
}
