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

        }

        public int Radius { get; set; }

        public Circle(int radius) { Radius = radius; }
    }
}
