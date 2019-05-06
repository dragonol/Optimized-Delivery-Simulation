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
using Priority_Queue;

namespace Optimized_Delivery_Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    partial class MainWindow
    {
        public class Axis
        {
            public static int Horizonal = 0;
            public static int Vertical = 1;
        }
        public static class Direction
        {
            public static int Left = 0;
            public static int Up = 1;
            public static int Right = 2;
            public static int Down = 3;
        }
        public class WorldMap
        {
            private Unit[,] units;
            private List<NodeUnit> nodes;
            private readonly int height;
            private readonly int width;

            public Unit[,] Units { get => units; set => units = value; }
            public Unit this[int y, int x] { get => units[y, x]; set => units[y, x] = value; }
            public Unit this[Point point] { get => units[point.Y, point.X]; set => units[point.Y, point.X] = value; }
            public List<NodeUnit> Nodes { get => nodes; set => nodes = value; }
            public int Height => height;
            public int Width => width;

            public WorldMap(int height, int width)
            {
                this.height = height;
                this.width = width;
                units = new Unit[height, width];
                nodes = new List<NodeUnit>();
            }
        }
        public class Unit
        {
            private Point point;

            public Point Point { get => point; set => point = value; }
            public Unit(int y, int x)
            {
                point = new Point(y, x);
            }
            public Unit(Point point)
            {
                Point = point;
            }
            public static int Distance(Unit unit1, Unit unit2)
            {
                return Math.Abs(unit1.Point.X - unit2.Point.X) + Math.Abs(unit1.Point.Y - unit2.Point.Y);
            }
        }
        public class RouteUnit : Unit
        {
            private int axis;

            public int Axis { get => axis; set => axis = value; }
            public RouteUnit(int y, int x, int axis) : base(y, x)
            {
                Axis = axis;
            }
        }
        public class NodeUnit : Unit
        {
            private NodeUnit[] adjacentNodes;
            private int[] adjacentDistances;

            public NodeUnit[] AdjacentNodes { get => adjacentNodes; set => adjacentNodes = value; }
            public int[] AdjacentDistances { get => adjacentDistances; set => adjacentDistances = value; }
            public NodeUnit(int y, int x) : base(y, x)
            {
                adjacentNodes = new NodeUnit[4];
                adjacentDistances = new int[4];
                Map.Nodes.Add(this);
            }
            public NodeUnit(Point point) : base(point)
            {
                adjacentNodes = new NodeUnit[4];
                adjacentDistances = new int[4];
                Map.Nodes.Add(this);
            }
            public static void Connect(NodeUnit node1, NodeUnit node2, int dirNode1, int dirNode2, int distance)
            {
                node1.AdjacentNodes[dirNode2] = node2;
                node1.AdjacentDistances[dirNode2] = distance;
                node2.AdjacentNodes[dirNode1] = node1;
                node2.AdjacentDistances[dirNode1] = distance;
            }
            public static void CreateNode(RouteUnit routeUnit)
            {
                int run1 = 1;
                int run2 = 1;
                int x = (int)routeUnit.Point.X;
                int y = (int)routeUnit.Point.Y;
                NodeUnit node1;
                NodeUnit node2;
                NodeUnit newNode;

                if (routeUnit.Axis == Axis.Horizonal)
                {
                    while ((node1 = (Map[y, x + run1] as NodeUnit)) == null)
                        run1++;
                    while ((node2 = (Map[y, x - run2] as NodeUnit)) == null)
                        run2++;

                    newNode = new NodeUnit(routeUnit.Point);

                    Connect(newNode, node1, Direction.Left, Direction.Right, run1);
                    Connect(newNode, node2, Direction.Right, Direction.Left, run2);
                }
                else
                {
                    while ((node1 = (Map[y + run1, x] as NodeUnit)) == null)
                        run1++;
                    while ((node2 = (Map[y - run2, x] as NodeUnit)) == null)
                        run2++;

                    newNode = new NodeUnit(routeUnit.Point);

                    Connect(newNode, node1, Direction.Up, Direction.Down, run1);
                    Connect(newNode, node2, Direction.Down, Direction.Up, run2);
                }
                Map[y, x] = newNode;
            }
        }

        public class Trail
        {
            private Point previous;
            private int distance;

            public Point Previous { get => previous; set => previous = value; }
            public int Distance { get => distance; set => distance = value; }
            public Trail(Point previous, int distance)
            {
                Previous = previous;
                Distance = distance;
            }
        }

        public class Point : FastPriorityQueueNode, IEquatable<Point>
        {
            private int x;
            private int y;

            public int X { get => x; set => x = value; }
            public int Y { get => y; set => y = value; }

            public Point(int y, int x)
            {
                this.x = x;
                this.y = y;
            }
            public static bool operator==(Point a, Point b)
            {
                return a.x == b.x && a.y == b.y;
            }
            public static bool operator !=(Point a, Point b)
            {
                return a.x != b.x || a.y != b.y;
            }
            public bool Equals(Point other)
            {
                return this == other;
            }
        }

        
    }
}