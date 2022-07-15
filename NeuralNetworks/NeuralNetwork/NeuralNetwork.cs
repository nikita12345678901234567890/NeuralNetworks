using Perceptron;
using System;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public Layer[] Layers;
        public ErrorFunction Error;

        public NeuralNetwork(ErrorFunction error, params (int, ActivationFunction)[] neuronsPerLayer)
        {
            Layers = new Layer[neuronsPerLayer.Length];

            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                Layers[i] = new Layer(neuronsPerLayer[i].Item2, neuronsPerLayer[i].Item1, i == 0 ? null : Layers[i - 1]);
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
            double[] stuff = inputs;
            for (int i = 0; i < Layers.Length - 1; i++)
            {
                stuff = Layers[i].Compute(stuff);
            }

            return Layers[Layers.Length - 1].Compute();
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