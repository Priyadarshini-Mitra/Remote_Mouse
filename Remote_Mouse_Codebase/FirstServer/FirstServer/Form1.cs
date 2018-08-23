using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Security.Principal;
using System.IO;

namespace FirstServer
{
    public partial class frm_Server : Form
    {
        parallelListener parallel;
        Thread thread = null;
        public volatile String exitcode;

        public frm_Server()
        {
            InitializeComponent();

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                runAsAdmin();
            }

            exitcode = "";

            if (File.Exists("ServerLog.txt"))
            {
                String contents = File.ReadAllText("ServerLog.txt");
                txtb_serverDisplay.Text = contents;
            }
        }

        private void runAsAdmin()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";

            try
            {
                Process.Start(proc);
            }
            catch
            {
                // The user refused the elevation.
                // Do nothing and return directly ...                
            }
            Environment.Exit(0);
        }

        public void displayLine(String message)
        {
            DateTime dt = DateTime.Now;
            String timestamp = "[" + dt.Date + " " + dt.TimeOfDay + "] ";
            txtb_serverDisplay.AppendText("\r\n" + timestamp + " " + message);

            File.AppendAllText("ServerLog.txt", Environment.NewLine + timestamp + " " + message);            
        }
              
        private void StartListening()
        {
            exitcode = "";            
            parallel = new parallelListener(this);            
            thread = new Thread(new ThreadStart(parallel.listen));            
            thread.Start();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            btn_stopServer.Enabled = true;
            btn_startListening.Enabled = false;
            StartListening();
        }

        private void shutdownServer()
        {
            exitcode = "exit";

            if (thread != null)
            {
                parallel.serverSocket.Stop();
                if (parallel.clientSocket != null)
                    parallel.clientSocket.Close();
            }
        }

        private void frm_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            shutdownServer();
        }

        private void btn_stopServer_Click(object sender, EventArgs e)
        {
            shutdownServer();
            btn_stopServer.Enabled = false;
            btn_startListening.Enabled = true;
        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            txtb_serverDisplay.Text = "";
            if (File.Exists("ArchivedServerLog.txt"))
            {
                String logcontents = File.ReadAllText("ServerLog.txt");
                File.AppendAllText("ArchivedServerLog.txt", logcontents);
                File.Delete("ServerLog.txt");
            }
            else
                File.Move("ServerLog.txt", "ArchivedServerLog.txt");
        }
    }
}
