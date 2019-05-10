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
    public partial class MainWindow : Window
    {
        public static readonly Random Random = new Random();
        public static readonly (int height, int width) MapSize = (15, 25);
        public static readonly int Space = 40;
        public static readonly int SplitChance = 5;
        public static readonly double AverageDistance = 0.2;
        public static readonly int Thickness = 30;

        public static WorldMap Map = new WorldMap(MapSize.height,MapSize.width);
        public static Dictionary<Point, Dictionary<Point, Trail>> LookupPath = new Dictionary<Point, Dictionary<Point, Trail>>();
        public static List<Point> OptimizedRoute = new List<Point>();
        public static Point Start;
        public static List<Point> Depots = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
            GenerateMap(Brushes.Coral);
            DrawNode(Start, Brushes.AntiqueWhite, Thickness / 2);
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var rawPoint = Mouse.GetPosition(Grid);
            Point currMousePos = Map[new Point((int)((rawPoint.Y + Space / 2) / Space), (int)((rawPoint.X + Space / 2) / Space))].Point;
            
            Depots.Add(currMousePos);
            DrawNode(currMousePos, Brushes.Blue, Thickness / 2);

            if(Depots.Count==15)
            {
                CreateLookupDistances(Depots);
                CreateOptimizedRoute();
                for (int i = 0; i < OptimizedRoute.Count; i++)
                    DrawRoute(OptimizedRoute[i], OptimizedRoute[(i + 1) % OptimizedRoute.Count]);
            }


            //foreach (var node in Map.Nodes)
            //{
            //    Point run = node.Point;

            //    while (run != currMousePos)
            //    {
            //        Point pre = LookupPath[currMousePos][run].Previous;
            //        DrawPath(run, pre, Brushes.Blue, Thickness / 2);
            //        run = pre;
            //    }
            //}

        }
    }
}
