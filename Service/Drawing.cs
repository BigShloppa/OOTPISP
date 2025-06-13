using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Drawing
    {
        public Color? drColor;
        public Color? flColor;
        public int thickness;

        public Graphics gr;
        public Point previousPoint;
        public Point point;

        public Figure? figure;
        public delegate Figure figureConstructor(Point FPoint, Point SPoint);
        public figureConstructor Constructor;

        public Drawing()
        {
            point = new Point(-1, -1);
            previousPoint = new Point(-1, -1);
            drColor = Color.Black;
            flColor = null;
            thickness = 3;
            figure = null;
        }

        public Drawing(Drawing dr) 
        {
            point = new Point(dr.point.X, dr.point.Y);
            previousPoint = new Point(dr.previousPoint.X, dr.previousPoint.Y);
            Constructor = dr.Constructor;
            drColor = dr.drColor;
            figure = Constructor(previousPoint, point);
            if (figure.Points != null)
            {
                figure.Points = new Point[dr.figure.Points.Length];
                for (int i = 0; i < dr.figure.Points.Length; i++)
                {
                    figure.Points[i] = dr.figure.Points[i];
                }
            }
            gr = dr.gr;
            thickness = dr.thickness;
        }

        public void setDrawMode(Color drawColor, int thicknessOfDrawing)
        {
            flColor = null;
            figure = null;
            thickness = thicknessOfDrawing;
            drColor = drawColor;
        }

        public void setPoint(Point point)
        {
            if (figure is Polyline polyline)
            {
                polyline.AddPoint(point);
                previousPoint = this.point = point;
            }
            else
            {
                if (previousPoint.X == -1 && previousPoint.Y == -1)
                    previousPoint = point;

                this.point = point;
                this.figure = this.Constructor(this.previousPoint, this.point);
            }
        }

        public void Draw(PictureBox pb)
        {
            
                if (figure != null)
                {
                    int x = Math.Min(previousPoint.X, point.X);
                    int y = Math.Min(previousPoint.Y, point.Y);
                    figure.Build(x, y);

                if (figure.Points?.Length > 1)
                    {
                        using Pen pen = new Pen((Color)drColor, thickness);
                            gr.DrawLines(pen, figure.Points);
                    }
                }
        }
    }
}
