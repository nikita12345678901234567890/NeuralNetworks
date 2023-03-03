﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_art
{
    public class TriangleArt
    {
        int maxTriangles;
        List<Triangle> triangles;
        Bitmap originalImage;
        public TriangleArt(int maxTriangles, Bitmap originalImage)
        {
            this.maxTriangles = maxTriangles;
            this.originalImage = originalImage;
        }

        public void Mutate(Random random)
        {
            switch (random.NextDouble())
            {
                case < Constants.addChance:
                    triangles.Add(Triangle.RandomTriangle(random));
                    if (triangles.Count >= maxTriangles) triangles.RemoveAt(0);
                    break;

                case < Constants.mutateChance:
                    triangles[random.Next(0, triangles.Count)].Mutate(random);
                    break;

                default://remove
                    triangles.RemoveAt(random.Next(0, triangles.Count));
                    break;
            }
        }

        public Bitmap DrawImage(int width, int height)
        {
            
        }

        public void CopyTo(TriangleArt other)
        {
            
        }

        public double GetError()
        {
            
        }
    }
}