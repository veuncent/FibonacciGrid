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
            Value += increment;
            IsFibonacci = _fibonacciCheckerService.IsFibonacci(Value);
        }

        public void ResetValue()
        {
            Value = 0;
            IsFibonacci = true;
            IsSequentialFibonacci = false;
        }

        public void SetAsSequentialFibonacci()
        {
            IsSequentialFibonacci = true;
        }
    }
}
