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
        public void DrawPath(Point point1, Point point2, Brush color, double thickness)
        {
            Line line = new Line();
            line.Stroke = color;
            line.X1 = point1.X * Space;
            line.X2 = point2.X * Space;
            line.Y1 = point1.Y * Space;
            line.Y2 = point2.Y * Space;
            line.HorizontalAlignment = HorizontalAlignment.Left;
            line.VerticalAlignment = VerticalAlignment.Top;
            line.StrokeThickness = thickness;
            Grid.Children.Add(line);

            DrawNode(point1, color, thickness);
            DrawNode(point2, color, thickness);
        }
        public void DrawNode(Point point, Brush color, double thickness)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Stroke = color;
            rectangle.RadiusX = 90;
            rectangle.RadiusY = 90;
            rectangle.Height = thickness;
            rectangle.Width = thickness;
            rectangle.StrokeThickness = thickness / 2;
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.Margin = new Thickness(point.X * Space - thickness / 2, point.Y * Space - thickness / 2, 0, 0);

            Grid.Children.Add(rectangle);
        }
        public void DrawRoute(Point point1, Point point2)
        {
            Point run = point1;

            while (run != point2)
            {
                DrawPath(run, LookupPath[point1][run].Previous, Brushes.Blue, Thickness / 2);
                run = LookupPath[point1][run].Previous;
            }
        }
    }
}
