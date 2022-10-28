using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTheory
{
    public class MiniMax<T> where T : IGameState<T>
    {//Max is X, Min is O
        public int Minimax(IGameState<T> state, bool isMax, int alpha = int.MinValue, int beta = int.MaxValue, int depth = 0)
        {
            if (state.IsTerminal)
            {
                return state.Value;
            }

            //if maximizer turn, find greatest value in all the possible turns
            //else if minimizer turn, find smallest value in all the possible turns
            
            List<T> children = state.GetChildren().ToList();

            if (isMax) //if maximizer turn, find greatest value
            {
                int value = int.MinValue;
                foreach (T move in children)
                {
                    value = Math.Max(value, Minimax(move, false, alpha, beta, depth + 1)); //flip turn on recursion
                    alpha = Math.Max(alpha, value); //store best value for maximizer
                    if (alpha >= beta)
                    {
                        break; //cutoff if better value is assured
                    }
                }
                return value;
            }
            else //else if minimizer turn, find smallest value
            {
                int value = int.MaxValue;
                foreach (T move in children)
                {
                    value = Math.Min(value, Minimax(move, true, alpha, beta, depth + 1)); //flip turn on recursion
                    beta = Math.Min(beta, value); //store best value for minimizer
                    if (alpha >= beta)
                    {
                        break; //cutoff if better value is assured
                    }
                }
                return value;
            }










            /*
            var possibilities = state.GetChildren();
            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 0; i < possibilities.Length; i++)
            {
                if (possibilities[i].Value < possibilities[minIndex].Value) minIndex = i;
                if (possibilities[i].Value > possibilities[maxIndex].Value) maxIndex = i;
            }
            if (isMax) return maxIndex;
            return minIndex;
            */
        }
    }
}