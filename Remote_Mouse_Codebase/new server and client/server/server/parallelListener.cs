using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    class parallelListener
    {
        private frm_Main frm_Main;
        public TcpListener serverSocket = null;
        public TcpClient clientSocket = null;
        List<ClientDetails> listOfClients = new List<ClientDetails>();

        public parallelListener(frm_Main frm_Main)
        {
            this.frm_Main = frm_Main;
        }

        public void displayInMainForm(String message)
        {
            try
            {
                if (frm_Main.InvokeRequired)
                {
                    frm_Main.Invoke((MethodInvoker)delegate
                    {
                        frm_Main.displayLine(message);
                    });
                }
                else
                {
                    frm_Main.displayLine(message);
                }
            }
            catch (Exception)
            { }
        }

        public void listen()
        {
            String host = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            String ServerIP = "";
            foreach (IPAddress local in localIPs)
            {
                if (local.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (local.ToString().IndexOf("192.168.1.") != -1)
                    {
                        displayInMainForm("Server IP: " + local.ToString());
                        ServerIP = local.ToString();
                    }
                }
            }
            

            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            serverSocket = new TcpListener(IPAddress.Any, 6669);

            clientSocket = default(TcpClient);
            displayInMainForm("Listening Started");

            serverSocket.Start();
            displayInMainForm("Accepting connections from client");

            List<Thread> listOfServers = new List<Thread>();

            while (frm_Main.exitcode != "exit")
            {
                try
                {
                    clientSocket = serverSocket.AcceptTcpClient();

                    Server newServerObj = new Server(frm_Main, clientSocket, listOfClients);
                    Thread newServer = new Thread(new ThreadStart(newServerObj.main));
                    listOfServers.Add(newServer);
                    newServer.Start();
                }
                catch (Exception)
                { }
            }

            try
            {
                clientSocket.Close();
                serverSocket.Stop();
                foreach (Thread obj in listOfServers)
                    obj.Abort();
            }
            catch (Exception)
            { }

            displayInMainForm("Listening Stopped");
        }

    }
}
