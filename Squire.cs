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
            Points = new Point[5];

            Points[0] = new Point(x, y); 
            Points[1] = new Point(x + FirstSide, y); 
            Points[2] = new Point(x + FirstSide, y + FirstSide); 
            Points[3] = new Point(x, y + FirstSide);
            Points[4] = Points[0];
        }

        public Squire(int aSide) : base(aSide, 90) { FirstSide = aSide; }
    }
}
