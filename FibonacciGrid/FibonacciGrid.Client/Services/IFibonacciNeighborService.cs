using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IFibonacciNeighborService
    {
        List<List<Tuple<int, int>>> FindNeighbors(Grid grid, List<Tuple<int, int>> cellsToCheck);
    }
}