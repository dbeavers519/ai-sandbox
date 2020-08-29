using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using AISandbox.Nodes;
using AISandbox.Utilities.Structures;
using AISandbox.Utilities.Enums;

namespace AISandbox
{
    namespace AI
    {
        class AStarAI : BaseAI
        {
            private Node currNode;
            private Node goalNode;
            private SimpleMap map;
            private List<Node> currPlan;
            private bool debug;

            public AStarAI(Node startNode, Node goalNode, SimpleMap map, bool debug = false)
            {
                this.currNode = startNode;
                this.currNode.SetCostFromStart(0);
                this.goalNode = goalNode;
                this.map = map;
                this.debug = debug;
            }

            public void Plan()
            {
                // Initialize lists
                MinHeap<Node> openList = new MinHeap<Node>(map.GetNumNodes());
                List<Node> closedList = new List<Node>();
                Node[] neighbors = new Node[8];
                Node q;
                Node result;

                // Put start node on open list
                openList.AddItem(currNode);
                // While open list isn't empty
                while(openList.Count() > 0)
                {
                    // Pop node with least heuristic value ('f') on open list ('q')
                    q = openList.DeleteMin();

                    if (debug) { Debug.WriteLine("Current Node: " + q); }

                    // Add q to closed list
                    closedList.Add(q);

                    if(q.Equals(goalNode))
                    {
                        TracePath(q);
                        return;
                    }

                    // Get q's neighbors
                    neighbors = GetNeighbors(q);

                    // For each neighbor ('p')
                    for (int i = 0; i < neighbors.Length; i++) {
                        if (neighbors[i] != null)
                        {
                            // If p is on the closed list, skip p
                            result = closedList.Find(m => m.Equals(neighbors[i]));
                            if (result != null)
                            {
                                continue;
                            }

                            if (debug) { Debug.Write(neighbors[i] + " ==> "); }
                            // p.g = q.g + distance between p and q
                            neighbors[i].SetCostFromStart(q.GetCostFromStart() + neighbors[i].CalculateDistance(q, TraversalTypes.Euclidean));

                            // p.h = distance from r to p
                            neighbors[i].SetCostToGoal(neighbors[i].CalculateDistance(goalNode, TraversalTypes.Euclidean));

                            // p.f = p.g + p.h
                            neighbors[i].UpdateEstimate();
                            if (debug) { Debug.WriteLine(neighbors[i]); }

                            // If p is on the open list with a lower f, skip p
                            result = openList.FindItems(neighbors[i]).Where(m => m.CompareTo(neighbors[i]) < 0).FirstOrDefault();
                            if (result != null)
                            {
                                continue;
                            }

                            // Set neighbor node's parent to the current node
                            neighbors[i].SetParentNode(q);

                            // Otherwise add p to open list
                            openList.AddItem(neighbors[i]);
                        }
                    }
                    if (debug) { Debug.WriteLine(""); }
                }
            }

            public void OutlinePlan()
            {
                var displayNum = 1;
                for(int i = currPlan.Count - 1; i >= 0; i--)
                {
                    if (debug) { Debug.WriteLine(string.Format("Node {0}: {1}", displayNum, currPlan[i])); }
                    displayNum++;
                }
            }

            public void DisplayPlan()
            {
                map.OverlayPlan(currPlan);

                map.DisplayMap();
            }

            private Node[] GetNeighbors(Node q)
            {
                Node[] neighborNodes = new Node[8];

                // Order of neighbors to check
                neighborNodes[0] = GetNeighbor(q, Directions.N);
                neighborNodes[1] = GetNeighbor(q, Directions.S);
                neighborNodes[2] = GetNeighbor(q, Directions.E);
                neighborNodes[3] = GetNeighbor(q, Directions.W);

                neighborNodes[4] = GetNeighbor(q, Directions.NE);
                neighborNodes[5] = GetNeighbor(q, Directions.NW);
                neighborNodes[6] = GetNeighbor(q, Directions.SE);
                neighborNodes[7] = GetNeighbor(q, Directions.SW);

                return neighborNodes;
            }

            private bool IsValidNeighbor(int x, int y)
            {
                if(x < 0 || x >= map.GetWidth() || y < 0 || y >= map.GetHeight())
                {
                    return false;
                } else if(map.GetNode(x, y).IsImpassable())
                {
                    return false;
                }
                return true;
            }

            private void TracePath(Node currNode)
            {
                Node cNode = currNode;
                currPlan = new List<Node>();

                if (debug) { Debug.WriteLine("Total Path Cost: " + currNode.GetCostFromStart().ToString()); }

                while (cNode != null)
                {
                    currPlan.Add(cNode);
                    cNode = cNode.GetParentNode();
                }
            }

            private Quadrants_2D GetGoalDirection(Node currNode)
            {
                int goalX = goalNode.GetX();
                int goalY = goalNode.GetY();
                int currX = currNode.GetX();
                int currY = currNode.GetY();
                if (goalX <= currX && goalY >= currY)
                {
                    return Quadrants_2D.NW;
                } else if (goalX >= currX && goalY >= currY)
                {
                    return Quadrants_2D.NE;
                } else if (goalX >= currX && goalY <= currY)
                {
                    return Quadrants_2D.SE;
                } else
                {
                    return Quadrants_2D.SW;
                }
            }

            private Node GetNeighbor(Node node, Directions dir)
            {
                int currX = node.GetX();
                int currY = node.GetY();
                int newX = 0;
                int newY = 0;
                Node tmpNode = new Node(0, 0);
                switch(dir)
                {
                    case Directions.N:
                        newX = currX ;
                        newY = currY + 1;
                        break;
                    case Directions.NE:
                        newX = currX + 1;
                        newY = currY + 1;
                        break;
                    case Directions.E:
                        newX = currX + 1;
                        newY = currY;
                        break;
                    case Directions.SE:
                        newX = currX + 1;
                        newY = currY - 1;
                        break;
                    case Directions.S:
                        newX = currX;
                        newY = currY - 1;
                        break;
                    case Directions.SW:
                        newX = currX - 1;
                        newY = currY - 1;
                        break;
                    case Directions.W:
                        newX = currX - 1;
                        newY = currY;
                        break;
                    case Directions.NW:
                        newX = currX - 1;
                        newY = currY + 1;
                        break;
                    default:
                        throw new Exception("Error in GetNeighbor(): Neighbor direction not implemented.");
                }

                if(IsValidNeighbor(newX, newY))
                {
                    tmpNode = map.GetNode(newX, newY);
                    return new Node(tmpNode);
                } else
                {
                    return null;
                }
            }
        }
    }
}
