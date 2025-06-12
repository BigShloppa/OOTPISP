using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Polyline : Figure
    {
        protected List<Point> pointsList = new List<Point>();

        public Polyline(Point start, Point end)
        {
            pointsList.Add(start);
            pointsList.Add(end);
            Points = pointsList.ToArray();
        }

        public void AddPoint(Point p)
        {
            pointsList.Add(p);
            Points = pointsList.ToArray();
        }

        public override void Build(int x, int y)
        {
            if (Points == null || Points.Length == 0) return;

            for (int i = 0; i < pointsList.Count; i++)
            {
                pointsList[i] = new Point(pointsList[i].X + x, pointsList[i].Y + y);
            }

            Points = pointsList.ToArray();
        }
    }
}
