using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class Monte<T> where T : IGameState<T>
    {  //don't murder children
        public static T MCTS(int iterations, T startingState, Random random)
        {
            //Generate the monte-carlo tree
            var rootNode = new Node<T>(startingState);
            for (int i = 0; i < iterations; i++)
            {
                var selectedNode = Select(rootNode);
                var expandedChild = Expand(selectedNode);
                int value = Simulate(expandedChild, random);
                Backpropagate(value, expandedChild);
            }

            //return the best child
            var sortedChildren = rootNode.Children.OrderByDescending((state) => state.W);
            var topChild = sortedChildren.First();
            return topChild.State;
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
                    double val = child.UCT(currentNode.number);

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

        static IGameState<T> Expand(IGameState<T> currentNode)
        {
            currentNode.GetChildren();

            if (currentNode.Children.Count == 0) return currentNode;
            return currentNode.Children[0];
        }

        static int Simulate(IGameState<T> currentNode, Random random)
        {
            while (!currentNode.IsTerminal)
            {
                currentNode.GetChildren();
                int randomIndex = random.Next(0, currentNode.Children.Count);
                currentNode = currentNode.Children[randomIndex];
            }
            if (currentNode.XWin) return 1;
            else if (currentNode.IsTie) return 0;
            else return -1;
        }

        static void Backpropagate(int value, IGameState<T> simulatedNode)
        {
            IGameState<T> currentNode = simulatedNode;
            while (currentNode != null)
            {
                value = -value;
                currentNode.number++;
                currentNode.win += value;
                currentNode = currentNode.Parent;
            }
        }
    }
}