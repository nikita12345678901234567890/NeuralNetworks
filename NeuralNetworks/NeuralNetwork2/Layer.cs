using Perceptron;

using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork2
{
    public class Layer
    {
        public Neuron[] Neurons { get; }

        public Layer(ActivationFunction activation, int neuronCount, Layer previousLayer)
        {
            Neurons = new Neuron[neuronCount];

            for (int i = 0; i < neuronCount; i++)
            {
                if (previousLayer != null)
                {
                    Neurons[i] = new Neuron(activation, previousLayer.Neurons);
                }
                else
                {
                    Neurons[i] = new Neuron(activation);
                }
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
            var Outputs = new double[Neurons.Length];

            for (int i = 0; i < Neurons.Length; i++)
            {
                Outputs[i] = Neurons[i].Compute();
            }

            return Outputs;
        }

        public double[] Compute(double[] inputs)
        {
            var Outputs = new double[Neurons.Length];

            if (Neurons[0].dendrites.Length == 0)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    Outputs[i] = Neurons[i].Compute(inputs[i]);
                }
            }
            else
            {
                for (int i = 0; i < Neurons.Length; i++)
                {
                    Outputs[i] = Neurons[i].Compute();
                }
            }

            return Outputs;
        }

        public void ApplyUpdates()
        {
            foreach (Neuron neuron in Neurons)
            {
                neuron.ApplyUpdates();
            }
        }
    }
}
