using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Rhombus : Parallelogram
    {
        public override void Build(int x, int y) { }

        public Rhombus(int aSide, int angle) : base(aSide, aSide, angle) { FirstSide = aSide; Angle = angle;  }
    }
}
