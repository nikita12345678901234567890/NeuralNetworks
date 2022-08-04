using Perceptron;

using System;

namespace NeuralNetwork2
{
    public class Network
    {
        public Layer[] Layers;
        public ErrorFunction Error;

        public Network(ErrorFunction error, params (int, ActivationFunction)[] neuronsPerLayer)
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

        public void ApplyUpdates()
        {
            foreach (Layer layer in Layers)
            {
                layer.ApplyUpdates();
            }
        }

        public void Backprop(double learningRate, double[] desiredOutputs)
        {
            for (int i = 0; i < Layers[Layers.Length - 1].Neurons.Length; i++)
            {
                Layers[Layers.Length - 1].Neurons[i].Delta = Error.Derivative(Layers[Layers.Length - 1].Neurons[i].Output, desiredOutputs[i]);
            }

            for (int i = Layers.Length - 1; i > 0; i--)
            {
                Layers[i].Backprop(learningRate);
            }
        }

        public double Train(double[][] inputs, double[][] desiredOutputs, double learingRate)
        {
            double ErrorSum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                ErrorSum += GetError(inputs[i], desiredOutputs[i]);

                Backprop(learingRate, desiredOutputs[i]);
            }

            ApplyUpdates();

            return ErrorSum / inputs.Length;
        }
    }
}