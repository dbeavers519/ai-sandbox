#ifndef MINHEAP_H
#define MINHEAP_H

#include <cmath>
#include <cstring>
#include <iostream>

namespace Utilities {
	namespace Structures {
		template<class T>
		class MinHeapObj {
			private:
			public:
		};

		template<class T>
		class MinHeapArray {
			private:
				T* heap;
				int numNodes;
				int heapLength;
				int levels;
				int levelPower;
				void resize();
				int percolateUp(int hole, T item);
				int percolateDown(int hole, T item);

			public:
				MinHeapArray(int length);
				~MinHeapArray();
				int count();
				void addItem(T item);
				T getRoot();
				T findItem(T item);
				T deleteMin();
				void displayHeap();
		};

		template<class T>
		MinHeapArray<T>::MinHeapArray(int length) {
			// Waste 0
			heap = new T[length + 1];
			heapLength = length + 1;
			numNodes = 0;
			levels = 1;
			levelPower = 0;
		}

		template<class T>
		MinHeapArray<T>::~MinHeapArray() {
		}

		template<class T>
		int MinHeapArray<T>::count() {
			return numNodes;
		}

		template<class T>
		void MinHeapArray<T>::resize() {
			int newLength = heapLength * 2;
			T* newHeap = new T[newLength];
			memcpy(newHeap, heap, newLength * sizeof(T));
			delete[] heap;
			heap = newHeap;
		}

		template<class T>
		int MinHeapArray<T>::percolateUp(int hole, T item) {
			while (hole > 1 && item < heap[hole / 2])
			{
				heap[hole] = heap[hole / 2];
				hole /= 2;
			}
			return hole;
		}

		template<class T>
		int MinHeapArray<T>::percolateDown(int hole, T item) {
			int left = 0;
			int right = 0;
			int swap = 0;
			while (2 * hole <= numNodes)
			{
				swap = 0;
				left = 2 * hole;
				right = left + 1;
				if (item > heap[left])
				{
					swap = left;
				}
				else if (item > heap[right])
				{
					swap = right;
				}
				if (swap != 0)
				{
					heap[hole] = heap[swap];
					hole = swap;
				}
				else
				{
					return hole;
				}
			}
			return hole;
		}

		template<class T>
		void MinHeapArray<T>::addItem(T item) {
			int hole;
			if (numNodes == heapLength - 1)
			{
				// Resize
				resize();
			}
			numNodes++;
			hole = percolateUp(numNodes, item);
			heap[hole] = item;

			if (pow(2, levelPower) < numNodes)
			{
				levels++;
				levelPower++;
			}
		}

		template<class T>
		T MinHeapArray<T>::getRoot() {
			return heap[1];
		}

		template<class T>
		T MinHeapArray<T>::findItem(T item) {
			for (int i = 1; i <= numNodes; i++)
			{
				if (heap[i] == item)
				{
					return heap[i];
				}
			}
			return NULL;
		}

		template<class T>
		T MinHeapArray<T>::deleteMin() {
			if (numNodes == 0)
			{
				// Error
				std::cout << "Error in deleteMin(): Heap is empty.\n";
				return NULL;
			}
			T result = heap[1];
			numNodes--;
			int hole = percolateDown(1, heap[numNodes + 1]);
			heap[hole] = heap[numNodes + 1];
			return result;
		}

		template<class T>
		void MinHeapArray<T>::displayHeap() {
			if (numNodes < 1)
			{
				// Error
				std::cout << "Error in DisplayHeap(): No nodes in heap.\n";
			}

			for (int i = 1; i <= levels; i++)
			{
				// Initial row tabs
				for (int j = 1; j <= pow(2, levels - i); j++)
				{
					std::cout << "\t";
				}
				// Children in row
				for (int j = (int)pow(2, i - 1); j < fmin(pow(2, i), numNodes + 1); j++)
				{
					std::cout << heap[j];

					// Tabs between children
					for (int k = 1; k <= pow(2, levels - i + 1); k++)
					{
						std::cout << "\t";
					}
				}
				std::cout << "\n";
			}
		}
	}
}
#endif