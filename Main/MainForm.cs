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
        new("Квадрат", (x, y) => new Square(x, y)),
        new("Параллелограмм", (x, y) => new Parallelogram(x, y)),
        new("Прямоугольник", (x, y) => new Rectangle(x, y)),
        new("Окружность", (x, y) => new Circle(x, y)),
        new("Ромб", (x, y) => new Rhombus(x, y)),
        new("Линия", (x, y) => new Line(x, y)),
        new("Эллипс", (x, y) => new Ellipse(x, y)),
        new("Ломаная", (x, y) => new Polyline(x,y)),
        new("Многоугольник", (x, y) => new Polygon(new List<Point> { x,y }))
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
            painting.setMode("Окружность", 3, Color.Black, Color.White);
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
                saveFileDialog.Title = "Сохранить как";
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
                openFileDialog.Title = "Открыть";
                openFileDialog.Filter = "Binary File (*.bbl)|*.bbl|All Files (*.*)|*.*"; // .bbl -- bitmap binary list
                openFileDialog.ShowDialog();
                string filePath = openFileDialog.FileName;

                painting = serializator.Deserialize(filePath);

                painting.Undo();
                painting.Redo();
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Управление данной программой осуществляется при помощи клавиш. \n\n " +
                "\"D\" открывает меню настройки кисти;\n" +
                "\"S\" открывает меню сохранения рисунка;\n" +
                "\"O\" открывает меню открытия уже существующего рисунка;\n" +
                "\"<-\" и \"->\" используются как redo и undo;\n\n" +
                "Рисунки сохраняются в формат .bbl, представляющий собой последовательность действий," +
                " совершённых пользователем при отрисовке изображения.\n\n" +
                "Также Вы можете добавлять собственные плагин-фигуры. Для этого поместите в папку с программой .dll C# класс, содержащий вашу фигуру.\n" +
                "Наследуйтесь от класса Figure, реализуйте метод Build для построения точек, описывающих вашу фигуру и создайте конструктор для вашей" +
                "фигуры от точек левого верхнего и правого нижнего краёв фигуры.\n\n" +
                "Плагины можно в любой момент выключить в создаваемом после первого запуска в папке с программой файле dlls.conf\n" +
                "Значения \"true\" или \"false\" после имени плагина и знака равно задаёт видимость Вашей фигуры.");
        }
    }
}