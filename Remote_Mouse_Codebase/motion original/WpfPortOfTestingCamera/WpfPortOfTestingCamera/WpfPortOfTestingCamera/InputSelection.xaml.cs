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
using System.Windows.Shapes;

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
    /// Interaction logic for InputSelection.xaml
    /// </summary>
    public partial class InputSelection : Window
    {
        VideoCaptureDevice _captureDevice;
        FilterInfoCollection _videoDevices;

        public VideoCaptureDevice CaptureDevice
        {
            get
            {
                return _captureDevice;
            }
        }

        public InputSelection()
        {
            InitializeComponent();
            // show device list
            try
            {
                // enumerate video devices
                _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (_videoDevices.Count == 0)
                    throw new ApplicationException();

                // add all devices to combo
                foreach (FilterInfo device in _videoDevices)
                {
                    comboBox1.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                comboBox1.Items.Add("No local capture devices");
                comboBox1.IsEnabled = false;
                ButtonOK.IsEnabled = false;
                //DialogResult = false;
            }

            comboBox1.SelectedIndex = 0;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            _captureDevice = new VideoCaptureDevice(_videoDevices[comboBox1.SelectedIndex].MonikerString);
            DialogResult = true;
        }
    }
}
