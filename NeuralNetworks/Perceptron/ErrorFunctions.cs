using System;
using System.Collections.Generic;
using System.Text;

namespace Perceptron
{
    public static class ErrorFunctions
    {
        public static ErrorFunction MAE = new ErrorFunction((x, y) => Math.Abs(x - y), (x, y) => x < 0 ? -1 : 1);

        public static ErrorFunction MSE = new ErrorFunction((x, y) => Math.Pow(x - y, 2), (x, y) => 2*x);
    }
}
