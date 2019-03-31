using System;
using System.Collections.Generic;

namespace FibonacciGrid.Client.Services
{
    public interface IFibonacciSequenceService
    {
        List<List<Tuple<int, int>>> FindFibonacciSequences(List<List<Tuple<int, int>>> groupedFibonacciCells);
    }
}