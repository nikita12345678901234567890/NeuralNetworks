using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    public static class ActivationFunctions
    {
        public static ActivationFunction Identity = new ActivationFunction(x => x, x => 1);

        public static ActivationFunction BinaryStep = new ActivationFunction(x => x < 0 ? 0 : 1, x => 0);

        public static ActivationFunction GateFilter = new ActivationFunction(x => (int)Math.Round(x), x => 0);
    }
}