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
            gr = dr.gr;
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
                if (previousPoint.X == -1 && previousPoint.Y == -1)
                    previousPoint = point;

                this.point = point;
                this.figure = this.Constructor(this.previousPoint, this.point);
            
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

                        if (flColor != null && figure.Points.Length >= 3 &&
                            figure.Points[0] == figure.Points[figure.Points.Length - 1])
                        {
                            using SolidBrush brush = new SolidBrush((Color)flColor);
                            gr.FillPolygon(brush, figure.Points);
                        }

                        if (figure.Points.Length >= 3 && figure.Points[0] == figure.Points[figure.Points.Length - 1])
                        {
                            gr.DrawPolygon(pen, figure.Points);
                        }
                        else
                        {
                            gr.DrawLines(pen, figure.Points);
                        }
                    }
                }
        }
    }
}
