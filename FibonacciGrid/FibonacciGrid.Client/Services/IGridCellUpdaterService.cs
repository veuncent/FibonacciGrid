using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IGridCellUpdaterService
    {
        List<GridCell> UpdateCell(Grid grid, int increment, int rowIndex, int columnIndex);
    }
}