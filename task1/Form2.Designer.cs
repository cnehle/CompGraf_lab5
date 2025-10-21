namespace lab5
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblLoadedFile = new System.Windows.Forms.Label();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.cmbFractal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblLoadedFile);
            this.panel1.Controls.Add(this.btnLoadFromFile);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.chkRandom);
            this.panel1.Controls.Add(this.numIterations);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnDraw);
            this.panel1.Controls.Add(this.cmbFractal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 80);
            this.panel1.TabIndex = 0;
            // 
            // lblLoadedFile
            // 
            this.lblLoadedFile.AutoSize = true;
            this.lblLoadedFile.ForeColor = System.Drawing.Color.Blue;
            this.lblLoadedFile.Location = new System.Drawing.Point(10, 55);
            this.lblLoadedFile.Name = "lblLoadedFile";
            this.lblLoadedFile.Size = new System.Drawing.Size(94, 13);
            this.lblLoadedFile.TabIndex = 9;
            this.lblLoadedFile.Text = "Файл не загружен";
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(460, 30);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(120, 25);
            this.btnLoadFromFile.TabIndex = 8;
            this.btnLoadFromFile.Text = "Загрузить из файла";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(590, 30);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 25);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkRandom
            // 
            this.chkRandom.AutoSize = true;
            this.chkRandom.Location = new System.Drawing.Point(320, 35);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(134, 17);
            this.chkRandom.TabIndex = 6;
            this.chkRandom.Text = "Случайность (угол/шаг)";
            this.chkRandom.UseVisualStyleBackColor = true;
            this.chkRandom.CheckedChanged += new System.EventHandler(this.chkRandom_CheckedChanged);
            // 
            // numIterations
            // 
            this.numIterations.Location = new System.Drawing.Point(320, 10);
            this.numIterations.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(50, 20);
            this.numIterations.TabIndex = 5;
            this.numIterations.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Итерации:";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(680, 30);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(90, 25);
            this.btnDraw.TabIndex = 3;
            this.btnDraw.Text = "Построить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // cmbFractal
            // 
            this.cmbFractal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFractal.FormattingEnabled = true;
            this.cmbFractal.Items.AddRange(new object[] {
            "Кривая Коха",
            "Квадратный остров Коха",
            "Кривая дракона",
            "Куст 1",
            "Снежинка Коха"});
            this.cmbFractal.Location = new System.Drawing.Point(100, 10);
            this.cmbFractal.Name = "cmbFractal";
            this.cmbFractal.Size = new System.Drawing.Size(130, 21);
            this.cmbFractal.TabIndex = 1;
            this.cmbFractal.SelectedIndexChanged += new System.EventHandler(this.cmbFractal_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Тип фрактала:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
            this.Text = "L-системы (базовые)";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.ComboBox cmbFractal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkRandom;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoadFromFile;
        private System.Windows.Forms.Label lblLoadedFile;
    }
}