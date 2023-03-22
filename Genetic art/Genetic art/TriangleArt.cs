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

            System.Drawing.Imaging.BitmapData myData = DrawImage(originalImage.Width, originalImage.Height).LockBits(
                                                        new Rectangle(0, 0, (int)originalImage.HorizontalResolution, (int)originalImage.VerticalResolution),
                                                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);

            // Get the address of the first line.
            IntPtr ptr1 = myData.Scan0;
            IntPtr ptr2 = originalData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes1 = Math.Abs(myData.Stride) * (int)originalImage.VerticalResolution;
            byte[] myRgbValues = new byte[bytes1];

            int bytes2 = Math.Abs(originalData.Stride) * (int)originalImage.VerticalResolution;
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

            return sumError / (originalImage.HorizontalResolution * originalImage.VerticalResolution);
        }
    }
}