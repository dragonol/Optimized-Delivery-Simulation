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
        public void Draw(GeometryGroup geometryGroup, Canvas canvas, Brush color, double thickness)
        {
            Path path = new Path();
            path.StrokeThickness = thickness;
            path.Stroke = color;
            path.Data = geometryGroup;
            canvas.Children.Add(path);
        }
        public void Draw(GeometryGroup geometries, UIElement element, Image image, System.Windows.Point pos, Brush color, double thickness)
        {
            GeometryDrawing geometryDrawing = new GeometryDrawing(
                Brushes.Transparent,
                new Pen(color, thickness), geometries);

            DrawingImage drawingImage = new DrawingImage(geometryDrawing);
            drawingImage.Freeze();

            image.Source = drawingImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            Canvas.SetLeft(image, pos.X);
            Canvas.SetTop(image, pos.Y);

            ((Canvas)element).Children.Add(image);
        }
        public void DrawMap(GeometryGroup[] geometryGroups, Canvas canvas, double thickness)
        {
            Path path1 = new Path();
            path1.Stroke = new SolidColorBrush(Color.FromRgb(0x37, 0xDC, 0x94));
            path1.StrokeThickness = thickness;
            path1.Data = geometryGroups[0];

            Path path2 = new Path();
            path2.Stroke = new SolidColorBrush(Color.FromRgb(0xFD, 0x9A, 0x28));
            path2.StrokeThickness = thickness;
            path2.Data = geometryGroups[1];

            Path path3 = new Path();
            path3.Stroke = new SolidColorBrush(Color.FromRgb(0xFA, 0x5C, 0x65));
            path3.StrokeThickness = thickness;
            path3.Data = geometryGroups[2];

            Path path4 = new Path();
            path4.Stroke = Brushes.Gray;
            path4.StrokeThickness = thickness/2;
            path4.Data = MapDirection.Clone();

            Path path5 = new Path();
            path5.Stroke = new SolidColorBrush(Color.FromRgb(0x26, 0x8A, 0xFF));
            path5.StrokeThickness = thickness * 1.5;
            path5.Data = MapNodes[0];

            Path path6 = new Path();
            path6.Stroke = Brushes.White;
            path6.StrokeThickness = thickness;
            path6.Data = MapNodes[1];

            canvas.Children.Add(path1);
            canvas.Children.Add(path2);
            canvas.Children.Add(path3);
            canvas.Children.Add(path4);
            canvas.Children.Add(path5);
            canvas.Children.Add(path6);

            Canvas.SetZIndex(path4, 2);
            Canvas.SetZIndex(path5, 2);
            Canvas.SetZIndex(path6, 2);
        }
        public void DrawMap(GeometryGroup[] geometries, UIElement element, Image image, System.Windows.Point pos, double thickness)
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            drawingGroup.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(new SolidColorBrush(Color.FromRgb(0x37, 0xDC, 0x94)), thickness), geometries[0]));
            drawingGroup.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(new SolidColorBrush(Color.FromRgb(0xFD, 0x9A, 0x28)), thickness), geometries[1]));
            drawingGroup.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(new SolidColorBrush(Color.FromRgb(0xFA, 0x5C, 0x65)), thickness), geometries[2]));
            drawingGroup.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.Gray, thickness / 2), MapDirection));
            drawingGroup.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(new SolidColorBrush(Color.FromRgb(0x26, 0x8A, 0xFF)), thickness * 1.5), MapNodes[0]));
            drawingGroup.Children.Add(new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.White, thickness), MapNodes[1]));
            
            DrawingImage drawingImage = new DrawingImage(drawingGroup);
            drawingImage.Freeze();

            image.Source = drawingImage;
            image.Stretch = Stretch.None;
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.VerticalAlignment = VerticalAlignment.Center;

            Canvas.SetLeft(image, pos.X);
            Canvas.SetTop(image, pos.Y);

            ((Canvas)element).Children.Add(image);
        }
        public void AddDrawMapPath(System.Windows.Point point1, System.Windows.Point point2, GeometryGroup geometries)
        {
            LineGeometry line = new LineGeometry(
                new System.Windows.Point(point1.X * Space, point1.Y * Space),
                new System.Windows.Point(point2.X * Space, point2.Y * Space));
            geometries.Children.Add(line);

            if (!MapNodeCheck[(int)point1.Y,(int)point1.X])
            {
                AddDrawNode(point1, MapNodes[0]);
                AddDrawNode(point1, MapNodes[1]);
                MapNodeCheck[(int)point1.Y, (int)point1.X] = true;
            }
            if (!MapNodeCheck[(int)point2.Y, (int)point2.X])
            {
                AddDrawNode(point2, MapNodes[0]);
                AddDrawNode(point2, MapNodes[1]);
                MapNodeCheck[(int)point2.Y, (int)point2.X] = true;
            }
        }
        public static void AddDirection(System.Windows.Point point, int dir, GeometryGroup geometry)
        {
            RectangleGeometry rect = new RectangleGeometry(
                new Rect(point.X * Space, point.Y * Space, Thickness/6*5, 0.01), 90, 90);
            rect.Transform = new RotateTransform(dir * 90 + 180, point.X * Space, point.Y * Space);
            geometry.Children.Add(rect);
        }
        public void AddDrawPath(System.Windows.Point point1, System.Windows.Point point2, GeometryGroup geometries)
        {
            LineGeometry line = new LineGeometry(
                new System.Windows.Point(point1.X * Space, point1.Y * Space),
                new System.Windows.Point(point2.X * Space, point2.Y * Space));
            geometries.Children.Add(line);

            AddDrawNode(point1,geometries);
            AddDrawNode(point2,geometries);
        }
        public void AddDrawNode(System.Windows.Point point, GeometryGroup geometry)
        {
            RectangleGeometry rect = new RectangleGeometry(
                new Rect(point.X * Space, point.Y * Space, 0, 0), 90, 90);
            geometry.Children.Add(rect);
        }
        public void AddDrawRoute(Point point1, Point point2, GeometryGroup geometries)
        {
            Point run = point2;
            while (run != point1)
            {
                RouteImageAnchor.X = Math.Min(run.X * Space + Thickness/12*7, RouteImageAnchor.X);
                RouteImageAnchor.Y = Math.Min(run.Y * Space + Thickness/32*29, RouteImageAnchor.Y);
                AddDrawPath(run, LookupPath[point1][run].Previous, geometries);
                run = LookupPath[point1][run].Previous;
            }
        }
        public void InitSingleNodesStorage(GeometryGroup geometryGroup, Canvas canvas, Brush color, double thickness)
        {
            Path singleNodes = new Path();
            singleNodes.Stroke = color;
            singleNodes.StrokeThickness = thickness;
            singleNodes.Data = geometryGroup;
            canvas.Children.Add(singleNodes);
            Canvas.SetZIndex(singleNodes, 3);
        }
        public void DrawSingleNode(System.Windows.Point point)
        {
            RectangleGeometry rect = new RectangleGeometry(
                new Rect(point.X * Space, point.Y * Space, 0, 0), 90, 90);

            SingleNodes.Children.Add(rect);
        }
        
    }
}
