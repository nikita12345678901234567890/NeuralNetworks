using Perceptron;

using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork2
{
    public class Neuron
    {
        public double bias;
        public Dendrite[] dendrites;

        public double Output { get; set; }
        public double Input { get; private set; }
        public ActivationFunction Activation { get; set; }

        public double Delta { get; set; }
        double biasUpdate;

        public Neuron(ActivationFunction activation, Neuron[] previousNerons)
        {
            dendrites = new Dendrite[previousNerons.Length];

            for (int i = 0; i < previousNerons.Length; i++)
            {
                dendrites[i] = new Dendrite(previousNerons[i], this, 1);
            }


            Activation = activation;
        }

        public Neuron(ActivationFunction activation)
        {
            dendrites = new Dendrite[0];

            Activation = activation;
        }

        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].Weight = random.NextDouble(min, max);
            }
            bias = random.NextDouble(min, max);
        }

        public double Compute()
        {
            Input = 0;
            foreach (var dendrite in dendrites)
            {
                Input += dendrite.Compute();
            }
            Input += bias;

            Output = Activation.Function(Input);

            Delta = 0;

            return Output;
        }

        public double Compute(double input)
        {
            Input = input;

            Output = Activation.Function(Input);

            Delta = 0;

            return Output;
        }

        public void setWeights(Dendrite[] Dendrites)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].Weight = Dendrites[i].Weight;
            }
        }

        public void ApplyUpdates()
        {
            bias *= biasUpdate;
            foreach (Dendrite dendrite in dendrites)
            {
                dendrite.ApplyUpdates();
            }
        }

        public void Backprop(double learningRate)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                double derivative = Delta * Activation.Derivative(Input) * dendrites[i].Previous.Output;

                dendrites[i].WeightUpdate = learningRate * -derivative;

                dendrites[i].Previous.Delta += Delta * Activation.Derivative(Input) * dendrites[i].Weight;
            }
        }
    }
}