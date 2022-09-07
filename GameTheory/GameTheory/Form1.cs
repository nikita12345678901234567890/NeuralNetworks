using System.Windows.Forms;

using static System.Windows.Forms.AxHost;

namespace GameTheory
{
    public partial class Form1 : Form
    {//Player is X, Min is O
        const int number = 3;

        CheckBox[,] Grid;

        ToeTicTac Game = new ToeTicTac(number);

        MiniMax<ToeTicTac> miniMax = new MiniMax<ToeTicTac>();

        public Form1()
        {
            InitializeComponent();
            Grid = new CheckBox[number, number];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game.XTurn = true;
            for (int x = 0; x < number; x++)
            {
                for (int y = 0; y < number; y++)
                {
                    CheckBox Fische = new CheckBox() { Location = new Point((x * 55) + 50, (y * 55) + 50), ThreeState = true, Size = new Size(40, 40) };
                    Controls.Add(Fische);
                    Fische.Click += Clicked;
                    Grid[y, x] = Fische;
                }
            }
        }

        void Clicked(object sender, EventArgs e)
        {
            var Sender = (CheckBox) sender;
            if (!Game.XTurn)
            {
                //Unclicking the checkbox
                if (Sender.CheckState != CheckState.Unchecked)
                {
                    Sender.CheckState--;
                }
                else
                {
                    Sender.CheckState = CheckState.Indeterminate;
                }
            }
            else
            {
                Game.UpdateGrid(convertGrid(), !Game.XTurn);
            }
        }

        int[,] convertGrid()
        {
            int[,] newGrid = new int[number, number];
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    newGrid[y, x] = (int)Grid[y, x].CheckState;
                }
            }

            return newGrid;
        }

        //make new overload for ^that

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Game.XTurn)
            {
                var possibilities = Game.GetChildren();
                Game.UpdateGrid(convertGrid(possibilities[miniMax.Minimax(Game, Game.XTurn)], Game.XTurn));
            }
        }
    }
}