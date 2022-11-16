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

    public class Chackers : IGameState<Chackers>//Player = X = Blue
    {
        private int number;

        public Pieces[,] Grid;

        public Statte aktuellStatte = Statte.Gaming;

        public bool Xturn = true;
        public int Value { get; set; }

        public bool XWin => aktuellStatte == Statte.XWin;

        public bool IsTie => aktuellStatte == Statte.Tie;

        public bool OWin => aktuellStatte == Statte.OWin;

        public bool IsTerminal => aktuellStatte != Statte.Gaming;

        public List<Chackers> children;

        public Chackers(int number)
        {
            this.number = number;
            Grid = new Pieces[number, number];
            children = new List<Chackers>();
        }

        public Chackers(int number, Pieces[,] Grid)
        {
            this.number = number;
            this.Grid = Grid;
            children = new List<Chackers>();
        }

        public Chackers[] GetChildren()
        {
            CheckGameOver();
            children.Clear();

            if (!IsTerminal)
            {
                for (int y = 0; y < number; y++)
                {
                    for (int x = 0; x < number; x++)
                    {
                        if ((Xturn && Grid[y, x] == Pieces.Blue) || (!Xturn && Grid[y, x] == Pieces.Red))
                        {
                            var moves = GetMoves(x, y);
                            foreach (var Move in moves)
                            { 
                                children.Add(Move);
                            }
                        }
                    }
                }
            }

            return children.ToArray();
        }

        public bool Move(Point piece, Point destination)
        {
            //Do error checking here!

            Grid[destination.Y, destination.X] = Grid[piece.Y, piece.X];
            Grid[piece.Y, piece.X] = 0;

            return true;
        }

        private void CheckGameOver()
        { 
            
        }

        private Chackers[] GetMoves(int x, int y)
        {
            List<Chackers> moves = new List<Chackers>();

            if (Grid[y, x] == Pieces.Blue) //Player, bottom
            {
                if (y >= 2 && x >= 2) //up left
                {
                    if (Grid[y, x] == Pieces.Empty)
                    {
                        moves.Add(new Chackers(number, Grid));
                        moves[moves.Count - 1].Grid[y, x] = Pieces.Empty;
                        moves[moves.Count - 1].Grid[y - 2, x - 2] = Pieces.Blue;

                        if()
                    }
                }

                if (y >= 2 && x < number - 2) //up right
                { 
                    
                }
            }

            if (Grid[y, x] == Pieces.Red) //AI, top
            {
                if (y < number - 2 && x >= 2) //down left
                { 
                    
                }

                if (y < number - 2 && x < number - 2) //down right
                { 
                    
                }
            }
        }

        public void ResetBoard()
        {
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    if ((y % 2 != 0) != (x % 2 != 0)) //XOR
                    {
                        Grid[y, x] = Pieces.Dead;
                    }
                    else
                    {
                        if (y <= 2)
                        {
                            Grid[y, x] = Pieces.Red;
                        }
                        if (y >= number - 3)
                        {
                            Grid[y, x] = Pieces.Blue;
                        }
                    }
                }
            }
        }

    }
}