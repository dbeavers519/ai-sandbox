using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AISandbox
{
    namespace Utilities
    {
        namespace Structures
        {
            public class MinHeap<T> where T : IComparable
            {
                private T[] heap;
                private int numNodes;
                private int levels;
                private int levelPower;

                public MinHeap(int length) {
                    // Waste 0
                    heap = new T[length + 1];
                    numNodes = 0;
                    levels = 1;
                    levelPower = 0;
                }

                public int Count()
                {
                    return numNodes;
                }

                public void AddItem(T item)
                {
                    int hole;
                    if(numNodes == heap.Length - 1)
                    {
                        // Resize
                        Resize();
                    }
                    numNodes++;
                    hole = PercolateUp(numNodes, item);
                    heap[hole] = item;

                    if(Math.Pow(2, levelPower) < numNodes)
                    {
                        levels++;
                        levelPower++;
                    }
                }

                public T GetRoot()
                {
                    return heap[1];
                }

                public T FindItem(T item)
                {
                    for (int i = 1; i <= numNodes; i++)
                    {
                        if (heap[i].Equals(item))
                        {
                            return heap[i];
                        }
                    }
                    return default;
                }

                public List<T> FindItems(T item)
                {
                    List<T> items = new List<T>();
                    for (int i = 1; i <= numNodes; i++)
                    {
                        if (heap[i].Equals(item))
                        {
                            items.Add(heap[i]);
                        }
                    }
                    return items;
                }

                public T DeleteMin()
                {
                    if(numNodes == 0)
                    {
                        Debug.Write("Error in DeleteMin(): Heap empty.\n");
                    }
                    T result = heap[1];
                    numNodes--;
                    int hole = PercolateDown(1, heap[numNodes + 1]);
                    heap[hole] = heap[numNodes + 1];
                    return result;
                }

                private void Resize()
                {
                    T[] newHeap = new T[heap.Length * 2];
                    for(int i = 1; i < heap.Length; i++)
                    {
                        newHeap[i] = heap[i];
                    }
                    heap = newHeap;
                }

                private int PercolateUp(int hole, T item)
                {
                    while (hole > 1 && item.CompareTo(heap[hole / 2]) < 0)
                    {
                        heap[hole] = heap[hole / 2];
                        hole /= 2;
                    }
                    return hole;
                }

                private int PercolateDown(int hole, T item)
                {
                    int left = 0;
                    int right = 0;
                    int swap = 0;
                    while(2 * hole <= numNodes)
                    {
                        swap = 0;
                        left = 2 * hole;
                        right = left + 1;
                        if(item.CompareTo(heap[left]) > 0)
                        {
                            swap = left;
                        } else if(item.CompareTo(heap[right]) > 0)
                        {
                            swap = right;
                        }
                        if(swap != 0)
                        {
                            heap[hole] = heap[swap];
                            hole = swap;
                        } else
                        {
                            return hole;
                        }
                    }
                    return hole;
                }

                public void DisplayHeap()
                {
                    if(numNodes < 1)
                    {
                        Debug.Write("Error in DisplayHeap(): No nodes in heap.\n");
                    }

                    for(int i = 1; i <= levels; i++)
                    {
                        // Initial row tabs
                        for(int j = 1; j <= Math.Pow(2, levels - i); j++)
                        {
                            Debug.Write("\t");
                        }
                        // Children in row
                        for(int j = (int)Math.Pow(2, i - 1); j < Math.Min(Math.Pow(2, i), numNodes + 1); j++)
                        {
                            Debug.Write(heap[j].ToString());

                            // Tabs between children
                            for (int k = 1; k <= Math.Pow(2, levels - i + 1); k++)
                            {
                                Debug.Write("\t");
                            }
                        }
                        Debug.Write("\n");
                    }
                }
            }
        }
    }
}
