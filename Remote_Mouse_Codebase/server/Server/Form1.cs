using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frm_Main : Form
    {
        private bool ServerStatus;
        parallelListener parallel;
        Thread thread = null;
        public string exitcode;

        public frm_Main()
        {
            InitializeComponent();

            resetServer();
        }

        private void resetServer()
        {
            try
            {
                exitcode = "exit";
                ServerStatus = false;
                btn_ServerOnOff.Text = "Turn Server On";

                if (thread != null)
                {
                    parallel.serverSocket.Stop();
                    if (parallel.clientSocket != null)
                        parallel.clientSocket.Close();
                }
            }
            catch (Exception ex)
            {
                displayLine(ex.Message);
            }
        }        

        private void btn_ServerOnOff_Click(object sender, EventArgs e)
        {
            if (!ServerStatus)
            {
                turnServerOn();
                ServerStatus = true;
            }
            else
            {
                turnServerOff();
                ServerStatus = false;
            }
        }

        public void displayLine(String message)
        {
            DateTime dt = DateTime.Now;
            String timestamp = "[" + dt.Date + " " + dt.TimeOfDay + "] ";
            txtb_ServerDisplay.AppendText("\r\n" + timestamp + " " + message);            
        }

        private void turnServerOff()
        {
            if (exitcode != "exit")
            {
                resetServer();

                displayLine("Server shutdown");
            }
        }

        private void turnServerOn()
        {
            try
            {
                exitcode = "";
                ServerStatus = true;
                btn_ServerOnOff.Text = "Turn Server Off";

                displayLine("Server started");                

                startListening();
            }
            catch (Exception ex)
            {
                displayLine(ex.Message);
            }
        }

        private void startListening()
        {
            parallel = new parallelListener(this);
            thread = new Thread(new ThreadStart(parallel.listen));
            thread.Start();
        }

        private void frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread.IsAlive)
                thread.Abort();
        }
    }
}
