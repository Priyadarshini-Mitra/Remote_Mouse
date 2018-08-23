using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstServer
{
    class parallelListener
    {
        private frm_Server form;
        public TcpListener serverSocket=null;
        public TcpClient clientSocket=null;
        List<ClientDetails> listOfClients = new List<ClientDetails>();        

        public parallelListener(frm_Server _form)
        {
            form = _form;            
        }

        public void displayInMainForm(String message)
        {
            try
            {
                if (form.InvokeRequired)
                {
                    form.Invoke((MethodInvoker)delegate
                    {
                        form.displayLine(message);
                    });
                }
                else
                {
                    form.displayLine(message);
                }
            }
            catch (Exception)
            { }
        }

        public void listen()
        {
            string host = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            String publicServerIP = "";
            String privateServerIP = "";

            /*string url = "http://checkip.dyndns.org";
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            System.Net.WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            string a4 = a3[0];

            displayInMainForm("Server Public IP: " + a4);

            publicServerIP = a4;*/

            foreach (IPAddress local in localIPs)
            {
                if (local.AddressFamily == AddressFamily.InterNetwork)
                {
                    if (local.ToString().IndexOf("192.168.1.") != -1)
                    {
                        displayInMainForm("Server IP: " + local.ToString());
                        privateServerIP = local.ToString();
                    }
                }
            }

            IPAddress localAddr = IPAddress.Parse("0.0.0.0");
            serverSocket = new TcpListener(IPAddress.Any,6669);
            
            clientSocket = default(TcpClient);
            displayInMainForm("Server Started");            
           
            serverSocket.Start();
            displayInMainForm("Accepting connections from client");

            List<Thread> listOfServers = new List<Thread>();            

            while (form.exitcode != "exit")
            {
                try
                {
                    clientSocket = serverSocket.AcceptTcpClient();

                    Server newServerObj = new Server(form, clientSocket, listOfClients);
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
                foreach(Thread obj in listOfServers)                
                    obj.Abort();
            }
            catch (Exception)
            { }

            displayInMainForm("Server Shutdown");                
        }
    }
}
