using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FibonacciGrid.Client.Models
{
    public class GridCell
    {
        public int Value { get; set; } = 0;

        public bool IsFibonacci { get; }
    }
}
