using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public interface IGridCellResetterApplicationService
    {
        event Action OnGridCellsReset;
        Task ResetGridCells(List<GridCell> sequenceCells);
    }
}