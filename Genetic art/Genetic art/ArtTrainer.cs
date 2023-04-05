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

        public int bestIndex { get; private set; }

        public ArtTrainer(Bitmap orignalImage, int maxTriangles, int populationSize)
        {
            population = new TriangleArt[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                population[i] = new TriangleArt(maxTriangles, orignalImage);
            }

            bestIndex = 0;
        }

        public void Train(Random random)
        {
            updateBest();
            setToBest();

            for (int i = 0; i < population.Length; i++)
            {
                if (i == bestIndex) continue;
                else
                {
                    population[i].Mutate(random);
                }
            }

            //updateBest();
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

        private void setToBest()
        {
            for (int i = 0; i < population.Length; i++)
            {
                if (i != bestIndex)
                {
                    population[bestIndex].CopyTo(population[i]);
                }
            }
        }

        public Bitmap GetImage(int x, int y, int index)
        {
            return population[index].DrawImage(x, y);
        }
    }
}