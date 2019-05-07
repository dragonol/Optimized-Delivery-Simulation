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
        public static readonly (int height, int width) MapSize = (10, 20);
        public static readonly int Space = 50;
        public static readonly int SplitChance = 5;
        public static readonly double AverageDistance = 0.3;
        public static readonly int Thickness = 30;

        public static WorldMap Map = new WorldMap(MapSize.height,MapSize.width);
        public static Dictionary<Point, Dictionary<Point, Trail>> LookupPath = new Dictionary<Point, Dictionary<Point, Trail>>();
        public static List<Point> points = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
            GenerateMap(Brushes.Coral);
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var rawPoint = Mouse.GetPosition(Grid);
            Point currMousePos = new Point((int)((rawPoint.Y + Space / 2) / Space), (int)((rawPoint.X + Space / 2) / Space));

            //points.Add(Map[currMousePos].Point);
            //DrawNode(currMousePos, Brushes.Blue, Thickness/2);

            //if (points.Count == 2) 
            //{
            //    CreateLookupDistances(points);
            //    Point run = points[1];

            //    while (run != points[0]) 
            //    {
            //        DrawPath(run, LookupPath[points[0]][run].Previous, Brushes.Blue, Thickness/2);
            //        run = LookupPath[points[0]][run].Previous;
            //    }
            //    points.Clear();
            //}

            points.Add(Map[currMousePos].Point);
            CreateLookupDistances(points);
        }
    }
}
