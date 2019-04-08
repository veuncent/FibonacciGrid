using System;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public interface IGridCellIncrementerApplicationService
    {
        event Action OnGridCellsReset;
        void IncrementGridCell(int row, int column, Grid fibonacciGrid);
    }
}