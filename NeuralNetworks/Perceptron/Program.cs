﻿using System;
using System.Linq;

namespace Perceptron
{
    class Program
    {
        Random random = new Random();

        static void Main(string[] args)
        {
            Perceptron perceptron = new Perceptron(2, -1, 1);


            double[][] testValues = { new double[] { 0, 0 }, new double[] { 0 , 1 }, new double[] { 1, 1 }, new double[] { 1, 0 } };
            double[] desiredValues = { 0, 0, 1, 0};

            double currentError = perceptron.GetError(testValues, desiredValues);
            perceptron.TrainWithHillClimbing(new double[][]
            {

            }, new double[] { }, ref currentError);




            ////var results = perceptron.Compute(testValues);

            ///perceptron.

            //foreach (var result in results)
            //{
            //    Console.WriteLine(result);
            //}
        }
    }
}