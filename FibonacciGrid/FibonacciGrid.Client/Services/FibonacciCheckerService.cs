using System;
using System.Collections.Generic;

namespace FibonacciGrid.Client.Services
{
    public class FibonacciCheckerService : IFibonacciCheckerService
    {
        private readonly List<int> _cachedFibonacciList = new List<int>();
        private readonly List<int> _cachedNotFibonacciList = new List<int>();

        public bool IsFibonacci(int number)
        {
            if (_cachedFibonacciList.Contains(number)) { return true; }

            if (_cachedNotFibonacciList.Contains(number)) { return false; }

            var isFibonacci = CheckIfIsFibonacci(number);
            CacheFibonacciResult(number, isFibonacci);

            return isFibonacci;
        }

        private static bool CheckIfIsFibonacci(int number)
        {
            if (number == 0)
            {
                return false;
            }

            var fiveNSquare = 5 * number * number;
            return IsPerfectSquare(fiveNSquare - 4) || IsPerfectSquare(fiveNSquare + 4);
        }

        private static bool IsPerfectSquare(int number)
        {
            var squareRoot = (int) Math.Sqrt(number);
            return (squareRoot * squareRoot == number);
        }

        private void CacheFibonacciResult(int number, bool isFibonacci)
        {
            if (isFibonacci)
            {
                _cachedFibonacciList.Add(number);
            }
            else
            {
                _cachedNotFibonacciList.Add(number);
            }
        }
    }
}
