using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class Monte<T> where T : IGameState<T>
    {
        private Random random = new Random(2);

        public T MCTS(int iterations, T startingState)
        {
            random = new Random(2);

            //Generate the monte-carlo tree
            IGameState<T> rootNode = startingState;
            for (int i = 0; i < iterations; i++)
            {
                var selectedNode = Select(rootNode);
                var expandedChild = Expand(selectedNode);
                int value = Simulate(expandedChild);
                Backpropagate(value, expandedChild);
            }

            //return the best child
            var sortedChildren = rootNode.Children.OrderByDescending((state) => state.win);
            var topChild = sortedChildren.First();
            return topChild;
        }

        private IGameState<T> Select(IGameState<T> rootNode)
        {
            IGameState<T> currentNode = rootNode;
            while (currentNode.IsExpanded)
            {
                //Find the index of the child with the highest UCT value
                IGameState<T> highestUCTChild = null;
                double highestUCT = double.NegativeInfinity;

                foreach (var child in currentNode.GetChildren())
                {
                    double val = child.UCT();

                    if (val > highestUCT)
                    {
                        highestUCT = val;
                        highestUCTChild = child;
                    }
                }
                if (highestUCTChild == null) break;

                currentNode = highestUCTChild;
            }
            return currentNode;
        }

        private IGameState<T> Expand(IGameState<T> currentNode)
        {
            currentNode.GetChildren();
            currentNode.IsExpanded = true;

            if (currentNode.Children.Count == 0) return currentNode;
            return currentNode.Children[random.Next(0, currentNode.Children.Count)];
        }

        private int Simulate(IGameState<T> currentNode)
        {
            while (!currentNode.IsTerminal)
            {
                currentNode.GetChildren();
                
                currentNode = currentNode.Children[random.Next(0, currentNode.Children.Count)];
            }
            int value = 0;
            if (currentNode.XWin) value = 1;
            else if (currentNode.IsTie) value = 0;
            else value = -1;
            if (currentNode.XTurn) value *= -1;
            return value;
        }

        private static void Backpropagate(int value, IGameState<T> simulatedNode)
        {
            IGameState<T> currentNode = simulatedNode;
            while (currentNode != null)
            {
                currentNode.number++;
                currentNode.win += value;
                currentNode = currentNode.Parent;
                if (currentNode != null)
                {
                    value = -value;
                }
            }
        }
    }
}