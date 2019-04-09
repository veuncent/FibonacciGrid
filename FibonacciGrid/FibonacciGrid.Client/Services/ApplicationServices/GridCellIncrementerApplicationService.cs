using System;
using System.Collections.Generic;
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
        private readonly IGridCellResetterApplicationService _gridCellResetterApplicationService;
        private const int GridCellIncrement = 1;

        public event Action OnGridCellsChange;

        public GridCellIncrementerApplicationService(IGridCellUpdaterService gridCellUpdaterService,
            IFibonacciNeighborService fibonacciNeighborService,
            IFibonacciSequenceService fibonacciSequenceService,
            IGridCellResetterApplicationService gridCellResetterApplicationService)
        {
            _gridCellUpdaterService = gridCellUpdaterService;
            _fibonacciNeighborService = fibonacciNeighborService;
            _fibonacciSequenceService = fibonacciSequenceService;
            _gridCellResetterApplicationService = gridCellResetterApplicationService;

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _gridCellResetterApplicationService.OnGridCellsReset += OnGridCellsResetEventHandler;
        }

        private void OnGridCellsResetEventHandler()
        {
            OnGridCellsChange?.Invoke();
        }

        public async Task IncrementGridCell(int row, int column, IGrid fibonacciGrid)
        {
            var fibonacciCells = _gridCellUpdaterService.UpdateCell(fibonacciGrid, GridCellIncrement, row, column);
            var fibonacciNeighbors = _fibonacciNeighborService.FindNeighbors(fibonacciGrid, fibonacciCells);
            var sequenceCells = _fibonacciSequenceService.FindFibonacciSequences(fibonacciNeighbors);

            HighlightFibonacciSequences(sequenceCells);
            await ResetFibonacciSequences(sequenceCells);
        }

        private static void HighlightFibonacciSequences(List<GridCell> sequenceCells)
        {
            sequenceCells.ForEach(gridCell =>
                gridCell.SetAsSequentialFibonacci());
        }

        private async Task ResetFibonacciSequences(List<GridCell> sequenceCells)
        {
           await _gridCellResetterApplicationService.ResetGridCells(sequenceCells);
        }
    }
}