using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FibonacciGrid.Client.Models;
using FibonacciGrid.Client.Services.DomainServices;

namespace FibonacciGrid.Client.Services.ApplicationServices
{
    public class GridCellIncrementerApplicationService : IGridCellIncrementerApplicationService
    {
        private readonly IGridCellUpdaterService _gridCellUpdaterService;
        private readonly IFibonacciNeighborService _fibonacciNeighborService;
        private readonly IFibonacciSequenceService _fibonacciSequenceService;
        private const int GridCellIncrement = 1;

        public event Action OnGridCellsReset;

        public GridCellIncrementerApplicationService(IGridCellUpdaterService gridCellUpdaterService,
            IFibonacciNeighborService fibonacciNeighborService,
            IFibonacciSequenceService fibonacciSequenceService)
        {
            _gridCellUpdaterService = gridCellUpdaterService;
            _fibonacciNeighborService = fibonacciNeighborService;
            _fibonacciSequenceService = fibonacciSequenceService;
        }

        public void IncrementGridCell(int row, int column, IGrid fibonacciGrid)
        {
            var fibonacciCells = _gridCellUpdaterService.UpdateCell(fibonacciGrid, GridCellIncrement, row, column);
            var fibonacciNeighbors = _fibonacciNeighborService.FindNeighbors(fibonacciGrid, fibonacciCells);
            var sequenceCells = _fibonacciSequenceService.FindFibonacciSequences(fibonacciNeighbors);

            HighlightFibonacciSequences(sequenceCells);
            ResetFibonacciSequences(sequenceCells);
        }

        private static void HighlightFibonacciSequences(List<GridCell> sequenceCells)
        {
            sequenceCells.ForEach(gridCell =>
                gridCell.SetAsSequentialFibonacci());
        }

        private void ResetFibonacciSequences(List<GridCell> sequenceCells)
        {
            Task.Run(() =>
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