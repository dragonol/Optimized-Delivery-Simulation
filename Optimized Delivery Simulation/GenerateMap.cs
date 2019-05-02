using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Optimized_Delivery_Simulation
{
    partial class MainWindow
    {
        public void GenerateMap(Unit[,] map, int height, int width, Brush color, int space = 50, int thickness = 30, float dist = 0.2f, int splitChance = 4)
        {
            // Initialize map
            map = new Unit[height, width];
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = new Unit();

            // Start node
            Node startNode = new Node(true, true, true, true, width / 2, height / 2);

            // Build map
            int aveDist = (int)((height + width) / 2 * dist);
            BuildMap(startNode, map, splitChance, aveDist);

            // Draw map
            bool[,] lookUp = new bool[height, width];
            lookUp.Initialize();
            DrawMap(startNode, lookUp, 40, 25, Brushes.Gray);
        }
    }
}
