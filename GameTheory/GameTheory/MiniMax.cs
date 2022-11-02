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
                state.Value = -1;
                int bestIndex = 0;
                for (int i = 0; i < children.Count(); i++)
                {
                    int temp = Minimax(children[i], false, alpha, beta, depth + 1); //flip turn on recursion
                    if (temp > state.Value)
                    {
                        state.Value = temp;
                        bestIndex = i;
                    }
                    
                    if (alpha > state.Value) //store best value for maximizer
                    {
                        alpha = state.Value;
                        //bestIndex = i;
                    }
                    if (alpha >= beta)
                    {
                        break; //cutoff if better value is assured
                    }
                }
                if (depth == 0)
                {
                    return bestIndex;
                }
                return state.Value;
            }
            else //else if minimizer turn, find smallest value
            {
                state.Value = 1;
                int bestIndex = 0;
                for (int i = 0; i < children.Count(); i++)
                {
                    int temp = Minimax(children[i], true, alpha, beta, depth + 1); //flip turn on recursion
                    if (temp < state.Value)
                    {
                        state.Value = temp;
                        bestIndex = i;
                    }

                    if (beta < state.Value) //store best value for minimizer
                    {
                        beta = state.Value;
                        //bestIndex = i;
                    }
                    if (alpha >= beta)
                    {
                        break; //cutoff if better value is assured
                    }
                }
                if (depth == 0)
                {
                    return bestIndex;
                }
                return state.Value;
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