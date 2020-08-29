using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AISandbox.Utilities.Enums;

namespace AISandbox
{
    namespace Nodes
    {
        class Node : BaseNode
        {
            #region Variables
            private int terrain; // 0 - 100 (0 => impassible, 100 => no inhibition)
            private int elevation; // 0 - 100
            private int cover; // 0 - 15 (bitwise operation) => N, S, E, W => 0000, 0000, 0000, 0000 (0000 => no cover, 1111 => full cover)
            private Node parentNode;
            #endregion

            #region Constructors
            public Node(int x, int y) : base(x, y)
            {
                this.terrain = 100;
                this.elevation = 0;
                this.cover = 0;
            }

            public Node(int x, int y, int terrain, int elevation, int cover) : base(x, y)
            {
                ValidateValues(terrain, elevation, cover);

                this.terrain = terrain;
                this.elevation = elevation;
                this.cover = cover;
            }

            public Node(int x, int y, int z) : base(x, y, z)
            {
                this.terrain = 100;
                this.elevation = 0;
                this.cover = 0;
            }

            public Node(int x, int y, int z, int terrain, int elevation, int cover) : base(x, y, z)
            {
                this.terrain = terrain;
                this.elevation = elevation;
                this.cover = cover;
            }

            public Node(Node node) : base(node.GetX(), node.GetY(), node.GetZ())
            {
                this.terrain = node.GetTerrain();
                this.elevation = node.GetElevation();
                this.cover = node.GetCover();
            }
            #endregion

            #region Access Functions
            public int GetTerrain()
            {
                return terrain;
            }

            public void SetTerrain(int terrain)
            {
                this.terrain = terrain;
            }

            public int GetElevation()
            {
                return elevation;
            }

            public void SetElevation(int elevation)
            {
                this.elevation = elevation;
            }

            public int GetCover()
            {
                return cover;
            }

            public void SetCover(int cover)
            {
                this.cover = cover;
            }

            public Node GetParentNode()
            {
                return parentNode;
            }

            public void SetParentNode(Node parentNode)
            {
                this.parentNode = parentNode;
            }
            #endregion

            public void ValidateValues(int terrain, int elevation, int cover)
            {
                int i = 0;
                string error = "";
                string[] errors = new string[3];
                if (terrain < 0 || terrain > 100)
                {
                    errors[i] += "Terrain must be between 0 and 100.\n";
                    i++;
                }
                if (elevation < 0 || elevation > 100)
                {
                    errors[i] += "Elevation must be between 0 and 100.\n";
                    i++;
                }
                if (cover < 0 || cover > 15)
                {
                    errors[i] += "Cover must be between 0 and 15.\n";
                    i++;
                }
                for (int j = 0; j < i; i++)
                {
                    error += errors[j];
                }
                throw new Exception(error);
            }

            public float CalculateDistance(Node target, TraversalTypes traversalType)
            {
                switch (traversalType)
                {
                    case TraversalTypes.Manhattan:
                        return Math.Abs(this.GetX() - target.GetX()) + Math.Abs(this.GetY() - target.GetY() + Math.Abs(this.GetZ() - target.GetZ()));
                    case TraversalTypes.Diagonal:
                        return Math.Max(Math.Abs(this.GetX() - target.GetX()), Math.Max(Math.Abs(this.GetY() - target.GetY()), Math.Abs(this.GetZ() - target.GetZ())));
                    case TraversalTypes.Euclidean:
                        return (float)Math.Sqrt(Math.Pow(this.GetX() - target.GetX(), 2) + Math.Pow(this.GetY() - target.GetY(), 2) + Math.Pow(this.GetZ() - target.GetZ(), 2));
                    default:
                        throw new Exception("Error in CalculateDistance(): Traversal type not implemented.");
                }
            }

            public bool IsImpassable()
            {
                return (terrain == 0 ? true : false);
            }

            public override string ToString()
            {
                return string.Format("({0}, {1}, {2}) => g: {3} h: {4} f: {5}", this.GetX(), this.GetY(), this.GetZ(), this.GetCostFromStart(), this.GetCostToGoal(), this.GetEstimatedCost());
            }
        }
    }
}
