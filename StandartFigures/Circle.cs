using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Circle : Ellipse
    {
        public int Radius { get; set; }

        public Circle(int radius) : base(radius * 2, radius * 2) { Radius = radius; }

        public Circle(Point firstPoint, Point secondPoint) : base(firstPoint, secondPoint)
        {
            Radius = (int)Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) +
                                    Math.Pow(firstPoint.Y - secondPoint.Y, 2));
        }

        public override void Build(int x, int y)
        {
            int pointsCount = Radius * 6;
            Points = new Point[pointsCount];
            double angleStep = 2 * Math.PI / pointsCount;

            for (int i = 0; i < pointsCount; i++)
            {
                double angle = i * angleStep;
                Points[i] = new Point(x + (int)(Radius * Math.Cos(angle)),
                                      y + (int)(Radius * Math.Sin(angle)));
            }
        }
    }
}
