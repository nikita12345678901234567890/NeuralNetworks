using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_art
{
    public class ArtTrainer
    {
        TriangleArt[] population;
        
        int bestIndex = 0;

        public ArtTrainer(Bitmap orignalImage, int maxTriangles, int populationSize)
        {
            population = new TriangleArt[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new TriangleArt(maxTriangles, orignalImage);
            }
        }

        public void Train(Random random)
        {
            updateBest();

            for (int i = 0; i < population.Length; i++)
            {
                if (i == bestIndex) i++;

                population[i].Mutate(random);
            }

            updateBest();
        }

        private void updateBest()
        {
            double bestError = double.MaxValue;
            int besti = 0;

            for (int i = 0; i < population.Length; i++)
            {
                if (population[i].GetError() < bestError)
                {
                    bestError = population[i].GetError();
                    besti = i;
                }
            }

            bestIndex = besti;
        }

        public Bitmap GetBestImage(int x, int y)
        {
            updateBest();

            return population[bestIndex].DrawImage(x, y);
        }
    }
}
