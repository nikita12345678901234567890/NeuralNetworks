using System.Windows.Forms;

namespace GameTheory
{
    public partial class Form1 : Form
    {
        const int number = 3;

        CheckBox[,] Grid;

        public Form1()
        {
            InitializeComponent();
            Grid = new CheckBox[number, number];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < number; x++)
            {
                for (int y = 0; y < number; y++)
                {
                    CheckBox Fische = new CheckBox() { Location = new Point(x * 55, y * 55), ThreeState = true, Size = new Size(40, 40) };
                    Controls.Add(Fische);
                    Fische.Click += Clicked;
                    Grid[y, x] = Fische;
                }
            }
        }

        void Clicked(object sender, EventArgs e)
        { 
        
        }
    }
}