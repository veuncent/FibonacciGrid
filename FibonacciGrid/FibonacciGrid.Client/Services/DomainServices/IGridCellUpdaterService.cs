using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.DomainServices
{
    public interface IGridCellUpdaterService
    {
        List<Tuple<int, int>> UpdateCell(IGrid grid, int increment, int rowIndex, int columnIndex);
    }
}