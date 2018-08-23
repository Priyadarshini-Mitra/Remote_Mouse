using CerebroClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace firstClient
{
    public partial class Client : Form
    {
        TcpClient clientSocket;
        String myIP = "";
        String name = "";
        PollingThread pollingThread=null;
        Thread polling = null;        

        public Client()
        {
            InitializeComponent();
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                runAsAdmin();
            }

            cmb_clientList.Items.Add("None");
            cmb_clientList.SelectedIndex = 0;

            if (CerebroClient.Properties.Settings.Default.AutoConnect)            
                setupConnection();

            txtb_clientView.Text = CerebroClient.Properties.Settings.Default.TextLog;
        }

        private void getName()
        {
            if (CerebroClient.Properties.Settings.Default.UserSetName == "")
            {
                getMyIP();
                txtb_name.Text = myIP;
            }
            else
            {
                txtb_name.Text = CerebroClient.Properties.Settings.Default.UserSetName;
                name = txtb_name.Text;

                try
                {
                     NetworkStream serverStream = clientSocket.GetStream();
                     byte[] outStream = Encoding.ASCII.GetBytes(myIP + ":SetName:" + txtb_name.Text + "$$");
                     serverStream.Write(outStream, 0, outStream.Length);
                     serverStream.Flush();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
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
            txtb_clientDisplay.AppendText("\r\n" + message);
        }

        public void displayClientView(String message)
        {
            txtb_clientView.AppendText("\r\n" + message);

            CerebroClient.Properties.Settings.Default.TextLog += Environment.NewLine + message;
            CerebroClient.Properties.Settings.Default.Save();
        }

        private void getMyIP()
        {
            string host = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            
            foreach (IPAddress local in localIPs)
            {
                if (local.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (local.ToString().IndexOf("192.168.1.") != -1)                                            
                        myIP = local.ToString();                    
                }
            }
        }

        public void setupConnection()
        {
            displayLine("Client started");
            displayLine("Polling IP addresses");
                           
            try
            { 
                clientSocket = new TcpClient();
                displayLine("Checking IP address:" + CerebroClient.Properties.Settings.Default.ServerIP);                
                clientSocket.Connect(CerebroClient.Properties.Settings.Default.ServerIP, 6669);                                
                displayLine("Server Connected");

                getMyIP();                
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(myIP + "$$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                refreshConnectedClientList();              
                               
                pollingThread = new PollingThread(this, myIP, clientSocket);
                polling = new Thread(new ThreadStart(pollingThread.poll));
                polling.Start();
                
                getName(); 
                return;
            }
            catch (Exception)
            {               
                displayLine("No server found in that IP");
            }
                        
            displayLine("Server Not Found");
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (cmb_clientList.Text == "None")
                return;
            try
            {
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(myIP + ":" + cmb_clientList.Text + ":" + txtb_clientChat.Text + "$$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                displayClientView("ME: " + txtb_clientChat.Text);
                txtb_clientChat.Text = "";

                byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                string returnData = Encoding.ASCII.GetString(inStream);

                returnData = returnData.Substring(0, returnData.IndexOf("$$"));
                if (returnData == "NotAvailable")
                {
                    displayClientView("Unable to Deliver message. " + cmb_clientList.Text + " Is Offline");
                    refreshConnectedClientList();
                }
            }
            catch (Exception)
            {
                displayClientView("Connection Terminated....");
            }
        }        

        public void refreshConnectedClientList()
        {
            try
            { 
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes("ClientList" + "$$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                string returnData = Encoding.ASCII.GetString(inStream);

                int count = cmb_clientList.Items.Count;
                for (int i = 0; i < count; i++)
                    cmb_clientList.Items.RemoveAt(0);                

                String[] users = returnData.Split(':');

                bool flag = false;

                for (int i = 0; i < users.Length - 1; i++)
                {
                    if (users[i] != myIP && users[i] != name)
                    {
                        flag = true;                       
                        
                        cmb_clientList.Items.Add(users[i].Trim());
                    }
                }
                if (flag == false)
                    cmb_clientList.Items.Add("None");
                cmb_clientList.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                displayClientView("Server Disconnected... " + ex.Message);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            refreshConnectedClientList();
        }        

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (polling != null && polling.IsAlive)
                polling.Abort();
        }

        private void btn_setName_Click(object sender, EventArgs e)
        {
            CerebroClient.Properties.Settings.Default.UserSetName = txtb_name.Text;
            CerebroClient.Properties.Settings.Default.Save();
            name = txtb_name.Text;

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(myIP + ":SetName:" + txtb_name.Text + "$$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();          
        }       

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtb_name.Text=name;        
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientSocket.Close();
            myIP = "";
            name = "";
            pollingThread = null;
            if (polling != null)
                polling.Abort();
            polling = null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void connectToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerebroClient.SetServerForm obj = new CerebroClient.SetServerForm(this);
            obj.Show();
        }

        private void btn_attachFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.FileName = "";
            if (openfiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NetworkStream serverStream = clientSocket.GetStream();
                displayClientView("Sending " + Path.GetFileName(openfiledialog.FileName) + " To server");
                int lenghtOfFile = File.ReadAllBytes(openfiledialog.FileName).Length;
                byte[] outStream = Encoding.ASCII.GetBytes(myIP + ":SendFile:" + cmb_clientList.Text + ":" + Path.GetFileName(openfiledialog.FileName) +":"+ lenghtOfFile.ToString()+"$$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                serverStream.Write(File.ReadAllBytes(openfiledialog.FileName), 0, lenghtOfFile);
                serverStream.Flush();
            }
        }

        private void btn_clearLog_Click(object sender, EventArgs e)
        {
            txtb_clientView.Text = "";
            CerebroClient.Properties.Settings.Default.TextLog = "";
            CerebroClient.Properties.Settings.Default.Save();
        }        
    }
}
