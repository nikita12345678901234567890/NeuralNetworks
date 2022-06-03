﻿using Perceptron;

using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class Neuron
    {
        double bias;
        Dendrite[] dendrites;
        public double Output { get; set; }
        public double Input { get; private set; }
        public ActivationFunction Activation { get; set; }

        public Neuron(ActivationFunction activation, Neuron[] previousNerons)
        {
            dendrites = new Dendrite[previousNerons.Length];

            for (int i = 0; i < previousNerons.Length; i++)
            {
                dendrites[i] = new Dendrite(previousNerons[i], this, 1);
            }

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
            foreach (var dendrite in dendrites)
            {
                Input += dendrite.Compute();
            }
            Input += bias;

            Output = Activation.Function(Input);
            return Output;
        }

        public double Compute(double input)
        {
            Input = input;

            Output = Activation.Function(Input);
            return Output;
        }
    }
}
