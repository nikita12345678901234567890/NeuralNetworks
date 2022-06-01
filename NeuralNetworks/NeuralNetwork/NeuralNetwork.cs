using Perceptron;

using System;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        Layer[] layers;
        ErrorFunction errorFunc;

        public NeuralNetwork(ActivationFunction activation, ErrorFunction errorFunc, params int[] neuronsPerLayer)
        {

        }

        public void Randomize(Random random, double min, double max)
        {

        }

        public double[] Compute(double[] inputs)
        {

        }

        public double GetError(double[] inputs, double[] desiredOutputs)
        {

        }

    }
}
