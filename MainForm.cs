using Microsoft.VisualBasic.Devices;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Reflection;

namespace lab2
{
    public partial class MainWindowForm : Form
    {
        static bool mouseEnteredPanel = false;
        static bool redoCalled = false;
        static bool figureDrawing = false;

        internal Serializator serializator = new Serializator();
        public List<Bitmap>? states;
        private int statesActiveIndex;
        internal FigureList figureList = new FigureList();

        internal KeyValuePair<String, Drawing.figureConstructor>[] staticPairs =

        {
            new KeyValuePair<String, Drawing.figureConstructor>("Квадрат", (x, y) => new Squire(x, y)),
            new KeyValuePair<String, Drawing.figureConstructor>("Параллелограмм", (x, y) => new Parallelogram(x, y)),
            new KeyValuePair<String, Drawing.figureConstructor>("Прямоугольник", (x, y) => new Rectangle(x, y)),
            new KeyValuePair<String, Drawing.figureConstructor>("Окружность", (x, y) => new Circle(x, y)),
            new KeyValuePair<String, Drawing.figureConstructor>("Ромб", (x, y) => new Rhombus(x, y))
        };

        private struct Part
        {
            bool cursor;

            Color? drColor;

            Color? flColor;

            int thickness;

            public Point point;

            public Figure? figure;
        }

        Part part;

        internal Drawing drawing = new Drawing();

        bool drawingActive = false;
        public MainWindowForm()
        {
            InitializeComponent();
            pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            drawing.gr = Graphics.FromImage(pictureBox.Image);
            states = new List<Bitmap>();
            states.Add((Bitmap)pictureBox.Image.Clone());
            statesActiveIndex = 0;
            figureList.figuresList = new Dictionary<string, Drawing.figureConstructor>(staticPairs);
            figureList.ReadDLLs();
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
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Открыть";
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

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            drawingActive = true;
            if (redoCalled)
            {
                states.RemoveRange(statesActiveIndex + 1, states.Count - statesActiveIndex - 1);
                redoCalled = false;
            }
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawingActive)
            {


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
            figureDrawing = false;
            drawingActive = false;
            drawing.PreviousPoint = new Point(0, 0);
            states.Add(new Bitmap(pictureBox.Image));
            statesActiveIndex++;
            while (!drawing.cursor && statesActiveIndex < states.Count - 1)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = (Image)states[statesActiveIndex].Clone();
                drawing.gr = Graphics.FromImage(pictureBox.Image);
                pictureBox.Invalidate();
                statesActiveIndex++;
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