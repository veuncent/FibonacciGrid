using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services
{
    public class GridCellUpdaterService : IGridCellUpdaterService
    {
        private readonly IFibonacciCheckerService _fibonacciCheckerService;

        public GridCellUpdaterService(IFibonacciCheckerService fibonacciCheckerService)
        {
            _fibonacciCheckerService = fibonacciCheckerService;
        }

        public void UpdateCell(GridCell gridCell, int value)
        {
            var isFibonacci = _fibonacciCheckerService.IsFibonacci(value);
            gridCell.SetGridCellValue(value, isFibonacci);
        }
    }
}
