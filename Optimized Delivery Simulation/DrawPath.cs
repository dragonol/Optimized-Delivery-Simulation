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
        public void DrawPath(Unit node1, Unit node2, Brush color, int space, int thickness)
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

            Rectangle rectangle1 = new Rectangle();
            rectangle1.Stroke = color;
            rectangle1.RadiusX = 90;
            rectangle1.RadiusY = 90;
            rectangle1.Height = thickness;
            rectangle1.Width = thickness;
            rectangle1.StrokeThickness = thickness / 2;
            rectangle1.VerticalAlignment = VerticalAlignment.Top;
            rectangle1.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle1.Margin = new Thickness(node1.Point.X * space - thickness / 2, node1.Point.Y * space - thickness / 2, 0, 0);

            Rectangle rectangle2 = new Rectangle();
            rectangle2.Stroke = color;
            rectangle2.RadiusX = 90;
            rectangle2.RadiusY = 90;
            rectangle2.Height = thickness;
            rectangle2.Width = thickness;
            rectangle2.StrokeThickness = thickness / 2;
            rectangle2.VerticalAlignment = VerticalAlignment.Top;
            rectangle2.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle2.Margin = new Thickness(node2.Point.X * space - thickness / 2, node2.Point.Y * space - thickness / 2, 0, 0);

            Grid.Children.Add(line);
            Grid.Children.Add(rectangle1);
            Grid.Children.Add(rectangle2);
        }
    }
}
