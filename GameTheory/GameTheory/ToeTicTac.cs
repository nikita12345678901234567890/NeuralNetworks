using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    enum Statte //Pronounced like latte
    {
        Win,
        Tie,
        Loss,
        Höllenfeuer,
    };

    public class ToeTicTac : IGameState<ToeTicTac>
    {
        Statte aktuellStatte = Statte.Höllenfeuer;

        public int Value => throw new NotImplementedException();

        public bool IsWin => aktuellStatte == Statte.Win;

        public bool IsTie => aktuellStatte == Statte.Tie;

        public bool IsLoss => aktuellStatte == Statte.Loss;

        public bool IsTerminal => aktuellStatte != Statte.Höllenfeuer;

        CheckBox[,] Grid;

        int number;

        public ToeTicTac[] GetChildren()
        {
            throw new NotImplementedException();
        }

        public ToeTicTac(int number, Form1 form)
        {
            this.number = number;
            //Add checkboxes
            Grid = new CheckBox[number, number];
            for (int x = 0; x < number; x++)
            {
                for (int y = 0; y < number; y++)
                {
                    CheckBox Fische = new CheckBox() { Location = new Point(x * 55, y * 55), ThreeState = true, Size = new Size(40, 40) };
                    form.Controls.Add(Fische);
                    Fische.Click += Clicked;
                }
            }
        }

        public void Clicked(object sender, EventArgs e)
        {
            //Horizontal lines
            for (int y = 0; y < number; y++)
            {
                bool win = true;
                bool loss = true;
                for (int x = 0; x < number; x++)
                {
                    if (Grid[y, x].CheckState != CheckState.Indeterminate) win = false;
                    if (Grid[y, x].CheckState != CheckState.Checked) loss = false;
                }
                if (win) aktuellStatte = Statte.Win;
                if (loss) aktuellStatte = Statte.Loss;
            }

            //Vertical lines
            for (int x = 0; x < number; x++)
            {
                bool win = true;
                bool loss = true;
                for (int y = 0; y < number; y++)
                {
                    if (Grid[y, x].CheckState != CheckState.Indeterminate) win = false;
                    if (Grid[y, x].CheckState != CheckState.Checked) loss = false;
                }
                if (win) aktuellStatte = Statte.Win;
                if (loss) aktuellStatte = Statte.Loss;
            }

            //Diagonal lines
            //left as an excersise for next class.
        }
    }
}