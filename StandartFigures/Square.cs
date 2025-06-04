using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Square : Rhombus
    {
        public Square(int aSide) : base(aSide, 90) { }

        public Square(Point firstPoint, Point secondPoint)
            : base(Math.Min(Math.Abs(firstPoint.X - secondPoint.X), Math.Abs(firstPoint.Y - secondPoint.Y)), 90) { }

        public override void Build(int x, int y)
        {
            Points = new Point[5];
            Points[0] = new Point(x, y);
            Points[1] = new Point(x + FirstSide, y);
            Points[2] = new Point(x + FirstSide, y + FirstSide);
            Points[3] = new Point(x, y + FirstSide);
            Points[4] = Points[0];
        }
    }
}
