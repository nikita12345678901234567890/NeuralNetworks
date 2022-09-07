using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    enum Statte //Pronounced like latte
    {
        XWin,
        OWin,
        Tie,
        Gaming,
    };
    //X plays 1, O playes 2
    public class ToeTicTac : IGameState<ToeTicTac>
    {
        Statte aktuellStatte = Statte.Gaming;

        public int Value { get; private set; }

        public bool XWin => aktuellStatte == Statte.XWin;

        public bool IsTie => aktuellStatte == Statte.Tie;

        public bool OWin => aktuellStatte == Statte.OWin;

        public bool IsTerminal => aktuellStatte != Statte.Gaming;

        int[,] Grid;

        public bool XTurn = true;

        int number;

        public ToeTicTac[] GetChildren()
        {
            List<ToeTicTac> children = new List<ToeTicTac>();
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    if (Grid[y, x] == 0)
                    {
                        ToeTicTac child = new ToeTicTac(number);
                        child.Grid = Grid;

                        //Do the move:
                        if (XTurn) child.Grid[y, x] = 1;
                        else child.Grid[y, x] = 2;

                        //Update flags:
                        child.CheckGameOver();

                        children.Add(child);
                    }
                }
            }
            return children.ToArray();
        }

        public ToeTicTac(int number)
        {
            this.number = number;
            Grid = new int[number, number];
            for (int x = 0; x < number; x++)
            {
                for (int y = 0; y < number; y++)
                {
                    Grid[y, x] = 0;
                }
            }

            Value = 0;
            CheckGameOver();
        }

        public void CheckGameOver()
        {
            //Horizontal lines
            for (int y = 0; y < number; y++)
            {
                bool XWin = true;
                bool OWin = true;
                for (int x = 0; x < number; x++)
                {
                    if (Grid[y, x] != 1) XWin = false;
                    if (Grid[y, x] != 2) OWin = false;
                }
                if (XWin) aktuellStatte = Statte.XWin;
                if (OWin) aktuellStatte = Statte.OWin;
            }

            //Vertical lines
            for (int x = 0; x < number; x++)
            {
                bool XWin = true;
                bool OWin = true;
                for (int y = 0; y < number; y++)
                {
                    if (Grid[y, x] != 1) XWin = false;
                    if (Grid[y, x] != 2) OWin = false;
                }
                if (XWin) aktuellStatte = Statte.XWin;
                if (OWin) aktuellStatte = Statte.OWin;
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
                }
            }
            //down left diagonal
            var h = Grid[0, number - 1];
            if (h != 0)
            {
                for (int i = 0; i < number; i++)
                {
                    if (Grid[i, number - i] != h)
                    {
                        diagonal = false;
                    }
                }
                if (diagonal)
                {
                    if (h == 2) aktuellStatte = Statte.OWin;
                    else aktuellStatte = Statte.XWin;
                }
            }

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
            }
        }

        public void UpdateGrid(int[,] grid, bool XTurn)
        {
            Grid = grid;
            this.XTurn = XTurn;
            CheckGameOver();
        }
    }
}