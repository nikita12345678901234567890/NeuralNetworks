using System.Runtime.CompilerServices;

namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        const int number = 8;

        Button[,] buttons;
        const int spacing = 50;

        bool selected = false;
        Point selectedPos = new Point();

        Chackers Game;

        public Form1()
        {
            InitializeComponent();
            buttons = new Button[number, number];
            Game = new Chackers(number);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    buttons[y, x] = new Button();

                    buttons[y, x].Location = new Point(spacing * x, spacing * y);

                    //buttons[y, x].Text = "Test";
                    buttons[y, x].Size = new Size(20, 20);


                    buttons[y, x].Tag = new Point(y, x);

                    Controls.Add(buttons[y, x]);

                    buttons[y, x].Click += Clicked;
                }
            }

            Game.ResetBoard();
            UpdateGrid();
        }

        void Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point position = (Point)button.Tag;

            if (!selected && Game.Grid[position.Y, position.X] != Pieces.Death)
            {
                button.BackColor = Color.Yellow;
                selected = true;
                selectedPos = position;
            }

            else if (selected && Game.Grid[position.Y, position.X] == 0)
            {
                if (Game.Move(selectedPos, position))
                {
                    UpdateGrid();
                }
            }

            else if (selected && position == selectedPos)
            {
                selected = false;
                UpdateGrid();
            }
        }

        void UpdateGrid()
        {
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    if (Game.Grid[y, x] == -1) //XOR
                    {
                        buttons[y, x].Enabled = false;
                        buttons[y, x].Visible = false;
                    }
                    else
                    {
                        buttons[y, x].Enabled = true;
                        buttons[y, x].Visible = true;

                        if (Game.Grid[y, x] == 2)
                        {
                            buttons[y, x].BackColor = Color.Red;
                        }
                        else if (Game.Grid[y, x] == 1)
                        {
                            buttons[y, x].BackColor = Color.Blue;
                        }
                        else
                        {
                            buttons[y, x].BackColor = Color.Black;
                        }
                    }
                }
            }
        }
    }
}