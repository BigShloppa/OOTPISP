using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    internal class Serializator
    {
        private struct figureData {
            public String Name;
            public Point[] points;
        }

        private struct drawingData {
            public figureData drawingFigure;
            public Color? drColor;
            public int thickness;
            public Point point;
            public Point previousPoint;
        }
        private struct PaintingData {
            public drawingData[] drawings;
        }

        private PictureBox pictureBox;
        private FigureList fList;
        private Painting painting;

        public Serializator(FigureList fList, PictureBox pbox) { this.fList = fList; this.pictureBox = pbox; }
        public void SetPainting(Painting painting)
            { this.painting = painting; }

        public void Serialize(string filePath)
        {
            PaintingData data = new PaintingData();
            data.drawings = new drawingData[painting.getCount()];

            for (int i = 0; i < painting.getCount(); i++) // фикс
            {
                Drawing current = painting.getDrawing(i);
                data.drawings[i] = new drawingData(); // фикс

                data.drawings[i].drColor = current.drColor;
                data.drawings[i].thickness = current.thickness;
                data.drawings[i].point = new Point(current.point.X, current.point.Y);
                data.drawings[i].previousPoint = new Point(current.previousPoint.X, current.previousPoint.Y);

                data.drawings[i].drawingFigure = new figureData();
                data.drawings[i].drawingFigure.points = (Point[])current.figure.Points.Clone();

                // Найти имя фигуры по делегату
                foreach (var pair in fList.figuresList)
                {
                    if (pair.Value == current.Constructor)
                    {
                        data.drawings[i].drawingFigure.Name = pair.Key;
                        break;
                    }
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(data.drawings.Length);
                foreach (var drawing in data.drawings)
                {
                    if (drawing.drawingFigure.points == null)
                        continue;

                    // Write figure name
                    writer.Write(drawing.drawingFigure.Name ?? "");

                    // Write points
                    writer.Write(drawing.drawingFigure.points.Length);
                    foreach (var pt in drawing.drawingFigure.points)
                    {
                        writer.Write(pt.X);
                        writer.Write(pt.Y);
                    }

                    // Write color
                    writer.Write(drawing.drColor.HasValue);
                    if (drawing.drColor.HasValue)
                        writer.Write(drawing.drColor.Value.ToArgb());

                    // Write thickness and points
                    writer.Write(drawing.thickness);
                    writer.Write(drawing.point.X);
                    writer.Write(drawing.point.Y);
                    writer.Write(drawing.previousPoint.X);
                    writer.Write(drawing.previousPoint.Y);
                }
            }
        }

        public Painting Deserialize(string filePath)
        {
            Painting deserializedP = new Painting(pictureBox, fList);
            PaintingData data = new PaintingData();

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int count = reader.ReadInt32();
                data.drawings = new drawingData[count];

                for (int i = 0; i < count; i++) // ← исправлено
                {
                    drawingData drawing = new drawingData();
                    drawing.drawingFigure = new figureData(); // ← обязательно

                    drawing.drawingFigure.Name = reader.ReadString();

                    int pointCount = reader.ReadInt32();
                    drawing.drawingFigure.points = new Point[pointCount];
                    for (int j = 0; j < pointCount; j++)
                    {
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        drawing.drawingFigure.points[j] = new Point(x, y);
                    }

                    bool hasColor = reader.ReadBoolean();
                    drawing.drColor = hasColor ? Color.FromArgb(reader.ReadInt32()) : (Color?)null;

                    drawing.thickness = reader.ReadInt32();
                    drawing.point = new Point(reader.ReadInt32(), reader.ReadInt32());
                    drawing.previousPoint = new Point(reader.ReadInt32(), reader.ReadInt32());

                    data.drawings[i] = drawing;
                }
            }

            Drawing[] drawings = new Drawing[data.drawings.Length]; // ← исправлено

            for (int i = 0; i < drawings.Length; i++)
            {
                drawings[i] = new Drawing();
                drawings[i].gr = pictureBox.CreateGraphics();

                string figName = data.drawings[i].drawingFigure.Name;
                if (!fList.figuresList.TryGetValue(figName, out var constructor))
                {
                    throw new InvalidOperationException($"Не удалось найти конструктор для фигуры '{figName}'.");
                }

                drawings[i].Constructor = constructor;
                drawings[i].drColor = data.drawings[i].drColor;
                drawings[i].point = new Point(data.drawings[i].point.X, data.drawings[i].point.Y);
                drawings[i].previousPoint = new Point(data.drawings[i].previousPoint.X, data.drawings[i].previousPoint.Y);
                drawings[i].thickness = data.drawings[i].thickness;
                drawings[i].figure = drawings[i].Constructor(drawings[i].point, drawings[i].previousPoint);
            }

            deserializedP.setList(drawings);
            return deserializedP;
        }
    }
}
