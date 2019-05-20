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
        public void Draw(GeometryGroup geometries, UIElement element, Image image, System.Windows.Point pos,Brush color, double thickness)
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing(
                Brushes.Transparent,
                new Pen(color, thickness), geometries);

            DrawingImage drawingImage = new DrawingImage(geometryDrawing);
            drawingImage.Freeze();

            Console.WriteLine(geometryDrawing.IsFrozen);

            image.Source = drawingImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            Canvas.SetLeft(image, pos.X);
            Canvas.SetTop(image, pos.Y);

            ((Canvas)element).Children.Add(image);
        }
        public void AddDrawPath(System.Windows.Point point1, System.Windows.Point point2, GeometryGroup geometries)
        {
            LineGeometry line = new LineGeometry(
                new System.Windows.Point(point1.X * Space, point1.Y * Space),
                new System.Windows.Point(point2.X * Space, point2.Y * Space));
            geometries.Children.Add(line);
            
            AddDrawNode(point1, geometries);
            AddDrawNode(point2, geometries);
        }
        public void AddDrawNode(System.Windows.Point point, GeometryGroup geometries)
        {
            RectangleGeometry rect = new RectangleGeometry(
                new Rect(point.X * Space, point.Y * Space, 0, 0), 90, 90);
            geometries.Children.Add(rect);
        }
        public void AddDrawRoute(Point point1, Point point2, GeometryGroup geometries)
        {
            Point run = point1;

            while (run != point2)
            {
                RouteImageAnchor.X = Math.Min(run.X * Space + Thickness/3, RouteImageAnchor.X);
                RouteImageAnchor.Y = Math.Min(run.Y * Space + Thickness/3, RouteImageAnchor.Y);
                AddDrawPath(run, LookupPath[point2][run].Previous, geometries);
                run = LookupPath[point2][run].Previous;
            }
        }
        public void DrawSingleNode(System.Windows.Point point, UIElement element, Brush color, double thickness)
        {
            RectangleGeometry rect = new RectangleGeometry(
                new Rect(0, 0, 0, 0), 90, 90);

            GeometryDrawing geometryDrawing = new GeometryDrawing(
                Brushes.Black,
                new Pen(color, thickness), rect);
            
            DrawingImage drawingImage = new DrawingImage(geometryDrawing);
            drawingImage.Freeze();

            Image image = new Image();
            image.Source = drawingImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            Canvas.SetLeft(image, point.X * Space + thickness/2);
            Canvas.SetTop(image, point.Y * Space + thickness/2);

            ((Canvas)element).Children.Add(image);
        }
        
    }
}
