using System;
using System.Threading.Tasks;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public interface IGridCellIncrementerApplicationService
    {
        event Action OnGridCellsChange;
        Task IncrementGridCell(int row, int column, IGrid fibonacciGrid);
    }
}