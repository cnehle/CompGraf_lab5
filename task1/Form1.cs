using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace lab5
{
    public partial class Form1 : Form
    {
        private List<PointF> points = new List<PointF>();
        private float currentX, currentY;
        private float currentAngle;
        private Stack<TurtleState> stateStack = new Stack<TurtleState>();
        private Random random = new Random();
        private float scaleFactor = 1.0f;
        private PointF centerOffset;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += Form1_Paint;
            this.Resize += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawKochCurve();
        }

        private void DrawKochCurve()
        {
            string lSystem = "F 60 0\nF -> F-F++F-F";
            ParseLSystem(lSystem, 4);
            CalculateScaleAndOffset();
            this.Invalidate();
        }

        private void DrawTree()
        {
            string lSystem = "X 25 90\nF -> FF\nX -> F[+X]F[-X]+X";
            ParseLSystem(lSystem, 6);
            CalculateScaleAndOffset();
            this.Invalidate();
        }

        private void ParseLSystem(string lSystem, int iterations)
        {
            points.Clear();
            stateStack.Clear();

            using (StringReader reader = new StringReader(lSystem))
            {
                string firstLine = reader.ReadLine();
                string[] parameters = firstLine.Split(' ');

                string axiom = parameters[0];
                float angle = float.Parse(parameters[1], CultureInfo.InvariantCulture);
                float startAngle = float.Parse(parameters[2], CultureInfo.InvariantCulture);

                Dictionary<char, string> rules = new Dictionary<char, string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
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

                InterpretLSystem(result, angle, 10, true);
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

        private void InterpretLSystem(string lSystem, float angle, float step, bool useRandomness)
        {
            foreach (char c in lSystem)
            {
                switch (c)
                {
                    case 'F':
                    case 'G':
                        float actualStep = useRandomness ? step * (0.8f + (float)random.NextDouble() * 0.4f) : step;
                        currentX += actualStep * (float)Math.Cos(currentAngle * Math.PI / 180);
                        currentY += actualStep * (float)Math.Sin(currentAngle * Math.PI / 180);
                        points.Add(new PointF(currentX, currentY));
                        break;

                    case 'f':
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
                }
            }
        }

        private void CalculateScaleAndOffset()
        {
            if (points.Count == 0) return;

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

            float scaleX = (this.ClientSize.Width - 40) / width;
            float scaleY = (this.ClientSize.Height - 40) / height;
            scaleFactor = Math.Min(scaleX, scaleY);

            centerOffset = new PointF(
                (this.ClientSize.Width - width * scaleFactor) / 2 - minX * scaleFactor,
                (this.ClientSize.Height - height * scaleFactor) / 2 - minY * scaleFactor
            );
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.White);

            if (points.Count < 2) return;

            using (Pen pen = new Pen(Color.Black, 1))
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            CalculateScaleAndOffset();
            this.Invalidate();
        }

        private void kochCurveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawKochCurve();
        }

        private void treeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawTree();
        }
    }

    public class TurtleState
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
    }
}