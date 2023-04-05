using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                    if (triangles.Count > maxTriangles) triangles.RemoveAt(0);
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

        public void CopyTo(TriangleArt other)
        {
            other.triangles.Clear();

            for (int i = 0; i < triangles.Count; i++)
            {
                other.triangles.Add(triangles[i].copy());
            }
        }

        public double GetError()
        {
            ///*
            Bitmap image = DrawImage(originalImage.Width, originalImage.Height);

            int sumError = 0;

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    int pixelErrorSum = Math.Abs(originalImage.GetPixel(x, y).A - image.GetPixel(x, y).A)
                                        + Math.Abs(originalImage.GetPixel(x, y).R - image.GetPixel(x, y).R)
                                        + Math.Abs(originalImage.GetPixel(x, y).G - image.GetPixel(x, y).G)
                                        + Math.Abs(originalImage.GetPixel(x, y).B - image.GetPixel(x, y).B);
                    int pixelError = pixelErrorSum * pixelErrorSum;
                    sumError += pixelError;
                }
            }

            return sumError / (originalImage.Width * originalImage.Height);
            //*/

            /* Optimisation, do later
            System.Drawing.Imaging.BitmapData originalData = originalImage.LockBits(
                                                        new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

            System.Drawing.Imaging.BitmapData myData = DrawImage(originalImage.Width, originalImage.Height).LockBits(
                                                        new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

            // Get the address of the first line.
            IntPtr ptr1 = myData.Scan0;
            IntPtr ptr2 = originalData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes1 = Math.Abs(myData.Stride) * originalImage.Height;
            byte[] myRgbValues = new byte[bytes1];

            int bytes2 = Math.Abs(originalData.Stride) * originalImage.Height;
            byte[] originalRgbValues = new byte[bytes2];


            //Bitmap image = DrawImage(originalImage.Width, originalImage.Height);

            int sumError = 0;

            for (int i = 0; i < myRgbValues.Length; i++)
            {
                int pixelErrorSum = originalRgbValues[i] - myRgbValues[i];
                int pixelError = pixelErrorSum * pixelErrorSum;
                sumError += pixelError;
            }

            originalImage.UnlockBits(originalData);

            return (int)((double)sumError / (originalImage.Width * originalImage.Height));
            //*/
        }
    }
}