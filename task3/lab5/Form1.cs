using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace lab5
{
    public partial class Form1 : Form
    {
        private List<PointF> points;
        private Bitmap bmp;

        private PointF additionalPoint;
        private int index_of_moving_point; // индекс точки, которую будем передвигать

        public Form1()
        {
            InitializeComponent();
            points = new List<PointF>();
            radioButton1.Checked = true; // режим добавления по умолчанию
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            index_of_moving_point = -1;
            checkBox2.Checked = true; // показывать полилинию по умолчанию
            additionalPoint = new PointF();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Очистка 
        private void button2_Click(object sender, EventArgs e)
        {
            points.Clear();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            radioButton1.Checked = true;
            checkBox2.Checked = true;
            index_of_moving_point = -1;
            additionalPoint = new PointF();
            DrawElements();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (radioButton1.Checked) // добавление точки
            {
                points.Add(e.Location);
            }
            else if (radioButton2.Checked) // удаление точки
            {
                DeletePoint(e.Location.X, e.Location.Y);
            }
            DrawElements();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton3.Checked)
            {
                index_of_moving_point = points.FindIndex(el => (el.X > e.X - 6) && (el.X < e.X + 6) && (el.Y > e.Y - 6) && (el.Y < e.Y + 6));
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (radioButton3.Checked)
            {
                if (index_of_moving_point >= 0)
                {
                    points[index_of_moving_point] = new PointF(e.X, e.Y);
                    // удаляем возможную временную точку
                    DeletePoint(additionalPoint.X, additionalPoint.Y);
                    DrawElements();
                    index_of_moving_point = -1;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // во время перетаскивания обновляем положение точки "вживую"
            if (radioButton3.Checked && index_of_moving_point >= 0)
            {
                points[index_of_moving_point] = new PointF(e.X, e.Y);
                DrawElements();
            }
            else
            {
                // можно подсвечивать точку при наведении
                int hover = points.FindIndex(el => (el.X > e.X - 6) && (el.X < e.X + 6) && (el.Y > e.Y - 6) && (el.Y < e.Y + 6));
                if (hover >= 0)
                {
                    pictureBox1.Cursor = Cursors.Hand;
                }
                else
                {
                    pictureBox1.Cursor = Cursors.Cross;
                }
            }
        }

        // Удаление точки (по близости)
        private void DeletePoint(float x, float y)
        {
            int index_for_delete = points.FindIndex(el => (el.X > x - 6) && (el.X < x + 6) && (el.Y > y - 6) && (el.Y < y + 6));
            if (index_for_delete >= 0)
            {
                points.RemoveAt(index_for_delete);
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = bmp;
                DrawElements();
            }
        }

        // Отрисовка всех элементов — фон, сетка, полилиния опорных точек, сами точки, и кривые Безье
        private void DrawElements()
        {
            if (bmp == null)
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // 1) Фон — вертикальный градиент
                using (LinearGradientBrush br = new LinearGradientBrush(pictureBox1.ClientRectangle, Color.FromArgb(30, 40, 60), Color.FromArgb(10, 15, 25), 90f))
                {
                    g.FillRectangle(br, pictureBox1.ClientRectangle);
                }

                // 2) Сетка
                int grid = 25;
                using (Pen gridPen = new Pen(Color.FromArgb(30, 255, 255, 255)))
                {
                    for (int x = 0; x < pictureBox1.Width; x += grid)
                        g.DrawLine(gridPen, x, 0, x, pictureBox1.Height);
                    for (int y = 0; y < pictureBox1.Height; y += grid)
                        g.DrawLine(gridPen, 0, y, pictureBox1.Width, y);
                }

                // 3) Полилиния между опорными точками (пунктиром) — если опция включена
                if (checkBox2.Checked && points.Count > 1)
                {
                    using (Pen polyPen = new Pen(Color.FromArgb(180, 200, 200, 255), 1.5f))
                    {
                        polyPen.DashStyle = DashStyle.Dash;
                        g.DrawLines(polyPen, points.ToArray());
                    }
                }

                // 4) Кривые Безье (кусочно из каждых 4 последовательных точек)
                if (points.Count >= 4)
                {
                    // рисуем насыщенную цветную кривую
                    // пройдемся по последовательным блокам из 4 точек (0..3,1..4,2..5,...)
                    for (int start = 0; start <= points.Count - 4; start += 1)
                    {
                        List<PointF> bezierPts = new List<PointF>();
                        // нарисуем сглаженную кривую путем вычисления точек по формуле кубического Безье
                        for (float t = 0; t <= 1.0001f; t += 0.01f)
                        {
                            PointF p = EvalCubicBezier(points[start], points[start + 1], points[start + 2], points[start + 3], t);
                            bezierPts.Add(p);
                        }

                        // градиентный пер для кривой (изменение цвета по длине)
                        using (Pen bezPen = new Pen(Color.FromArgb(220, 255, 140, 80), 3.5f))
                        {
                            bezPen.LineJoin = LineJoin.Round;
                            if (bezierPts.Count > 1)
                                g.DrawLines(bezPen, bezierPts.ToArray());
                        }

                        // небольшая "сияющая" обводка
                        using (Pen glow = new Pen(Color.FromArgb(60, 255, 200, 120), 8f))
                        {
                            glow.LineJoin = LineJoin.Round;
                            if (bezierPts.Count > 1)
                                g.DrawLines(glow, bezierPts.ToArray());
                        }
                    }
                }

                // 5) Рисуем опорные точки — крупные закрашенные маркеры с номером
                for (int i = 0; i < points.Count; i++)
                {
                    PointF p = points[i];
                    float size = (float)numericUpDown1.Value; // размер точки можно регулировать
                    RectangleF rect = new RectangleF(p.X - size / 2f, p.Y - size / 2f, size, size);

                    // тень/свечение
                    using (SolidBrush shadow = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                    {
                        g.FillEllipse(shadow, rect.X + 2, rect.Y + 2, rect.Width, rect.Height);
                    }

                    // основной заполненный круг
                    using (LinearGradientBrush fill = new LinearGradientBrush(rect, Color.FromArgb(255, 255, 190, 100), Color.FromArgb(255, 200, 90, 30), 45f))
                    {
                        g.FillEllipse(fill, rect);
                    }

                    // обводка
                    using (Pen border = new Pen(Color.FromArgb(220, 60, 30, 0), 2f))
                    {
                        g.DrawEllipse(border, rect.X, rect.Y, rect.Width, rect.Height);
                    }

                    // номер точки
                    using (SolidBrush txtBrush = new SolidBrush(Color.White))
                    using (Font f = new Font("Segoe UI", 8f, FontStyle.Bold))
                    {
                        string idx = i.ToString();
                        SizeF ts = g.MeasureString(idx, f);
                        g.DrawString(idx, f, txtBrush, p.X - ts.Width / 2f, p.Y - ts.Height / 2f - size / 2f - 2);
                    }
                }

                // 6) при режиме перемещения подсвечиваем выбранную точку (если есть)
                if (index_of_moving_point >= 0 && index_of_moving_point < points.Count)
                {
                    PointF p = points[index_of_moving_point];
                    using (Pen sel = new Pen(Color.Lime, 2f))
                    {
                        float s = (float)numericUpDown1.Value + 6;
                        g.DrawEllipse(sel, p.X - s / 2f, p.Y - s / 2f, s, s);
                    }
                }
            }

            pictureBox1.Invalidate();
        }

        private PointF EvalCubicBezier(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            // стандартная формула кубического Безье
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            float x = uuu * p0.X;
            x += 3 * uu * t * p1.X;
            x += 3 * u * tt * p2.X;
            x += ttt * p3.X;

            float y = uuu * p0.Y;
            y += 3 * uu * t * p1.Y;
            y += 3 * u * tt * p2.Y;
            y += ttt * p3.Y;

            return new PointF(x, y);
        }

        // при изменении размеров контрольного поля пересоздаем bitmap
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Width > 0 && pictureBox1.Height > 0)
            {
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = bmp;
                DrawElements();
            }
        }

        // изменение значения размера маркера
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DrawElements();
        }
    }
}