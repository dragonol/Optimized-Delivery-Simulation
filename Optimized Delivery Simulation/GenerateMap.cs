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
        public void GenerateMap(Brush color)
        {
            int height = MapSize.height;
            int width = MapSize.width;
            Map = new WorldMap(height, width);

            NodeUnit start = new NodeUnit(Random.Next(0, height - 1), Random.Next(0, width - 1));
            BuildMap(start);

            void BuildMap(NodeUnit nodeUnit)
            {
                int x = (int)nodeUnit.Point.X;
                int y = (int)nodeUnit.Point.Y;
                int[] lim = new int[4];

                Map[y, x] = nodeUnit;

                double distance = AverageDistance * (width + height) / 2;
                lim[Direction.Left] = Math.Max(0, x - Random.Next((int)distance, (int)(distance * 1.5)));
                lim[Direction.Up] = Math.Max(0, y - Random.Next((int)distance, (int)(distance * 1.5)));
                lim[Direction.Right] = Math.Min(width - 1, x + Random.Next((int)distance, (int)(distance * 1.5)));
                lim[Direction.Down] = Math.Min(height - 1, y + Random.Next((int)distance, (int)(distance * 1.5)));

                NodeUnit result;
                if (Process(nodeUnit, lim, Direction.Left, out result))
                    BuildMap(result);
                if (Process(nodeUnit, lim, Direction.Right, out result))
                    BuildMap(result);
                if (Process(nodeUnit, lim, Direction.Up, out result))
                    BuildMap(result);
                if (Process(nodeUnit, lim, Direction.Down, out result))
                    BuildMap(result);
            }
            bool Process(NodeUnit nodeUnit, int[] lim, int direction, out NodeUnit result)
            {
                result = null;

                if (direction == 0 && nodeUnit.Point.X == 0)
                    return false;
                if (direction == 1 && nodeUnit.Point.Y == 0)
                    return false;
                if (direction == 2 && nodeUnit.Point.X == width - 1)
                    return false;
                if (direction == 3 && nodeUnit.Point.Y == height - 1)
                    return false;
                if (nodeUnit.AdjacentNodes[direction] != null || Random.Next(0, SplitChance) == 0)
                    return false;

                unsafe
                {
                    int[] position = new int[2];
                    position[0] = (int)nodeUnit.Point.X;
                    position[1] = (int)nodeUnit.Point.Y;
                    int x = (int)nodeUnit.Point.X;
                    int y = (int)nodeUnit.Point.Y;

                    int temp = 0;
                    int* m = &temp;
                    int* n = &temp;
                    *m = *n = 0;

                    int i = 1;
                    if (direction % 2 == 0)
                        n = &i;
                    else
                        m = &i;

                    Unit curr;

                    for (; i <= Math.Abs(lim[direction] - position[direction % 2]); i++)
                    {
                        curr = Map[y + *m * (direction / 2 * 2 - 1), x + *n * (direction / 2 * 2 - 1)];

                        if ((curr as NodeUnit) != null)
                        {
                            NodeUnit.Connect((NodeUnit)curr, nodeUnit, direction, (direction + 2) % 4, i);
                            DrawPath(nodeUnit.Point, curr.Point, color, Thickness);
                            return false;
                        }

                        if ((curr as RouteUnit) != null)
                        {
                            NodeUnit.CreateNode((RouteUnit)curr);
                            NodeUnit.Connect((NodeUnit)Map[curr.Point], nodeUnit, direction, (direction + 2) % 4, i);
                            DrawPath(nodeUnit.Point, curr.Point, color, Thickness);
                            return false;
                        }

                        Map[y + *m * (direction / 2 * 2 - 1), x + *n * (direction / 2 * 2 - 1)] =
                            new RouteUnit(y + *m * (direction / 2 * 2 - 1), x + *n * (direction / 2 * 2 - 1), direction % 2);
                    }

                    i--;

                    curr = Map[y + *m * (direction / 2 * 2 - 1), x + *n * (direction / 2 * 2 - 1)] =
                        new NodeUnit(y + *m * (direction / 2 * 2 - 1), x + *n * (direction / 2 * 2 - 1));

                    NodeUnit.Connect((NodeUnit)curr, nodeUnit, direction, (direction + 2) % 4, i);
                    DrawPath(nodeUnit.Point, curr.Point, color, Thickness);
                    
                    result = (NodeUnit)curr;
                    return true;
                }
            }
        }
    }
}
