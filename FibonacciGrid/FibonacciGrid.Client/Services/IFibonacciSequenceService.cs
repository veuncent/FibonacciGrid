using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IFibonacciSequenceService
    {
        List<List<GridCell>> FindFibonacciSequences(List<List<GridCell>> groupedFibonacciCells);
    }
}