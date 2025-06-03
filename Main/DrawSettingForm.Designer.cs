namespace lab
{
    partial class DrawSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            colorDialog1 = new ColorDialog();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            button9 = new Button();
            label4 = new Label();
            comboBox2 = new ComboBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Consolas", 14.25F);
            button1.Location = new Point(198, 24);
            button1.Name = "button1";
            button1.Size = new Size(140, 35);
            button1.TabIndex = 0;
            button1.Text = "Выбрать";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 14.25F);
            label1.Location = new Point(12, 30);
            label1.Name = "label1";
            label1.Size = new Size(110, 22);
            label1.TabIndex = 1;
            label1.Text = "Цвет линии";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 14.25F);
            label2.Location = new Point(12, 79);
            label2.Name = "label2";
            label2.Size = new Size(140, 22);
            label2.TabIndex = 2;
            label2.Text = "Толщина линии";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Consolas", 14.25F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2", "3", "5", "7", "10", "13", "15", "17", "20", "25", "30" });
            comboBox1.Location = new Point(198, 77);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(140, 30);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox1.TextChanged += comboBox1_TextChanged;
            // 
            // button9
            // 
            button9.Font = new Font("Consolas", 14.25F);
            button9.Location = new Point(198, 172);
            button9.Name = "button9";
            button9.Size = new Size(140, 45);
            button9.TabIndex = 4;
            button9.Text = "Закрыть";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 14.25F);
            label4.Location = new Point(12, 127);
            label4.Name = "label4";
            label4.Size = new Size(70, 22);
            label4.TabIndex = 13;
            label4.Text = "Фигура";
            // 
            // comboBox2
            // 
            comboBox2.BackColor = SystemColors.Window;
            comboBox2.Font = new Font("Consolas", 14.25F);
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(198, 124);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(140, 30);
            comboBox2.TabIndex = 3;
            // 
            // DrawSettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 230);
            Controls.Add(comboBox2);
            Controls.Add(label4);
            Controls.Add(button9);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "DrawSettingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки кисти";
            FormClosed += DrawSettingForm_FormClosed;
            Shown += DrawSettingForm_Shown;
            KeyDown += DrawSettingForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ColorDialog colorDialog1;
        private Button button1;
        private Label label1;
        private Label label2;
        private ComboBox comboBox1;
        private Button button9;
        private Label label4;
        private ComboBox comboBox2;
    }
}