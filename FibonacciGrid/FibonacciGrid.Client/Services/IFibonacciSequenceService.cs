using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IFibonacciSequenceService
    {
        List<List<Tuple<int, int>>> FindFibonacciSequences(Grid grid, List<List<Tuple<int, int>>> groupedFibonacciCells);
    }
}