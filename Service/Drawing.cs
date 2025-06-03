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
        public bool cursor;

        public Color ?drColor;

        public Color ?flColor;

        int thickness;

        public Graphics gr;
        public Point PreviousPoint;
        public Point point;

        public Figure ?figure;

        public delegate Figure figureConstructor(Point FPoint, Point SPoint);
        public figureConstructor Constructor;

        public Drawing() 
        {
            point = new Point(0,0);
            cursor = true;
            drColor = Color.Black;
            flColor = null;
            thickness = 3;
            figure = null;
        }

        public void setDrawMode(Color drawColor, int thicknessOfDrawing) 
        {
            flColor = null;
            figure = null;
            cursor = true;
            thickness = thicknessOfDrawing;
            drColor = drawColor;
        }
        public void setDrawMode(Color drawColor, Color fillColor, int thicknessOfDrawing, Figure figureToDraw) 
        {
            cursor = false;
            drColor = drawColor;
            flColor = fillColor;
            thickness = thicknessOfDrawing;
            figure = figureToDraw;
        }
        
        public void setPoint(Point point) 
        {
            if (cursor)
            {
                if (PreviousPoint.X == 0 && PreviousPoint.Y == 0)
                    PreviousPoint = point;
                else
                    this.PreviousPoint = this.point;
                this.point = point;
            }
            else
            {
                if (PreviousPoint.X == 0 && PreviousPoint.Y == 0)
                    PreviousPoint = point;
                this.point = point;
                this.figure = this.Constructor(this.PreviousPoint, this.point);
            }
        }
        
        public void Draw(PictureBox pb) 
        {
            if (cursor) 
            {
                if (drColor != null)
                {     
                    gr.DrawCurve(new Pen((Color)drColor, thickness), new Point[] { PreviousPoint, 
                        new Point(point.X + int.Sign(point.X - PreviousPoint.X) * thickness / 3,
                        point.Y + int.Sign(point.Y - PreviousPoint.Y) * thickness / 3) });
                }
            }
            else
            {
                if (figure != null)
                {
                    if (point.X < PreviousPoint.X && point.Y < PreviousPoint.Y)
                        figure.Build(point.X, point.Y);
                    else if (point.X > PreviousPoint.X && point.Y < PreviousPoint.Y)
                        figure.Build(PreviousPoint.X, point.Y);
                    else if (point.X < PreviousPoint.X && point.Y > PreviousPoint.Y)
                        figure.Build(point.X, PreviousPoint.Y);
                    else
                        figure.Build(PreviousPoint.X, PreviousPoint.Y);
                    if (figure.Points.Length > 0)
                    {
                        figure.Points[figure.Points.Length - 1].Y -= thickness / 2;
                        gr.DrawLines(new Pen((Color)drColor, thickness), figure.Points);
                    }
                }
            }
            pb.Invalidate();
        }
    }
}
