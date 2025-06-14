using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

namespace lab
{
    public partial class MainWindowForm : Form
    {

        internal Serializator serializator;
        internal FigureList figureList = new FigureList();
        internal Painting painting;

        internal KeyValuePair<string, Drawing.figureConstructor>[] staticPairs =
        {
        new("�������", (x, y) => new Square(x, y)),
        new("��������������", (x, y) => new Parallelogram(x, y)),
        new("�������������", (x, y) => new Rectangle(x, y)),
        new("����������", (x, y) => new Circle(x, y)),
        new("����", (x, y) => new Rhombus(x, y)),
        new("�����", (x, y) => new Line(x, y)),
        new("������", (x, y) => new Ellipse(x, y)),
        new("�������", (x, y) => new Polyline(x,y)),
        new("�������������", (x, y) => new Polygon(new List<Point> { x,y }))
    };

        internal Drawing drawing = new Drawing();
        bool drawingActive = false;

        public MainWindowForm()
        {
            InitializeComponent();
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            drawing.gr = Graphics.FromImage(pictureBox.Image);
            figureList.figuresList = new Dictionary<string, Drawing.figureConstructor>(staticPairs);
            figureList.ReadDLLs();
            painting = new Painting(pictureBox, figureList);
            painting.setMode("����������", 3, Color.Black, Color.White);
            {
                this.SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.UserPaint |
            ControlStyles.ResizeRedraw |
            ControlStyles.SupportsTransparentBackColor,
            true);
                this.UpdateStyles();
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            painting.penDown(e);
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            painting.penMove(e);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            painting.penUp(e);
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
                painting.Undo();
            }
            else if (e.KeyCode == Keys.Right)
            {
                painting.Redo();
            }
            else if (e.KeyCode == Keys.S)
            {
                serializator = new Serializator(figureList, pictureBox);
                serializator.SetPainting(painting);

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
                serializator = new Serializator(figureList, pictureBox);
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "�������";
                openFileDialog.Filter = "Binary File (*.bbl)|*.bbl|All Files (*.*)|*.*"; // .bbl -- bitmap binary list
                openFileDialog.ShowDialog();
                string filePath = openFileDialog.FileName;

                painting = serializator.Deserialize(filePath);

                painting.Undo();
                painting.Redo();
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