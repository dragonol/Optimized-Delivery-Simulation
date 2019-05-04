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
    partial class MainWindow
    {
        public void DrawPath(Unit node1, Unit node2, Brush color, int thickness, int space)
        {
            Line line = new Line();
            line.Stroke = color;
            line.X1 = node1.Point.X * space;
            line.X2 = node2.Point.X * space;
            line.Y1 = node1.Point.Y * space;
            line.Y2 = node2.Point.Y * space;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Top;
            line.StrokeThickness = thickness;

            //Rectangle rectangle = new Rectangle();
            //rectangle.Stroke = color;
            //rectangle.RadiusX = 90;
            //rectangle.RadiusY = 90;
            //rectangle.Height = thickness;
            //rectangle.Width = thickness;
            //rectangle.StrokeThickness = thickness / 2;
            //rectangle.VerticalAlignment = VerticalAlignment.Top;
            //rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            //rectangle.Margin = new Thickness(node.X * space - thickness / 2, node.Y * space - thickness / 2, 0, 0);

            Grid.Children.Add(line);
        }
    }
}
