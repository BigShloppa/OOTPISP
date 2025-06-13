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
            Points = pointsList.ToArray();
        }

        public void AddPoint(Point p)
        {
            if(pointsList.Count > 0 && pointsList[0].X == 0 && pointsList[0].Y == 0) 
                pointsList.RemoveAt(0);
            pointsList.Add(p);
            Points = pointsList.ToArray();
        }

        public override void Build(int x, int y)
        {
        }
    }
}
