using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public class GridCellResetterApplicationService : IGridCellResetterApplicationService
    {
        public event Action OnGridCellsReset;

        public async Task ResetGridCells(List<GridCell> sequenceCells)
        {
            await Task.Run(() =>
            {
                if (!sequenceCells.Any()) return;

                DelayReset();
                ResetGridValues(sequenceCells);

                OnGridCellsReset?.Invoke();
            });
        }

        private static void DelayReset()
        {
            Thread.Sleep(1000);
        }

        private static void ResetGridValues(List<GridCell> sequenceCells)
        {
            sequenceCells.ForEach(gridCell =>
                gridCell.ResetValue());
        }
    }
}
