using System;

namespace Perceptron
{
    class Program
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            double[] weights = { 0.75, -1.25 };
            double bias = 0.5;
            Perceptron perceptron = new Perceptron(weights, bias);

            double[][] testValues = { new double[] { 0, 0 }, new double[] { 0.3, -0.7 }, new double[] { 1, 1 }, new double[] { -1, -1 }, new double[] { -0.5, 0.5 } };

            var results = perceptron.Compute(testValues);

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}