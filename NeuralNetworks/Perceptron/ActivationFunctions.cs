using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    public static class ActivationFunctions
    {
        public static ActivationFunction Identity = new ActivationFunction(x => x, x => 1);

        public static ActivationFunction BinaryStep = new ActivationFunction(x => x < 0 ? 0 : 1, x => 0);

        public static ActivationFunction GateFilter = new ActivationFunction(x => (int)Math.Round(x), x => 1);

        public static ActivationFunction Sigmoid = new ActivationFunction(x => 1/(1 + Math.Pow(Math.E, -x)), x => Sigmoid.Function(x) * (1 - Sigmoid.Function(x)));

        public static ActivationFunction TanH = new ActivationFunction(x => Math.Tanh(x), x => 1 - Math.Pow(Math.Tanh(x), 2));

        public static ActivationFunction ReLU = new ActivationFunction(x => x <= 0 ? 0 : x, x => x <= 0 ? 0 : 1);
    }
}