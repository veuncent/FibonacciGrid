using FibonacciGrid.Client.Services;

namespace FibonacciGrid.Client.Models
{
    public class GridCell
    {
        private readonly IFibonacciCheckerService _fibonacciCheckerService;

        public int Value { get; private set; }
        public bool IsFibonacci { get; private set; } = true;
        public bool IsSequentialFibonacci { get; private set; }

        public GridCell(IFibonacciCheckerService fibonacciCheckerService)
        {
            _fibonacciCheckerService = fibonacciCheckerService;
        }

        public void IncrementGridCellValue(int increment)
        {
            var isFibonacci = _fibonacciCheckerService.IsFibonacci(increment);

            Value += increment;
            IsFibonacci = isFibonacci;

        public void SetAsSequentialFibonacci()
        {
            IsSequentialFibonacci = true;
        }
    }
}
