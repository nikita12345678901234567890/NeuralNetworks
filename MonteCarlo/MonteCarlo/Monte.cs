using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{
    public class Monte<T> where T : IGameState<T>
    {
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
    }
}
