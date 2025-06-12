using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Ellipse : Figure
    {
        public int RadiusX { get; set; }
        public int RadiusY { get; set; }
        public Point Center { get; set; }

        public Ellipse(int rx, int ry)
        {
            RadiusX = rx;
            RadiusY = ry;
            Center = new Point(0, 0);
        }

        public Ellipse(Point p1, Point p2)
        {
            RadiusX = Math.Abs(p2.X - p1.X) / 2;
            RadiusY = Math.Abs(p2.Y - p1.Y) / 2;
            Center = new Point(
                Math.Min(p1.X, p2.X) + RadiusX,
                Math.Min(p1.Y, p2.Y) + RadiusY
            );
        }

        public override void Build(int x, int y)
        {
            int pointsCount = (RadiusX + RadiusY) * 4;
            Points = new Point[pointsCount + 1]; 
            double angleStep = 2 * Math.PI / pointsCount;

            for (int i = 0; i < pointsCount; i++)
            {
                double angle = i * angleStep;
                Points[i] = new Point(
                    Center.X + (int)(RadiusX * Math.Cos(angle)),
                    Center.Y + (int)(RadiusY * Math.Sin(angle))
                );
            }

            Points[pointsCount] = Points[0]; 

            for(int i = 0; i < pointsCount; i++) 
            {
                Points[i] = new Point(Points[i].X + x, Points[i].Y + y);
            }
        }
    }
}
