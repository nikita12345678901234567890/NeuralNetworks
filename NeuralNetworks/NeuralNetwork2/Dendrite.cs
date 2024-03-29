﻿using Perceptron;

using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork2
{
    public class Dendrite
    {
        public Neuron Previous { get; }
        public Neuron Next { get; }
        public double Weight { get; set; }
        public double WeightUpdate { get; set; }
        public double PrevWeightUpdate { get; set; }

        public Dendrite(Neuron previous, Neuron next, double weight)
        {
            Previous = previous;
            Next = next;
            Weight = weight;
        }

        public double Compute()
        {
            return Previous.Output * Weight;
        }
        
        public void ApplyUpdates(double momentum)
        {
            WeightUpdate += PrevWeightUpdate * momentum;
            Weight += WeightUpdate;
            PrevWeightUpdate = WeightUpdate;
            WeightUpdate = 0;
        }
    }
}