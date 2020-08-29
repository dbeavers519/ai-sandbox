using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AISandbox.AI;
using AISandbox.Nodes;
using AISandbox.Utilities.Structures;
namespace AISandbox
{
    public partial class AISandbox : Form
    {
        public AISandbox()
        {
            InitializeComponent();

            Debug.WriteLine("** Map 0 **");

            SimpleMap map0 = new SimpleMap(10, 10);
            map0.LoadMapFromFile("../../Resources/Maps/Maze0.txt");
            map0.UpdateDisplayMap();
            map0.DisplayMap();

            Debug.WriteLine("");

            AStarAI ai = new AStarAI(new Node(1, 9), new Node(7, 4), map0);
            ai.Plan();
            ai.OutlinePlan();
            ai.DisplayPlan();

            Debug.WriteLine("\n** Map 1 **");

            SimpleMap map1 = new SimpleMap(10, 10);
            map1.LoadMapFromFile("../../Resources/Maps/Maze1.txt");
            map1.UpdateDisplayMap();
            map1.DisplayMap();

            Debug.WriteLine("");

            ai = new AStarAI(new Node(1, 0), new Node(9, 0), map1);
            ai.Plan();
            ai.OutlinePlan();
            ai.DisplayPlan();

            Debug.WriteLine("\n** Map 2 **");

            SimpleMap map2 = new SimpleMap(10, 10);
            map2.LoadMapFromFile("../../Resources/Maps/Maze2.txt");
            map2.UpdateDisplayMap();
            map2.DisplayMap();

            Debug.WriteLine("");

            ai = new AStarAI(new Node(0, 5), new Node(9, 2), map2);
            ai.Plan();
            ai.OutlinePlan();
            ai.DisplayPlan();
        }
    }
}
