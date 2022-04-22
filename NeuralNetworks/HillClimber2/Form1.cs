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
        Bitmap canvas;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Coordinates = new List<(TextBox xBox, TextBox yBox)>();

            canvas = new Bitmap(pictureBox1.Size.Width, pictureBox1.Height);
            pictureBox1.Image = canvas;
            g = pictureBox1.CreateGraphics();
        }
        
        //gridlines
        //make dots show up
        //detect when textbox changed and update point
        //make line

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

            xBox.TextChanged += XBox_TextChanged;
            yBox.TextChanged += YBox_TextChanged;

            this.Controls.Add(Coordinates[Coordinates.Count - 1].xBox);
            this.Controls.Add(Coordinates[Coordinates.Count - 1].yBox);

            drawPoints();
        }

        private void XBox_TextChanged(object s, EventArgs e)
        {
            TextBox sender = (TextBox)s;
            drawPoints();
        }

        private void YBox_TextChanged(object s, EventArgs e)
        {
            TextBox sender = (TextBox)s;
            drawPoints();
        }
        public Point ConvertCoords(Point coordinates)
        {
            return new Point(coordinates.X, pictureBox1.Height - coordinates.Y);
        }

        public void drawPoints()
        {
            int radius = 20;

            for (int i = 0; i < Coordinates.Count; i++)
            {
                g.FillEllipse(Brushes.Chocolate, new Rectangle(ConvertCoords(new Point(int.Parse(Coordinates[i].xBox.Text) - radius, int.Parse(Coordinates[i].yBox.Text) + radius)), new Size(radius*2, radius*2)));
            }
        }
    }
}