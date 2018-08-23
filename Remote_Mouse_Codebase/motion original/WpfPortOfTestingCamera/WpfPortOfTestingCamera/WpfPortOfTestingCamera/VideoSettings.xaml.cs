using System;
using System.Windows;

using AForge.Video.DirectShow;

namespace WpfPortOfTestingCamera
{
    /// <summary>
    /// Interaction logic for VideoSettings.xaml
    /// </summary>
    public partial class VideoSettings : Window
    {
        VideoCaptureDevice _captureDevice;
        System.Drawing.Size _FrameSize;
        int _Fps;

        public System.Drawing.Size FrameSize
        {
            get
            {
                return _FrameSize;
            }
        }

        public int Fps
        {
            get
            {
                return _Fps;
            }
        }

        public VideoSettings(VideoCaptureDevice captureDevice)
        {
            InitializeComponent();
            _captureDevice = captureDevice;
            foreach (VideoCapabilities set in _captureDevice.VideoCapabilities)
            {
                comboBox1.Items.Add(String.Format("{0} x {1}", set.FrameSize.Width, set.FrameSize.Height));
            }
        }

        private void comboBox2_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            button1.IsEnabled = true;
            labelSelectedSize.Content = String.Format("{0} @ {1} FPS", comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _Fps = int.Parse(comboBox2.SelectedItem.ToString());
            _FrameSize = _captureDevice.VideoCapabilities[comboBox1.SelectedIndex].FrameSize;
            DialogResult = true;
        }

        private void comboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            button1.IsEnabled = false;
            labelSelectedSize.Content = "Select Framerate";
            comboBox2.Items.Clear();
            int MaxFramerate = _captureDevice.VideoCapabilities[comboBox1.SelectedIndex].MaxFrameRate;

            for (int i = 1; i < MaxFramerate; i++)
            {
                if (i % 5 == 0)
                {
                    comboBox2.Items.Add(i);
                }                
            }
            comboBox2.Items.Add(MaxFramerate);
            comboBox2.IsEnabled = true;
        }
    }
}
