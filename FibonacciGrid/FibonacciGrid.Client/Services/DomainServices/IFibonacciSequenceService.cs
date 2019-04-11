using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.DomainServices
{
    public interface IFibonacciSequenceService
    {
        GridCell[] FindFibonacciSequences(List<List<GridCell>> groupedFibonacciCells);
    }
}