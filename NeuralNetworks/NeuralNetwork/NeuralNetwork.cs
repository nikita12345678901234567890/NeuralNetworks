using Perceptron;

using System;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public Layer[] Layers;
        public ErrorFunction Error;

        public NeuralNetwork(ActivationFunction activation, ErrorFunction error, params int[] neuronsPerLayer)
        {
            Layers = new Layer[neuronsPerLayer.Length];

            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                Layers[i] = new Layer(activation, neuronsPerLayer[i], i == 0 ? null : Layers[i - 1]);
            }

            Error = error;
        }

        public void Randomize(Random random, double min, double max)
        {
            foreach (var Layer in Layers)
            {
                Layer.Randomize(random, min, max);
            }
        }

        public double[] Compute(double[] inputs)
        {
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Compute(inputs);
            }

            return Layers[Layers.Length - 1].Outputs;
        }

        public double GetError(double[] inputs, double[] desiredOutputs)
        {
            var phish = Compute(inputs);

            double error = 0;
            for (int i = 0; i < desiredOutputs.Length; i++)
            {
                error += Error.Function(phish[i], desiredOutputs[i]);
            }

            return error;
        }
    }
}