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
        public static readonly (int height, int width) MapSize = (30, 60);
        public static readonly int Space = 35;
        public static readonly int SplitChance = 7;
        public static readonly double AverageDistance = 8;
        public static readonly double Thickness = 15;

        public static WorldMap Map = new WorldMap(MapSize.height, MapSize.width);
        public static Dictionary<Point, Dictionary<Point, Trail>> LookupPath = new Dictionary<Point, Dictionary<Point, Trail>>();
        public static List<Point> OptimizedRoute = new List<Point>();
        public static Point Start;
        public static List<Point> Depots = new List<Point>();

        public static bool[,] MapNodeCheck = new bool[MapSize.height, MapSize.width];
        public static GeometryGroup[] MapComponents = new GeometryGroup[3];
        public static GeometryGroup[] MapNodes = new GeometryGroup[2];
        public static GeometryGroup MapDirection = new GeometryGroup();
        public static GeometryGroup InputNodes = new GeometryGroup();
        public static GeometryGroup OutputRoute = new GeometryGroup();
        public static Image MapImage = new Image();
        public static Image NodesImage = new Image();
        public static Image RouteImage = new Image();
        public static System.Windows.Point RouteImageAnchor = new System.Windows.Point(int.MaxValue, int.MaxValue);

        public static System.Windows.Point GridMargin = new System.Windows.Point(0, 0);
        public static System.Windows.Point BasePosition;
        public static System.Windows.Point CurrPosition;
        public static bool IsDragging = false;
        public static bool MouseLeftDown = false;

        public static double ZoomLimit = 0;
        public static System.Windows.Point ZoominPoint = new System.Windows.Point();
        public static double MapScale = 1;

        public static System.Windows.Point temp = new System.Windows.Point(-1, -1);

        public MainWindow()
        {
            InitializeComponent();
            GenerateMap(Brushes.PowderBlue);
            DrawSingleNode(Start, NodeLayer,Brushes.AntiqueWhite, Thickness / 2);
            MapSectionCover.PreviewMouseRightButtonUp += (s, e) =>
            {
                var curr = e.GetPosition(MapSection);
                curr.X = (int)((curr.X + Space / 4) / Space);
                curr.Y = (int)((curr.Y + Space / 4) / Space);
                if (temp != new System.Windows.Point(-1, -1))
                {
                    Console.WriteLine();
                    //Console.WriteLine(LookupPath[Map[curr].Point][Map[temp].Point].Distance);
                    //Console.WriteLine(LookupPath[Map[temp].Point][Map[curr].Point].Distance);
                    if (curr.X == temp.X)
                        if (curr.Y < temp.Y)
                        {
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Down]);
                            Console.WriteLine(((NodeUnit)Map[temp]).AdjacentTraffic[Direction.Up]);
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Down] * Unit.Distance(curr, temp));
                        }
                        else
                        {
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Up]);
                            Console.WriteLine(((NodeUnit)Map[temp]).AdjacentTraffic[Direction.Down]);
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Up] * Unit.Distance(curr, temp));
                        }
                    else
                    {
                        if (curr.X < temp.X)
                        {
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Right]);
                            Console.WriteLine(((NodeUnit)Map[temp]).AdjacentTraffic[Direction.Left]);
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Right]* Unit.Distance(curr,temp));
                        }
                        else
                        {
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Left]);
                            Console.WriteLine(((NodeUnit)Map[temp]).AdjacentTraffic[Direction.Right]);
                            Console.WriteLine(((NodeUnit)Map[curr]).AdjacentTraffic[Direction.Left] * Unit.Distance(curr, temp));
                        }
                    }
                    temp = new System.Windows.Point(-1, -1);
                }
                else
                    temp = curr;
            };
        }

        private void MapSectionCover_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BasePosition = Mouse.GetPosition(MapSectionCover);
            MouseLeftDown = true;
        }

        private void MapSectionCover_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (MouseLeftDown)
            {
                IsDragging = true;
                CurrPosition = Mouse.GetPosition(MapSectionCover);
                double currTop = Canvas.GetTop(MapSection);
                double currLeft = Canvas.GetLeft(MapSection);

                double absLeft = CurrPosition.X - BasePosition.X;
                double absTop = CurrPosition.Y - BasePosition.Y;

                if (currTop + absTop >= -350 && currTop + absTop <= 700)
                    Canvas.SetTop(MapSection, currTop + absTop);
                if (currLeft + absLeft <= 1300 && currLeft + absLeft >= -940)
                    Canvas.SetLeft(MapSection, currLeft + absLeft);

                BasePosition = CurrPosition;

            }
        }

        private void MapSectionCover_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDragging = false;
            MouseLeftDown = false;
        }

        private void MapSectionCover_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDragging)
                return;

            var currPosition = e.GetPosition(MapSection);
            currPosition.X = (int)((currPosition.X + Space / 4) / Space);
            currPosition.Y = (int)((currPosition.Y + Space / 4) / Space);

            if (currPosition.X < 0 || currPosition.Y < 0 || Map.isNull(currPosition))
                return;

            
            //Console.WriteLine("///////////");
            Console.WriteLine("currPos:" + currPosition);
            if(Map[currPosition] as NodeUnit != null)
            {
                var node = ((NodeUnit)Map[currPosition]).AdjacentNodes;
                Console.WriteLine("Adjac: ");
                foreach (var i in node)
                    if (i == null)
                        Console.WriteLine("NULL");
                    else
                        Console.WriteLine(i.Point);
                Console.WriteLine();
            }

            Depots.Add(Map[currPosition].Point);
            DrawSingleNode(currPosition, NodeLayer, Brushes.Yellow, Thickness / 2);

            if (Depots.Count == 5) 
            {
                CreateLookupDistances(Depots);
                CreateOptimizedRoute();
                Console.WriteLine("Route: " + OptimizedRoute.Count);

                for (int i = 0; i < OptimizedRoute.Count; i++)
                {
                    AddDrawRoute(OptimizedRoute[i], OptimizedRoute[(i + 1) % OptimizedRoute.Count], OutputRoute);
                    Console.WriteLine(OptimizedRoute[i]);
                }
                Draw(OutputRoute, MapSection, RouteImage, RouteImageAnchor, Brushes.Red, Thickness / 3);
            }
            //Console.WriteLine("///////////");
        }

        private void MapSectionCover_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var currPosition = e.GetPosition(MapSection);
            var matrix = MapSection.RenderTransform.Value;

            if (e.Delta > 0)
            {
                if (ZoomLimit >= 1)
                    return;
                matrix.ScaleAtPrepend(1.1, 1.1, currPosition.X, currPosition.Y);
                ZoominPoint = currPosition;
                ZoomLimit += 0.1;
            }
            else
            {
                if (ZoomLimit <= 0)
                    return;
                matrix.ScaleAtPrepend(1 / 1.1, 1 / 1.1, ZoominPoint.X, ZoominPoint.Y);
                ZoomLimit -= 0.1;
            }

            MapSection.RenderTransform = new MatrixTransform(matrix);
        }
    }
    
}
