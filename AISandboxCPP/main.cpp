#include "MinHeap.h"
#include "Enums.h"
#include <iostream>

int main() {
	// Create map

	// Create AI

	// Perform A*

	// Display results
	Utilities::Enums::Directions d = Utilities::Enums::Directions::S;

	Utilities::Structures::MinHeapArray<int> mh(5);

	mh.addItem(5);
	std::cout << "Count: " << mh.count() << "\n";
	std::cout << "Root: " << mh.getRoot() << "\n";
	mh.displayHeap();

	mh.addItem(6);
	std::cout << "Count: " << mh.count() << "\n";
	std::cout << "Root: " << mh.getRoot() << "\n";
	mh.displayHeap();

	mh.addItem(2);
	std::cout << "Count: " << mh.count() << "\n";
	std::cout << "Root: " << mh.getRoot() << "\n";
	mh.displayHeap();

	mh.~MinHeapArray();

	Utilities::Structures::MinHeapArray<double> mh2(5);

	mh2.addItem(5.5);
	std::cout << "Count: " << mh2.count() << "\n";
	std::cout << "Root: " << mh2.getRoot() << "\n";
	mh2.displayHeap();

	mh2.addItem(6.6);
	std::cout << "Count: " << mh2.count() << "\n";
	std::cout << "Root: " << mh2.getRoot() << "\n";
	mh2.displayHeap();

	mh2.addItem(2.2);
	std::cout << "Count: " << mh2.count() << "\n";
	std::cout << "Root: " << mh2.getRoot() << "\n";
	mh2.displayHeap();

	mh2.~MinHeapArray();

	return 0;
}