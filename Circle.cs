using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Circle : Figure
    {
        public override void Build(int x, int y)
        {
            int PointsCount = this.Radius * 6;
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
    }
}
