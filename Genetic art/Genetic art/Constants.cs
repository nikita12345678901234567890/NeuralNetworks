using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_art
{
    public static class Constants
    {
        public const float mutateColorChance = 0.5f;//vs mutating points
        public const int mutateColorBounds = 25;//25;

        public const int minAlpha = 25;//for random triangle

        public const float addChance = 0.2f;
        public const float mutateChance = 0.7f + addChance;
        public const float removeChance = 1 - (addChance + mutateChance);

        public static Color backgroundColor = Color.White;

        public static int maxTriangles = 400;
    }
}