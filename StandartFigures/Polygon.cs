using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Polygon : Polyline
    {
        private bool isClosed = false;
        public List<Point> Vertices { get; set; }

        public Polygon(List<Point> points) : base(
    points.Count > 0 ? points[0] : new Point(0, 0),
    points.Count > 1 ? points[1] : new Point(0, 0))
        {
            Vertices = points;
        }

        public void Close()
        {
            Points = Points.Append(Points[0]).ToArray();
        }

        public override void Build(int x, int y)
        {
            Close();
        }
    }
}
