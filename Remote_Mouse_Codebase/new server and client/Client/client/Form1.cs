using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class frm_main : Form
    {
        TcpClient clientSocket = null;
        public bool isAsleep;
        Thread wakeUp = null;

        public frm_main()
        {
            InitializeComponent();

            isAsleep = true;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (txtb_clientID.Text == "")
            {
                MessageBox.Show("The client ID cannot be empty");
                return;
            }
            setupConnection();

        }

        public void displayLine(String message)
        {
            txtb_display.AppendText(Environment.NewLine + message);
        }


        public void setupConnection()
        {     
            if (clientSocket != null)
                disconnect();

            displayLine("Client started");

            try
            {
                clientSocket = new TcpClient();
                displayLine("Checking IP address: 192.168.1.10");
                clientSocket.Connect("192.168.1.10", 6669);
                displayLine("Server Connected");

                writeToServer(txtb_clientID.Text);
                
                startPolling();
                return;
            }
            catch (Exception)
            {
                displayLine("No server found in that IP");
            }

            displayLine("Server Not Found");
        }

        public string readFromServer()
        {
            try
            {
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize];
                networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                networkStream.Flush();

                String dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                return dataFromClient.Substring(0, dataFromClient.IndexOf("$$"));
            }
            catch (Exception)
            { }
            return null;
        }

        private void writeToServer(String serverResponse)
        {
            try
            {
                NetworkStream networkStream = clientSocket.GetStream();

                serverResponse += "$$";
                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
            catch (Exception)
            { 

            }
        }

        private void disconnect()
        {
            try
            {
                if (clientSocket != null)
                {
                    clientSocket.Close();
                    clientSocket = null;
                }                
            }
            catch (Exception)
            { }
        }

        public void startPolling()
        {
            PollingThread obj = new PollingThread(this,clientSocket,txtb_clientID.Text);
            wakeUp = new Thread(new ThreadStart(obj.main));
            wakeUp.Start();
        }

        public void sleep()
        {
            isAsleep = true;
            btn_sleep.Text = "Wake";            
            writeToServer("sleep:" + txtb_NextClient.Text);           
        }

        public void wake()
        {
            isAsleep = false;
            btn_sleep.Text = "Sleep";
            writeToServer("wake");
        }

        private void btn_sleep_Click(object sender, EventArgs e)
        {
            if (!isAsleep)
            {
                txtb_NextClient.Enabled = false;
                sleep();
            }
            else
            {
                txtb_NextClient.Enabled = true;
                wake();
            }
        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (wakeUp.IsAlive)
                {
                    wakeUp.Abort();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
