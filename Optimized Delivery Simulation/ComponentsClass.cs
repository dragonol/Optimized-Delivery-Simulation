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
    public class Unit
    {
        private bool status = false;
        private Node node = null;

        public bool Status { get => status; set => status = value; }
        public Node Node { get => node; set => node = value; }
        public Unit()
        {
            Status = false;
            Node = null;
        }
    }
    public class Node
    {
        private Node left = null;
        private Node right = null;
        private Node up = null;
        private Node down = null;
        private int leftDis;
        private int rightDis;
        private int upDis;
        private int downDis;
        private bool goLeft;
        private bool goRight;
        private bool goUp;
        private bool goDown;
        private int x;
        private int y;
        private bool depot;

        public Node Left { get => left; set => left = value; }
        public Node Right { get => right; set => right = value; }
        public Node Up { get => up; set => up = value; }
        public Node Down { get => down; set => down = value; }
        public int LeftDis { get => leftDis; set => leftDis = value; }
        public int RightDis { get => rightDis; set => rightDis = value; }
        public int UpDis { get => upDis; set => upDis = value; }
        public int DownDis { get => downDis; set => downDis = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public bool GoLeft { get => goLeft; set => goLeft = value; }
        public bool GoRight { get => goRight; set => goRight = value; }
        public bool GoUp { get => goUp; set => goUp = value; }
        public bool GoDown { get => goDown; set => goDown = value; }
        public bool Depot { get => depot; set => depot = value; }

        public Node(bool left, bool right, bool up, bool down, int x, int y)
        {
            GoLeft = left;
            GoRight = right;
            GoUp = up;
            GoDown = down;
            X = x;
            Y = y;
            Left = Right = Up = Down = null;
            depot = false;
        }
    }
    public class TreePath
    {
        Dictionary<(int x, int y), (int x, int y)> path;
        Dictionary<(int x, int y), int> distance;

        public Dictionary<(int x, int y), (int x, int y)> Path { get => path; set => path = value; }
        public Dictionary<(int x, int y), int> Distance { get => distance; set => distance = value; }
    }
}