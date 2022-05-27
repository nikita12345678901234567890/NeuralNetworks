using System;
using System.Linq;

namespace Perceptron
{
    class Program
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            (double[][] inputs, double[] outputs) = OrAndInOuts();
            Perceptron perceptron = new Perceptron(inputs[0].Length, -1, 1, 0.1, ErrorFunctions.MSE, ActivationFunctions.GateFilter);

            double currentError = perceptron.GetError(inputs, outputs);
            bool debug = true;

            for (int x = 0; x < 20000 && currentError > 0; x++)
            {
                if(debug)
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < inputs.Length; i++)
                    {
                        Console.Write("Inputs: ");
                        Console.Write(string.Join(", ", inputs[i]));
                        Console.Write(" Output: " + Math.Round(perceptron.ComputeWithoutActivation(inputs[i]), 3));
                        Console.WriteLine();
                    }
                    Console.WriteLine("Error: " + Math.Round(currentError, 3));
                }

                perceptron.TrainWithHillClimbing(inputs, outputs, ref currentError);
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

                var ans = perceptron.Compute(inputs[i]);
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
    }
}