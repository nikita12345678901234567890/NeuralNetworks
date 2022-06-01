using Perceptron;

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

        }

        public void Randomize(Random random, double min, double max)
        {

        }

        public double Compute()
        {

        }
    }
}
