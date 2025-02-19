using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Rectangle : Parallelogram
    {
        public override void Build(int x, int y) { }
        public Rectangle(int aSide, int bSide) : base ( aSide, bSide, 90) { FirstSide = aSide; SecondSide = bSide; }
    }
}
