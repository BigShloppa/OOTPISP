using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Parallelogram : Figure
    {
        public int FirstSide { get; set; }
        public int SecondSide { get; set; }
        public int Angle { get; set; }

        public Parallelogram(int aSide, int bSide, int angle)
        {
            FirstSide = aSide;
            SecondSide = bSide;
            Angle = angle;
        }

        public Parallelogram(Point firstPoint, Point secondPoint)
        {
            FirstSide = (int)Math.Round(Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) +
                                                  Math.Pow(secondPoint.Y - firstPoint.Y, 2)));
            Angle = 45;
            SecondSide = FirstSide / 2;
        }

        public override void Build(int x, int y)
        {
            Points = new Point[5];
            double angleInRadians = Math.PI * Angle / 180;

            Points[0] = new Point(x, y);
            Points[1] = new Point(x + FirstSide, y);
            Points[2] = new Point(x + FirstSide + (int)(SecondSide * Math.Cos(angleInRadians)),
                                  y + (int)(SecondSide * Math.Sin(angleInRadians)));
            Points[3] = new Point(x + (int)(SecondSide * Math.Cos(angleInRadians)),
                                  y + (int)(SecondSide * Math.Sin(angleInRadians)));
            Points[4] = Points[0];
        }
    }
}
