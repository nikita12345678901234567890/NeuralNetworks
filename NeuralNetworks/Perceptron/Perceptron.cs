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
                weights[i] = (double)(random.Next(min, max));
            }
        }

        public double Compute(double[] inputs)/*computes the output with given input*/
        {
            
        }

        public double[] Compute(double[][] inputs)/*computes the output for each row of inputs*/
        {
            
        }
    }
}
