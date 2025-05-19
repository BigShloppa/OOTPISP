namespace lab2
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
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            label3 = new Label();
            button8 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label1.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 24);
            label1.Name = "label1";
            label1.Size = new Size(110, 22);
            label1.TabIndex = 1;
            label1.Text = "Цвет линии";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 108);
            label2.Name = "label2";
            label2.Size = new Size(140, 22);
            label2.TabIndex = 2;
            label2.Text = "Толщина линии";
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2", "3", "5", "7", "10", "13", "15", "17", "20", "25", "30" });
            comboBox1.Location = new Point(198, 106);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(140, 30);
            comboBox1.TabIndex = 3;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button2
            // 
            button2.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(16, 171);
            button2.Name = "button2";
            button2.Size = new Size(50, 45);
            button2.TabIndex = 4;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(72, 171);
            button3.Name = "button3";
            button3.Size = new Size(50, 45);
            button3.TabIndex = 5;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(16, 222);
            button4.Name = "button4";
            button4.Size = new Size(50, 45);
            button4.TabIndex = 6;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(128, 171);
            button5.Name = "button5";
            button5.Size = new Size(50, 45);
            button5.TabIndex = 7;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button6.Location = new Point(72, 222);
            button6.Name = "button6";
            button6.Size = new Size(50, 45);
            button6.TabIndex = 8;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button7.Location = new Point(128, 222);
            button7.Name = "button7";
            button7.Size = new Size(50, 45);
            button7.TabIndex = 9;
            button7.Text = "button7";
            button7.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 65);
            label3.Name = "label3";
            label3.Size = new Size(130, 22);
            label3.TabIndex = 11;
            label3.Text = "Цвет заливки";
            // 
            // button8
            // 
            button8.Font = new Font("Consolas", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            button8.Location = new Point(198, 65);
            button8.Name = "button8";
            button8.Size = new Size(140, 35);
            button8.TabIndex = 10;
            button8.Text = "Выбрать";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // DrawSettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 282);
            Controls.Add(label3);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(comboBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "DrawSettingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form2";
            FormClosed += DrawSettingForm_FormClosed;
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
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Label label3;
        private Button button8;
    }
}