using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    public static class Extensions
    {
        public static double NextDouble(this Random random, double min, double max)
        {
            return min + random.NextDouble() * (max - min);
        }
    }
}