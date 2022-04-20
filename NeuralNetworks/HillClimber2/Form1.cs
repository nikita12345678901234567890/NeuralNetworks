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

        public Form1()
        {
            Coordinates = new List<(TextBox xBox, TextBox yBox)>();

            InitializeComponent();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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
            var xBox = new TextBox()
            {
                Location = LocX,
                Text = e.X.ToString(),
                Size = suze,
            };
            var yBox = new TextBox()
            {
                Location = LocY,
                Text = e.Y.ToString(),
                Size = suze,
            };
            Coordinates.Add((xBox, yBox));
            this.Controls.Add(Coordinates[Coordinates.Count - 1].xBox);
            this.Controls.Add(Coordinates[Coordinates.Count - 1].yBox);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {

        }
    }
}
