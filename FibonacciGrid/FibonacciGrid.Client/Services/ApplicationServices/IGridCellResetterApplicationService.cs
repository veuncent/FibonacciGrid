using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public interface IGridCellResetterApplicationService
    {
        event Action OnGridCellsReset;
        void ResetGridCells(List<GridCell> sequenceCells);
    }
}