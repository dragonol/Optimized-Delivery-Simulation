﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace Optimized_Delivery_Simulation
{
    partial class MainWindow
    {
        public void CreateLookupDistances(Point sourcePos)
        {
            // Initialize LookupPath
            LookupPath = new Dictionary<Point, Dictionary<Point, Trail>>();

            // Create nodes at depot position if its position is not a node

            if ((Map[sourcePos.Y, sourcePos.X] as RouteUnit) != null)
                NodeUnit.CreateNode((RouteUnit)Map[sourcePos.Y, sourcePos.X]);


            // Create LookupPath

            Dictionary<Point, Trail> sourcePath = LookupPath[sourcePos] = new Dictionary<Point, Trail>();
            FastPriorityQueue<Point> set = new FastPriorityQueue<Point>(Map.Nodes.Count);

            foreach (NodeUnit depot in Map.Nodes)
            {
                Point depotPos = depot.Point;
                sourcePath[depotPos] = new Trail(null, int.MaxValue);
                set.Enqueue(depotPos, sourcePath[depotPos].Distance);
            }

            sourcePath[sourcePos].Distance = 0;
            set.UpdatePriority(sourcePos, 0);

            while (set.Count > 0)
            {
                Point curr = set.Dequeue();
                int currDistance = sourcePath[curr].Distance;

                foreach (NodeUnit node in ((NodeUnit)Map[curr.Y, curr.X]).AdjacentNodes)
                {
                    if (node == null)
                        continue;

                    int alt = currDistance + Unit.Distance(node, Map[curr.Y, curr.X]);

                    if (alt < sourcePath[node.Point].Distance)
                    {
                        sourcePath[node.Point].Distance = alt;
                        sourcePath[node.Point].Previous = curr;
                        set.UpdatePriority(node.Point, alt);
                    }
                }
            }

        }
    }
}

