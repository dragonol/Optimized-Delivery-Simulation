using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimized_Delivery_Simulation
{
    //partial class MainWindow
    //{
    //    public class Comparer: IComparer<(int dist, Node node)>
    //    {
    //        public int Compare((int dist, Node node) a, (int dist, Node node) b)
    //        {
    //            if (a.dist == b.dist && a.node.Equals(b.node))
    //                return 0;
    //            if (a.dist > b.dist)
    //                return 1;
    //            return -1;
    //        }
    //    }
    //    public void CreateLookupDistance(Position[] depots)
    //    {
    //        LookupDistance = new Dictionary<Position, Path>();
    //        SortedSet<(int dist, Node node)> set = new SortedSet<(int dist, Node node)>(new Comparer());

            
    //        for (int i = 0; i < Map.GetLength(0); i++)
    //            for (int j = 0; j < Map.GetLength(1); j++)
    //                if (Map[i, j].Node != null)
    //                    set.Add((int.MaxValue, Map[i, j].Node));

    //        foreach (Position position in depots)
    //        {
    //            if (Map[position.Y, position.X].Node == null)
    //            {
    //                if(Map[position.Y,position.X].Direction==1)
    //                {
    //                    int run1 = 1;
    //                    int run2 = 1;
    //                    while (Map[position.Y, position.X + run1].Node == null) 
    //                        run1++;
    //                    while (Map[position.Y, position.X - run2].Node == null)
    //                        run2++;
    //                    Node newNode = new Node(true, true, false, false, position.X, position.Y);
    //                    newNode.Left = Map[position.Y, position.X - run2].Node;
    //                    newNode.LeftDis = run2;
    //                    newNode.Right = Map[position.Y, position.X + run1].Node;
    //                    newNode.RightDis = run1;
    //                    set.Add((int.MaxValue, newNode));
    //                }
    //                else
    //                {
    //                    int run1 = 1;
    //                    int run2 = 1;
    //                    while (Map[position.Y + run1, position.X].Node == null)
    //                        run1++;
    //                    while (Map[position.Y - run2, position.X].Node == null)
    //                        run2++;
    //                    Node newNode = new Node(false, false, true, true, position.X, position.Y);
    //                    newNode.Up = Map[position.Y - run2, position.X].Node;
    //                    newNode.UpDis = run2;
    //                    newNode.Down = Map[position.Y + run1, position.X].Node;
    //                    newNode.DownDis = run1;
    //                    set.Add((int.MaxValue, newNode));
    //                }
    //            }
    //        }

    //        foreach ((int dist, Node node) depot in set) 
    //        {
    //            Position position = new Position(depot.node.Y, depot.node.X);
    //            SortedSet<(int dist, Node node)> remain = new SortedSet<(int dist, Node node)>(new Comparer());
    //            foreach ((int dist, Node node) i in set)
    //                remain.Add(i);
    //            remain.Remove((int.MaxValue, Map[position.Y, position.X].Node));
    //            remain.Add((0, Map[position.Y, position.X].Node));
    //            LookupDistance[position].Distance[position] = 0;

    //            while(remain.Count>0)
    //            {
    //                (int dist, Node node) temp = remain.Min;
    //                remain.Remove(temp);
                    
    //                if(temp.node.Left!=null)
    //                {
    //                    int alt = temp.dist + temp.node.LeftDis;
    //                    if (LookupDistance[position].Distance.ContainsKey(new Position(temp.node.Left.Y, temp.node.Left.X)))
    //                    {
    //                        Position curr = new Position(temp.node.Left.Y, temp.node.Left.X);
    //                        int currDist = LookupDistance[position].Distance[curr];
    //                        if (currDist > alt)
    //                        {
    //                            LookupDistance[position].Distance[curr] = alt;
    //                            LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                            remain.Remove((currDist, temp.node.Left));
    //                            remain.Add((alt, temp.node.Left));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Position curr = new Position(temp.node.Left.Y, temp.node.Left.X);
    //                        int currDist = LookupDistance[position].Distance[curr];

    //                        LookupDistance[position].Distance[curr] = alt;
    //                        LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                        remain.Remove((currDist, temp.node.Left));
    //                        remain.Add((alt, temp.node.Left));
    //                    }
    //                }

    //                if (temp.node.Right != null)
    //                {
    //                    int alt = temp.dist + temp.node.RightDis;
    //                    if (LookupDistance[position].Distance.ContainsKey(new Position(temp.node.Right.Y, temp.node.Right.X)))
    //                    {
    //                        Position curr = new Position(temp.node.Right.Y, temp.node.Right.X);
    //                        int currDist = LookupDistance[position].Distance[curr];
    //                        if (currDist > alt)
    //                        {
    //                            LookupDistance[position].Distance[curr] = alt;
    //                            LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                            remain.Remove((currDist, temp.node.Right));
    //                            remain.Add((alt, temp.node.Right));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Position curr = new Position(temp.node.Right.Y, temp.node.Right.X);
    //                        int currDist = LookupDistance[position].Distance[curr];

    //                        LookupDistance[position].Distance[curr] = alt;
    //                        LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                        remain.Remove((currDist, temp.node.Right));
    //                        remain.Add((alt, temp.node.Right));
    //                    }
    //                }

    //                if (temp.node.Up != null)
    //                {
    //                    int alt = temp.dist + temp.node.UpDis;
    //                    if (LookupDistance[position].Distance.ContainsKey(new Position(temp.node.Up.Y, temp.node.Up.X)))
    //                    {
    //                        Position curr = new Position(temp.node.Up.Y, temp.node.Up.X);
    //                        int currDist = LookupDistance[position].Distance[curr];
    //                        if (currDist > alt)
    //                        {
    //                            LookupDistance[position].Distance[curr] = alt;
    //                            LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                            remain.Remove((currDist, temp.node.Up));
    //                            remain.Add((alt, temp.node.Up));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Position curr = new Position(temp.node.Up.Y, temp.node.Up.X);
    //                        int currDist = LookupDistance[position].Distance[curr];

    //                        LookupDistance[position].Distance[curr] = alt;
    //                        LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                        remain.Remove((currDist, temp.node.Up));
    //                        remain.Add((alt, temp.node.Up));
    //                    }
    //                }

    //                if (temp.node.Down != null)
    //                {
    //                    int alt = temp.dist + temp.node.DownDis;
    //                    if (LookupDistance[position].Distance.ContainsKey(new Position(temp.node.Down.Y, temp.node.Down.X)))
    //                    {
    //                        Position curr = new Position(temp.node.Down.Y, temp.node.Down.X);
    //                        int currDist = LookupDistance[position].Distance[curr];
    //                        if (currDist > alt)
    //                        {
    //                            LookupDistance[position].Distance[curr] = alt;
    //                            LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                            remain.Remove((currDist, temp.node.Down));
    //                            remain.Add((alt, temp.node.Down));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        Position curr = new Position(temp.node.Down.Y, temp.node.Down.X);
    //                        int currDist = LookupDistance[position].Distance[curr];

    //                        LookupDistance[position].Distance[curr] = alt;
    //                        LookupDistance[position].Pre[curr] = new Position(temp.node.Y, temp.node.X);
    //                        remain.Remove((currDist, temp.node.Down));
    //                        remain.Add((alt, temp.node.Down));
    //                    }
    //                }
    //            }
    //        }
            
    //    }
    //}
}
