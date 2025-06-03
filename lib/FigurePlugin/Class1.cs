using lab;
using System;
using System.Drawing;
using System.Runtime.InteropServices;


namespace FigurePlugin
{
    public class Triangle : Figure
    {

        public static string name = "Треугольник";
        public override void Build(int x, int y)
        {
            
        }

        public Triangle(Point FirstPoint, Point SecondPoint) 
        {

            Point B = new Point(SecondPoint.X,
                FirstPoint.Y + (int.Sign(FirstPoint.Y - SecondPoint.Y) == 1 ? - int.Abs(FirstPoint.Y - SecondPoint.Y) / 2 : int.Abs(FirstPoint.Y - SecondPoint.Y) / 2)
                );

            Point C = new Point(
                FirstPoint.X + (int.Sign(FirstPoint.X - SecondPoint.X) == 1 ? -int.Abs(FirstPoint.X - SecondPoint.X) / 2 : int.Abs(FirstPoint.X - SecondPoint.Y) / 2)
                , SecondPoint.Y);

            Points = new Point[] { FirstPoint, B, C, FirstPoint };
        }
    }
}
