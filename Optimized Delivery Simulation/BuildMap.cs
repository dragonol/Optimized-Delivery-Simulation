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
        public void GenerateMap(int height, int width, int splitChance, int averageDistance)
        {
            Map = new Unit[height, width];
            NodeUnit start = new NodeUnit(random.Next(0, height - 1), random.Next(0, width - 1));
            BuildMap(start, height, width, splitChance, averageDistance);
        }
        public (bool build, NodeUnit node) Process(NodeUnit nodeUnit, int[] lim, int direction)
        {
            unsafe
            {
                int[] position = new int[2];
                position[0] = (int)nodeUnit.Point.X;
                position[1] = (int)nodeUnit.Point.Y;
                int x = (int)nodeUnit.Point.X;
                int y = (int)nodeUnit.Point.Y;

                int i = 1;
                int temp = 0;
                int* m = &temp;
                int* n = &temp;
                if (direction % 2 == 0)
                    n = &i;
                else
                    m = &i;

                Unit curr;

                if (nodeUnit.AdjacentNodes[direction] == null)
                {
                    for (; i <= Math.Abs(lim[direction] - position[direction % 2]); i++)
                    {
                        curr = Map[y + *m * (direction % 2 * 2 - 1), x + *n * (direction % 2 * 2 - 1)];
                        if ((curr as NodeUnit) != null)
                        {
                            NodeUnit.Connect((NodeUnit)curr, nodeUnit, direction, (direction + 2) % 4, i);
                            break;
                        }
                        else if ((curr as RouteUnit) != null)
                        {
                            NodeUnit.CreateNode((RouteUnit)curr);
                            break;
                        }
                        else 
                            Map[y + *m * (direction % 2 * 2 - 1), x + *n * (direction % 2 * 2 - 1)] = 
                                new RouteUnit(y + *m * (direction % 2 * 2 - 1), x + *n * (direction % 2 * 2 - 1), direction % 2);
                    }

                    i--;
                    if (Map[y + *m * (direction % 2 * 2 - 1), x + *n * (direction % 2 * 2 - 1)] == null) 
                    {
                        curr = Map[y + *m * (direction % 2 * 2 - 1), x + *n * (direction % 2 * 2 - 1)] =
                            new NodeUnit(y + *m * (direction % 2 * 2 - 1), x + *n * (direction % 2 * 2 - 1));
                        NodeUnit.Connect((NodeUnit)curr, nodeUnit, direction, (direction + 2) % 4, i);
                        return (true, (NodeUnit)curr);
                    }
                }
            }
            return (false, null);
        }
        public void BuildMap(NodeUnit nodeUnit, int height, int width, int splitChance, int aveDist)
        {
            int x = (int)nodeUnit.Point.X;
            int y = (int)nodeUnit.Point.Y;
            int[] distance = new int[4];
            int[] lim = new int[4];

            Map[y, x] = nodeUnit;

            lim[Direction.Left] = Math.Max(0, x - random.Next((int)Math.Round(aveDist * 0.5), (int)Math.Round(aveDist * 1.5)));
            lim[Direction.Up] = Math.Max(0, y - random.Next((int)Math.Round(aveDist * 0.5), (int)Math.Round(aveDist * 1.5)));
            lim[Direction.Right] = Math.Min(width - 1, x + random.Next((int)Math.Round(aveDist * 0.5), (int)Math.Round(aveDist * 1.5)));
            lim[Direction.Down] = Math.Min(height - 1, y + random.Next((int)Math.Round(aveDist * 0.5), (int)Math.Round(aveDist * 1.5)));

            (bool build, NodeUnit node) result;
            if ((result = Process(nodeUnit, lim, Direction.Left)).build)
                BuildMap(result.node, height, width, splitChance, aveDist);
            if ((result = Process(nodeUnit, lim, Direction.Right)).build)
                BuildMap(result.node, height, width, splitChance, aveDist);
            if ((result = Process(nodeUnit, lim, Direction.Up)).build)
                BuildMap(result.node, height, width, splitChance, aveDist);
            if ((result = Process(nodeUnit, lim, Direction.Down)).build)
                BuildMap(result.node, height, width, splitChance, aveDist);

            //// Left
            //if (nodeUnit.AdjacentNodes[Direction.Left] == null) 
            //{
            //    bool check = true;
            //    for(int i = 1; i <= Math.Abs(lim[Direction.Left]-x); i++)
            //    {
            //        Unit curr = Map[y, x - i];
            //        if ((curr as NodeUnit) != null)
            //        {
            //            NodeUnit.Connect((NodeUnit)curr, nodeUnit, Direction.Left, Direction.Right, i);
            //            check = false;
            //            break;
            //        }
            //        else if ((curr as RouteUnit) != null) 
            //        {
            //            NodeUnit.CreateNode((RouteUnit)curr);
            //            check = false;
            //            break;
            //        }
            //        else
            //            Map[y, x - i] = new RouteUnit(y, x - i, Axis.Horizonal);
            //    }

            //}

            //// Right
            //if (nodeUnit.AdjacentNodes[Direction.Right] == null)
            //{
            //    for (int i = 1; i <= Math.Abs(lim[Direction.Right] - x); i++)
            //    {
            //        Unit curr = Map[y, x + i];
            //        if ((curr as NodeUnit) != null)
            //        {
            //            NodeUnit.Connect((NodeUnit)curr, nodeUnit, Direction.Right, Direction.Left, i);
            //            break;
            //        }
            //        else if ((curr as RouteUnit) != null)
            //        {
            //            NodeUnit.CreateNode((RouteUnit)curr);
            //            break;
            //        }
            //        else
            //            Map[y, x + i] = new RouteUnit(y, x + i, Axis.Horizonal);
            //    }
            //}

            ////Up
            //if (nodeUnit.AdjacentNodes[Direction.Up] == null)
            //{
            //    for (int i = 1; i <= Math.Abs(lim[Direction.Up] - y); i++)
            //    {
            //        Unit curr = Map[y - i, x];
            //        if ((curr as NodeUnit) != null)
            //        {
            //            NodeUnit.Connect((NodeUnit)curr, nodeUnit, Direction.Up, Direction.Down, i);
            //            break;
            //        }
            //        else if ((curr as RouteUnit) != null)
            //        {
            //            NodeUnit.CreateNode((RouteUnit)curr);
            //            break;
            //        }
            //        else
            //            Map[y - i, x] = new RouteUnit(y - i, x, Axis.Vertical);
            //    }
            //}

            ////Down
            //if (nodeUnit.AdjacentNodes[Direction.Down] == null)
            //{
            //    for (int i = 1; i <= Math.Abs(lim[Direction.Down] - y); i++)
            //    {
            //        Unit curr = Map[y + i, x];
            //        if ((curr as NodeUnit) != null)
            //        {
            //            NodeUnit.Connect((NodeUnit)curr, nodeUnit, Direction.Down, Direction.Up, i);
            //            break;
            //        }
            //        else if ((curr as RouteUnit) != null)
            //        {
            //            NodeUnit.CreateNode((RouteUnit)curr);
            //            break;
            //        }
            //        else
            //            Map[y + i, x] = new RouteUnit(y + i, x, Axis.Vertical);
            //    }
            //}



        }
    }
}
