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
            if (random.NextDouble() <= Constants.mutateColorChance)
            {
                switch (random.NextDouble())
                {
                    case < 0.25://I love this
                        color = Color.FromArgb(color.A + random.Next(-Constants.mutateColorBounds, Constants.mutateColorBounds), color);
                        break;

                    case < 0.5:
                        color = Color.FromArgb(color.A, color.R + random.Next(-Constants.mutateColorBounds, Constants.mutateColorBounds), color.G, color.B);
                        break;

                    case < 0.75:
                        color = Color.FromArgb(color.A, color.R, color.G + random.Next(-Constants.mutateColorBounds, Constants.mutateColorBounds), color.B);
                        break;

                    default:
                        color = Color.FromArgb(color.A, color.R, color.G, color.B + random.Next(-Constants.mutateColorBounds, Constants.mutateColorBounds));
                        break;
                }
            }
            else
            {
                int point = (int)(random.NextDouble() * 2.999999f);

                if (random.NextDouble() < 0.5f)
                {
                    points[point].X = (float)random.NextDouble();
                }
                else
                {
                    points[point].Y = (float)random.NextDouble();
                }
            }
        }

        /*public Triangle Copy()
        {
            
        }*/

        public static Triangle RandomTriangle(Random random)
        {
            
        }
    }
}