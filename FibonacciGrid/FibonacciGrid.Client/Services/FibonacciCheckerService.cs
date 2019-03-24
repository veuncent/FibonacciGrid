using System;

namespace FibonacciGrid.Client.Services
{
    public class FibonacciCheckerService
    {
        public bool IsFibonacci(int number)
        {
            var fiveNSquare = 5 * number * number;
            return IsPerfectSquare(fiveNSquare - 4) || IsPerfectSquare(fiveNSquare + 4);
        }

        private static bool IsPerfectSquare(int number)
        {
            var squareRoot = (int) Math.Sqrt(number);
            return (squareRoot * squareRoot == number);
        }
    }
}
