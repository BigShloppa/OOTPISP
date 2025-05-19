using Microsoft.VisualBasic.Devices;
using System.ComponentModel;

namespace lab2
{
    public partial class MainWindowForm : Form
    {
        static bool mouseEnteredPanel = false;
        static bool redoCalled = false;

        private List<Bitmap>? states;
        private int statesActiveIndex;
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
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
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
            else if(e.KeyCode == Keys.Right) 
            {
                statesActiveIndex++;
                if(statesActiveIndex < states.Count)
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = (Image)states[statesActiveIndex].Clone();
                    drawing.gr = Graphics.FromImage(pictureBox.Image);
                    pictureBox.Invalidate();
                }
                else
                    statesActiveIndex--;
            }
        }

        private void MainWindowForm_Click(object sender, EventArgs e)
        {

        }

        private void MainWindowForm_MouseDown(object sender, MouseEventArgs e)
        {

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
                drawing.setPoint(new Point(e.X, e.Y));
                drawing.Draw(pictureBox);
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            drawingActive = false;
            drawing.PreviousPoint = new Point(0,0);
            states.Add(new Bitmap(pictureBox.Image));
            statesActiveIndex++;
        }
    }
}