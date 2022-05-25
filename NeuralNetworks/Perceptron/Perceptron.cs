using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    public class Perceptron
    {
        public double[] weights;
        public double bias;
        public double mutationAmount;
        Random random = new Random();
        ActivationFunction activationFunction;
        ErrorFunction errorFunction;//implement these;

        public Perceptron(double[] initialWeightValues, double initialBiasValue, double mutationAmount)/*initializes the weights array and bias*/
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
            this.mutationAmount = mutationAmount;
        }

        public Perceptron(int amountOfInputs, double min, double max, double mutationAmount)/*Initializes the weights array given the amount of inputs*/
        {
            weights = new double[amountOfInputs];
            this.mutationAmount = mutationAmount;

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
            double sum = bias;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }

            return sum;
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
            var outputs = Compute(inputs);

            for (int j = 0; j < outputs.Length; j++)
            {
                sum += Math.Pow(outputs[j] - desiredOutputs[j], 2);
            }

            return (float)sum / inputs.Length;
        }

        public double TrainWithHillClimbing(double[][] inputs, double[] desiredOutputs, ref double currentError) //attempts one hill climbing training iteration and returns the new current error
        {
            double mut = random.NextDouble(-mutationAmount, mutationAmount);

            int index = random.Next(-1, weights.Length);
            if (index >= 0)
            {
                weights[index] += mut;
            }
            else
            {
                bias += mut;
            }

            double newError = GetError(inputs, desiredOutputs);

            if (newError < currentError)
            {
                currentError = newError;
                return newError;
            }
            else
            {
                if (index >= 0)
                {
                    weights[index] -= mut;
                }
                else
                {
                    bias -= mut;
                }
                return currentError;
            }
        }
    }

    public class ActivationFunction
    {
        Func<double, double> function;
        Func<double, double> derivative;

        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double input)
        {
            return function(input);
        }

        public double Derivative(double input)
        {
            return derivative(input);
        }
    }

    public class ErrorFunction
    {
        Func<double, double, double> function;
        Func<double, double, double> derivative;
        public ErrorFunction(Func<double, double, double> function, Func<double, double, double> derivative)
        {
            this.function = function;
            this.derivative = derivative;
        }

        public double Function(double output, double desiredOutput)
        {
            return function(output, desiredOutput);
        }
        public double Derivative(double output, double desiredOutput)
        {
            return derivative(output, desiredOutput);
        }
    }
}