using System;
using System.Collections.Generic;
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
            _gridCellResetterApplicationService.ResetGridCells(sequenceCells);
        }
    }
}