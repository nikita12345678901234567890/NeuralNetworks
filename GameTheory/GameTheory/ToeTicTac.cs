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

        public ToeTicTac[] GetChildren()
        {
            throw new NotImplementedException();
        }

        public ToeTicTac(int number)
        {
            for (int x = 0; x < number; x++)
            {
                for (int y = 0; y < number; y++)
                {
                    //make new Kontrollkästchen
                }
            }
        }
    }
}