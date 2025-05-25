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

        int thickness = 5;

        public DrawSettingForm()
        {
            InitializeComponent();
        }

        private void DrawSettingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            (MainWindowParent.drawing = new Drawing()).setDrawMode(drColor, thickness);
            if (comboBox2.Text == "Кисть-курсор")
                MainWindowParent.drawing.cursor = true;
            else
            {
                MainWindowParent.drawing.Constructor = MainWindowParent.figureList.figuresList.FirstOrDefault(pair => pair.Key == comboBox2.Text).Value;
                MainWindowParent.drawing.cursor = false;
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

        }

        private void DrawSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            Int32.TryParse(comboBox1.Text, out thickness);
        }

        private void DrawSettingForm_Shown(object sender, EventArgs e)
        {
            comboBox2.Items.Add("Кисть-курсор");
            for (int i = 0; i < MainWindowParent.figureList.figuresList.Count; i++)
            {
                comboBox2.Items.Add(MainWindowParent.figureList.figuresList.Keys.ToArray()[i]);
            }
        }
    }
}
