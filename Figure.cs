using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public abstract class Figure
    {
        public abstract void Build(int x, int y);

        public Point[]? Points;
    }
}
