using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    public class MiniMax<T> where T : IGameState<T>
    {//Max is X, Min is O
        int Minimax(IGameState<T> state, bool isMax)
        {
            /*
            if (state.IsTerminal) return state's value;
            if (isMax) //if maximizer turn, find greatest value in all the possible turns
            else //else if minimizer turn, find smallest value in all the possible turns
            */
            if (state.IsTerminal)
            {
                if (state.XWin) return -1;
                if (state.IsTie) return 0;
                if (state.OWin) return 1;
            }

            var possibilities = state.GetChildren();
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 0; i < possibilities.Length; i++)
            {
                if (possibilities[i].Value < possibilities[minIndex].Value) minIndex = i;
                if (possibilities[i].Value > possibilities[maxIndex].Value) maxIndex = i;
            }
            if (isMax) return possibilities[maxIndex].Value;
            return possibilities[minIndex].Value;
        }
    }
}