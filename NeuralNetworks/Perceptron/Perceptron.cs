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

        public Perceptron(int amountOfInputs)/*Initializes the weights array given the amount of inputs*/
        {
            
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
                double output = (int)((weights[i] * inputs[i]) + b);

                sum += Math.Pow(y - Points[i].Y, 2);
            }

            return (float)sum / Points.Count;
        }

        public double TrainWithHillClimbing(double[][] inputs, double[] desiredOutputs, double currentError)/*attempts one hill climbing training iteration and returns the new current error*/ 
        { 
        
        }
    }
}
