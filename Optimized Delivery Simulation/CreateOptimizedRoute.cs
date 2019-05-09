using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimized_Delivery_Simulation
{
    partial class MainWindow
    {
        public void CreateOptimizedRoute()
        {
            int totalDistance = 0;
            OptimizedRoute.AddLast(Start);
            foreach (Point depot in Depots)
            {
                OptimizedRoute.AddLast(depot);
                totalDistance += LookupPath[depot][OptimizedRoute.Last.Previous.Point].Distance;
            }


        Start:

            bool dirNext1 = Traverse.Next;
            for (var i = OptimizedRoute.First.Next; i != OptimizedRoute.Last; i = NextTo(i, ref dirNext1))
            {
                bool dirNext2 = dirNext1 ? true : false;
                int distance_First_FirstPre = LookupPath[GetPrevious(i, dirNext1).Point][i.Point].Distance;
                for (var j = NextTo(i, ref dirNext2); j != OptimizedRoute.Last; j = NextTo(j, ref dirNext2))
                {
                    int distance_First_SecondNext = LookupPath[GetNext(j, dirNext2).Point][i.Point].Distance;
                    int distance_Second_SecondNext = LookupPath[GetNext(j, dirNext2).Point][j.Point].Distance;
                    int distance_Second_FirstPre = LookupPath[GetPrevious(i, dirNext1).Point][j.Point].Distance;
                    if (distance_First_FirstPre + distance_Second_SecondNext > distance_First_SecondNext + distance_Second_FirstPre)
                    {
                        Two_OPT(i, j, dirNext1, dirNext2);
                        goto Start;
                    }
                }
            }

            
        }
        public void Two_OPT(RouteNode first, RouteNode second, bool dir1, bool dir2)
        {
            RouteNode firstPrevious = GetPrevious(first, dir1);
            RouteNode secondNext = GetNext(second, dir2);

            RouteNode temp = firstPrevious;
            firstPrevious = secondNext;
            secondNext = temp;
        }

        public RouteNode GetPrevious(RouteNode node, bool direction)
        {
            if (direction)
                return node.Previous;
            else
                return node.Next;
        }
        public RouteNode GetNext(RouteNode node, bool direction)
        {
            if (!direction)
                return node.Previous;
            else
                return node.Next;
        }
        public RouteNode NextTo(RouteNode node, ref bool direction)
        {
            if (direction)
            {
                if (node == node.Next.Next)
                    direction = (!direction);
                return node.Next;
            }
            else
            {
                if (node == node.Previous.Previous)
                    direction = (!direction);
                return node.Previous;
            }
        }
    }
}
