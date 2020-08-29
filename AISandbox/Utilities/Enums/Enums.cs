using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISandbox.Utilities.Enums
{
    enum TraversalTypes : int
    {
        Manhattan = 0, // 4-directions (N, S, E, W)
        Diagonal = 1, // 8-directions (NW, N, NE, W, E, SW, S, SE)
        Euclidean = 2 // Any direction
    }

    enum Directions : int
    {
        N = 0,
        NE = 1,
        E = 2,
        SE = 3,
        S = 4,
        SW = 5,
        W = 6,
        NW = 7
    }

    enum Quadrants_2D : int
    {
        // Quadrants in clock-wise order starting at NW (top-left)
        NW = 0,
        NE = 1,
        SE = 2,
        SW = 3
    }
}
