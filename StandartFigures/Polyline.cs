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

        public override void Build(int leftX, int topY)
        {
            if (Points == null || Points.Length == 0) return;

            int offsetX = leftX - Points[0].X;
            int offsetY = topY - Points[0].Y;

            for (int i = 0; i < pointsList.Count; i++)
            {
                pointsList[i] = new Point(pointsList[i].X + offsetX, pointsList[i].Y + offsetY);
            }

            Points = pointsList.ToArray();
        }
    }
}
