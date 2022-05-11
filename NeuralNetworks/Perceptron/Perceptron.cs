using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    public class Perceptron
    {
        public double[] weights;
        public double bias;
        Random random = new Random();

        public Perceptron(double[] initialWeightValues, double initialBiasValue)/*initializes the weights array and bias*/ 
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
        }

        public Perceptron(int amountOfInputs, double min, double max)/*Initializes the weights array given the amount of inputs*/
        {
            weights = new double[amountOfInputs];

            Randomize(amountOfInputs, min, max);
        }

        public void Randomize(int numInputs, double min, double max)/*Randomly generates values for every weight including the bias*/
        {
            for (int i = 0; i < numInputs; i++)
            {
                weights[i] = random.NextDouble(min, max);
            }
            bias = random.NextDouble(min, max);
        }

        public double Compute(double[] inputs)/*computes the output with given input*/
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }

            return sum + bias;
        }

        public double[] Compute(double[][] inputs)/*computes the output for each row of inputs*/
        {
            double[] results = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                results[i] = Compute(inputs[i]);
            }

            return results;
        }

        public double GetError(double[][] inputs, double[] desiredOutputs)/*computes the output using the inputs returns the average error between each output row and each desired output row using errorFunc*/
        {
            double sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    double output = (int)((weights[j] * inputs[i][j]) + bias);

                    sum += Math.Pow(output - desiredOutputs[i], 2);
                }
            }

            return (float)sum / inputs.Length;
        }

        public double TrainWithHillClimbing(double[][] inputs, double[] desiredOutputs, ref double currentError) //attempts one hill climbing training iteration and returns the new current error
        {
            var prevWeights = weights;
            var prevBias = bias;

            if (random.Next(0, 2) == 0)
            {
                weights[random.Next(weights.Length)] += (random.Next(0, 2) == 0 ? 1 : -1);
            }
            else
            { 
                bias += (random.Next(0, 2) == 0 ? 1 : -1);
            }

            double newError = GetError(inputs, desiredOutputs);

            if (newError < currentError)
            {
                currentError = newError;
                return newError;
            }
            else
            {
                weights = prevWeights;
                bias = prevBias;
                return currentError;
            }
        }
    }
}
