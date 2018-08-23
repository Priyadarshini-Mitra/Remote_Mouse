using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Drawing;

using AForge;
using AForge.Math.Geometry;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace WpfPortOfTestingCamera
{
    class ImageProcessor
    {
        //Private
        Point _TargetCenter;

        //Events
        public delegate void NewTargetPositionHandler(IntPoint Center, Bitmap image);
        public event NewTargetPositionHandler NewTargetPosition;

        public ImageProcessor()
        {
            _TargetCenter = new Point(0, 0);
        }

        public void Process(UnmanagedImage uimage)
        {
            //Debug.WriteLine("Process Thread ID: {0}", Thread.CurrentThread.ManagedThreadId);
            //Kopia zapasowa
            Bitmap image = uimage.ToManagedImage();
            // 1 - Color Filter

            ColorFiltering filter = new ColorFiltering();
           // filter.Red = new IntRange(0,150);
            //filter.Green = new IntRange(170,255);
           // filter.Blue = new IntRange(0,190);
            filter.Red = new IntRange(0,100);
            filter.Green = new IntRange(140,200);
            filter.Blue = new IntRange(0,100);
            filter.ApplyInPlace(uimage);

            // 2 - grayscale image
            uimage = Grayscale.CommonAlgorithms.BT709.Apply(uimage);

            // 3 - treshold
            Threshold filterThreshold = new Threshold(180);
            //OtsuThreshold filterThreshold = new OtsuThreshold();
            filterThreshold.ApplyInPlace(uimage);

            // 4 - Blob counting
            BlobCounter blobCounter = new BlobCounter();
            blobCounter.FilterBlobs = true;
            blobCounter.MinWidth = 3;
            blobCounter.MinWidth = 3;
            blobCounter.MaxWidth = 320;
            blobCounter.MaxHeight = 160;

            blobCounter.ProcessImage(uimage);
            Blob[] blobs = blobCounter.GetObjectsInformation();



            Graphics g = Graphics.FromImage(image);
            Pen penRect = new Pen(Color.Red, 3);
            Pen penLine = new Pen(Color.Red, 3);
            IntPoint BigestCenter = new IntPoint(0, 0);

            if (blobs.Length > 0)
            {
                Blob BigestBlob = blobs[0];
                foreach (Blob blob in blobs)
                {
                    if (blob.Area > BigestBlob.Area)
                    {
                        BigestBlob = blob;
                    }
                }

                BigestCenter = (IntPoint)BigestBlob.CenterOfGravity;
                g.DrawRectangle(penRect, BigestBlob.Rectangle);
                g.DrawLine(penLine, BigestBlob.CenterOfGravity.X, 0, BigestBlob.CenterOfGravity.X, image.Height);
                g.DrawLine(penLine, 0, BigestBlob.CenterOfGravity.Y, image.Width, BigestBlob.CenterOfGravity.Y);
            }

            NewTargetPosition(BigestCenter, image);

            g.Dispose();
            penRect.Dispose();
            penLine.Dispose();
        }
    }
}