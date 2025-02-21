using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Circle : Figure
    {
        public override void Build(int x, int y) 
        {
            Points = new Point[] { new Point(0, 0) };
            for (int xBase = -Radius; xBase <= Radius; xBase++)
            {
                for (int yBase = 0; yBase <= Radius; yBase++)
                {
                    if (xBase * xBase + yBase * yBase == Radius * Radius)
                    {
                        Points = Points.Append(new Point(xBase + x, yBase + y)).ToArray();
                    }
                }
            }
            for (int xBase = Radius; xBase >= -Radius; xBase--)
            {
                for (int yBase = -Radius; yBase <= 0; yBase++)
                {
                    if (xBase * xBase + yBase * yBase == Radius * Radius)
                    {
                        Points = Points.Append(new Point(xBase + x, yBase + y)).ToArray();
                    }
                }
            }
            Points = Points.Skip(1).ToArray();
        }

        public int Radius { get; set; }

        public Circle(int radius) { Radius = radius; }
    }
}
