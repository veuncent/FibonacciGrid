using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IGridCellUpdaterService
    {
        List<Tuple<int, int>> UpdateCell(Grid grid, int increment, int rowIndex, int columnIndex);
    }
}