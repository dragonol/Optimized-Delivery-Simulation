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
            int nodeCount = OptimizedRoute.Count;
            int temperature = 50;

            for (int i = 0; i < Depots.Count; i++)
            {
                OptimizedRoute.Add(Depots[i]);
                totalDistance += LookupPath[Depots[i]][Depots[(i + 1) % Depots.Count]].Distance;
            }
            
        Start:

            for (int i = 1; i < nodeCount; i++)
            {
                for (int j = i + 1; j < nodeCount; j++)
                {
                    int distance_FirstPrevios_First = LookupPath[OptimizedRoute[i - 1]][OptimizedRoute[i]].Distance;
                    int distance_SecondNext_Second = LookupPath[OptimizedRoute[(j + 1) % nodeCount]][OptimizedRoute[j]].Distance;
                    int distance_FirstPrevios_Second = LookupPath[OptimizedRoute[i - 1]][OptimizedRoute[j]].Distance;
                    int distance_SecondNext_First = LookupPath[OptimizedRoute[(j + 1) % nodeCount]][OptimizedRoute[i]].Distance;
                    if (distance_FirstPrevios_First + distance_SecondNext_Second > distance_FirstPrevios_Second + distance_SecondNext_First || 
                        Random.Next(100) < temperature)
                    {
                        Two_OPT(i, j);
                        temperature = Math.Max(temperature - 1, 0);
                        goto Start;
                    }
                }
            }

            void Two_OPT(int i, int j)
            {
                for (int k = 0; k < (j - i + 1) / 2; k++)
                {
                    Point temp = OptimizedRoute[(k + i) % nodeCount];
                    OptimizedRoute[(k + i) % nodeCount] = OptimizedRoute[j - k];
                    OptimizedRoute[j - k] = temp;
                }
            }
        }
        
    }
}
