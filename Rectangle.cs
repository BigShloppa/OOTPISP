using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Rectangle : Parallelogram
    {
        public override void Build(int x, int y) 
        {
            Points = new Point[5];

            Points[0] = new Point(x, y); 
            Points[1] = new Point(x + FirstSide, y); 
            Points[2] = new Point(x + FirstSide, y + SecondSide); 
            Points[3] = new Point(x, y + SecondSide);
            Points[4] = Points[0];
        }
        public Rectangle(int aSide, int bSide) : base ( aSide, bSide, 90) { FirstSide = aSide; SecondSide = bSide; }
    }
}
