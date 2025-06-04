using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Reflection;

namespace lab
{
    public partial class MainWindowForm : Form
    {
        static bool mouseEnteredPanel = false;
        static bool redoCalled = false;
        static bool figureDrawing = false;
        static bool multiPointMode = false; 

        internal Serializator serializator = new Serializator();
        public List<Bitmap>? states;
        private int statesActiveIndex;
        internal FigureList figureList = new FigureList();

        internal KeyValuePair<string, Drawing.figureConstructor>[] staticPairs =
        {
        new("�������", (x, y) => new Square(x, y)),
        new("��������������", (x, y) => new Parallelogram(x, y)),
        new("�������������", (x, y) => new Rectangle(x, y)),
        new("����������", (x, y) => new Circle(x, y)),
        new("����", (x, y) => new Rhombus(x, y)),
        new("�����", (x, y) => new Line(x, y)),
        new("������", (x, y) => new Ellipse(x, y)),
        new("���������", (x, y) => new Polyline(x,y)),
        new("�������", (x, y) => new Polygon(new List<Point> { x,y }))
    };

        internal Drawing drawing = new Drawing();
        bool drawingActive = false;

        public MainWindowForm()
        {
            InitializeComponent();
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            drawing.gr = Graphics.FromImage(pictureBox.Image);
            states = new List<Bitmap> { (Bitmap)pictureBox.Image.Clone() };
            statesActiveIndex = 0;
            figureList.figuresList = new Dictionary<string, Drawing.figureConstructor>(staticPairs);
            figureList.ReadDLLs();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            drawingActive = true;
            if (redoCalled)
            {
                states.RemoveRange(statesActiveIndex + 1, states.Count - statesActiveIndex - 1);
                redoCalled = false;
            }

            if (!drawing.cursor)
            {

                if (drawing.figure is Polyline poly)
                {
                    if (poly.Points[poly.Points.Length - 1] == e.Location)
                    {
                        poly.Build(e.X, e.Y);

                        drawing.Draw(pictureBox);
                        states.Add(new Bitmap(pictureBox.Image));
                        statesActiveIndex++;
                        multiPointMode = false;
                        return;
                    }
                    poly.AddPoint(e.Location);
                    poly.Build(e.X, e.Y);
                    drawing.Draw(pictureBox);
                    multiPointMode = true;
                    states.Add(new Bitmap(pictureBox.Image));
                    statesActiveIndex++;
                }
                else if (drawing.figure is Polygon polygon)
                {
                    if (polygon.Vertices[polygon.Vertices.Count - 1] == e.Location)
                    {

                        polygon.Close();
                        polygon.Build(0, 0);
                        drawing.Draw(pictureBox);

                        states.Add(new Bitmap(pictureBox.Image));
                        statesActiveIndex++;

                        multiPointMode = false;
                        return;
                    }
                    polygon.Vertices.Add(e.Location);
                    polygon.Build(0, 0);
                    drawing.Draw(pictureBox);

                    states.Add(new Bitmap(pictureBox.Image));
                    statesActiveIndex++;
                }
                else if (!multiPointMode)
                {
                    drawing.figure = drawing.Constructor(e.Location, e.Location);
                }

            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawingActive)
            {

                if (drawing.figure is Polyline)
                {
                    return;
                }
                else
                if (!drawing.cursor)
                {
                    if (figureDrawing)
                    {
                        pictureBox.Image?.Dispose();
                        pictureBox.Image = (Image)states[statesActiveIndex].Clone();
                        drawing.gr = Graphics.FromImage(pictureBox.Image);
                        pictureBox.Invalidate();
                    }
                    else
                    {
                        states.Add(new Bitmap(pictureBox.Image));
                        statesActiveIndex++;
                        figureDrawing = true;
                    }

                }
                drawing.setPoint(new Point(e.X, e.Y));
                drawing.Draw(pictureBox);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            drawingActive = false;
            drawing.PreviousPoint = new Point(0, 0);

            if (drawing.cursor) 
            {
                states.Add(new Bitmap(pictureBox.Image));
                statesActiveIndex++;
            }
            else if (!multiPointMode) 
            {
                states.Add(new Bitmap(pictureBox.Image));
                statesActiveIndex++;
            }
        }

        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void MainWindowForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                DrawSettingForm SettingForm = new DrawSettingForm();
                SettingForm.MainWindowParent = this;
                SettingForm.ShowDialog();
                drawing.gr = Graphics.FromImage(pictureBox.Image);
            }
            else if (e.KeyCode == Keys.Left)
            {
                statesActiveIndex--;
                if (statesActiveIndex > -1)
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = (Image)states[statesActiveIndex].Clone();
                    drawing.gr = Graphics.FromImage(pictureBox.Image);
                    pictureBox.Invalidate();
                    redoCalled = true;
                }
                else
                    statesActiveIndex = 0;
            }
            else if (e.KeyCode == Keys.Right)
            {
                statesActiveIndex++;
                if (statesActiveIndex < states.Count)
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = (Image)states[statesActiveIndex].Clone();
                    drawing.gr = Graphics.FromImage(pictureBox.Image);
                    pictureBox.Invalidate();
                }
                else
                    statesActiveIndex--;
            }
            else if (e.KeyCode == Keys.S)
            {
                serializator.states = [.. states.ToArray()];
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "��������� ���";
                saveFileDialog.Filter = "Binary File (*.bbl)|*.bbl|All Files (*.*)|*.*"; // .bbl -- bitmap binary list
                saveFileDialog.FileName = "drawing.bbl";
                saveFileDialog.ShowDialog();
                string filePath = saveFileDialog.FileName;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                serializator.Serialize(filePath);
            }
            else if (e.KeyCode == Keys.O)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "�������";
                openFileDialog.Filter = "Binary File (*.bbl)|*.bbl|All Files (*.*)|*.*"; // .bbl -- bitmap binary list
                openFileDialog.ShowDialog();
                string filePath = openFileDialog.FileName;

                serializator.Deserialize(filePath);
                states = [.. serializator.states.ToArray()];
                statesActiveIndex = states.ToArray().Length - 1;

                pictureBox.Image?.Dispose();
                pictureBox.Image = (Image)states[statesActiveIndex].Clone();
                drawing.gr = Graphics.FromImage(pictureBox.Image);
                pictureBox.Invalidate();
            }
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("���������� ������ ���������� �������������� ��� ������ ������. \n\n " +
                "\"D\" ��������� ���� ��������� �����;\n" +
                "\"S\" ��������� ���� ���������� �������;\n" +
                "\"O\" ��������� ���� �������� ��� ������������� �������;\n" +
                "\"<-\" � \"->\" ������������ ��� redo � undo;\n\n" +
                "������� ����������� � ������ .bbl, �������������� ����� ������������������ ��������," +
                " ����������� ������������� ��� ��������� �����������.\n\n" +
                "����� �� ������ ��������� ����������� ������-������. ��� ����� ��������� � ����� � ���������� .dll C# �����, ���������� ���� ������.\n" +
                "������������ �� ������ Figure, ���������� ����� Build ��� ���������� �����, ����������� ���� ������ � �������� ����������� ��� �����" +
                "������ �� ����� ������ �������� � ������� ������� ���� ������.\n\n" +
                "������� ����� � ����� ������ ��������� � ����������� ����� ������� ������� � ����� � ���������� ����� dlls.conf\n" +
                "�������� \"true\" ��� \"false\" ����� ����� ������� � ����� ����� ����� ��������� ����� ������.");
        }
    }
}