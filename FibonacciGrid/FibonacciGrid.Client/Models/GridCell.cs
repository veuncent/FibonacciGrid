namespace FibonacciGrid.Client.Models
{
    public class GridCell
    {
        public int Value { get; private set; }

        public bool IsFibonacci { get; private set; } = true;

        public void SetGridCellValue(int value, bool isFibonacci)
        {
            Value = value;
            IsFibonacci = isFibonacci;
        }
    }
}
