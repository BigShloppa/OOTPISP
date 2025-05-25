using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Rhombus : Parallelogram
    {
        public override void Build(int x, int y)
        {
            Points = new Point[5];

            double angleInRadians = Math.PI * Angle / 180;

            Points[0] = new Point(x, y);
            Points[1] = new Point((int)(x + FirstSide * Math.Cos(0)), (int)(y + FirstSide * Math.Sin(0)));
            Points[2] = new Point((int)(x + FirstSide * Math.Cos(angleInRadians)),
                                  (int)(y + FirstSide * Math.Sin(angleInRadians)));
            Points[3] = new Point((int)(x + FirstSide * Math.Cos(angleInRadians) - FirstSide),
                                  (int)(y + FirstSide * Math.Sin(angleInRadians)));
            Points[4] = Points[0];
        }

        public Rhombus(int aSide, int angle) : base(aSide, aSide, angle) { }
        public Rhombus(Point firstPoint, Point secondPoint) : base(
        (int)(0.5 * Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2))),
        (int)(0.5 * Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2))),
        (int)(2 * (180 / Math.PI) * Math.Atan2(Math.Abs(firstPoint.Y - secondPoint.Y), Math.Abs(firstPoint.X - secondPoint.X)))
    )
        {
        }
    }
}
