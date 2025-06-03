using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Circle : Figure
    {
        public override void Build(int x, int y)
        {
            int PointsCount = Radius * 6;
            Points = new Point[PointsCount];
            double angleStep = 2 * Math.PI / PointsCount;

            for (int i = 0; i < PointsCount; i++)
            {
                double angle = i * angleStep;
                int xOffset = (int)(Radius * Math.Cos(angle));
                int yOffset = (int)(Radius * Math.Sin(angle));
                Points[i] = new Point(x + xOffset, y + yOffset);
            }
        }

        public int Radius { get; set; }

        public Circle(int radius) { Radius = radius; }
        public Circle(Point FirstPoint, Point SecondPoint) { Radius = double.ConvertToInteger<int>(Math.Sqrt((FirstPoint.X - SecondPoint.X) * (FirstPoint.X - SecondPoint.X)
                + (FirstPoint.Y - SecondPoint.Y) * (FirstPoint.Y - SecondPoint.Y))); }
    }
}
