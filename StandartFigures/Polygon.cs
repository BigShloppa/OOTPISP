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

        public override void Build(int x, int y)
        {
            Points = new Point[Vertices.Count + 1];
            for (int i = 0; i < Vertices.Count; i++)
                Points[i] = Vertices[i];
            Points[Vertices.Count] = Vertices[0]; 
        }

        public void Close()
        {
            isClosed = true;
            if (pointsList.Count > 2 && pointsList[0] != pointsList[^1])
            {
                pointsList.Add(pointsList[0]);
            }
            Points = pointsList.ToArray();
        }

    }
}
