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
        public void BuildMap(Node node, Unit[,] map, int splitChance, int aveDist)
        {
            int width = map.GetLength(1);
            int height = map.GetLength(0);
            int distance = random.Next(aveDist, (int)Math.Round(aveDist * 2.0));

            int limRight = Math.Min(width - 1, node.X + distance);
            int limLeft = Math.Max(0, node.X - distance);
            int limDown = Math.Min(height - 1, node.Y + distance);
            int limUp = Math.Max(0, node.Y - distance);

            map[node.Y, node.X].Status = true;
            map[node.Y, node.X].Node = node;
            Unit unit;
           
            //left
            if (node.GoLeft)
            {
                node.GoLeft = false;
                if (random.Next(0, splitChance) > 0)
                {
                    bool check = true;
                    for (int i = node.X - 1; i >= limLeft; i--)
                    {
                        unit = map[node.Y, i];
                        if (unit.Status)
                        {
                            if (unit.Node != null)
                            {
                                unit.Node.Right = node;
                                unit.Node.RightDis = Math.Abs(i - node.X);
                                unit.Node.GoRight = false;
                                node.Left = unit.Node;
                                node.LeftDis = Math.Abs(i - node.X);
                            }
                            else
                            {
                                Node node1;
                                Node node2;
                                int run1 = 1;
                                int run2 = 1;
                                while (map[node.Y - run1, i].Node == null)
                                    run1++;
                                while (map[node.Y + run2, i].Node == null)
                                    run2++;
                                node1 = map[node.Y - run1, i].Node;
                                node2 = map[node.Y + run2, i].Node;

                                Node newNode = new Node(i > 0, false, false, false, i, node.Y);
                                unit.Status = true;
                                unit.Node = newNode;

                                node.Left = newNode;
                                node.LeftDis = Math.Abs(i - node.X);
                                node1.Down = newNode;
                                node1.DownDis = run1;
                                node2.Up = newNode;
                                node2.UpDis = run2;

                                newNode.Right = node;
                                newNode.RightDis = node.LeftDis;
                                newNode.Up = node1;
                                newNode.UpDis = run1;
                                newNode.Down = node2;
                                newNode.DownDis = run2;
                            }
                            check = false;
                            break;
                        }
                        else
                            unit.Status = true;
                    }
                    if (check)
                    {
                        Node newNode = new Node(limLeft > 0, false, node.Y > 0, node.Y < height - 1, limLeft, node.Y);
                        newNode.Right = node;
                        newNode.RightDis = Math.Abs(limLeft - node.X);
                        node.Left = newNode;
                        node.LeftDis = Math.Abs(limLeft - node.X);
                        map[node.Y, limLeft].Node = newNode;
                        BuildMap(newNode, map,splitChance, aveDist);
                    }
                }
            }
            //right
            if (node.GoRight)
            {
                node.GoRight = false;
                if (random.Next(0, splitChance) > 0)
                {
                    bool check = true;
                    for (int i = node.X + 1; i <= limRight; i++)
                    {
                        unit = map[node.Y, i];
                        if (unit.Status)
                        {
                            if (unit.Node != null)
                            {
                                unit.Node.Left = node;
                                unit.Node.LeftDis = Math.Abs(i - node.X);
                                unit.Node.GoLeft = false;
                                node.Right = unit.Node;
                                node.RightDis = Math.Abs(i - node.X);
                            }
                            else
                            {
                                Node node1;
                                Node node2;
                                int run1 = 1;
                                int run2 = 1;
                                while (map[node.Y - run1, i].Node == null)
                                    run1++;
                                while (map[node.Y + run2, i].Node == null)
                                    run2++;
                                node1 = map[node.Y - run1, i].Node;
                                node2 = map[node.Y + run2, i].Node;

                                Node newNode = new Node(false, i < width - 1, false, false, i, node.Y);
                                unit.Status = true;
                                unit.Node = newNode;

                                node.Right = newNode;
                                node.RightDis = Math.Abs(i - node.X);
                                node1.Down = newNode;
                                node1.DownDis = run1;
                                node2.Up = newNode;
                                node2.UpDis = run2;

                                newNode.Left = node;
                                newNode.LeftDis = node.RightDis;
                                newNode.Up = node1;
                                newNode.UpDis = run1;
                                newNode.Down = node2;
                                newNode.DownDis = run2;
                            }
                            check = false;
                            break;
                        }
                        else
                            unit.Status = true;
                    }
                    if (check)
                    {
                        Node newNode = new Node(false, limRight < width - 1, node.Y > 0, node.Y < height - 1, limRight, node.Y);
                        newNode.Left = node;
                        newNode.LeftDis = Math.Abs(limRight - node.X);
                        node.Right = newNode;
                        node.RightDis = newNode.LeftDis;
                        map[node.Y, limRight].Node = newNode;
                        BuildMap(newNode, map,splitChance, aveDist);
                    }
                }
            }
            //up
            if (node.GoUp)
            {
                node.GoUp = false;
                if (random.Next(0, splitChance) > 0)
                {
                    bool check = true;
                    for (int i = node.Y - 1; i >= limUp; i--)
                    {
                        unit = map[i, node.X];
                        if (unit.Status)
                        {
                            if (unit.Node != null)
                            {
                                unit.Node.Down = node;
                                unit.Node.DownDis = Math.Abs(i - node.Y);
                                unit.Node.GoDown = false;
                                node.Up = unit.Node;
                                node.UpDis = unit.Node.DownDis;
                            }
                            else
                            {
                                Node node1;
                                Node node2;
                                int run1 = 1;
                                int run2 = 1;
                                while (map[i, node.X + run1].Node == null)
                                    run1++;
                                while (map[i, node.X - run2].Node == null)
                                    run2++;
                                node1 = map[i, node.X + run1].Node;
                                node2 = map[i, node.X - run2].Node;

                                Node newNode = new Node(false, false, i > 0, false, node.X, i);
                                unit.Status = true;
                                unit.Node = newNode;

                                node.Up = newNode;
                                node.UpDis = Math.Abs(i - node.Y);
                                node1.Left = newNode;
                                node1.LeftDis = run1;
                                node2.Right = newNode;
                                node2.RightDis = run2;

                                newNode.Down = node;
                                newNode.DownDis = node.UpDis;
                                newNode.Right = node1;
                                newNode.RightDis = run1;
                                newNode.Left = node2;
                                newNode.LeftDis = run2;
                            }
                            check = false;
                            break;
                        }
                        else
                            unit.Status = true;
                    }
                    if (check)
                    {
                        Node newNode = new Node(node.X > 0, node.X < width - 1, limUp > 0, false, node.X, limUp);
                        newNode.Down = node;
                        newNode.DownDis = Math.Abs(limUp - node.Y);
                        node.Up = newNode;
                        node.UpDis = newNode.DownDis;
                        map[limUp, node.X].Node = newNode;
                        BuildMap(newNode, map,splitChance, aveDist);
                    }
                }
            }
            //down
            if (node.GoDown)
            {
                node.GoDown = false;
                if (random.Next(0, splitChance) > 0)
                {
                    bool check = true;
                    for (int i = node.Y + 1; i <= limDown; i++)
                    {
                        unit = map[i, node.X];
                        if (unit.Status)
                        {
                            if (unit.Node != null)
                            {
                                unit.Node.Up = node;
                                unit.Node.UpDis = Math.Abs(i - node.Y);
                                unit.Node.GoUp = false;
                                node.Down = unit.Node;
                                node.DownDis = unit.Node.UpDis;
                            }
                            else
                            {
                                Node node1;
                                Node node2;
                                int run1 = 1;
                                int run2 = 1;
                                while (map[i, node.X + run1].Node == null)
                                    run1++;
                                while (map[i, node.X - run2].Node == null)
                                    run2++;
                                node1 = map[i, node.X + run1].Node;
                                node2 = map[i, node.X - run2].Node;

                                Node newNode = new Node(false, false, false, i < height - 1, node.X, i);
                                unit.Status = true;
                                unit.Node = newNode;

                                node.Down = newNode;
                                node.DownDis = Math.Abs(i - node.Y);
                                node1.Left = newNode;
                                node1.LeftDis = run1;
                                node2.Right = newNode;
                                node2.RightDis = run2;

                                newNode.Up = node;
                                newNode.UpDis = node.DownDis;
                                newNode.Right = node1;
                                newNode.RightDis = run1;
                                newNode.Left = node2;
                                newNode.LeftDis = run2;
                            }
                            check = false;
                            break;
                        }
                        else
                            unit.Status = true;
                    }
                    if (check)
                    {
                        Node newNode = new Node(node.X > 0, node.X < width - 1, false, limDown < height - 1, node.X, limDown);
                        newNode.Up = node;
                        newNode.UpDis = Math.Abs(limDown - node.Y);
                        node.Down = newNode;
                        node.DownDis = newNode.UpDis;
                        map[limDown, node.X].Node = newNode;
                        BuildMap(newNode, map,splitChance, aveDist);
                    }
                }
            }
        }
    }
}
