using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab5_task2
{
    public partial class Form1 : Form
    {
        private class Edge
        {
            public PointF Left { get; set; }
            public PointF Right { get; set; }

            public Edge(PointF left, PointF right)
            {
                Left = left;
                Right = right;
            }
        }

        private Bitmap bmp;
        private Graphics graphics;
        private List<Edge> edges = new List<Edge>();
        private Random random = new Random();
        private double roughness;

        public Form1()
        {
            InitializeComponent();
            InitializeApplication();
        }

        private void InitializeApplication()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            graphics = Graphics.FromImage(bmp);

            initLLength.Maximum = pictureBox1.Height;
            initRLength.Maximum = pictureBox1.Height;
            initLLength.Value = pictureBox1.Height / 2;
            initRLength.Value = pictureBox1.Height / 2;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void NextStep_Click(object sender, EventArgs e)
        {
            if (edges.Count == 0)
            {
                InitializeFirstEdge();
            }
            else
            {
                GenerateNextStep();
            }
        }

        private void InitializeFirstEdge()
        {
            double leftLength = (double)initLLength.Value;
            double rightLength = (double)initRLength.Value;

            if (!double.TryParse(initRoughness.Text, out roughness))
            {
                roughness = 0.4;
            }

            initLLength.Enabled = false;
            initRLength.Enabled = false;
            initRoughness.Enabled = false;

            Edge firstEdge = new Edge(
                new PointF(0, (float)(bmp.Height - leftLength)),
                new PointF(bmp.Width, (float)(bmp.Height - rightLength))
            );

            edges.Add(firstEdge);
            DrawEdges();
        }

        private void GenerateNextStep()
        {
            List<Edge> scatteredEdges = new List<Edge>();

            foreach (Edge edge in edges)
            {
                double length = CalculateEdgeLength(edge);
                double newHeight = (edge.Left.Y + edge.Right.Y) / 2 +
                                 (random.NextDouble() - 0.5) * roughness * length;

                PointF middle = new PointF(
                    (edge.Left.X + edge.Right.X) / 2,
                    (float)newHeight
                );

                scatteredEdges.Add(new Edge(edge.Left, middle));
                scatteredEdges.Add(new Edge(middle, edge.Right));
            }

            edges = scatteredEdges;
            DrawEdges();
        }

        private double CalculateEdgeLength(Edge edge)
        {
            double deltaX = edge.Right.X - edge.Left.X;
            double deltaY = edge.Right.Y - edge.Left.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        private void DrawEdges()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bmp);

            foreach (Edge edge in edges)
            {
                DrawEdge(edge);
            }

            pictureBox1.Image = bmp;
            pictureBox1.Invalidate();
        }

        private void DrawEdge(Edge edge)
        {
            graphics.DrawLine(Pens.Black, edge.Left, edge.Right);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            ResetControls();
            edges.Clear();
            ClearCanvas();
        }

        private void ResetControls()
        {
            initLLength.Enabled = true;
            initRLength.Enabled = true;
            initRoughness.Enabled = true;
        }

        private void ClearCanvas()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bmp;
            roughness = 0;
        }

        private void PlusBtn_Click(object sender, EventArgs e)
        {
            AdjustRoughness(0.1);
        }

        private void MinusBtn_Click(object sender, EventArgs e)
        {
            AdjustRoughness(-0.1);
        }

        private void AdjustRoughness(double adjustment)
        {
            if (double.TryParse(initRoughness.Text, out double currentRoughness))
            {
                currentRoughness += adjustment;
                initRoughness.Text = Math.Max(0, currentRoughness).ToString("F1");
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}