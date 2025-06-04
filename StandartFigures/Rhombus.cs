using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Rhombus : Parallelogram
    {
        public Rhombus(int aSide, int angle) : base(aSide, aSide, angle) { }

        public Rhombus(Point firstPoint, Point secondPoint)
            : base((int)(0.5 * Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) +
                                         Math.Pow(firstPoint.Y - secondPoint.Y, 2))),
                   (int)(0.5 * Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) +
                                         Math.Pow(firstPoint.Y - secondPoint.Y, 2))),
                   (int)(2 * (180 / Math.PI) * Math.Atan2(Math.Abs(firstPoint.Y - secondPoint.Y),
                                                          Math.Abs(firstPoint.X - secondPoint.X))))
        { }

        public override void Build(int x, int y)
        {
            Points = new Point[5];
            double angleInRadians = Math.PI * Angle / 180;

            Points[0] = new Point(x, y);
            Points[1] = new Point(x + FirstSide, y);
            Points[2] = new Point(x + (int)(FirstSide * Math.Cos(angleInRadians)),
                                  y + (int)(FirstSide * Math.Sin(angleInRadians)));
            Points[3] = new Point(x - (int)(FirstSide * Math.Sin(angleInRadians)),
                                  y + (int)(FirstSide * Math.Cos(angleInRadians)));
            Points[4] = Points[0];
        }
    }
}
