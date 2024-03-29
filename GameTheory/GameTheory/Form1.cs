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

        TextBox ResultBox;

        int moveNum = 0;

        public Form1()
        {
            InitializeComponent();
            Grid = new CheckBox[number, number];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResultBox = new TextBox();
            ResultBox.Location = new Point(100, 100);
            ResultBox.Visible = false;
            ResultBox.Enabled = false;
            Controls.Add(ResultBox);

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

            //Grid[0, 0].CheckState = CheckState.Checked;
            //Grid[1, 0].CheckState = CheckState.Indeterminate;
            //Grid[1, 1].CheckState = CheckState.Checked;
            //Grid[1, 2].CheckState = CheckState.Checked;
            //Grid[2, 0].CheckState = CheckState.Indeterminate;
            //Grid[2, 2].CheckState = CheckState.Indeterminate;
            //Game.UpdateGrid(convertGrid(), true);
            //updateCheckboxes();
        }

        void Clicked(object sender, EventArgs e)
        {
            var Sender = (CheckBox) sender;
            Sender.Enabled = false;

            if (Game.XTurn)
            {
                Game.GetChildren();
                if (Game.children.Count != 0 || moveNum == 0)
                {
                    doMove();
                    moveNum++;
                    var possibilities = Game.GetChildren();
                    if (possibilities.Length != 0)
                    {
                        Game = possibilities[miniMax.Minimax(Game, Game.XTurn)];
                        updateCheckboxes();
                    }
                }

                if (Game.children.Count == 0 && moveNum != 0)//print game result
                {
                    switch (Game.aktuellStatte)
                    {
                        case Statte.Tie:
                            updateCheckboxes();
                            ResultBox.Text = "You tied";
                            ResultBox.Visible = true;
                            break;

                        case Statte.XWin:
                            updateCheckboxes();
                            ResultBox.Text = "Player won";
                            ResultBox.Visible = true;
                            break;

                        case Statte.OWin:
                            updateCheckboxes();
                            ResultBox.Text = "Computer won";
                            ResultBox.Visible = true;
                            break;
                    }
                }
            }
        }

        void doMove()
        {
            var grid = convertGrid();
            var possibilities = Game.GetChildren();

            for (int i = 0; i < possibilities.Length; i++)
            {
                bool matches = true;
                for (int y = 0; y < number; y++)
                {
                    for (int x = 0; x < number; x++)
                    {
                        if (possibilities[i].Grid[y, x] != grid[y, x]) matches = false;
                    }
                }

                if (matches)
                {
                    Game = possibilities[i];
                    return;
                }
            }

            throw new Exception("Move not possible");
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
                    else
                    {
                        Grid[y, x].BackColor = Color.Transparent;
                        Grid[y, x].Enabled = true;
                    }
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    Grid[y, x].CheckState = CheckState.Unchecked;
                }
            }

            Game.Reset();
            updateCheckboxes();
            moveNum = 0;

            ResultBox.Visible = false;
        }
    }
}