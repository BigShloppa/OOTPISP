using lab;
using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace FigurePlugin
{
    public class Trapezoid : Figure
    {

        public static string name = "Трапеция";
        public override void Build(int x, int y)
        {
            
        }

        public Trapezoid(Point FirstPoint, Point SecondPoint) 
        {

            Point A = new Point(int.Abs(FirstPoint.X - SecondPoint.X) / 5 + FirstPoint.X, FirstPoint.Y);

            Point B = new Point( - int.Abs(FirstPoint.X - SecondPoint.X) / 5 + SecondPoint.X, FirstPoint.Y);

            Point C = new Point(FirstPoint.X, SecondPoint.Y);

            Points = new Point[] { A, B, SecondPoint, C, A };
        }
    }
}
