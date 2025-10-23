namespace lab5
{
    partial class Form3
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
            this.cmbTreeType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLeafColor = new System.Windows.Forms.Button();
            this.btnTrunkColor = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.numThickness = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numAngle = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbTreeType);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnLeafColor);
            this.panel1.Controls.Add(this.btnTrunkColor);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.chkRandom);
            this.panel1.Controls.Add(this.numThickness);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.numLength);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.numAngle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnDraw);
            this.panel1.Controls.Add(this.numIterations);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1045, 72);
            this.panel1.TabIndex = 0;
            // 
            // cmbTreeType
            // 
            this.cmbTreeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTreeType.FormattingEnabled = true;
            this.cmbTreeType.Items.AddRange(new object[] {
            "Куст",
            "Дерево"});
            this.cmbTreeType.Location = new System.Drawing.Point(321, 40);
            this.cmbTreeType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTreeType.Name = "cmbTreeType";
            this.cmbTreeType.Size = new System.Drawing.Size(105, 24);
            this.cmbTreeType.TabIndex = 16;
            this.cmbTreeType.SelectedIndexChanged += new System.EventHandler(this.cmbTreeType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(278, 43);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Тип:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(594, 38);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(106, 31);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLeafColor
            // 
            this.btnLeafColor.BackColor = System.Drawing.Color.Green;
            this.btnLeafColor.Location = new System.Drawing.Point(853, 36);
            this.btnLeafColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLeafColor.Name = "btnLeafColor";
            this.btnLeafColor.Size = new System.Drawing.Size(80, 28);
            this.btnLeafColor.TabIndex = 13;
            this.btnLeafColor.UseVisualStyleBackColor = false;
            this.btnLeafColor.Click += new System.EventHandler(this.btnLeafColor_Click);
            // 
            // btnTrunkColor
            // 
            this.btnTrunkColor.BackColor = System.Drawing.Color.Brown;
            this.btnTrunkColor.Location = new System.Drawing.Point(853, 7);
            this.btnTrunkColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTrunkColor.Name = "btnTrunkColor";
            this.btnTrunkColor.Size = new System.Drawing.Size(80, 28);
            this.btnTrunkColor.TabIndex = 12;
            this.btnTrunkColor.UseVisualStyleBackColor = false;
            this.btnTrunkColor.Click += new System.EventHandler(this.btnTrunkColor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(747, 43);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Цвет листьев:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(747, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Цвет ствола:";
            // 
            // chkRandom
            // 
            this.chkRandom.AutoSize = true;
            this.chkRandom.Location = new System.Drawing.Point(16, 37);
            this.chkRandom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(148, 20);
            this.chkRandom.TabIndex = 9;
            this.chkRandom.Text = "Случайность угла";
            this.chkRandom.UseVisualStyleBackColor = true;
            this.chkRandom.CheckedChanged += new System.EventHandler(this.chkRandom_CheckedChanged);
            // 
            // numThickness
            // 
            this.numThickness.Location = new System.Drawing.Point(490, 10);
            this.numThickness.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numThickness.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numThickness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThickness.Name = "numThickness";
            this.numThickness.Size = new System.Drawing.Size(53, 22);
            this.numThickness.TabIndex = 8;
            this.numThickness.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(414, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Толщина:";
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(336, 10);
            this.numLength.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numLength.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(53, 22);
            this.numLength.TabIndex = 6;
            this.numLength.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(278, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Длина:";
            // 
            // numAngle
            // 
            this.numAngle.Location = new System.Drawing.Point(198, 10);
            this.numAngle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numAngle.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numAngle.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numAngle.Name = "numAngle";
            this.numAngle.Size = new System.Drawing.Size(53, 22);
            this.numAngle.TabIndex = 4;
            this.numAngle.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Угол:";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(594, 6);
            this.btnDraw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(106, 31);
            this.btnDraw.TabIndex = 2;
            this.btnDraw.Text = "Построить";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // numIterations
            // 
            this.numIterations.Location = new System.Drawing.Point(96, 10);
            this.numIterations.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numIterations.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(40, 22);
            this.numIterations.TabIndex = 1;
            this.numIterations.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Итерации:";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form3";
            this.Text = "Фрактальное дерево";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numAngle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numThickness;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkRandom;
        private System.Windows.Forms.Button btnLeafColor;
        private System.Windows.Forms.Button btnTrunkColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbTreeType;
        private System.Windows.Forms.Label label7;
    }
}