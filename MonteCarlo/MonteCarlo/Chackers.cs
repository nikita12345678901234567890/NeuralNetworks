using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonteCarlo
{
    public enum Pieces
    {
        Dead = -1,
        Empty,
        Blue,
        Red
    }

    public class Chackers : IGameState<Chackers> //Player = X = Blue
    {
        private int gridSize;

        public Pieces[,] Grid;

        public Statte aktuellStatte = Statte.Gaming;

        public bool XTurn { get; set; }
        public int Value { get; set; }

        public bool XWin => aktuellStatte == Statte.XWin;

        public bool IsTie => aktuellStatte == Statte.Tie;

        public bool OWin => aktuellStatte == Statte.OWin;

        public bool IsTerminal => !IsExpanded || (IsExpanded && Children.Count == 0);//aktuellStatte != Statte.Gaming;

        public List<Chackers> Children { get; set; }

        public Chackers Parent { get; set; }

        public bool IsExpanded { get; set; }
        public int win { get; set; }
        public int number { get; set; }

        public Chackers(int number)
        {
            XTurn = true;
            this.gridSize = number;
            Grid = new Pieces[number, number];
            Children = new List<Chackers>();

            this.number = 1;
            win = 0;
            IsExpanded = false;
        }

        public Chackers(int number, Pieces[,] Grid)
        {
            XTurn = true;
            this.gridSize = number;
            this.Grid = Grid;
            Children = new List<Chackers>();

            this.number = 1;
            win = 0;
            IsExpanded = false;
        }

        public void setGrid(Pieces[,] source)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Grid[y, x] = source[y, x];
                }
            }
        }

        public Chackers[] GetChildren()
        {
            if (Children.Count == 0)//!IsTerminal)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    for (int x = 0; x < gridSize; x++)
                    {
                        if ((XTurn && Grid[y, x] == Pieces.Blue) || (!XTurn && Grid[y, x] == Pieces.Red))
                        {
                            var moves = GetMoves(x, y);
                            foreach (var Move in moves)
                            {
                                Move.Parent = this;
                                //Move.CheckGameOver();
                                Children.Add(Move);
                            }
                        }
                    }
                }
            }


            IsExpanded = true;
            return Children.ToArray();
        }

        public bool Move(Point piece, Point destination)
        {
            CheckGameOver();
            var moves = GetMoves(piece.X, piece.Y);

            Chackers goodMove = null;

            bool found = false;
            foreach (var move in moves)
            {
                if (move.Grid[piece.Y, piece.X] == Pieces.Empty && move.Grid[destination.Y, destination.X] == (XTurn ? Pieces.Blue : Pieces.Red))
                {
                    if (found)
                    {
                        throw new Exception("Multiple moves found");
                    }
                    found = true;
                    goodMove = move;
                }
            }
            if (!found) return false;

            setGrid(goodMove.Grid);

            XTurn = !XTurn;

            CheckGameOver();

            return true;
        }

        public void CheckGameOver()
        {
            GetChildren();

            if (Children.Count == 0)
            {
                if (XTurn)
                {
                    aktuellStatte = Statte.OWin;
                    Value = -1;
                }
                else
                {
                    aktuellStatte = Statte.XWin;
                    Value = 1;
                }
            }
            else
            {
                aktuellStatte = Statte.Gaming;
                Value = 0;
            }
        }

        private Chackers[] GetMoves(int x, int y)
        {
            List<Chackers> moves = new List<Chackers>();

            if (Grid[y, x] == Pieces.Blue) //Player, bottom
            {
                if (y >= 1 && x >= 1) //up left
                {
                    if (Grid[y - 1, x - 1] == Pieces.Empty) //Move
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 1, x - 1] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                    else if ((y >= 2 && x >= 2) && Grid[y - 1, x - 1] == Pieces.Red && Grid[y - 2, x - 2] == Pieces.Empty) //Take
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 1, x - 1] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 2, x - 2] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                }

                if (y >= 1 && x < gridSize - 1) //up right
                {
                    if (Grid[y - 1, x + 1] == Pieces.Empty) //Move
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 1, x + 1] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                    else if ((y >= 2 && x < gridSize - 2) && Grid[y - 1, x + 1] == Pieces.Red && Grid[y - 2, x + 2] == Pieces.Empty) //Take
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 1, x + 1] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 2, x + 2] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                }
            }

            if (Grid[y, x] == Pieces.Red) //AI, top
            {
                if (y < gridSize - 1 && x >= 1) //down left
                {
                    if (Grid[y + 1, x - 1] == Pieces.Empty) //Move
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y + 1, x - 1] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                    else if ((y < gridSize - 2 && x >= 2) && Grid[y + 1, x - 1] == Pieces.Blue && Grid[y + 2, x - 2] == Pieces.Empty) //Take
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y + 1, x - 1] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y + 2, x - 2] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                }

                if (y < gridSize - 1 && x < gridSize - 1) //down right
                {
                    if (Grid[y + 1, x + 1] == Pieces.Empty) //Move
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y + 1, x + 1] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                    else if ((y < gridSize - 2 && x < gridSize - 2) && Grid[y + 1, x + 1] == Pieces.Blue && Grid[y + 2, x + 2] == Pieces.Empty) //Take
                    {
                        moves.Add(new Chackers(gridSize));
                        moves[moves.Count - 1].setGrid(Grid);
                        var temp = moves[moves.Count - 1].Grid[y, x];
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y + 1, x + 1] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y + 2, x + 2] = temp;
                        moves[moves.Count - 1].XTurn = !XTurn;
                    }
                }
            }

            return moves.ToArray();
        }

        public void ResetBoard(bool empty = false)
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if ((y % 2 != 0) != (x % 2 != 0)) //XOR
                    {
                        Grid[y, x] = Pieces.Dead;
                    }
                    else if (!empty)
                    {
                        if (y <= 2)
                        {
                            Grid[y, x] = Pieces.Red;
                        }
                        else if (y >= gridSize - 3)
                        {
                            Grid[y, x] = Pieces.Blue;
                        }
                        else
                        {
                            Grid[y, x] = Pieces.Empty;
                        }
                    }
                    else
                    {
                        Grid[y, x] = Pieces.Empty;
                    }
                }
            }

            XTurn = true;
        }
    }
}