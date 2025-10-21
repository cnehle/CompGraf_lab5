using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace lab5
{
    public partial class Form3 : Form
    {
        private List<TreeSegment> segments = new List<TreeSegment>();
        private Random random = new Random();
        private float scaleFactor = 1.0f;
        private PointF centerOffset;

        public Form3()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form3_Paint;
            this.Resize += Form3_Resize;

            // Добавляем обработчики для числовых полей
            numIterations.ValueChanged += NumericValueChanged;
            numAngle.ValueChanged += NumericValueChanged;
            numLength.ValueChanged += NumericValueChanged;
            numThickness.ValueChanged += NumericValueChanged;
        }

        private void NumericValueChanged(object sender, EventArgs e)
        {
            // Автоматически перерисовываем дерево при изменении числовых значений
            if (segments.Count > 0)
            {
                GenerateTree();
                CalculateScaleAndOffset();
                this.Invalidate();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // Устанавливаем значения по умолчанию
            numIterations.Value = 5;
            numAngle.Value = 25;
            numLength.Value = 15;
            numThickness.Value = 5;
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            GenerateTree();
            CalculateScaleAndOffset();
            this.Invalidate();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            segments.Clear();
            this.Invalidate();
        }

        private void GenerateTree()
        {
            segments.Clear();

            int iterations = (int)numIterations.Value;
            float angle = (float)numAngle.Value;
            float length = (float)numLength.Value;
            float thickness = (float)numThickness.Value;
            bool useRandomness = chkRandom.Checked;

            Color trunkColor = btnTrunkColor.BackColor;
            Color leafColor = btnLeafColor.BackColor;

            string axiom = "X";
            Dictionary<char, string> rules = new Dictionary<char, string>
            {
                { 'F', "FF" },
                { 'X', "F[+X]F[-X]+X" }
            };

            string lSystem = GenerateLSystem(axiom, rules, iterations);
            InterpretTree(lSystem, angle, length, thickness, trunkColor, leafColor, useRandomness);
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

        private void InterpretTree(string lSystem, float angle, float step, float thickness,
                                 Color trunkColor, Color leafColor, bool useRandomness)
        {
            Stack<TreeState> stateStack = new Stack<TreeState>();
            TreeState currentState = new TreeState
            {
                X = 0,
                Y = 0,
                Angle = -90,
                Thickness = thickness,
                Depth = 0
            };

            // Сначала собираем все сегменты временно, чтобы вычислить максимальную глубину
            List<TempSegment> tempSegments = new List<TempSegment>();
            int maxDepth = 0;

            foreach (char c in lSystem)
            {
                switch (c)
                {
                    case 'F':
                    case 'X':
                        float actualStep = useRandomness ?
                            step * (0.7f + (float)random.NextDouble() * 0.6f) : step;

                        float actualAngle = useRandomness ?
                            currentState.Angle + (float)(random.NextDouble() - 0.5) * 10 : currentState.Angle;

                        float endX = currentState.X + actualStep * (float)Math.Cos(actualAngle * Math.PI / 180);
                        float endY = currentState.Y + actualStep * (float)Math.Sin(actualAngle * Math.PI / 180);

                        // Сохраняем сегмент с информацией о глубине
                        tempSegments.Add(new TempSegment
                        {
                            StartX = currentState.X,
                            StartY = currentState.Y,
                            EndX = endX,
                            EndY = endY,
                            Thickness = currentState.Thickness,
                            Depth = currentState.Depth
                        });

                        // Обновляем максимальную глубину
                        if (currentState.Depth > maxDepth)
                            maxDepth = currentState.Depth;

                        currentState.X = endX;
                        currentState.Y = endY;
                        currentState.Angle = actualAngle;
                        break;

                    case '+':
                        float leftAngle = useRandomness ?
                            angle * (0.8f + (float)random.NextDouble() * 0.4f) : angle;
                        currentState.Angle += leftAngle;
                        break;

                    case '-':
                        float rightAngle = useRandomness ?
                            angle * (0.8f + (float)random.NextDouble() * 0.4f) : angle;
                        currentState.Angle -= rightAngle;
                        break;

                    case '[':
                        stateStack.Push(new TreeState
                        {
                            X = currentState.X,
                            Y = currentState.Y,
                            Angle = currentState.Angle,
                            Thickness = currentState.Thickness * 0.7f,
                            Depth = currentState.Depth + 1
                        });
                        break;

                    case ']':
                        if (stateStack.Count > 0)
                        {
                            currentState = stateStack.Pop();
                        }
                        break;
                }
            }

            // Теперь создаем финальные сегменты с правильными цветами
            foreach (var temp in tempSegments)
            {
                float colorRatio = maxDepth > 0 ? (float)temp.Depth / maxDepth : 0;
                Color segmentColor = InterpolateColor(trunkColor, leafColor, colorRatio);

                segments.Add(new TreeSegment
                {
                    StartX = temp.StartX,
                    StartY = temp.StartY,
                    EndX = temp.EndX,
                    EndY = temp.EndY,
                    Thickness = temp.Thickness,
                    Color = segmentColor
                });
            }
        }

        private Color InterpolateColor(Color color1, Color color2, float ratio)
        {
            ratio = Math.Max(0, Math.Min(1, ratio));

            int r = (int)(color1.R + (color2.R - color1.R) * ratio);
            int g = (int)(color1.G + (color2.G - color1.G) * ratio);
            int b = (int)(color1.B + (color2.B - color1.B) * ratio);

            r = Math.Max(0, Math.Min(255, r));
            g = Math.Max(0, Math.Min(255, g));
            b = Math.Max(0, Math.Min(255, b));

            return Color.FromArgb(r, g, b);
        }

        private void CalculateScaleAndOffset()
        {
            if (segments.Count == 0) return;

            float minX = float.MaxValue, maxX = float.MinValue;
            float minY = float.MaxValue, maxY = float.MinValue;

            foreach (TreeSegment segment in segments)
            {
                minX = Math.Min(minX, Math.Min(segment.StartX, segment.EndX));
                maxX = Math.Max(maxX, Math.Max(segment.StartX, segment.EndX));
                minY = Math.Min(minY, Math.Min(segment.StartY, segment.EndY));
                maxY = Math.Max(maxY, Math.Max(segment.StartY, segment.EndY));
            }

            float width = maxX - minX;
            float height = maxY - minY;

            if (width <= 0 || height <= 0)
            {
                width = 100;
                height = 100;
                minX = -50;
                minY = -50;
            }

            // Увеличиваем запас для больших деревьев и случайных углов
            float padding = 40f;

            // Для больших итераций увеличиваем запас
            int iterations = (int)numIterations.Value;
            if (iterations > 6)
            {
                padding = 60f;
            }

            // Если включена случайность, еще увеличиваем запас
            if (chkRandom.Checked)
            {
                padding += 20f;
            }

            // Вычисляем масштаб с учетом отступов
            float scaleX = (this.ClientSize.Width - 2 * padding) / width;
            float scaleY = (this.ClientSize.Height - 2 * padding) / height;

            // Берем минимальный масштаб, чтобы гарантировать помещение в окно
            scaleFactor = Math.Min(scaleX, scaleY);

            // Для больших деревьев и случайных углов увеличиваем запас масштабирования
            float scaleMargin = 0.9f; // 10% запас по умолчанию
            if (iterations > 6 || chkRandom.Checked)
            {
                scaleMargin = 0.8f; // 20% запас для сложных случаев
            }

            scaleFactor *= scaleMargin;

            // Ограничиваем минимальный и максимальный масштаб
            scaleFactor = Math.Max(scaleFactor, 0.05f); // Уменьшил минимальный масштаб
            scaleFactor = Math.Min(scaleFactor, 10f);   // Увеличил максимальный масштаб

            // Вычисляем смещение для центрирования
            centerOffset = new PointF(
                (this.ClientSize.Width - width * scaleFactor) / 2 - minX * scaleFactor,
                (this.ClientSize.Height - height * scaleFactor) / 2 - minY * scaleFactor
            );
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.White);

            foreach (TreeSegment segment in segments)
            {
                PointF start = new PointF(
                    segment.StartX * scaleFactor + centerOffset.X,
                    segment.StartY * scaleFactor + centerOffset.Y
                );
                PointF end = new PointF(
                    segment.EndX * scaleFactor + centerOffset.X,
                    segment.EndY * scaleFactor + centerOffset.Y
                );

                using (Pen pen = new Pen(segment.Color, Math.Max(segment.Thickness * scaleFactor, 0.3f)))
                {
                    e.Graphics.DrawLine(pen, start, end);
                }
            }
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            if (segments.Count > 0)
            {
                CalculateScaleAndOffset();
                this.Invalidate();
            }
        }

        private void btnTrunkColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = btnTrunkColor.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnTrunkColor.BackColor = dialog.Color;
                if (segments.Count > 0)
                {
                    GenerateTree();
                    this.Invalidate();
                }
            }
        }

        private void btnLeafColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = btnLeafColor.BackColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                btnLeafColor.BackColor = dialog.Color;
                if (segments.Count > 0)
                {
                    GenerateTree();
                    this.Invalidate();
                }
            }
        }

        private void chkRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (segments.Count > 0)
            {
                GenerateTree();
                this.Invalidate();
            }
        }
    }

    public class TreeState
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
        public float Thickness { get; set; }
        public int Depth { get; set; }
    }

    public class TreeSegment
    {
        public float StartX { get; set; }
        public float StartY { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }
        public float Thickness { get; set; }
        public Color Color { get; set; }
    }

    public class TempSegment
    {
        public float StartX { get; set; }
        public float StartY { get; set; }
        public float EndX { get; set; }
        public float EndY { get; set; }
        public float Thickness { get; set; }
        public int Depth { get; set; }
    }
}