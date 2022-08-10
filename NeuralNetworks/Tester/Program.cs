using NeuralNetwork2;

using Perceptron;

using System;
using System.Threading;

namespace Tester
{
    class Program
    {
        static Random random = new Random(1);

        static void Main(string[] args)
        {
            (double[][] inputs, double[][] outputs) = AndInOuts();
            Network network = new Network(ErrorFunctions.MSE, (2, ActivationFunctions.Identity), (2, ActivationFunctions.Sigmoid), (2, ActivationFunctions.Sigmoid), (1, ActivationFunctions.Sigmoid));

            network.Randomize(random, -1, 1);

            double error = double.PositiveInfinity;

            for (int x = 0; x < 50000; x++)
            {
                if (x % 1000 == 0)
                    ;

                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < inputs.Length; i++)
                {
                    Console.Write("Inputs: ");
                    Console.Write(string.Join(", ", inputs[i]));
                    Console.Write(" Output: " + network.Compute(inputs[i])[0]);
                    Console.WriteLine();
                }

                error = network.Train(inputs, outputs, .01);
                Console.WriteLine("Error: " + error);
                

            }

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Finalized output:");
            for (int i = 0; i < inputs.Length; i++)
            {
                Console.Write("Inputs: ");
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (j != 0)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(inputs[i][j]);
                }

                var ans = network.Compute(inputs[i])[0];
                Console.Write($" Output: {Math.Round(ans)}");
                Console.WriteLine();
            }
        }



        static (double[][] inputs, double[][] outputs) AndInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 }, new double[] { 1, 0 } };
            double[][] outputs = new double[][] { new double[] { 0}, new double[] { 0}, new double[] { 1}, new double[] { 0 } };
            return (inputs, outputs);
        }

        static (double[][] inputs, double[][] outputs) OrInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0 }, new double[] { 1, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 } };
            double[][] outputs = new double[][] { new double[] { 0 }, new double[] { 1 }, new double[] { 1 }, new double[] { 1 } };
            return (inputs, outputs);
        }

        static (double[][] inputs, double[][] outputs) OrAndInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0, 0 }, new double[] { 1, 0, 0 }, new double[] { 0, 1, 0 }, new double[] { 1, 1, 0 },
                                               new double[] { 0, 0, 1 }, new double[] { 1, 0, 1 }, new double[] { 0, 1, 1 }, new double[] { 1, 1, 1 }};
            double[][] outputs = new double[][] { new double[] { 0 }, new double[] { 1 }, new double[] { 1 }, new double[] { 1 },
                                          new double[] { 0 }, new double[] { 0 }, new double[] { 0 }, new double[] { 1 } };
            return (inputs, outputs);
        }
    }
}
