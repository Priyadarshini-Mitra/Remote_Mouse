using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Windows.Threading;

using AForge;
using AForge.Math.Geometry;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision;

namespace WpfPortOfTestingCamera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Private
        ImageProcessor CamProc;
        VideoCaptureDevice captureDevice;

        public MainWindow()
        {
            InitializeComponent();
            CamProc = new ImageProcessor();
            CamProc.NewTargetPosition += new ImageProcessor.NewTargetPositionHandler(CamProc_NewTargetPosition);
            //Dispatcher.BeginInvoke((Action)(() => { Window_Loaded_Settings(); }), DispatcherPriority.Loaded, null);
            Dispatcher.BeginInvoke((Action)Window_Loaded_Settings, DispatcherPriority.Loaded, null);
        }

        void CamProc_NewTargetPosition(IntPoint Center, System.Drawing.Bitmap image)
        {
            IntPtr hBitMap = image.GetHbitmap();
            BitmapSource bmaps = Imaging.CreateBitmapSourceFromHBitmap(hBitMap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            bmaps.Freeze();

            Dispatcher.Invoke((Action)(() =>
            {
                labelX.Content = String.Format("X: {0}", Center.X);
                labelY.Content = String.Format("Y: {0}", Center.Y);
                pictureBoxMain.Source = bmaps;
            }), DispatcherPriority.Render, null);
        }

        private void Window_Loaded_Settings()
        {
            InputSelection impdevform = new InputSelection();
            impdevform.Owner = this;
            if ((bool)impdevform.ShowDialog())
            {
                captureDevice = impdevform.CaptureDevice;
                captureDevice.NewFrame += new NewFrameEventHandler(captureDevice_NewFrame);
                buttonVideoProperties.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else
            {
                if (Microsoft.Windows.Controls.MessageBox.Show("No video devices connected.\nTry again?", "No devices", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Window_Loaded_Settings();
                    return;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        void captureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            UnmanagedImage uimage = UnmanagedImage.FromManagedImage(eventArgs.Frame);
            try
            {
                CamProc.Process(uimage);
                //pictureBoxMain.Image = bitmap;
            }
            catch { }
        }

        private void buttonVideoProperties_Click(object sender, RoutedEventArgs e)
        {
            VideoSettings form = new VideoSettings(captureDevice);
            form.Owner = this;
            if ((bool)form.ShowDialog())
            {
                captureDevice.DesiredFrameSize = form.FrameSize;
                captureDevice.DesiredFrameRate = form.Fps;
            }
        }

        private void buttonWebCamSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                captureDevice.DisplayPropertyPage(hwnd);
            }
            catch { }
        }

        private void buttonStartStop_Click(object sender, RoutedEventArgs e)
        {
            if (captureDevice.IsRunning)
            {
                captureDevice.SignalToStop();
                buttonStartStop.Content = "Start";
                buttonVideoProperties.IsEnabled = true;
            }
            else
            {                
                captureDevice.Start();
                buttonStartStop.Content = "Stop";
                buttonVideoProperties.IsEnabled = false;
            }
        }
    }
}
