/*
Copyright © Ashish Derhgawen, 2008
 
Written by Ashish Derhagwen (http://ashishrd.blogspot.com)
for MSDN: Coding4Fun (http://msdn.microsoft.com/coding4fun/)

E-mail: ashish.derhgawen@gmail.com
*/
namespace motion
{
    using System;
    using System.Drawing;

    using AForge.Imaging;
    using AForge.Imaging.Filters;
    using System.Collections;
    using System.Runtime.InteropServices;
    using System.Reflection;
    using Tiger.Video.VFW;
    using System.Windows.Forms;
using System.Drawing.Imaging;

    /// <summary>
    /// Laser Tracking
    /// </summary>
    public class MotionDetector1 : IMotionDetector
    {
        private bool leftMouseButtonDown = false;
        private int imageWidth, imageHeight;

        private MainForm _mForm; 

        public MainForm mForm
        {
            get { return _mForm; }
            set { _mForm = value; }
        }

        // Constructor
        public MotionDetector1()
        {
        }

        // Reset detector to initial state
        public void Reset()
        {
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;


        // Process new frame
        public void ProcessFrame(ref Bitmap image)
        {
            if (_mForm.leftMouseButtonDown == true)
            {
                //Code to draw a rubberband selection rectangle

                //Compute width and height of rectangle
                int w = Math.Abs((int)_mForm.x2 - (int)_mForm.x1);
                int h = Math.Abs((int)_mForm.y2 - (int)_mForm.y1);

                //Decide x and y of rectangle
                int x = 0, y = 0;

                if (_mForm.x1 < _mForm.x2)
                    x = (int)_mForm.x1;
                else if ((int)_mForm.x1 > _mForm.x2)
                    x = (int)_mForm.x2;

                if (_mForm.y1 < _mForm.y2)
                    y = (int)_mForm.y1;
                else if (_mForm.y1 > _mForm.y2)
                    y = (int)_mForm.y2;

                //Draw the rectangle
                Rectangle rect = new Rectangle(x, y, w, h);

                Graphics g = Graphics.FromImage((System.Drawing.Image)image);

                g.FillRectangle(new SolidBrush(Color.FromArgb
                    (60, 184, 184, 0)), rect); //Draws a semi-transparent rectangle using alpha blending

                g.Dispose();
                return;
            }

            if (_mForm.videoCropHeight == -1)
            {
                //Set or reset video cropping so that nothing gets trimmed out
                _mForm.videoCropHeight = image.Height;
                _mForm.videoCropWidth = image.Width;
            }

            //Crop the bitmap
            Bitmap tmpImage0 = image.Clone(new Rectangle(_mForm.videoCropX,
        _mForm.videoCropY, _mForm.videoCropWidth, _mForm.videoCropHeight),
        System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            //Resize the bitmap
            AForge.Imaging.Filters.Resize resize = new Resize(image.Width / _mForm.resizeFactor,
                image.Height / _mForm.resizeFactor, InterpolationMethod.NearestNeighbor);
                
            Bitmap tmpImage1 = resize.Apply(tmpImage0);

            //Get width and height of Bitmap
            imageWidth = tmpImage1.Width;
            imageHeight = tmpImage1.Height;
           
            tmpImage0.Dispose();
                   
            Utility.UnsafeBitmap uBitmap = new Utility.UnsafeBitmap(tmpImage1);

            //Locate the brightest pixel in the bitmap

            bool brightnessFound = false;
            
            uBitmap.LockBitmap();

            float brightest = 0;
            int xPos = 0, yPos = 0;

            for (int y = 0; y < imageHeight; y += 1)
            {
                for (int x = 0; x < imageWidth; x += 1)
                {
                    byte red, green, blue;
                    red = uBitmap.GetPixel(x, y).red;
                    green = uBitmap.GetPixel(x, y).green;
                    blue = uBitmap.GetPixel(x, y).blue;

                    float brightness = (299 * red + 587 * green + 114 * blue) / 1000;

                    if (brightness > _mForm.threshold)
                    {
                        brightest = brightness;
                        xPos = x;
                        yPos = y;

                        brightnessFound = true;
                    } // (brightest > _mForm.threshold)

                } // x loop
            } // y loop

            if (brightnessFound == true)
            {                
                uBitmap.UnlockBitmap();

                //Encircle the brightest pixel
                Graphics dc = Graphics.FromImage(tmpImage1);
                Pen p = new Pen(Color.LimeGreen, 1);

                dc.DrawEllipse(p, xPos - 5, yPos - 5, 10, 10);

                //Show the x and y coordinates of the brightest pixel
                dc.DrawString("X: " + xPos.ToString() + ", Y: " +
                    yPos.ToString(), new Font("Verdana", 7, FontStyle.Regular),
                    Brushes.Yellow, new Point(xPos, yPos + 8));

                if (_mForm.controlMouse == true)
                    ControlCursor(xPos, yPos, _mForm.enableClick); //Set cursor position

                dc.Dispose();

                uBitmap.LockBitmap();

            }
            else
            {
                if (_mForm.enableClick == true && leftMouseButtonDown == true)
                {
                    //Generate a left mouse button click
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    leftMouseButtonDown = false;
                }
            }

            uBitmap.UnlockBitmap();
            uBitmap.Bitmap.Dispose();

            image.Dispose();
            image = tmpImage1;            
        }

        private void ControlCursor(int x, int y, bool click)
        {
            //Get screen width and height
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height + 34;

            float cursorX, cursorY;

            //Compute location of laser dot into camera coordinates
            if (_mForm.invertCursorMovements == true)
            {
                cursorX = screenWidth - (((float)screenWidth / imageWidth) * x);
                cursorY = screenHeight - (((float)screenHeight / imageHeight) * y);
            }
            else
            {
                cursorX = ((float)screenWidth / imageWidth) * x;
                cursorY = ((float)screenHeight / imageHeight) * y;
            }

            //Set cursor position
            Cursor.Position = new Point((int)cursorX, (int)cursorY);

            if (click == true)
            {
                leftMouseButtonDown = true;
            }

        }
    }
}