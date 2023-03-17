using System;
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
            triangles = new List<Triangle>();
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
                    if (triangles.Count > 0)
                    {
                        triangles[random.Next(0, triangles.Count)].Mutate(random);
                    }
                    break;

                default://remove
                    if (triangles.Count > 0)
                    {
                        triangles.RemoveAt(random.Next(0, triangles.Count));
                    }
                    break;
            }
        }

        public Bitmap DrawImage(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);

            Graphics Gary = Graphics.FromImage(bitmap);
            Gary.Clear(Constants.backgroundColor);

            foreach (Triangle triangle in triangles)
            {
                triangle.DrawTriangle(Gary, width, height);
            }

            return bitmap;
        }

        /*
        public void CopyTo(TriangleArt other)
        {
            
        }
        */

        public double GetError()
        {
            System.Drawing.Imaging.BitmapData originalData = originalImage.LockBits(
                                                        new Rectangle(0, 0, (int)originalImage.HorizontalResolution, (int)originalImage.VerticalResolution), 
                                                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

            System.Drawing.Imaging.BitmapData MyData = originalImage.LockBits(
                                                        new Rectangle(0, 0, (int)originalImage.HorizontalResolution, (int)originalImage.VerticalResolution),
                                                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

            // Get the address of the first line.
            IntPtr ptr1 = originalData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(originalData.Stride) * (int)originalImage.VerticalResolution;
            byte[] rgbValues = new byte[bytes];


            for (int i = 0; i < rgbValues.Length; i+= 3)
            {
                rgbValues[i] = 255;
            }
                


            //Bitmap image = DrawImage(originalImage.Width, originalImage.Height);

            int sumError = 0;

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    int pixelErrorSum = (originalImage.GetPixel(x, y).A - image.GetPixel(x, y).A)
                                        + (originalImage.GetPixel(x, y).R - image.GetPixel(x, y).R)
                                        + (originalImage.GetPixel(x, y).G - image.GetPixel(x, y).G)
                                        + (originalImage.GetPixel(x, y).B - image.GetPixel(x, y).B);
                    int pixelError = pixelErrorSum * pixelErrorSum;
                    sumError += pixelError;
                }
            }

            return sumError / (originalImage.Width * originalImage.Height);
        }
    }
}