using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class DrawSettingForm : Form
    {
        Color drColor = Color.Black;
        Color flColor = Color.White;

        internal MainWindowForm MainWindowParent;
        enum PressedButton
        {
            cursor, rhombus, rectangle, square, parallelogram, circle
        }
        int thickness = 5;
        PressedButton button = PressedButton.cursor;

        public DrawSettingForm()
        {
            InitializeComponent();
        }

        private void DrawSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (button == PressedButton.cursor)
            {
                (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, thickness);
            }
            else
            {
                switch (button)
                {
                    case PressedButton.parallelogram:
                        (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, flColor, thickness, new Parallelogram(0, 0, 0));
                        break;
                    case PressedButton.square:
                        (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, flColor, thickness, new Squire(0));
                        break;
                    case PressedButton.rectangle:
                        (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, flColor, thickness, new Rectangle(0, 0));
                        break;
                    case PressedButton.rhombus:
                        (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, flColor, thickness, new Rhombus(0, 0));
                        break;
                    case PressedButton.circle:
                        (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, flColor, thickness, new Circle(0));
                        break;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            drColor = colorDialog1.Color;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            flColor = colorDialog1.Color;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            thickness = Int32.Parse(comboBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button = PressedButton.cursor;
        }

        private void DrawSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
