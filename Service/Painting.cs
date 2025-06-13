using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab
{
    internal class Painting
    {
        private List<Drawing>? drawings;

        private Drawing currentDrawing;

        private PictureBox objectBox;

        private FigureList fList;

        private int currentIndex;

        private bool drawActive = false;

        public void setList(Drawing[] drawingArr) {
            drawings = new List<Drawing>(drawingArr);
            currentIndex = drawings.Count; 
        }
        public Drawing getDrawing(int index) { return drawings[index]; }
        public int getCount() { return currentIndex; }
        public Painting(PictureBox pBox, FigureList list) 
        {
            objectBox = pBox;
            fList = list;
            currentIndex = 0;
            drawings = new List<Drawing>();
        }
        public void Redraw() 
        {
            objectBox.Refresh();
            for (int i = 0; i < currentIndex; i++)
            {
                drawings[i].Draw(objectBox);
            }
        }
        public void Undo() 
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            Redraw();
        }

        public void Redo()
        {
            if (currentIndex < drawings.Count)
            {
                currentIndex++;
            }
            Redraw();
        }

        public void Add(Drawing drawing) 
        {
            drawings.Add(drawing);
            currentIndex++;
            Redraw();
        }

        public void Remove() 
        {
            if (drawings.Count > 0)
            {
                drawings.RemoveAt(drawings.Count - 1);
                currentIndex--;
            }
        }

        public void Save() 
        {
            if (currentIndex < drawings.Count)
            {
                drawings.RemoveRange(currentIndex, drawings.Count - currentIndex);
            }
        }

        public void setMode(String fString, int thickness, Color drColor) 
        {
            currentDrawing = new Drawing();
            currentDrawing.Constructor = fList.figuresList.GetValueOrDefault(fString);
            currentDrawing.figure = currentDrawing.Constructor(new Point(0, 0), new Point(0, 0));
            currentDrawing.gr = objectBox.CreateGraphics();
            currentDrawing.drColor = drColor;
            currentDrawing.thickness = thickness;
        }
        public void penDown(MouseEventArgs e) 
        {
            if (currentDrawing == null)
                return;
            Save();
                if (currentDrawing.figure is Polyline poly)
            {
                if (poly.Points.Length > 0 && e.Location == poly.Points[poly.Points.Length - 1])
                {

                    currentDrawing.setPoint(poly.Points[0]);
                    currentDrawing.setPoint(poly.Points[0]);

                    poly.Points = poly.Points.Take(poly.Points.Length - 2).ToArray();

                    Add(new Drawing(currentDrawing));

                    Drawing nDrawing = new Drawing();
                    nDrawing.gr = currentDrawing.gr;
                    nDrawing.Constructor = currentDrawing.Constructor;
                    nDrawing.figure = currentDrawing.Constructor(new Point(0, 0), new Point(0, 0));
                    nDrawing.gr = objectBox.CreateGraphics();
                    nDrawing.drColor = currentDrawing.drColor;
                    nDrawing.thickness = currentDrawing.thickness;

                    currentDrawing = new Drawing(nDrawing);
                }
                else
                {
                    poly.AddPoint(e.Location);
                    Remove();
                    Add(new Drawing(currentDrawing));
                }
            }
            else
            {
                currentDrawing.setPoint(e.Location);
                Add(new Drawing(currentDrawing));
                drawActive = true;
            }
        }

        public void penUp(MouseEventArgs e) 
        {
            if (currentDrawing == null)
                return;
            if (currentDrawing.figure is Polyline)
                return;
            Remove();
            Add(new Drawing(currentDrawing));

            Drawing nDrawing = new Drawing();
            nDrawing.gr = currentDrawing.gr;
            nDrawing.Constructor = currentDrawing.Constructor;
            nDrawing.figure = currentDrawing.Constructor(new Point(0, 0), new Point(0, 0));
            nDrawing.gr = objectBox.CreateGraphics();
            nDrawing.drColor = currentDrawing.drColor;
            nDrawing.thickness = currentDrawing.thickness;

            currentDrawing = new Drawing(nDrawing);
            drawActive = false;
        }

        public void penMove(MouseEventArgs e) 
        {
            if (currentDrawing == null)
                return;
            if (!drawActive)
                return;
            if (currentDrawing.figure is Polyline)
                return;
            Remove();
            currentDrawing.setPoint(e.Location);
            Add(new Drawing(currentDrawing));
        }
    }
}
