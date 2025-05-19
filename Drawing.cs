using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
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
            if (PreviousPoint.X == 0 && PreviousPoint.Y == 0)
                PreviousPoint = point;
            else
                this.PreviousPoint = this.point;
            this.point = point;
        }
        
        public void Draw(PictureBox pb) 
        {
            if (cursor) 
            {
                if (drColor != null)
                {     
                    gr.DrawCurve(new Pen((Color)drColor, thickness), new Point[] { PreviousPoint, point });
                }
            }
            else
            {
                if (figure != null && point != null)
                {
                    figure.Build(point.X, point.Y); 
                    gr.DrawLines(new Pen(Color.Red), figure.Points);
                }
            }
            pb.Invalidate();
        }
    }
}
