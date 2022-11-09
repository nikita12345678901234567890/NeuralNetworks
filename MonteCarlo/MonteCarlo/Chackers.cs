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
        Death = -1,
        Bob,
        Arnold
    }

    public class Chackers
    {
        private int number;

        public Pieces[,] Grid;

        public Chackers(int number)
        {
            this.number = number;
            Grid = new int[number, number];
        }

        public bool Move(Point piece, Point destination)
        {
            //Do error checking here!

            Grid[destination.Y, destination.X] = Grid[piece.Y, piece.X];
            Grid[piece.Y, piece.X] = 0;

            return true;
        }

        public void ResetBoard()
        {
            for (int y = 0; y < number; y++)
            {
                for (int x = 0; x < number; x++)
                {
                    if ((y % 2 != 0) != (x % 2 != 0)) //XOR
                    {
                        Grid[y, x] = -1;
                    }
                    else
                    {
                        if (y <= 2)
                        {
                            Grid[y, x] = 2;
                        }
                        if (y >= number - 3)
                        {
                            Grid[y, x] = 1;
                        }
                    }
                }
            }
        }
    }
}