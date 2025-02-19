using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Squire : Rhombus
    {
        public override void Build(int x, int y) 
        {
            int xBase, yBase;

            Points = new Point[] { new Point(0,0)};

            for(xBase = 0, yBase = 0; xBase < this.FirstSide; xBase++)
            {
                Points = Points.Append(new Point(xBase + x, yBase + y)).ToArray();
            }
            for (yBase = 0; yBase < this.FirstSide; yBase++)
            {
                Points = Points.Append(new Point(xBase + x, yBase + y)).ToArray();
            }
            for (; xBase > 0; xBase--)
            {
                Points = Points.Append(new Point(xBase + x, yBase + y)).ToArray();
            }
            for (; yBase > 0; yBase++)
            {
                Points = Points.Append(new Point(xBase + x, yBase + y)).ToArray();
            }
            Points = Points.Skip(1).ToArray();
        }

        public Squire(int aSide) : base(aSide, 90) { FirstSide = aSide; }
    }
}
