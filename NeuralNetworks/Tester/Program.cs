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
            (double[][] inputs, double[][] outputs) = SinInOuts();
            Network network = new Network(ErrorFunctions.MSE, (1, ActivationFunctions.Identity), (10, ActivationFunctions.TanH), (10, ActivationFunctions.TanH), (10, ActivationFunctions.TanH), (1, ActivationFunctions.TanH));

            network.Randomize(random, -1, 1);

            double error = double.PositiveInfinity;

            for (int x = 0; x < 100000; x++)
            {
                error = network.BatchTrain(inputs, outputs, 4, .01, 0);
                if (true)//x % 10000 == 0)
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        Console.Write("Inputs: ");
                        Console.Write(string.Join(", ", inputs[i]));
                        Console.Write(" \tTarget: " + outputs[i][0]);
                        Console.Write(" \tOutput: " + network.Compute(inputs[i])[0]);
                        Console.WriteLine();
                    }

                    Console.WriteLine("Error: " + error);
                }
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

        static (double[][] inputs, double[][] outputs) XorInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 }, new double[] { 1, 0 } };
            double[][] outputs = new double[][] { new double[] { 0 }, new double[] { 1 }, new double[] { 0 }, new double[] { 1 } };
            return (inputs, outputs);
        }

        static (double[][] inputs, double[][] outputs) SinInOuts()
        {
            double[][] inputs = new double[24][];
            for (int i = 0; i < 24; i++)
            {
                inputs[i] = new double[] { (Math.PI / 12) * i };
            }

            double[][] outputs = new double[24][];
            for (int i = 0; i < 24; i++)
            {
                outputs[i] = new double[] { Math.Sin(inputs[i][0])};
            }

            return (inputs, outputs);
        }
    }
}