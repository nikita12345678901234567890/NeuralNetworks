namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        Button[,] buttons;
        const int number = 8;
        const int spacing = 50;

        bool selected = false;
        Point selectedPos = new Point();


        public Form1()
        {
            InitializeComponent();
            buttons = new Button[number, number];
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

                    Controls.Add(buttons[y, x]);

                    buttons[y, x].Click += Clicked;

                    if ((y % 2 != 0) != (x % 2 != 0)) //XOR
                    {
                        buttons[y, x].Enabled = false;
                        buttons[y, x].Visible = false;
                    }
                    else
                    {
                        buttons[y, x].Enabled = true;
                        buttons[y, x].Visible = true;

                        if (y <= 2)
                        {
                            buttons[y, x].BackColor = Color.Red;
                        }
                        if (y >= number - 3)
                        {
                            buttons[y, x].BackColor = Color.Blue;
                        }
                    }
                }
            }
        }

        void Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point position = new Point();
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    if (buttons[y, x] == button)
                    {
                        position.X = x;
                        position.Y = y;
                    }
                }
            }

            if (!selected)
            {
                button.BackColor = Color.Yellow;
                selected = true;
            }
        }
    }
}