using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    public class MiniMax<T> where T : IGameState<T>
    {
        int Minimax(IGameState<T> state, bool isMax)
        {
            /*
            if (state.IsTerminal) return state's value;
            if (isMax) //if maximizer turn, find greatest value in all the possible turns
            else //else if minimizer turn, find smallest value in all the possible turns
            */
            if (state.IsTerminal)
            {
                if (isMax)
                {
                    if (state.IsWin) return 1;
                    if (state.IsTie) return 0;
                    if (state.IsLoss) return -1;
                }
                else
                {
                    if (state.IsWin) return -1;
                    if (state.IsTie) return 0;
                    if (state.IsLoss) return 1;
                }
            }
            if (isMax)
            {

            }
        }
    }

    public interface IGameState<T> where T : IGameState<T>
    {
        int value { get; }
        bool IsWin { get; }
        bool IsTie { get; }
        bool IsLoss { get; }
        bool IsTerminal { get; }
        T[] GetChildren();
    }
}