using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISandbox.Nodes
{
    class BaseNode : IComparable
    {
        #region Variables
        private int x;
        private int y;
        private int z;
        private float costFromStart; // 'g' => cost to move to this Node from the start
        private float costToGoal; // 'h' => estimated cost to move from this Node to the goal
        private float estimatedCost; // 'f' => g + h
        #endregion

        #region Constructors
        public BaseNode(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;

            InitializeCosts();
        }

        public BaseNode(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            InitializeCosts();
        }

        public BaseNode(int x, int y, float nodeValue)
        {
            this.x = x;
            this.y = y;
            this.z = 0;

            InitializeCosts();
        }

        public BaseNode(int x, int y, int z, float nodeValue)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            InitializeCosts();
        }
        #endregion

        #region Access Functions
        #region Position Values
        public int GetX()
        {
            return x;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public int GetZ()
        {
            return z;
        }

        public void SetZ(int z)
        {
            this.z = z;
        }
        #endregion

        #region Costs
        public float GetCostFromStart()
        {
            return costFromStart;
        }

        public void SetCostFromStart(float costFromStart)
        {
            this.costFromStart = costFromStart;
        }

        public void UpdateCostFromStart(float costFromStart)
        {
            this.costFromStart = costFromStart;
            UpdateEstimate();
        }

        public void IncreaseCostFromStart(float costToAdd)
        {
            this.costFromStart += costToAdd;
            UpdateEstimate();
        }

        public void DecreaseCostFromStart(float costToSubtract)
        {
            this.costFromStart -= costToSubtract;
            UpdateEstimate();
        }

        public float GetCostToGoal()
        {
            return costToGoal;
        }

        public void SetCostToGoal(float costToGoal)
        {
            this.costToGoal = costToGoal;
        }

        public void UpdateCostToGoal(float costToGoal)
        {
            this.costToGoal = costToGoal;
            UpdateEstimate();
        }

        public void IncreaseCostToGoal(float costToAdd)
        {
            this.costToGoal += costToAdd;
            UpdateEstimate();
        }

        public void DecreaseCostToGoal(float costToSubtract)
        {
            this.costToGoal -= costToSubtract;
            UpdateEstimate();
        }

        public float GetEstimatedCost()
        {
            return estimatedCost;
        }

        public void UpdateEstimate()
        {
            this.estimatedCost = costFromStart + costToGoal;
        }
        #endregion
        #endregion

        private void InitializeCosts(float costFromStart = 0, float costToGoal = 0)
        {
            this.costFromStart = costFromStart;
            this.costToGoal = costToGoal;

            UpdateEstimate();
        }

        #region Operations
        // Compares estimated cost
        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return -1;
            }

            BaseNode otherNode = obj as BaseNode;
            if(obj != null)
            {
                return this.estimatedCost.CompareTo(otherNode.GetEstimatedCost());
            } else
            {
                throw new Exception("Object is not a BaseNode.");
            }
        }

        // Compares location values
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            BaseNode otherNode = obj as BaseNode;
            if (obj != null)
            {
                return (x == otherNode.GetX() && y == otherNode.GetY() && z == otherNode.GetZ());
            }
            else
            {
                return false;
            }
        }
        
        /*public static bool operator ==(BaseNode nodeA, BaseNode nodeB)
        {
            return nodeA.GetX() == nodeB.GetX() && nodeA.GetY() == nodeB.GetY() && nodeA.GetZ() == nodeB.GetZ();
        }

        public static bool operator !=(BaseNode nodeA, BaseNode nodeB)
        {
            return !(nodeA.GetX() == nodeB.GetX() && nodeA.GetY() == nodeB.GetY() && nodeA.GetZ() == nodeB.GetZ());
        }*/
        #endregion
    }
}
