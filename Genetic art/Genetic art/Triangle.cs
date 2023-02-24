using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_art
{
    public class Triangle
    {
        Color color;
        PointF[] points = new PointF[3];

        public Triangle(PointF point0, PointF point1, PointF point2, Color color)
        {
            points[0] = point0;
            points[1] = point1;
            points[2] = point2;

            this.color = color;
        }

        public void DrawTriangle(Graphics gfx, float xCoefficent, float yCoefficent)
        {
            gfx.FillPolygon(new SolidBrush(color), points);
        }

        public void Mutate(Random random)
        {
            if (random.NextDouble() <= Constants.mutateColor)
            {
                switch (random.NextDouble())
                {
                    case < 0.25://I love this

                        break;

                    case < 0.5:

                        break;

                    case < 0.75:

                        break;

                    default:

                        break;
                }
            }
            else
            {
                
            }
        }

        public Triangle Copy()
        {
        
        }

        public static Triangle RandomTriangle(Random random)
        {
        
        }
    }
}