using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HillClimber2
{
    public partial class Form1 : Form
    {
        List<(TextBox xBox, TextBox yBox)> Coordinates;
        List<Point> Points;

        Bitmap canvas;
        Graphics g;
        Random random = new Random();

        public double m = 1;
        public double b = 1;

        Perceptron.Perceptron perceptron;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Coordinates = new List<(TextBox xBox, TextBox yBox)>();
            Points = new List<Point>();

            perceptron = new Perceptron.Perceptron(Points.Count, 0, 1000, 1);

            canvas = new Bitmap(pictureBox1.Size.Width, pictureBox1.Height);
            g = Graphics.FromImage(canvas);
            drawStuff();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int spacing = 25;
            Size suze = new Size(60, 15);

            var LocX = new Point(0, 0);
            var LocY = new Point(0, 0);

            //Drawing the boxes:
            if (Coordinates.Count > 0)
            {
                LocX = new Point(Coordinates[Coordinates.Count - 1].xBox.Location.X, Coordinates[Coordinates.Count - 1].xBox.Location.Y + spacing);
                LocY = new Point(Coordinates[Coordinates.Count - 1].yBox.Location.X, Coordinates[Coordinates.Count - 1].yBox.Location.Y + spacing);
            }
            else
            {
                LocX = new Point(10, 30);
                LocY = new Point(90, 30);
            }

            Point clickLocation = ConvertCoords(e.Location);
            var xBox = new TextBox()
            {
                Location = LocX,
                Text = clickLocation.X.ToString(),
                Size = suze,
            };
            var yBox = new TextBox()
            {
                Location = LocY,
                Text = clickLocation.Y.ToString(),
                Size = suze,
            };

            Coordinates.Add((xBox, yBox));
            Points.Add(clickLocation);

            xBox.TextChanged += XBox_TextChanged;
            yBox.TextChanged += YBox_TextChanged;

            xBox.Tag = Points.Count - 1;
            yBox.Tag = Points.Count - 1;

            this.Controls.Add(xBox);
            this.Controls.Add(yBox);

            drawStuff();
        }

        private void XBox_TextChanged(object s, EventArgs e)
        {
            TextBox sender = (TextBox)s;
            int tag = int.Parse(sender.Tag.ToString());
            if (sender.Text != "")
            {
                Points[tag] = new Point(int.Parse(sender.Text), Points[tag].Y);
                drawStuff();
            }
        }

        private void YBox_TextChanged(object s, EventArgs e)
        {
            TextBox sender = (TextBox)s;
            int tag = int.Parse(sender.Tag.ToString());
            if (sender.Text != "")
            {
                Points[tag] = new Point(Points[tag].X, int.Parse(sender.Text));
                drawStuff();
            }
        }
        public Point ConvertCoords(Point coordinates)
        {
            return new Point(coordinates.X, pictureBox1.Height - coordinates.Y);
        }


        public void drawStuff()
        {
            g.Clear(Color.White);
            drawGridlines();
            drawPoints();
            drawLine();
            drawProperLine();
            pictureBox1.Image = canvas;
        }

        public void drawPoints()
        {
            int radius = 5;

            for (int i = 0; i < Points.Count; i++)
            {
                g.FillEllipse(Brushes.Chocolate, new Rectangle(Point.Subtract(ConvertCoords(Points[i]), new Size(radius, radius)), new Size(radius * 2, radius * 2)));
            }
        }

        public void drawGridlines()
        {
            int interval = 20;

            for (int x = 0; x < pictureBox1.Bounds.Width; x += interval)
            {
                g.DrawLine(Pens.Gray, x, 0, x, pictureBox1.Bounds.Height);
            }

            for (int y = 0; y < pictureBox1.Bounds.Height; y += interval)
            {
                g.DrawLine(Pens.Gray, 0, y, pictureBox1.Bounds.Width, y);
            }
        }

        public void drawLine()
        {
            g.DrawLine(Pens.Red, ConvertCoords(new Point(0, (int)b)), ConvertCoords(new Point(pictureBox1.Width, (int)((m * pictureBox1.Width) + b))));
        }

        public double Compute(double x)
        {
            return m * x + b;
        }

        public void drawProperLine()
        {
            if (Points.Count > 1)
            {
                float xA = Points.Select(p => p.X).Sum() / (float)Points.Count;
                float yA = Points.Select(p => p.Y).Sum() / (float)Points.Count;

                float top = Points.Select(p => (p.X - xA) * (p.Y - yA)).Sum();
                float bot = Points.Select(p => (p.X - xA) * (p.X - xA)).Sum();

                float mf = top / (float)bot;

                int bf = (int)(yA - (mf * xA));

                g.DrawLine(Pens.Blue, ConvertCoords(new Point(0, bf)), ConvertCoords(new Point(pictureBox1.Width, (int)((mf * pictureBox1.Width) + bf))));
            }
        }

        private async void GenerateButton_Click(object sender, EventArgs e)
        {
            var Phish = PointsToInputs();//Phish is BACK!!!!!
            double error = 0;
            perceptron.TrainWithHillClimbing(Phish.inputs, Phish.outputs, ref error);

            m = perceptron.weights[0];
            b = perceptron.bias;

            drawStuff(); //don't make a double[] of 0;
            /*
            for (int i = 0; i < 10000; i++)
            {
                mutate();

                if (i % 100 == 0)
                {
                    await Task.Delay(1);
                    drawStuff();
                }
            }
            */
        }

        public (double[][] inputs, double[] outputs) PointsToInputs()
        {
            double[][] inputs = new double[Points.Count][];
            double[] outputs = new double[Points.Count];

            for (int i = 0; i < Points.Count; i++)//RIP Phish
            {
                inputs[i] = new double[] { Points[i].X };
                outputs[i] = Points[i].Y;
            }

            return (inputs, outputs);
        }

        public void mutate()
        {
            var prevError = error();
            var prevM = m;
            var prevB = b;

            int yeet = random.Next(5);
            if (yeet == 0)
            {
                m += (float)(random.NextDouble() - 0.5) * 25;
            }
            else if (yeet == 1)
            {
                b += (float)(random.NextDouble() - 0.5) * 25;
            }
            else
            {
                m += (float)(random.NextDouble() - 0.5) * 25;
                b += (float)(random.NextDouble() - 0.5) * 25;
            }

            if (error() >= prevError)
            {
                m = prevM;
                b = prevB;
            }
        }

        public float error()
        {
            double sum = 0;

            foreach (Point point in Points)
            {
                sum += Math.Pow(Compute(point.X) - point.Y, 2);
            }

            return (float)(sum / Points.Count);
        }
    }
}