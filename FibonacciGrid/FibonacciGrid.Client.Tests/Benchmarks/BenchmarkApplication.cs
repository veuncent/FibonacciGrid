using System;
using System.Diagnostics;
using System.Linq;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services.DomainServices;
using NUnit.Framework;

namespace FibonacciGrid.Client.Tests.Benchmarks
{
    public class BenchmarkApplication
    {
        [Test, Category("Benchmark")]
        public void BenchmarkApplicationTenTimes()
        {
            // Arrange
            var grid = new Grid(new FibonacciCheckerService());
            var gridCellUpdaterService = new GridCellUpdaterService();
            var fibonacciNeighborService = new FibonacciNeighborService();
            var fibonacciSequenceService = new FibonacciSequenceService();


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
            }
        }
    }
}
