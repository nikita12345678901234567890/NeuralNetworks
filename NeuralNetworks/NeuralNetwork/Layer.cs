using Perceptron;

using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class Layer
    {
        public Neuron[] Neurons { get; }
        public double[] Outputs { get; }

        public Layer(ActivationFunction activation, int neuronCount, Layer previousLayer)
        {
            
        }

        public void Randomize(Random random, double min, double max)
        {

        }

        public double[] Compute()
        {

        }
    }
}
