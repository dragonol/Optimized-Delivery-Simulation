using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimized_Delivery_Simulation
{
    partial class MainWindow
    {
        public void CreateLookupDistance(Node start, Node[] destinations)
        {
            foreach (Node dest in destinations)
                dest.Depot = true;
            LookupDistance = new TreePath[destinations.Length];
            HashSet<(int x, int y)> remain;

            foreach(Unit unit in Map)
            {
                if(unit.Status)
                    remain.Add((unit.))
            }
        }
    }
}
