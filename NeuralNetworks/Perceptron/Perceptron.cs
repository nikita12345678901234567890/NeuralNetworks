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
        ErrorFunction errorFunction;

        public Perceptron(double[] initialWeightValues, double initialBiasValue, double mutationAmount, ErrorFunction errorFunction, ActivationFunction activationFunction)/*initializes the weights array and bias*/
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
            this.mutationAmount = mutationAmount;

            this.errorFunction = errorFunction;
            this.activationFunction = activationFunction;
        }

        public Perceptron(int amountOfInputs, double min, double max, double mutationAmount, ErrorFunction errorFunction, ActivationFunction activationFunction)/*Initializes the weights array given the amount of inputs*/
        {
            weights = new double[amountOfInputs];
            this.mutationAmount = mutationAmount;

            Randomize(amountOfInputs, min, max);

            this.errorFunction = errorFunction;
            this.activationFunction = activationFunction;
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

            return activationFunction.Function(sum);
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

        public double ComputeWithoutActivation(double[] inputs)/*computes the output with given input*/
        {
            double sum = bias;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }

            return sum;
        }

        public double[] ComputeWithoutActivation(double[][] inputs)/*computes the output for each row of inputs*/
        {
            double[] results = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                results[i] = ComputeWithoutActivation(inputs[i]);
            }

            return results;
        }

        public double GetError(double[][] inputs, double[] desiredOutputs)/*computes the output using the inputs returns the average error between each output row and each desired output row using errorFunc*/
        {
            double sum = 0;
            var outputs = ComputeWithoutActivation(inputs);

            for (int j = 0; j < outputs.Length; j++)
            {
                sum += errorFunction.Function(outputs[j], desiredOutputs[j]);
            }

            return (float)sum / inputs.Length;
        }

        public void Train(double[] inputs, double desiredOutput, double learningRate)/*trains the perceptron using gradient descent for one iteration and returns the error */
        {
            for (int i = 0; i < weights.Length; i++)
            {
                double derivative = errorFunction.Derivative(ComputeWithoutActivation(inputs), desiredOutput) * activationFunction.Derivative(ComputeWithoutActivation(inputs)) * inputs[i];
                double changeInValue = learningRate * -derivative;
                weights[i] += changeInValue;
            }
            double biasDerivative = errorFunction.Derivative(ComputeWithoutActivation(inputs), desiredOutput) * activationFunction.Derivative(ComputeWithoutActivation(inputs));
            double changeInBiasValue = learningRate * -biasDerivative;
            bias += changeInBiasValue;
        }
        
        public double Train(double[][] inputs, double[] desiredOutputs, double learningRate)/*batch trains the perceptron using gradient descent for one iteration and returns the error */
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                Train(inputs[i], desiredOutputs[i], learningRate);
            }
            return GetError(inputs, desiredOutputs);
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