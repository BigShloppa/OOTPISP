using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Polygon : Figure
    {
        public List<Point> Vertices { get; set; }

        public Polygon(List<Point> points)
        {
            Vertices = points;
        }

        public override void Build(int x, int y)
        {
            Points = new Point[Vertices.Count + 1];
            for (int i = 0; i < Vertices.Count; i++)
                Points[i] = Vertices[i];
            Points[Vertices.Count] = Vertices[0]; 
        }
    }
}
