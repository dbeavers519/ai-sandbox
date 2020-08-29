using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using AISandbox.Nodes;

namespace AISandbox
{
    // 2D map structure treats x as width, y as height. A map of width 5, height 8 results in a 2D array of 8 rows, 5 columns
    class SimpleMap
    {
        #region Variables
        private Node[][] map;
        private char[,] displayMap;
        private int width;
        private int height;
        private bool displayNeedsUpdate;
        #endregion

        #region Constructors
        public SimpleMap(int width, int height)
        {
            map = new Node[height][];
            for(int i = 0; i < height; i++)
            {
                map[i] = new Node[width];
            }

            if(width < 2 || height < 2)
            {
                throw new Exception("Map must be at least 2 x 2.");
            }
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    map[i][j] = new Node(j, i);
                }
            }

            this.width = width;
            this.height = height;

            ConstructDisplayMap();
            displayNeedsUpdate = false;
        }
        #endregion

        #region Access Functions
        public Node[][] GetMap()
        {
            return map;
        }

        public int GetNumNodes()
        {
            return width * height;
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public Node GetNode(int x, int y)
        {
            return map[y][x];
        }

        public void SetNode(Node newNode)
        {
            map[newNode.GetY()][newNode.GetX()] = newNode;

            displayNeedsUpdate = true;
        }

        public bool DoesDisplayNeedUpdate()
        {
            return displayNeedsUpdate;
        }
        #endregion

        #region Display Map Functions
        private void ConstructDisplayMap()
        {
            displayMap = new char[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[y][x].IsImpassable())
                    {
                        displayMap[y, x] = 'X';
                    }
                    else
                    {
                        displayMap[y, x] = '.';
                    }
                }
            }
        }

        public void UpdateDisplayMap()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[y][x].IsImpassable())
                    {
                        displayMap[y, x] = 'X';
                    }
                    else
                    {
                        displayMap[y, x] = '.';
                    }
                }
            }
        }

        public void OverlayPlan(List<Node> plan)
        {
            for(int i = 0; i < plan.Count; i++)
            {
                if(displayMap[plan[i].GetY(), plan[i].GetX()] == 'X')
                {
                    throw new Exception("Plan involves impassable terrain.");
                } else
                {
                    displayMap[plan[i].GetY(), plan[i].GetX()] = '*';
                }
            }
        }

        public void DisplayMap()
        {
            for(int y = height - 1; y >= 0; y--)
            {
                for(int x = 0; x < width; x++)
                {
                    Debug.Write(" " + displayMap[y, x] + " ");
                }
                Debug.Write(" \n");
            }
        }
        #endregion

        #region XML Map Functions
        public void LoadMap(string mapFileName)
        {
        }
        #endregion

        #region File Map Functions
        public void LoadMapFromFile(string fileName)
        {
            // Initialize variables
            StreamReader sr = new StreamReader(fileName);
            char currChar;
            int pointer = 0;
            int counter = 0;
            int rowCount = 0;
            int colCount = 0;
            int currRows = 10;
            int currBufferSize = 1024;
            char[] buffer = new char[currBufferSize];
            int[] rowCols = new int[currRows];

            // Read from file
            while (!sr.EndOfStream)
            {
                do
                {
                    currChar = (char)sr.Read();
                } while (currChar == ' ');

                if(currChar == '\r')
                {
                    currChar = (char)sr.Read();

                    rowCols[rowCount++] = colCount;

                    colCount = 0;

                    // Resize row cols if max row reached
                    if(rowCount == currRows)
                    {
                        int[] tmpRowCols = new int[currRows * 2];
                        for(int i = 0; i < currRows; i++)
                        {
                            tmpRowCols[i] = rowCols[i];
                        }
                        currRows *= 2;
                        rowCols = tmpRowCols;
                    }
                } else
                {
                    buffer[pointer++] = currChar;
                    colCount++;
                }

                // Resize buffer if max size reached
                if(pointer == currBufferSize)
                {
                    char[] tmpBuffer = new char[currBufferSize * 2];
                    for(int i = 0; i < currBufferSize; i++)
                    {
                        tmpBuffer[i] = buffer[i];
                    }
                    buffer = tmpBuffer;
                    currBufferSize *= 2;
                }
            }

            rowCols[rowCount++] = colCount;

            // Build map from buffer
            map = new Node[rowCount][];
            for(int i = 0; i < rowCount; i++)
            {
                map[rowCount - i - 1] = new Node[rowCols[i]];
                for(int j = 0; j < rowCols[i]; j++)
                {
                    map[rowCount - i - 1][j] = new Node(j, rowCount - i - 1);
                    if(buffer[counter] == '0')
                    {
                        map[rowCount - i - 1][j].SetTerrain(100);
                    } else
                    {
                        map[rowCount - i - 1][j].SetTerrain(0);
                    }
                    counter++;
                }
            }
        }
        #endregion
    }
}
