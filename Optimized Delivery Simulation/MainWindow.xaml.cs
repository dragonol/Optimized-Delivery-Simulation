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

namespace Optimized_Delivery_Simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly Random random = new Random();
        public static (int height, int width) MapSize = (10, 20);

        public static Unit[,] Map;
        public static Dictionary<Point,Path> LookupPath;

        public MainWindow()
        {
            InitializeComponent();
            GenerateMap(1, 3, 10, 4, Brushes.Coral);

            //GenerateMap(15, 30, Brushes.SlateGray);
            //Position[] positions = new Position[3];
            //for(int i = 0; i < 3; i++)
            //{
            //    int y = random.Next(0, 14);
            //    int x = random.Next(0, 29);
            //    while(!Map[y,x].Status)
            //    {
            //        y = random.Next(0, 14);
            //        x = random.Next(0, 29);
            //    }
            //    positions[i] = new Position(y, x);
            //}
            //CreateLookupDistance(positions);
        }
    }
}
