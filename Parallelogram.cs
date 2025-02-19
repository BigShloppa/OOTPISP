using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class Parallelogram : Figure
    {
        public override void Build(int x, int y) { }
        public int FirstSide { get; set; }

        public int SecondSide { get; set; }

        public int Angle { get; set; }

        public Parallelogram(int aSide, int bSide, int angle) { FirstSide = aSide; SecondSide = bSide; Angle = angle; }
    }
}
