﻿using System;
using System.Collections.Generic;
using FibonacciGrid.Client.Models;

namespace FibonacciGrid.Client.Services.DomainServices
{
    public interface IFibonacciNeighborService
    {
        List<List<GridCell>> FindNeighbors(IGrid grid, List<Tuple<int, int>> cellsToCheck);
    }
}