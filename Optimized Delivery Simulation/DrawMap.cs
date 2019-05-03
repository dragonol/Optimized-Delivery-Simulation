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
    //partial class MainWindow
    //{
    //    public void DrawMap(Node node, bool[,] lookUp, int space, double thickness, Brush color)
    //    {
    //        lookUp[node.Y, node.X] = true;
    //        if (node.Left != null)
    //        {
    //            Line line = new Line();
    //            line.Stroke = color;
    //            line.X1 = node.X * space;
    //            line.X2 = node.Left.X * space;
    //            line.Y1 = node.Y * space;
    //            line.Y2 = node.Left.Y * space;
    //            line.HorizontalAlignment = HorizontalAlignment.Left;
    //            line.VerticalAlignment = VerticalAlignment.Top;
    //            line.StrokeThickness = thickness;

    //            Rectangle rectangle = new Rectangle();
    //            rectangle.Stroke = color;
    //            rectangle.RadiusX = 90;
    //            rectangle.RadiusY = 90;
    //            rectangle.Height = thickness;
    //            rectangle.Width = thickness;
    //            rectangle.StrokeThickness = thickness/2;
    //            rectangle.VerticalAlignment = VerticalAlignment.Top;
    //            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
    //            rectangle.Margin = new Thickness(node.X * space - thickness / 2, node.Y * space - thickness / 2, 0, 0);

    //            Grid.Children.Add(line);
    //            Grid.Children.Add(rectangle);

    //            if (lookUp[node.Left.Y, node.Left.X] == false)
    //                DrawMap(node.Left, lookUp, space, thickness, color);
    //        }
    //        if (node.Right != null)
    //        {
    //            Line line = new Line();
    //            line.Stroke = color;
    //            line.X1 = node.X * space;
    //            line.X2 = node.Right.X * space;
    //            line.Y1 = node.Y * space;
    //            line.Y2 = node.Right.Y * space;
    //            line.HorizontalAlignment = HorizontalAlignment.Left;
    //            line.VerticalAlignment = VerticalAlignment.Top;
    //            line.StrokeThickness = thickness;

    //            Rectangle rectangle = new Rectangle();
    //            rectangle.Stroke = color;
    //            rectangle.RadiusX = 90;
    //            rectangle.RadiusY = 90;
    //            rectangle.Height = thickness;
    //            rectangle.Width = thickness;
    //            rectangle.StrokeThickness = thickness/2;
    //            rectangle.VerticalAlignment = VerticalAlignment.Top;
    //            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
    //            rectangle.StrokeThickness = thickness;
    //            rectangle.Margin = new Thickness(node.X * space - thickness / 2, node.Y * space - thickness / 2, 0, 0);

    //            Grid.Children.Add(line);
    //            Grid.Children.Add(rectangle);

    //            if (lookUp[node.Right.Y, node.Right.X] == false)
    //                DrawMap(node.Right, lookUp, space, thickness, color);
    //        }
    //        if (node.Up != null)
    //        {
    //            Line line = new Line();
    //            line.Stroke = color;
    //            line.X1 = node.X * space;
    //            line.X2 = node.Up.X * space;
    //            line.Y1 = node.Y * space;
    //            line.Y2 = node.Up.Y * space;
    //            line.HorizontalAlignment = HorizontalAlignment.Left;
    //            line.VerticalAlignment = VerticalAlignment.Top;
    //            line.StrokeThickness = thickness;

    //            Rectangle rectangle = new Rectangle();
    //            rectangle.Stroke = color;
    //            rectangle.RadiusX = 90;
    //            rectangle.RadiusY = 90;
    //            rectangle.Height = thickness;
    //            rectangle.Width = thickness;
    //            rectangle.StrokeThickness = thickness/2;
    //            rectangle.VerticalAlignment = VerticalAlignment.Top;
    //            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
    //            rectangle.StrokeThickness = thickness;
    //            rectangle.Margin = new Thickness(node.X * space - thickness / 2, node.Y * space - thickness / 2, 0, 0);

    //            Grid.Children.Add(line);
    //            Grid.Children.Add(rectangle);

    //            if (lookUp[node.Up.Y, node.Up.X] == false)
    //                DrawMap(node.Up, lookUp, space, thickness, color);
    //        }
    //        if (node.Down != null)
    //        {
    //            Line line = new Line();
    //            line.Stroke = color;
    //            line.X1 = node.X * space;
    //            line.X2 = node.Down.X * space;
    //            line.Y1 = node.Y * space;
    //            line.Y2 = node.Down.Y * space;
    //            line.HorizontalAlignment = HorizontalAlignment.Left;
    //            line.VerticalAlignment = VerticalAlignment.Top;
    //            line.StrokeThickness = thickness;

    //            Rectangle rectangle = new Rectangle();
    //            rectangle.Stroke = color;
    //            rectangle.RadiusX = 90;
    //            rectangle.RadiusY = 90;
    //            rectangle.Height = thickness;
    //            rectangle.Width = thickness;
    //            rectangle.StrokeThickness = thickness/2;
    //            rectangle.VerticalAlignment = VerticalAlignment.Top;
    //            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
    //            rectangle.StrokeThickness = thickness;
    //            rectangle.Margin = new Thickness(node.X * space - thickness / 2, node.Y * space - thickness / 2, 0, 0);

    //            Grid.Children.Add(line);
    //            Grid.Children.Add(rectangle);

    //            if (lookUp[node.Down.Y, node.Down.X] == false)
    //                DrawMap(node.Down, lookUp, space, thickness, color);
    //        }
    //    }
    //}
}
