#ifndef ENUMS_H
#define ENUMS_H
namespace Utilities {
	namespace Enums {
		enum class TraversalTypes {
			Manhattan = 0, // 4-directions (N, S, E, W)
			Diagonal, // 8-directions (NW, N, NE, W, E, SW, S, SE)
			Euclidean // Any direction
		};

		enum class Directions {
			N = 0,
			NE,
			E,
			SE,
			S,
			SW,
			W,
			NW
		};

		// Quadrants in clock-wise order starting at NW (top-left)
		enum class Quadrants_2D
		{
			NW = 0,
			NE,
			SE,
			SW
		};
	}
}
#endif