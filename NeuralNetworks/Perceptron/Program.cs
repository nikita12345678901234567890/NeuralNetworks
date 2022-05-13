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
            Perceptron perceptron = new Perceptron(inputs[0].Length, -1, 1, 0.001);

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
                        for (int j = 0; j < inputs[i].Length; j++)
                        {
                            if (j != 0)
                            {
                                Console.Write(", ");
                            }
                            Console.Write(inputs[i][j]);
                        }

                        Console.Write(" Output: " + Math.Round(perceptron.Compute(inputs[i]), 3));
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

                int ans = (int)Math.Round(perceptron.Compute(inputs[i]));
                Console.Write($" Output: {ans}");
                Console.WriteLine();
            }

           ;
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


        /*
        Perceptron perceptron = new Perceptron(2, -1, 1, 0.001);


        double[][] testValues = { new double[] { 0, 0 }, new double[] { 0, 1 }, new double[] { 1, 1 }, new double[] { 1, 0 } };
        double[] desiredValues = { 0, 0, 1, 0 };

        double currentError = double.MaxValue;
        int iterations = 0;
        const int MAX_ITERATIONS = 10000;
        while (currentError > 0 && iterations < MAX_ITERATIONS)
        {
            perceptron.TrainWithHillClimbing(testValues, desiredValues, ref currentError);
            iterations++;
        }

        Console.WriteLine((int)perceptron.Compute(new double[] { 0, 0 }));
        Console.WriteLine((int)perceptron.Compute(new double[] { 0, 1 }));
        Console.WriteLine((int)perceptron.Compute(new double[] { 1, 0 }));
        Console.WriteLine((int)perceptron.Compute(new double[] { 1, 1 }));
        
        */
    }
}