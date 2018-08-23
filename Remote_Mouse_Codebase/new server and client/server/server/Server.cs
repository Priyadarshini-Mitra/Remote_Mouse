using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    class Server
    {
        private frm_Main frm_Main;
        private System.Net.Sockets.TcpClient clientSocket;
        private List<ClientDetails> listOfClients;
        private ClientDetails ClientID;

        public Server(frm_Main frm_Main, System.Net.Sockets.TcpClient clientSocket, List<ClientDetails> listOfClients)
        {            
            this.frm_Main = frm_Main;
            this.clientSocket = clientSocket;
            this.listOfClients = listOfClients;   
        }

        private string readFromClient()
        {
            NetworkStream networkStream = clientSocket.GetStream();
            byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize];
            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
            networkStream.Flush();

            String dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
            return dataFromClient.Substring(0, dataFromClient.IndexOf("$$"));
        }

        private void writeToClient(String serverResponse)
        {
            NetworkStream networkStream = clientSocket.GetStream();

            serverResponse+= "$$";
            byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
        }

        public void main()
        {
            string _ClientID = "";
            _ClientID = readFromClient(); 
            ClientID = new ClientDetails(_ClientID, clientSocket);            
            listOfClients.Add(ClientID);

            displayInMainForm("Client with User ID: " + ClientID.ClientID + " Logged in");

            String serverResponse;

            try
            {
                while (true)
                {
                    String dataFromClient = readFromClient();

                    displayInMainForm(dataFromClient);

                    if (dataFromClient.Contains("sleep"))
                    {
                        serverResponse = "Clear to sleep";
                        writeToClient(serverResponse);
                        //displayInMainForm("Writing " + serverResponse + " to " + ClientID.ClientID);

                        ClientID.status = "Asleep";

                        ClientDetails selectedClient = null;                        
                        string nextID = dataFromClient.Split(':')[1];
                        displayInMainForm("Trying to wake " + nextID);

                        if (listOfClients.Count > 1)
                        {
                            foreach (ClientDetails client in listOfClients)
                            {
                                if (client.ClientID == nextID)
                                    selectedClient = client;
                            }

                            if (selectedClient != null)
                            {
                                displayInMainForm("Waking client " + selectedClient.ClientID);

                                selectedClient.status = "Awake";
                            }
                        }
                    }
                    else if (dataFromClient == "wake")
                    {
                        ClientID.status = "Awake";
                    }
                    else if (dataFromClient.Contains("DataFromOthers"))
                    {
                        String id = dataFromClient.Split(':')[0];
                        serverResponse = "";

                        foreach (ClientDetails client in listOfClients)
                        {
                            if (client.ClientID == id)
                            {

                                displayInMainForm("Writing " + client.status + " to " + client.ClientID);
                                NetworkStream networkStream = client.clientSocket.GetStream();

                                serverResponse = client.status + "$$";

                                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                                networkStream.Write(sendBytes, 0, sendBytes.Length);
                                networkStream.Flush();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                displayInMainForm(ClientID.ClientID + " Disconnected ");
                listOfClients.Remove(ClientID);
            }
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
    }
}
