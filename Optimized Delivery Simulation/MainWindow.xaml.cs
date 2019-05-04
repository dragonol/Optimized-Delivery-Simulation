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
            GenerateMap(3, 0.25, 70, 30, Brushes.Coral);
        }
    }
}
