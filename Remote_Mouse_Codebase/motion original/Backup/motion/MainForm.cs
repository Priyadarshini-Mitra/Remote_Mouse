// Motion Detector
//
// Copyright © Andrew Kirillov, 2005
// andrew.kirillov@gmail.com
//

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using videosource;

namespace motion
{
	/// <summary>
	/// Summary description for MainForm
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		// statistics
		private const int	statLength = 15;
		private int			statIndex = 0, statReady = 0;
		private int[]		statCount = new int[statLength];

        private MotionDetector1 detector = new MotionDetector1();
        private bool croppingEnabled = true;

		private System.Windows.Forms.MenuItem fileItem;
		private System.Windows.Forms.MenuItem openFileItem;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem exitFileItem;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Timers.Timer timer;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.StatusBarPanel fpsPanel;
		private System.Windows.Forms.Panel panel;
        private motion.CameraWindow cameraWindow;
		private System.Windows.Forms.MenuItem openURLFileItem;
		private System.Windows.Forms.MenuItem openMMSFileItem;
		private System.Windows.Forms.MenuItem openLocalFileItem;
        private System.Windows.Forms.MenuItem openMJEPGFileItem;
		private System.Windows.Forms.MenuItem helpItem;
        private System.Windows.Forms.MenuItem aboutHelpItem;
        private TrackBar thresholdTrackBar;
        private Label label1;
        private Label thresholdLabel;
        private IContainer components;
        private CheckBox invertCursorCheckBox;
        private Label label6;
        private CheckBox enableClickCheckBox;
        private CheckBox controlCursorCheckBox;
        private NumericUpDown resizeFactorUpDown;


        //Public variables
        public int threshold = 220;
        public int videoCropX = 0;
        public int videoCropY = 0;
        public int videoCropWidth = -1; 
        public int videoCropHeight = -1;
        public bool controlMouse = false;
        public bool enableClick = false;
        public bool invertCursorMovements = false;
        public int resizeFactor = 3;
        public float x1 = 0, y1 = 0;
        public float x2 = 0, y2 = 0;
        public bool leftMouseButtonDown = false;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileItem = new System.Windows.Forms.MenuItem();
            this.openFileItem = new System.Windows.Forms.MenuItem();
            this.openURLFileItem = new System.Windows.Forms.MenuItem();
            this.openMJEPGFileItem = new System.Windows.Forms.MenuItem();
            this.openMMSFileItem = new System.Windows.Forms.MenuItem();
            this.openLocalFileItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.exitFileItem = new System.Windows.Forms.MenuItem();
            this.helpItem = new System.Windows.Forms.MenuItem();
            this.aboutHelpItem = new System.Windows.Forms.MenuItem();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Timers.Timer();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.fpsPanel = new System.Windows.Forms.StatusBarPanel();
            this.panel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.resizeFactorUpDown = new System.Windows.Forms.NumericUpDown();
            this.invertCursorCheckBox = new System.Windows.Forms.CheckBox();
            this.enableClickCheckBox = new System.Windows.Forms.CheckBox();
            this.controlCursorCheckBox = new System.Windows.Forms.CheckBox();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.thresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.cameraWindow = new motion.CameraWindow();
            ((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPanel)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resizeFactorUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.fileItem,
            this.helpItem});
            // 
            // fileItem
            // 
            this.fileItem.Index = 0;
            this.fileItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.openFileItem,
            this.openURLFileItem,
            this.openMJEPGFileItem,
            this.openMMSFileItem,
            this.openLocalFileItem,
            this.menuItem1,
            this.exitFileItem});
            this.fileItem.Text = "&File";
            // 
            // openFileItem
            // 
            this.openFileItem.Index = 0;
            this.openFileItem.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.openFileItem.Text = "&Open";
            this.openFileItem.Click += new System.EventHandler(this.openFileItem_Click);
            // 
            // openURLFileItem
            // 
            this.openURLFileItem.Index = 1;
            this.openURLFileItem.Text = "Open JPEG &URL";
            this.openURLFileItem.Click += new System.EventHandler(this.openURLFileItem_Click);
            // 
            // openMJEPGFileItem
            // 
            this.openMJEPGFileItem.Index = 2;
            this.openMJEPGFileItem.Text = "Open M&JPEG URL";
            this.openMJEPGFileItem.Click += new System.EventHandler(this.openMJEPGFileItem_Click);
            // 
            // openMMSFileItem
            // 
            this.openMMSFileItem.Index = 3;
            this.openMMSFileItem.Text = "Open &MMS Stream";
            this.openMMSFileItem.Click += new System.EventHandler(this.openMMSFileItem_Click);
            // 
            // openLocalFileItem
            // 
            this.openLocalFileItem.Index = 4;
            this.openLocalFileItem.Text = "Open &Local Device";
            this.openLocalFileItem.Click += new System.EventHandler(this.openLocalFileItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 5;
            this.menuItem1.Text = "-";
            // 
            // exitFileItem
            // 
            this.exitFileItem.Index = 6;
            this.exitFileItem.Text = "E&xit";
            this.exitFileItem.Click += new System.EventHandler(this.exitFileItem_Click);
            // 
            // helpItem
            // 
            this.helpItem.Index = 1;
            this.helpItem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.aboutHelpItem});
            this.helpItem.Text = "&Help";
            // 
            // aboutHelpItem
            // 
            this.aboutHelpItem.Index = 0;
            this.aboutHelpItem.Text = "&About";
            this.aboutHelpItem.Click += new System.EventHandler(this.aboutHelpItem_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "AVI files (*.avi)|*.avi";
            this.ofd.Title = "Open movie";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.SynchronizingObject = this;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 406);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.fpsPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(344, 26);
            this.statusBar.TabIndex = 1;
            // 
            // fpsPanel
            // 
            this.fpsPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.fpsPanel.Name = "fpsPanel";
            this.fpsPanel.Width = 327;
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.resizeFactorUpDown);
            this.panel.Controls.Add(this.invertCursorCheckBox);
            this.panel.Controls.Add(this.enableClickCheckBox);
            this.panel.Controls.Add(this.controlCursorCheckBox);
            this.panel.Controls.Add(this.thresholdLabel);
            this.panel.Controls.Add(this.label1);
            this.panel.Controls.Add(this.thresholdTrackBar);
            this.panel.Controls.Add(this.cameraWindow);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(344, 406);
            this.panel.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(7, 262);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(193, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Image Height and Width Resize Factor:";
            // 
            // resizeFactorUpDown
            // 
            this.resizeFactorUpDown.Location = new System.Drawing.Point(208, 260);
            this.resizeFactorUpDown.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.resizeFactorUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.resizeFactorUpDown.Name = "resizeFactorUpDown";
            this.resizeFactorUpDown.Size = new System.Drawing.Size(42, 20);
            this.resizeFactorUpDown.TabIndex = 1;
            this.resizeFactorUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.resizeFactorUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.resizeFactorUpDown.ValueChanged += new System.EventHandler(this.resizeFactorUpDown_ValueChanged);
            // 
            // invertCursorCheckBox
            // 
            this.invertCursorCheckBox.AutoSize = true;
            this.invertCursorCheckBox.Enabled = false;
            this.invertCursorCheckBox.Location = new System.Drawing.Point(10, 311);
            this.invertCursorCheckBox.Name = "invertCursorCheckBox";
            this.invertCursorCheckBox.Size = new System.Drawing.Size(147, 17);
            this.invertCursorCheckBox.TabIndex = 22;
            this.invertCursorCheckBox.Text = "Invert Cursor  Movements";
            this.invertCursorCheckBox.UseVisualStyleBackColor = true;
            this.invertCursorCheckBox.CheckedChanged += new System.EventHandler(this.invertCursorCheckBox_CheckedChanged);
            // 
            // enableClickCheckBox
            // 
            this.enableClickCheckBox.AutoSize = true;
            this.enableClickCheckBox.Enabled = false;
            this.enableClickCheckBox.Location = new System.Drawing.Point(109, 288);
            this.enableClickCheckBox.Name = "enableClickCheckBox";
            this.enableClickCheckBox.Size = new System.Drawing.Size(85, 17);
            this.enableClickCheckBox.TabIndex = 21;
            this.enableClickCheckBox.Text = "Enable Click";
            this.enableClickCheckBox.UseVisualStyleBackColor = true;
            this.enableClickCheckBox.CheckedChanged += new System.EventHandler(this.enableClickCheckBox_CheckedChanged);
            // 
            // controlCursorCheckBox
            // 
            this.controlCursorCheckBox.AutoSize = true;
            this.controlCursorCheckBox.Location = new System.Drawing.Point(10, 287);
            this.controlCursorCheckBox.Name = "controlCursorCheckBox";
            this.controlCursorCheckBox.Size = new System.Drawing.Size(92, 17);
            this.controlCursorCheckBox.TabIndex = 20;
            this.controlCursorCheckBox.Text = "Control Cursor";
            this.controlCursorCheckBox.UseVisualStyleBackColor = true;
            this.controlCursorCheckBox.CheckedChanged += new System.EventHandler(this.controlCursorCheckBox_CheckedChanged);
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Location = new System.Drawing.Point(219, 341);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(111, 13);
            this.thresholdLabel.TabIndex = 4;
            this.thresholdLabel.Text = "Current threshold: 220";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(7, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brightness threshold:";
            // 
            // thresholdTrackBar
            // 
            this.thresholdTrackBar.Location = new System.Drawing.Point(10, 357);
            this.thresholdTrackBar.Maximum = 255;
            this.thresholdTrackBar.Minimum = 100;
            this.thresholdTrackBar.Name = "thresholdTrackBar";
            this.thresholdTrackBar.Size = new System.Drawing.Size(320, 45);
            this.thresholdTrackBar.SmallChange = 5;
            this.thresholdTrackBar.TabIndex = 0;
            this.thresholdTrackBar.TickFrequency = 5;
            this.thresholdTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.thresholdTrackBar.Value = 220;
            this.thresholdTrackBar.Scroll += new System.EventHandler(this.thresholdTrackBar_Scroll);
            // 
            // cameraWindow
            // 
            this.cameraWindow.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.cameraWindow.Camera = null;
            this.cameraWindow.Enabled = false;
            this.cameraWindow.Location = new System.Drawing.Point(10, 10);
            this.cameraWindow.Name = "cameraWindow";
            this.cameraWindow.Size = new System.Drawing.Size(320, 240);
            this.cameraWindow.TabIndex = 0;
            this.cameraWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.cameraWindow_MouseMove);
            this.cameraWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cameraWindow_MouseClick);
            this.cameraWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cameraWindow_MouseDown);
            this.cameraWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cameraWindow_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 432);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.statusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Laser Tracking and Control";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fpsPanel)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resizeFactorUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		// On form closing
		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CloseFile();		
		}

		// Close the main form
		private void exitFileItem_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		// On "Help->About"
		private void aboutHelpItem_Click(object sender, System.EventArgs e)
		{
			AboutForm form = new AboutForm();
			form.ShowDialog();
		}

		// Open file
		private void openFileItem_Click(object sender, System.EventArgs e)
		{
			if (ofd.ShowDialog() == DialogResult.OK)
			{               
				// create video source
				VideoFileSource fileSource = new VideoFileSource();
				fileSource.VideoSource = ofd.FileName;

				// open it
				OpenVideoSource(fileSource);

                // enable camera window
                cameraWindow.Enabled = true;
			}		
		}

		// Open URL
		private void openURLFileItem_Click(object sender, System.EventArgs e)
		{
			URLForm	form = new URLForm();

			form.Description = "Enter URL of an updating JPEG from a web camera:";
			form.URLs = new string[]
				{
					"http://aleksandriacamk1.it.helsinki.fi/axis-cgi/jpg/image.cgi?resolution=320x240",
					"http://stareat.it.helsinki.fi/axis-cgi/jpg/image.cgi?resolution=320x240",
					"http://194.18.89.220/axis-cgi/jpg/image.cgi?resolution=320x240",
					"http://212.247.228.34/axis-cgi/jpg/image.cgi?resolution=352x240"
				};

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				// create video source
				JPEGSource jpegSource = new JPEGSource();
				jpegSource.VideoSource = form.URL;
                jpegSource.Password = form.Password;
                jpegSource.Login = form.Name;
                jpegSource.PreAuthenticate = form.PreAuthenticate;

				// open it
				OpenVideoSource(jpegSource);

                // enable camera window
                cameraWindow.Enabled = true;
			}
		}

		// Open MJPEG URL
		private void openMJEPGFileItem_Click(object sender, System.EventArgs e)
		{
			URLForm	form = new URLForm();

			form.Description = "Enter URL of an MJPEG video stream:";
			form.URLs = new string[]
				{
					"http://hanselman.dyndns.org:81/mjpeg.cgi",
                    "http://sun.jerseyinsight.com/trafficbeaumont/nph-update.cgi",
					"http://peeper.axisinc.com/nph-manupdate.cgi",
					"http://marc15ter.vac.hu/nphMotionJpeg?Resolution=320x240&Quality=Standard",
					"http://213.200.232.69:8080/axis-cgi/mjpg/video.cgi?resolution=320x240"
				};

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				// create video source
				MJPEGSource mjpegSource = new MJPEGSource();
				mjpegSource.VideoSource = form.URL;
                mjpegSource.Login = form.Login;
                mjpegSource.Password = form.Password;
                mjpegSource.PreAuthenticate = form.PreAuthenticate;
                mjpegSource.AuthWithHomePage = form.AuthWithHomePage;

				// open it
				OpenVideoSource(mjpegSource);

                // enable camera window
                cameraWindow.Enabled = true;
			}
		}

		// Open MMS
		private void openMMSFileItem_Click(object sender, System.EventArgs e)
		{
			MMSForm	form = new MMSForm();

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				// create video source
				VideoStream mmsSource = new VideoStream();
				mmsSource.VideoSource = form.URL;

				// open it
				OpenVideoSource(mmsSource);

                // enable camera window
                cameraWindow.Enabled = true;
			}
		}

		// Open local capture device
		private void openLocalFileItem_Click(object sender, System.EventArgs e)
		{
			CaptureDeviceForm form = new CaptureDeviceForm();

			if (form.ShowDialog(this) == DialogResult.OK)
			{
				// create video source
				CaptureDevice localSource = new CaptureDevice();
				localSource.VideoSource = form.Device;

				// open it
				OpenVideoSource(localSource);

                // enable camera window
                cameraWindow.Enabled = true;
			}
		}

		// Open video source
		private void OpenVideoSource(IVideoSource source)
		{
			// set busy cursor
			this.Cursor = Cursors.WaitCursor;

			// close previous file
			CloseFile();

			// create camera
			Camera camera = new Camera(source, detector);
			// start camera
			camera.Start();            

			// attach camera to camera window
			cameraWindow.Camera = camera;

			// reset statistics
			statIndex = statReady = 0;

			// start timer
			timer.Start();

			this.Cursor = Cursors.Default;
		}

		// Close current file
		private void CloseFile()
		{
			Camera	camera = cameraWindow.Camera;

			if (camera != null)
			{
				// detach camera from camera window
				cameraWindow.Camera = null;

				// signal camera to stop
				camera.SignalToStop();
				// wait for the camera
				camera.WaitForStop();

				camera = null;

				if (detector != null)
					detector.Reset();
			}
		}

		// On timer event - gather statistic
		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Camera	camera = cameraWindow.Camera;

			if (camera != null)
			{
				// get number of frames for the last second
				statCount[statIndex] = camera.FramesReceived;

				// increment indexes
				if (++statIndex >= statLength)
					statIndex = 0;
				if (statReady < statLength)
					statReady++;

				float	fps = 0;

				// calculate average value
				for (int i = 0; i < statReady; i++)
				{
					fps += statCount[i];
				}
				fps /= statReady;

				statCount[statIndex] = 0;

				fpsPanel.Text = fps.ToString("F2") + " fps";
			}
		}


		// Update motion detector
		private void SetMotionDetector()
		{
			Camera	camera = cameraWindow.Camera;
		
			if (camera != null)
			{
				camera.Lock();
				camera.MotionDetector = detector;

				// reset statistics
				statIndex = statReady = 0;
				camera.Unlock();
			}
		}

        private void thresholdTrackBar_Scroll(object sender, EventArgs e)
        {
            // Update value on the label
            thresholdLabel.Text = "Current threshold: " + thresholdTrackBar.Value.ToString();
            threshold = thresholdTrackBar.Value;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            detector.mForm = this;
        }

        private void controlCursorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Control cursor
            controlMouse = controlCursorCheckBox.Checked;
            enableClickCheckBox.Enabled = controlCursorCheckBox.Checked;
            invertCursorCheckBox.Enabled = controlCursorCheckBox.Checked;
        }

        private void enableClickCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Enable/Disable clicking with laser
            enableClick = enableClickCheckBox.Checked;
        }

        private void invertCursorCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //Invert cursor movements
            invertCursorMovements = invertCursorCheckBox.Checked;
        }

        private void resizeFactorUpDown_ValueChanged(object sender, EventArgs e)
        {
            //Set resize factor
            resizeFactor = (int)resizeFactorUpDown.Value;
        }

        private void cameraWindow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Reset video cropping
                croppingEnabled = true;
                videoCropX = 0;
                videoCropY = 0;
                videoCropWidth = -1;
                videoCropHeight = -1;  
            }
        }

        private void cameraWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (croppingEnabled)
                {
                    //Compute location of x2 and y2 into camera coordinates
                    x2 = e.X * ((float)videoCropWidth / cameraWindow.Width);
                    y2 = e.Y * ((float)videoCropHeight / cameraWindow.Height);

                    //Set internal flag to know the user is dragging with the left mouse button down
                    leftMouseButtonDown = true;
                }
            }
        }

        private void cameraWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (croppingEnabled)
                {
                    //Compute location of x1 and y1 into camera coordinates
                    x1 = e.X * ((float)videoCropWidth / cameraWindow.Width);
                    y1 = e.Y * ((float)videoCropHeight / cameraWindow.Height);
                }
            }
        }

        private void cameraWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftMouseButtonDown)
            {
                if (croppingEnabled)
                {
                    //Set internal flag to know the user is not dragging
                    //with the left mouse button down anymore
                    leftMouseButtonDown = false;
                    
                    //Disable video cropping
                    croppingEnabled = false;

                    //Calculate width and height of selected region
                    int width = Math.Abs((int)x2 - (int)x1);
                    int height = Math.Abs((int)y2 - (int)y1);

                    if ((width == 0) || (height == 0))
                    {
                        //Cancel cropping
                        croppingEnabled = true;
                        return;
                    }

                    //Determine videoCropX and videoCropY

                    if (x2 < x1)
                        x1 = x2;

                    if (y2 < y1)
                        y1 = y2;

                    videoCropX = (int)x1;
                    videoCropY = (int)y1;

                    //Check if the region selected by the user is within bounds
                    if (videoCropX < 0)
                        videoCropX = 0;
                    else if ((videoCropX + width) > videoCropWidth)
                        width -= width - (videoCropWidth - videoCropX);

                    if (videoCropY < 0)
                        videoCropY = 0;
                    else if ((videoCropY + height) > videoCropHeight)
                        height -= height - (videoCropHeight - videoCropY);

                    //Set video cropping width and height
                    videoCropWidth = width;
                    videoCropHeight = height;
                }
            }
        }


	}
}
