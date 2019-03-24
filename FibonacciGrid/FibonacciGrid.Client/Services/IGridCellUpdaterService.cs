using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public interface IGridCellUpdaterService
    {
        void UpdateCell(GridCell gridCell, int value);
    }
}