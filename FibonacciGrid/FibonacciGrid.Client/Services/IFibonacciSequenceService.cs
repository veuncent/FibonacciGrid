using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IFibonacciSequenceService
    {
        List<GridCell> FindFibonacciSequences(List<List<GridCell>> groupedFibonacciCells);
    }
}