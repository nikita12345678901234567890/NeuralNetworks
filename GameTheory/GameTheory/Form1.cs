using System.Windows.Forms;
#nullable disable
using static System.Windows.Forms.AxHost;

namespace GameTheory
{
    public partial class Form1 : Form
    {//Player is X, Min is O
        const int number = 3;

        CheckBox[,] Grid;

        ToeTicTac Game = new ToeTicTac(number, true);

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

            Grid[0, 0].CheckState = CheckState.Checked;
            Grid[1, 0].CheckState = CheckState.Indeterminate;
            Grid[2, 0].CheckState = CheckState.Indeterminate;
            Grid[1, 2].CheckState = CheckState.Checked;
        }

        void Clicked(object sender, EventArgs e)
        {
            var Sender = (CheckBox) sender;
            Sender.Enabled = false;

            if (Game.XTurn)
            {
                Game.UpdateGrid(convertGrid(), !Game.XTurn);
                if (Game.aktuellStatte == Statte.Gaming)
                {
                    var possibilities = Game.GetChildren();
                    Game.UpdateGrid(possibilities[miniMax.Minimax(Game, Game.XTurn)]); //When move made, it doesn't go down the tree or possibilities.
                    updateCheckboxes();
                }
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

        void updateCheckboxes()
        {
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    Grid[y, x].CheckState = (CheckState)Game.Grid[y, x];
                    if (Grid[y, x].CheckState == CheckState.Indeterminate)
                    {
                        Grid[y, x].BackColor = Color.Red;
                        Grid[y, x].Enabled = false;
                    }
                    else if (Grid[y, x].CheckState == CheckState.Checked)
                    {
                        Grid[y, x].BackColor = Color.Blue;
                        Grid[y, x].Enabled = false;
                    }
                }
            }
        }
    }
}