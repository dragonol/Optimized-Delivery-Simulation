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
        public static readonly int Space = 50;
        public static readonly int SplitChance = 3;
        public static readonly double AverageDistance = 8;
        public static readonly int Thickness = 35;

        public static WorldMap Map = new WorldMap(MapSize.height, MapSize.width);
        public static Dictionary<Point, Dictionary<Point, Trail>> LookupPath = new Dictionary<Point, Dictionary<Point, Trail>>();
        public static List<Point> OptimizedRoute = new List<Point>();
        public static Point Start;
        public static List<Point> Depots = new List<Point>();

        public static GeometryGroup MapComponents = new GeometryGroup();
        public static GeometryGroup InputNodes = new GeometryGroup();
        public static GeometryGroup OutputRoute = new GeometryGroup();
        public static Image MapImage = new Image();
        public static Image NodesImage = new Image();
        public static Image RouteImage = new Image();

        public static System.Windows.Point GridMargin = new System.Windows.Point(0, 0);
        public static System.Windows.Point BasePosition;
        public static System.Windows.Point CurrPosition;
        public static bool IsDragging = false;
        public static bool MouseLeftDown = false;

        public static double MapScale = 1;

        public MainWindow()
        {
            InitializeComponent();
            GenerateMap(Brushes.PowderBlue);
            //DrawNode(Start, Brushes.AntiqueWhite, Thickness / 2);
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
                double absLeft = CurrPosition.X - BasePosition.X;
                double absTop = CurrPosition.Y - BasePosition.Y;

                Canvas.SetLeft(MapSection, Canvas.GetLeft(MapSection) + absLeft);
                Canvas.SetTop(MapSection, Canvas.GetTop(MapSection) + absTop);
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
            Console.WriteLine(currPosition);
            currPosition.X = (int)((currPosition.X + Space / 4) / Space);
            currPosition.Y = (int)((currPosition.Y + Space / 4) / Space);

            if (Map.isNull(currPosition))
                return;



            DrawSingleNode(currPosition, MapSection, Brushes.Yellow, Thickness / 2);
        }
    }
    
}
