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
        public static int[] TrafficPool = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3 };
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
        public static class Traverse
        {
            public static bool Next = true;
            public static bool Previous = false;
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
            public Unit this[System.Windows.Point point] { get => units[(int)point.Y, (int)point.X]; set => units[(int)point.Y, (int)point.X] = value; }
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
            public bool isNull(System.Windows.Point point)
            {
                return this[point] == null;
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
            public static double Traffic(Unit unit1, Unit unit2)
            {
                if (unit1.point.X == unit2.point.X)
                {
                    if (unit1.point.Y > unit2.point.Y)
                        return ((NodeUnit)unit1).AdjacentTraffic[Direction.Up];
                    else
                        return ((NodeUnit)unit1).AdjacentTraffic[Direction.Down];
                }
                else
                {
                    if (unit1.point.X > unit2.point.X) 
                        return ((NodeUnit)unit1).AdjacentTraffic[Direction.Left];
                    else
                        return ((NodeUnit)unit1).AdjacentTraffic[Direction.Right];
                }
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
            private int[] adjacentTraffic;

            public NodeUnit[] AdjacentNodes { get => adjacentNodes; set => adjacentNodes = value; }
            public int[] AdjacentDistances { get => adjacentDistances; set => adjacentDistances = value; }
            public int[] AdjacentTraffic { get => adjacentTraffic; set => adjacentTraffic = value; }

            public NodeUnit(int y, int x) : base(y, x)
            {
                adjacentNodes = new NodeUnit[4];
                adjacentDistances = new int[4];
                adjacentTraffic = new int[4];
                Map.Nodes.Add(this);
            }
            public NodeUnit(Point point) : base(point)
            {
                adjacentNodes = new NodeUnit[4];
                adjacentDistances = new int[4];
                adjacentTraffic = new int[4];
                Map.Nodes.Add(this);
            }
            public static void Connect(NodeUnit node1, NodeUnit node2, int dirNode1, int dirNode2, int distance, int traffic)
            {
                node1.AdjacentNodes[dirNode2] = node2;
                node1.AdjacentDistances[dirNode2] = distance;
                node1.AdjacentTraffic[dirNode2] = traffic;

                if (!MapDirection.IsFrozen)
                    AddDirection(node1.Point, dirNode2, MapDirection);

                node2.AdjacentNodes[dirNode1] = node1;
                node2.AdjacentDistances[dirNode1] = distance;
                node2.AdjacentTraffic[dirNode1] = traffic;

                if (!MapDirection.IsFrozen)
                    AddDirection(node2.Point, dirNode1, MapDirection);
            }
            public static void ConnectOneWay(NodeUnit node1, NodeUnit node2, int dirNode1, int dirNode2, int distance, int traffic)
            {
                    node1.AdjacentNodes[dirNode2] = node2;
                    node1.AdjacentDistances[dirNode2] = distance;
                    node1.AdjacentTraffic[dirNode2] = traffic;

                    if (!MapDirection.IsFrozen)
                        AddDirection(node1.Point, dirNode2, MapDirection);
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

                    if (node1.adjacentNodes[Direction.Left] == null)
                    {
                        ConnectOneWay(newNode, node1, Direction.Left, Direction.Right, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                        ConnectOneWay(node2, newNode, Direction.Left, Direction.Right, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                    }
                    else if (node2.adjacentNodes[Direction.Right] == null)
                    {
                        ConnectOneWay(newNode, node2, Direction.Right, Direction.Left, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                        ConnectOneWay(node1, newNode, Direction.Right, Direction.Left, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                    }
                    else
                    {
                        Connect(newNode, node1, Direction.Left, Direction.Right, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                        Connect(newNode, node2, Direction.Right, Direction.Left, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                    }
                    //Connect(newNode, node1, Direction.Left, Direction.Right, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                    //Connect(newNode, node2, Direction.Right, Direction.Left, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                }
                else
                {
                    while ((node1 = (Map[y + run1, x] as NodeUnit)) == null)
                        run1++;
                    while ((node2 = (Map[y - run2, x] as NodeUnit)) == null)
                        run2++;

                    newNode = new NodeUnit(routeUnit.Point);

                    if (node1.adjacentNodes[Direction.Up] == null)
                    {
                        ConnectOneWay(newNode, node1, Direction.Up, Direction.Down, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                        ConnectOneWay(node2, newNode, Direction.Up, Direction.Down, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                    }
                    else if (node2.adjacentNodes[Direction.Down] == null)
                    {
                        ConnectOneWay(newNode, node2, Direction.Down, Direction.Up, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                        ConnectOneWay(node1, newNode, Direction.Down, Direction.Up, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                    }
                    else
                    {
                        Connect(newNode, node1, Direction.Up, Direction.Down, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                        Connect(newNode, node2, Direction.Down, Direction.Up, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                    }
                    //Connect(newNode, node1, Direction.Up, Direction.Down, run1, TrafficPool[Random.Next(TrafficPool.Length)]);
                    //Connect(newNode, node2, Direction.Down, Direction.Up, run2, TrafficPool[Random.Next(TrafficPool.Length)]);
                }
                Map[y, x] = newNode;
            }
        }

        public class Trail
        {
            private Point previous;
            private double distance;

            public Point Previous { get => previous; set => previous = value; }
            public double Distance { get => distance; set => distance = value; }
            public Trail(Point previous, int distance)
            {
                Previous = previous;
                Distance = distance;
            }
        }

        public class RouteNode
        {
            private Point point;
            private RouteNode next;
            private RouteNode previous;

            public Point Point { get => point; set => point = value; }
            public RouteNode Next { get => next; set => next = value; }
            public RouteNode Previous { get => previous; set => previous = value; }
            public RouteNode(Point point, RouteNode next, RouteNode pre)
            {
                Point = point;
                Next = next;
                Previous = pre;
            }
        }

        public class Route
        {
            private RouteNode first;
            private RouteNode last;
            private int count;

            public RouteNode First { get => first; set => first = value; }
            public RouteNode Last { get => last; set => last = value; }
            public int Count { get => count; set => count = value; }

            public void AddLast(Point point)
            {
                count++;
                if (last == null)
                {
                    first = last = new RouteNode(point, null, null);
                    return;
                }
                last.Next = new RouteNode(point, null, last);
                last = last.Next;
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
            public System.Windows.Point ToPoint()
            {
                return new System.Windows.Point(x, y);
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
            public static implicit operator System.Windows.Point(Point p)
            {
                return new System.Windows.Point(p.x, p.y);
            }
            public override string ToString()
            {
                return x + ", " + y;
            }
        }

        
    }
}