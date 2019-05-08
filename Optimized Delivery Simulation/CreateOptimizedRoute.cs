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
            foreach (NodeUnit vertice in Map.Nodes)
            {
                if (vertice.Point == Start)
                    continue;
                OptimizedRoute.AddLast(vertice.Point);
                totalDistance += LookupPath[OptimizedRoute.Last.Previous.Value][vertice.Point].Distance;
            }

            while(true)
            {
                for(var i = OptimizedRoute.First; i != OptimizedRoute.Last; i=i.Next)
                {
                    for(var j = i.Next; j != null; j=j.Next)
                    {

                    }
                }
            }

            void Two_OPT(LinkedListNode<Point> node1, LinkedListNode<Point> node2)
            {

            }
        }
    }
}
