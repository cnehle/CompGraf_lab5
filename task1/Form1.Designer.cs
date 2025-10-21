namespace lab5
{
    partial class Form1
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
            this.btnTask1 = new System.Windows.Forms.Button();
            this.btnTask2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTask1
            // 
            this.btnTask1.Location = new System.Drawing.Point(50, 50);
            this.btnTask1.Name = "btnTask1";
            this.btnTask1.Size = new System.Drawing.Size(200, 40);
            this.btnTask1.TabIndex = 0;
            this.btnTask1.Text = "1. L-системы (базовые)";
            this.btnTask1.UseVisualStyleBackColor = true;
            this.btnTask1.Click += new System.EventHandler(this.btnTask1_Click);
            // 
            // btnTask2
            // 
            this.btnTask2.Location = new System.Drawing.Point(50, 110);
            this.btnTask2.Name = "btnTask2";
            this.btnTask2.Size = new System.Drawing.Size(200, 40);
            this.btnTask2.TabIndex = 1;
            this.btnTask2.Text = "2. Фрактальное дерево";
            this.btnTask2.UseVisualStyleBackColor = true;
            this.btnTask2.Click += new System.EventHandler(this.btnTask2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Фракталы - L-системы";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 180);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTask2);
            this.Controls.Add(this.btnTask1);
            this.Name = "Form1";
            this.Text = "Главное меню";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTask1;
        private System.Windows.Forms.Button btnTask2;
        private System.Windows.Forms.Label label1;
    }
}