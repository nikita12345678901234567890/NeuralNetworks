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
            Neurons = new Neuron[neuronCount];
            Outputs = new double[neuronCount];

            for (int i = 0; i < neuronCount && previousLayer != null; i++)
            {
                Neurons[i] = new Neuron(activation, previousLayer.Neurons);
            }
        }

        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Randomize(random, min, max);
            }
        }

        public double[] Compute()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Outputs[i] = Neurons[i].Compute();
            }

            return Outputs;
        }

        public double[] Compute(double[] inputs)
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Outputs[i] = Neurons[i].Compute(inputs[i]);
            }

            return Outputs;
        }
    }
}