using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            gfx.FillPolygon(new SolidBrush(color), points.Select((m) => new Point((int)(m.X * xCoefficent), (int)(m.Y * yCoefficent))).ToArray());
        }

        public void Mutate(Random random)
        {
            if (random.NextDouble() <= Constants.mutateColorChance)
            {
                switch (random.NextDouble())
                {
                    case < 0.25://I love this
                        color = Color.FromArgb(mutateColor(color.A, random), color);
                        break;

                    case < 0.5:
                        color = Color.FromArgb(color.A, mutateColor(color.R, random), color.G, color.B);
                        break;

                    case < 0.75:
                        color = Color.FromArgb(color.A, color.R, mutateColor(color.G, random), color.B);
                        break;

                    default:
                        color = Color.FromArgb(color.A, color.R, color.G, mutateColor(color.B, random));
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

        public int mutateColor(int value, Random random)
        {
            int result = value + random.Next(-Constants.mutateColorBounds, Constants.mutateColorBounds);

            if (value < 0) value = 0;
            else if (value > 255) value = 255;

            return value;
        }

        public static Triangle RandomTriangle(Random random)        
            => new Triangle(new PointF((float)random.NextDouble(), (float)random.NextDouble()), 
                            new PointF((float)random.NextDouble(), (float)random.NextDouble()), 
                            new PointF((float)random.NextDouble(), (float)random.NextDouble()), 
                            Color.FromArgb(random.Next(Constants.minAlpha, 255), random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));

        public Triangle copy()
        {
            Triangle fish = new Triangle(new PointF(points[0].X, points[0].Y), new PointF(points[1].X, points[1].Y), new PointF(points[2].X, points[2].Y), color);

            return fish;
        }
    }
}