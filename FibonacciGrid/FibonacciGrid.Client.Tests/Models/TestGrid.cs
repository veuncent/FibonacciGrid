﻿using System;
using FibonacciGrid.Client.Models;
using NUnit.Framework;
using System.Diagnostics;
using FibonacciGrid.Client.Services;

namespace FibonacciGrid.Client.Tests.Models
{
    public class TestGrid
    {
        [Test]
        public void Given_Grid_When_ConstructingObject_Expect_InitializedTwoDimensionalArray()
        {
            // Arrange
            var fibonacciService = new FibonacciCheckerService();

            // Act
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var grid = new Grid(fibonacciService, 50);
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);

            stopwatch.Start();
            var gridList = new GridList(fibonacciService, 50);
            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed);

            // Assert
            Assert.AreEqual(50, grid.FibonacciGrid.GetLength(0));
            Assert.AreEqual(50, grid.FibonacciGrid.GetLength(1));

            var item = grid.FibonacciGrid[49, 49];
            Assert.IsNotNull(item);
            Assert.AreEqual(typeof(GridCell), item.GetType());
        }
    }
}
