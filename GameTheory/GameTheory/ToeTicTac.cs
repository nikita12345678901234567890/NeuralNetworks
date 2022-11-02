using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    class PronouncedLikeAttribute : Attribute
    {
        public string Description { get; set; }
        public PronouncedLikeAttribute(string description)
            => Description = description;        
    }

    
    [PronouncedLike("latte, but with a German accent")]
    public enum Statte
    {
        XWin,
        OWin,
        Tie,
        Gaming,
    };
    //X plays 1, O playes 2
    public class ToeTicTac : IGameState<ToeTicTac>
    {
        public Statte aktuellStatte = Statte.Gaming;

        public int Value { get; set; }

        public bool XWin => aktuellStatte == Statte.XWin;

        public bool IsTie => aktuellStatte == Statte.Tie;

        public bool OWin => aktuellStatte == Statte.OWin;

        public bool IsTerminal => aktuellStatte != Statte.Gaming;

        public int[,] Grid { get; private set; }

        public bool XTurn = true;

        int number;

        public List<ToeTicTac> children;

        public ToeTicTac(int number, bool XTurn)
        {
            children = new List<ToeTicTac>();
            this.number = number;
            this.XTurn = XTurn;
            Grid = new int[number, number];
            for (int x = 0; x < number; x++)
            {
                for (int y = 0; y < number; y++)
                {
                    Grid[y, x] = 0;
                }
            }

            CheckGameOver();
        }

        public ToeTicTac[] GetChildren()
        {
            CheckGameOver();
            children.Clear();

            if (!IsTerminal)
            {
                for (int y = 0; y < number; y++)
                {
                    for (int x = 0; x < number; x++)
                    {
                        if (Grid[y, x] == 0)
                        {
                            ToeTicTac child = new ToeTicTac(number, !XTurn);
                            child.Grid = copyArray(Grid);

                            //Do the move:
                            if (XTurn) child.Grid[y, x] = 1;
                            else child.Grid[y, x] = 2;

                            //Very important
                            //child.XTurn = !XTurn;

                            //Update flags:
                            child.CheckGameOver();

                            children.Add(child);
                        }
                    }
                }
            }

            return children.ToArray();
        }

        private int[,] copyArray(int[,] array)
        {
            int[,] copy = new int[array.GetLength(0), array.GetLength(1)];

            for (int y = 0; y < array.GetLength(0); y++)
            {
                for (int x = 0; x < array.GetLength(1); x++)
                {
                    copy[y, x] = array[y, x];
                }
            }

            return copy;
        }

        public void CheckGameOver()
        {
            //Horizontal lines
            bool XWin = true;
            bool OWin = true;
            for (int y = 0; y < number; y++)
            {
                XWin = true;
                OWin = true;
                for (int x = 0; x < number; x++)
                {
                    if (Grid[y, x] != 1) XWin = false;
                    if (Grid[y, x] != 2) OWin = false;
                }
                if (XWin) aktuellStatte = Statte.XWin;
                if (OWin) aktuellStatte = Statte.OWin;

                if (XWin || OWin)
                {
                    setValue();
                    return;
                }
            }


            //Vertical lines
            for (int x = 0; x < number; x++)
            {
                XWin = true;
                OWin = true;
                for (int y = 0; y < number; y++)
                {
                    if (Grid[y, x] != 1) XWin = false;
                    if (Grid[y, x] != 2) OWin = false;
                }
                if (XWin) aktuellStatte = Statte.XWin;
                if (OWin) aktuellStatte = Statte.OWin;

                if (XWin || OWin)
                {
                    setValue();
                    return;
                }
            }
            

            //Diagonal lines
            bool diagonal = true;
            var g = Grid[0, 0];
            //down right diagonal
            if (g != 0)
            {
                for (int i = 0; i < number; i++)
                {
                    if (Grid[i, i] != g)
                    {
                        diagonal = false;
                    }
                }
                if (diagonal)
                {
                    if (g == 2) aktuellStatte = Statte.OWin;
                    else aktuellStatte = Statte.XWin;
                    setValue();
                    return;
                }
            }

            //down left diagonal
            diagonal = true;
            var h = Grid[0, number - 1];
            if (h != 0)
            {
                for (int i = 0; i < number; i++)
                {
                    if (Grid[i, number - (1 + i)] != h)
                    {
                        diagonal = false;
                    }
                }
                if (diagonal)
                {
                    if (h == 2) aktuellStatte = Statte.OWin;
                    else aktuellStatte = Statte.XWin;
                    setValue();
                    return;
                }
            }

            if (aktuellStatte == Statte.Gaming)
            {
                bool gridFull = true;
                for (int y = 0; y < number; y++)
                {
                    for (int x = 0; x < number; x++)
                    {
                        if (Grid[y, x] == 0) gridFull = false;
                    }
                }
                if (gridFull) aktuellStatte = Statte.Tie;
            }

            setValue();
        }

        public void setValue()
        {
            switch (aktuellStatte)
            {
                case Statte.XWin:
                    Value = 1;
                    break;

                case Statte.OWin:
                    Value = -1;
                    break;

                case Statte.Tie:
                    Value = 0;
                    break;

                default:
                    Value = 0;
                    break;
            }
        }

        public void setState()
        {
            switch (Value)
            {
                case 1:
                    aktuellStatte = Statte.XWin;
                    break;

                case -1:
                    aktuellStatte = Statte.OWin;
                    break;

                case 0:
                    aktuellStatte = Statte.Tie;
                    break;
            }
        }

        public void UpdateGrid(int[,] grid, bool XTurn)
        {
            Grid = copyArray(grid);
            this.XTurn = XTurn;
            CheckGameOver();
        }

        public void UpdateGrid(ToeTicTac game) 
        {
            Grid = copyArray(game.Grid);
            XTurn = game.XTurn;
            CheckGameOver();
        }

        public void Reset()
        {
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    Grid[y, x] = 0;
                }
            }

            aktuellStatte = Statte.Gaming;
            XTurn = true;
            children.Clear();
        }
    }
}