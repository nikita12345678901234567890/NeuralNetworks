using System.Runtime.CompilerServices;

namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        #region ToeTicTac
        const int tNumber = 3;

        CheckBox[,] Grid;

        ToeTicTac Game = new ToeTicTac(tNumber, true);

        TextBox ResultBox;

        int moveNum = 0;

        Button ResetButtonT;

        Monte<ToeTicTac> MonTicTac;
        #endregion

        #region  Chackers
        const int cNumber = 8;

        Chackers cGame = new Chackers(cNumber);

        Button[,] buttons;
        const int spacing = 50;

        bool selected = false;
        Point selectedPos = new Point();

        Button ResetButtonC;

        Monte<Chackers> MonChacker;
        #endregion

        Random random;

        public Form1()
        {
            InitializeComponent();
            buttons = new Button[cNumber, cNumber];
            cGame = new Chackers(cNumber);

            Grid = new CheckBox[tNumber, tNumber];

            random = new Random();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }



        #region ToeTicTac
        private void TicTacButton_Click(object sender, EventArgs e)
        {
            LoadToeTicTac();
        }

        void LoadToeTicTac()
        {
            Controls.Clear();

            MonTicTac = new Monte<ToeTicTac>();


            ResultBox = new TextBox();
            ResultBox.Location = new Point(100, 100);
            ResultBox.Visible = false;
            ResultBox.Enabled = false;
            Controls.Add(ResultBox);

            Game.XTurn = true;
            for (int x = 0; x < tNumber; x++)
            {
                for (int y = 0; y < tNumber; y++)
                {
                    CheckBox Fische = new CheckBox() { Location = new Point((x * 55) + 50, (y * 55) + 50), ThreeState = true, Size = new Size(40, 40) };
                    Controls.Add(Fische);
                    Fische.Click += ClickTicTac;
                    Grid[y, x] = Fische;
                }
            }

            ResetButtonT = new Button();
            ResetButtonT.Location = new Point(424, 94);
            ResetButtonT.Visible = true;
            ResetButtonT.Enabled = true;
            ResetButtonT.Name = "ResetButton";
            ResetButtonT.Size = new Size(75, 23);
            ResetButtonT.Text = "Reset";
            ResetButtonT.UseVisualStyleBackColor = true;
            ResetButtonT.Click += ResetButtonClick;

            Controls.Add(ResetButtonT);

            //Grid[0, 0].CheckState = CheckState.Checked;
            //Grid[1, 0].CheckState = CheckState.Indeterminate;
            //Grid[1, 1].CheckState = CheckState.Checked;
            //Grid[1, 2].CheckState = CheckState.Indeterminate;
            //Grid[2, 0].CheckState = CheckState.Indeterminate;
            //Grid[2, 2].CheckState = CheckState.Indeterminate;
            Game.UpdateGrid(convertGrid(), true);
            updateCheckboxes();
        }

        void ClickTicTac(object sender, EventArgs e)
        {
            var Sender = (CheckBox)sender;
            Sender.Enabled = false;

            if (Game.XTurn)
            {
                Game.GetChildren();
                if (Game.Children.Count != 0 || moveNum == 0)
                {
                    doMove();
                    moveNum++;
                    var possibilities = Game.GetChildren();
                    if (possibilities.Length != 0)
                    {
                        Game = MonTicTac.MCTS(5000, Game);  //Monte called;
                        updateCheckboxes();
                    }
                }

                if (Game.Children.Count == 0 && moveNum != 0)//print game result
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
                for (int y = 0; y < tNumber; y++)
                {
                    for (int x = 0; x < tNumber; x++)
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
            int[,] newGrid = new int[tNumber, tNumber];
            for (int y = 0; y < tNumber; y++)
            {
                for (int x = 0; x < tNumber; x++)
                {
                    newGrid[y, x] = (int)Grid[y, x].CheckState;
                }
            }

            return newGrid;
        }

        void updateCheckboxes()
        {
            for (int y = 0; y < tNumber; y++)
            {
                for (int x = 0; x < tNumber; x++)
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

        void ResetButtonClick(object sender, EventArgs e)
        {
            for (int y = 0; y < tNumber; y++)
            {
                for (int x = 0; x < tNumber; x++)
                {
                    Grid[y, x].CheckState = CheckState.Unchecked;
                }
            }

            Game.Reset();
            updateCheckboxes();
            moveNum = 0;

            ResultBox.Visible = false;
        }
        #endregion

        #region Chackers
        private void ChackersButton_Click(object sender, EventArgs e)
        {
            LoadChackers();
        }

        void LoadChackers()
        {
            Controls.Clear();

            MonChacker = new Monte<Chackers>();

            this.BackColor = Color.Black;
            for (int y = 0; y < cNumber; y++)
            {
                for (int x = 0; x < cNumber; x++)
                {
                    buttons[y, x] = new Button();

                    buttons[y, x].Location = new Point(spacing * x, spacing * y);

                    buttons[y, x].Size = new Size(20, 20);


                    buttons[y, x].Tag = new Point(x, y);

                    Controls.Add(buttons[y, x]);

                    buttons[y, x].Click += ClickChackers;
                }
            }

            ResetButtonC = new Button();
            ResetButtonC.Location = new Point(424, 94);
            ResetButtonC.Visible = true;
            ResetButtonC.Enabled = true;
            ResetButtonC.Name = "ResetButton";
            ResetButtonC.Size = new Size(75, 23);
            ResetButtonC.Text = "Reset";
            ResetButtonC.UseVisualStyleBackColor = true;
            ResetButtonC.Click += Reset;

            Controls.Add(ResetButtonC);

            cGame.ResetBoard(true);

            cGame.Grid[4, 6] = Pieces.Blue;
            cGame.Grid[1, 5] = Pieces.Red;
            cGame.Grid[3, 5] = Pieces.Red;
            UpdateGrid();
        }

        void Reset(object sender, EventArgs e)
        {
            cGame.ResetBoard(false);
        }

        void ClickChackers(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Point position = (Point)button.Tag;

            if (cGame.XTurn)
            {
                if (!selected && cGame.Grid[position.Y, position.X] == Pieces.Blue)
                {
                    button.BackColor = Color.Yellow;
                    selected = true;
                    selectedPos = position;
                }

                else if (selected && cGame.Grid[position.Y, position.X] == Pieces.Empty)
                {
                    if (cGame.Move(selectedPos, position))
                    {
                        UpdateGrid();

                        var possibilities = cGame.GetChildren();
                        if (possibilities.Length != 0)
                        {
                            cGame = MonChacker.MCTS(5000, cGame);  //Monte called;
                            UpdateGrid();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid move");
                    }
                }

                else if (selected && position == selectedPos)
                {
                    selected = false;
                    UpdateGrid();
                }
            }
        }

        void UpdateGrid()
        {
            for (int y = 0; y < cNumber; y++)
            {
                for (int x = 0; x < cNumber; x++)
                {
                    if (cGame.Grid[y, x] == Pieces.Dead) //XOR
                    {
                        buttons[y, x].Enabled = false;
                        buttons[y, x].Visible = false;
                    }
                    else
                    {
                        buttons[y, x].Enabled = true;
                        buttons[y, x].Visible = true;

                        if (cGame.Grid[y, x] == Pieces.Red)
                        {
                            buttons[y, x].BackColor = Color.Red;
                        }
                        else if (cGame.Grid[y, x] == Pieces.Blue)
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

        #endregion

    }
}