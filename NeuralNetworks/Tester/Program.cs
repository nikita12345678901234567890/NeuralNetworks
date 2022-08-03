using NeuralNetwork2;

using Perceptron;

using System;

namespace Tester
{
    class Program
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            (double[][] inputs, double[] outputs) = OrAndInOuts();
            Network network = new Network(ErrorFunctions.MSE, (2, ActivationFunctions.Sigmoid), (2, ActivationFunctions.Sigmoid), (1, ActivationFunctions.BinaryStep));

            bool debug = true;

            for (int x = 0; x < 100; x++)
            {
                if (debug)
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        Console.Write("Inputs: ");
                        Console.Write(string.Join(", ", inputs[i]));
                        Console.Write(" Output: " + network.Compute(inputs[i]));
                        Console.WriteLine();
                    }
                    Console.WriteLine("Error: " + Math.Round(network.GetError(inputs[i], outputs), 3));
                }

                network.
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

                var ans = network.Compute(inputs[i]);
                Console.Write($" Output: {ans}");
                Console.WriteLine();
            }
        }



        static (double[][] inputs, double[] outputs) AndInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 }, new double[] { 1, 0 } };
            double[] outputs = new double[] { 0, 0, 1, 0 };
            return (inputs, outputs);
        }

        static (double[][] inputs, double[] outputs) OrInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0 }, new double[] { 1, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 } };
            double[] outputs = new double[] { 0, 1, 1, 1 };
            return (inputs, outputs);
        }

        static (double[][] inputs, double[] outputs) OrAndInOuts()
        {
            double[][] inputs = new double[][] { new double[] { 0, 0, 0 }, new double[] { 1, 0, 0 }, new double[] { 0, 1, 0 }, new double[] { 1, 1, 0 },
                                               new double[] { 0, 0, 1 }, new double[] { 1, 0, 1 }, new double[] { 0, 1, 1 }, new double[] { 1, 1, 1 }};
            double[] outputs = new double[] { 0, 1, 1, 1,
                                          0, 0, 0, 1 };
            return (inputs, outputs);
        }


        public void Train()
        {
            
        }
    }
}
