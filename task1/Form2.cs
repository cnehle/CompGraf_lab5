using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace lab5
{
    public partial class Form2 : Form
    {
        private List<PointF> points = new List<PointF>();
        private float currentX, currentY;
        private float currentAngle;
        private Stack<TurtleState> stateStack = new Stack<TurtleState>();
        private Random random = new Random();
        private float scaleFactor = 1.0f;
        private PointF centerOffset;
        private bool useRandomness = false;
        private string loadedLSystem = null;

        // Переменные для фиксированного масштабирования
        private float baseScaleFactor = 1.0f;
        private PointF baseCenterOffset;
        private Size baseClientSize = Size.Empty;

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form2_Paint;
            this.Resize += Form2_Resize;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cmbFractal.SelectedIndex = 0;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (loadedLSystem != null)
            {
                ParseLSystem(loadedLSystem, (int)numIterations.Value);
            }
            else
            {
                string lSystem = GetSelectedLSystem();
                ParseLSystem(lSystem, (int)numIterations.Value);
            }

            // Сохраняем базовые параметры масштабирования
            CalculateBaseScaleAndOffset();
            // Применяем текущее масштабирование
            ApplyCurrentScale();

            this.Invalidate();
        }

        private string GetSelectedLSystem()
        {
            switch (cmbFractal.SelectedIndex)
            {
                case 0: return "F 60 0\nF -> F-F++F-F";
                case 1: return "F+F+F+F 90 0\nF -> F+F-F-FF+F+F-F";
                case 2: return "FX 90 0\nF -> F\nX -> X+YF+\nY -> -FX-Y";
                case 3: return "F 22 90\nF -> FF-[F+F+F]+[+F-F-F]";
                case 4: return "F++F++F 60 0\nF -> F-F++F-F";
                default: return "F 60 0\nF -> F-F++F-F";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            points.Clear();
            loadedLSystem = null;
            lblLoadedFile.Text = "Файл не загружен";
            baseClientSize = Size.Empty;
            this.Invalidate();
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите файл L-системы";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileContent = File.ReadAllText(openFileDialog.FileName);
                    loadedLSystem = fileContent;
                    lblLoadedFile.Text = "Загружен: " + Path.GetFileName(openFileDialog.FileName);

                    ParseLSystem(loadedLSystem, (int)numIterations.Value);
                    CalculateBaseScaleAndOffset();
                    ApplyCurrentScale();
                    this.Invalidate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки файла: " + ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ParseLSystem(string lSystem, int iterations)
        {
            points.Clear();
            stateStack.Clear();

            using (StringReader reader = new StringReader(lSystem))
            {
                string firstLine = reader.ReadLine();
                if (firstLine == null) return;

                string[] parameters = firstLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (parameters.Length < 3)
                {
                    MessageBox.Show("Неверный формат файла. Первая строка должна содержать: <атом> <угол> <начальное направление>",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string axiom = parameters[0];
                float angle = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                float startAngle = float.Parse(parameters[2], CultureInfo.InvariantCulture);

                Dictionary<char, string> rules = new Dictionary<char, string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    if (line.Contains("->"))
                    {
                        string[] parts = line.Split(new[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 2)
                        {
                            char symbol = parts[0].Trim()[0];
                            rules[symbol] = parts[1].Trim();
                        }
                    }
                }

                string result = GenerateLSystem(axiom, rules, iterations);

                currentX = 0;
                currentY = 0;
                currentAngle = startAngle;
                points.Add(new PointF(currentX, currentY));

                InterpretLSystem(result, angle, 10);
            }
        }

        private string GenerateLSystem(string axiom, Dictionary<char, string> rules, int iterations)
        {
            string current = axiom;
            for (int i = 0; i < iterations; i++)
            {
                string next = "";
                foreach (char c in current)
                {
                    if (rules.ContainsKey(c))
                        next += rules[c];
                    else
                        next += c;
                }
                current = next;
            }
            return current;
        }

        private void InterpretLSystem(string lSystem, float angle, float step)
        {
            foreach (char c in lSystem)
            {
                switch (c)
                {
                    case 'F':
                    case 'G':
                    case 'A':
                    case 'B':
                        float actualStep = useRandomness ? step * (0.8f + (float)random.NextDouble() * 0.4f) : step;
                        currentX += actualStep * (float)Math.Cos(currentAngle * Math.PI / 180);
                        currentY += actualStep * (float)Math.Sin(currentAngle * Math.PI / 180);
                        points.Add(new PointF(currentX, currentY));
                        break;

                    case 'f':
                    case 'g':
                        currentX += step * (float)Math.Cos(currentAngle * Math.PI / 180);
                        currentY += step * (float)Math.Sin(currentAngle * Math.PI / 180);
                        break;

                    case '+':
                        float leftAngle = angle;
                        if (useRandomness)
                            leftAngle *= (0.8f + (float)random.NextDouble() * 0.4f);
                        currentAngle += leftAngle;
                        break;

                    case '-':
                        float rightAngle = angle;
                        if (useRandomness)
                            rightAngle *= (0.8f + (float)random.NextDouble() * 0.4f);
                        currentAngle -= rightAngle;
                        break;

                    case '[':
                        stateStack.Push(new TurtleState
                        {
                            X = currentX,
                            Y = currentY,
                            Angle = currentAngle
                        });
                        break;

                    case ']':
                        if (stateStack.Count > 0)
                        {
                            TurtleState state = stateStack.Pop();
                            currentX = state.X;
                            currentY = state.Y;
                            currentAngle = state.Angle;
                            points.Add(new PointF(float.NaN, float.NaN));
                            points.Add(new PointF(currentX, currentY));
                        }
                        break;

                    case '|':
                        currentAngle += 180;
                        break;
                }
            }
        }

        private void CalculateBaseScaleAndOffset()
        {
            if (points.Count == 0) return;

            // Находим реальные границы всех точек
            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;

            foreach (PointF point in points)
            {
                if (!float.IsNaN(point.X))
                {
                    minX = Math.Min(minX, point.X);
                    maxX = Math.Max(maxX, point.X);
                    minY = Math.Min(minY, point.Y);
                    maxY = Math.Max(maxY, point.Y);
                }
            }

            float width = maxX - minX;
            float height = maxY - minY;

            if (width <= 0) width = 1;
            if (height <= 0) height = 1;

            // Добавляем запас 20%
            float marginPercent = 0.2f;
            float marginX = width * marginPercent;
            float marginY = height * marginPercent;

            minX -= marginX;
            maxX += marginX;
            minY -= marginY;
            maxY += marginY;

            width = maxX - minX;
            height = maxY - minY;

            // Вычисляем базовый масштаб для размера окна при построении
            baseClientSize = this.ClientSize;
            float availableWidth = baseClientSize.Width * 0.9f;
            float availableHeight = baseClientSize.Height * 0.9f;

            float scaleX = availableWidth / width;
            float scaleY = availableHeight / height;

            baseScaleFactor = Math.Min(scaleX, scaleY);
            baseScaleFactor = Math.Max(baseScaleFactor, 0.01f);
            baseScaleFactor = Math.Min(baseScaleFactor, 100f);

            // Вычисляем базовое смещение
            baseCenterOffset = new PointF(
                (baseClientSize.Width - width * baseScaleFactor) / 2 - minX * baseScaleFactor,
                (baseClientSize.Height - height * baseScaleFactor) / 2 - minY * baseScaleFactor
            );
        }

        private void ApplyCurrentScale()
        {
            if (points.Count == 0 || baseClientSize == Size.Empty) return;

            // Используем базовый масштаб, но пересчитываем смещение для текущего размера окна
            scaleFactor = baseScaleFactor;

            // Находим границы (аналогично CalculateBaseScaleAndOffset, но без пересчета масштаба)
            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;

            foreach (PointF point in points)
            {
                if (!float.IsNaN(point.X))
                {
                    minX = Math.Min(minX, point.X);
                    maxX = Math.Max(maxX, point.X);
                    minY = Math.Min(minY, point.Y);
                    maxY = Math.Max(maxY, point.Y);
                }
            }

            float width = maxX - minX;
            float height = maxY - minY;

            if (width <= 0) width = 1;
            if (height <= 0) height = 1;

            // Добавляем такой же запас
            float marginPercent = 0.2f;
            float marginX = width * marginPercent;
            float marginY = height * marginPercent;

            minX -= marginX;
            maxX += marginX;
            minY -= marginY;
            maxY += marginY;

            width = maxX - minX;
            height = maxY - minY;

            // Центрируем в текущем окне с тем же масштабом
            centerOffset = new PointF(
                (this.ClientSize.Width - width * scaleFactor) / 2 - minX * scaleFactor,
                (this.ClientSize.Height - height * scaleFactor) / 2 - minY * scaleFactor
            );
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.White);

            if (points.Count < 2) return;

            using (Pen pen = new Pen(Color.Blue, 1))
            {
                for (int i = 1; i < points.Count; i++)
                {
                    PointF p1 = points[i - 1];
                    PointF p2 = points[i];

                    if (!float.IsNaN(p1.X) && !float.IsNaN(p2.X))
                    {
                        PointF scaledP1 = new PointF(
                            p1.X * scaleFactor + centerOffset.X,
                            p1.Y * scaleFactor + centerOffset.Y
                        );
                        PointF scaledP2 = new PointF(
                            p2.X * scaleFactor + centerOffset.X,
                            p2.Y * scaleFactor + centerOffset.Y
                        );

                        e.Graphics.DrawLine(pen, scaledP1, scaledP2);
                    }
                }
            }
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (points.Count > 0)
            {
                // При изменении размера окна применяем текущий масштаб с новым смещением
                ApplyCurrentScale();
                this.Invalidate();
            }
        }

        private void chkRandom_CheckedChanged(object sender, EventArgs e)
        {
            useRandomness = chkRandom.Checked;
        }

        private void cmbFractal_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadedLSystem = null;
            lblLoadedFile.Text = "Файл не загружен";
        }
    }

    public class TurtleState
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
    }
}