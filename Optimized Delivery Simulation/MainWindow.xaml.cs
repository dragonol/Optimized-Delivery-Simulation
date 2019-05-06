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
        public static readonly Random random = new Random();
        public static (int height, int width) MapSize = (10, 20);

        public static WorldMap Map;
        public static Dictionary<Point, Dictionary<Point, Trail>> LookupPath;

        public MainWindow()
        {
            InitializeComponent();

            int splitChance = 5;
            double averageDistance = 0.3;
            int space = 70;
            int thickness = 40;
            GenerateMap(splitChance, averageDistance, space, thickness, Brushes.Coral);
            Point[] points = new Point[4];
            for (int i = 0; i < 4; i++)
            {
                int x, y;
                do
                {
                    x = random.Next(0, MapSize.width - 1);
                    y = random.Next(0, MapSize.height - 1);
                } while (Map[y, x] == null);
                points[i] = Map[y, x].Point;
            }
            CreateLookupDistances(points);

            Point run = points[0];
            while(run!=points[1])
            {
                DrawPath(Map[run], Map[LookupPath[points[1]][run].Previous], Brushes.Blue, space, 20);
                run = LookupPath[points[1]][run].Previous;
            }
        }
    }
}
