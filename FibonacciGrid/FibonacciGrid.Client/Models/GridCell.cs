using FibonacciGrid.Client.Services;
using FibonacciGrid.Client.Services.DomainServices;

namespace FibonacciGrid.Client.Models
{
    public class GridCell
    {
        private readonly IFibonacciCheckerService _fibonacciCheckerService;

        public int Value { get; private set; }
        public bool IsFibonacci { get; private set; }
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
            IsFibonacci = false;
            IsSequentialFibonacci = false;
        }

        public void SetAsSequentialFibonacci()
        {
            IsSequentialFibonacci = true;
        }
    }
}
