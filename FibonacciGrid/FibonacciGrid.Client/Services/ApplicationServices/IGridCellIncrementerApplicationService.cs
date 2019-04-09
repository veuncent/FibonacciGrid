using System;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public interface IGridCellIncrementerApplicationService
    {
        event Action OnGridCellsChange;
        void IncrementGridCell(int row, int column, IGrid fibonacciGrid);
    }
}