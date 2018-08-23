using CerebroServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstServer
{
    class Server
    {
        private frm_Server form;
        public TcpClient clientSocket;
        private ClientDetails ClientID;

        List<ClientDetails> listOfClients;        

        public Server(frm_Server _form, TcpClient _clientSock, List<ClientDetails> _listOfClients)
        {
            form = _form;
            clientSocket = _clientSock;
            listOfClients = _listOfClients;                      
        }

        private ClientDetails findClient(String clientID)
        {
            foreach (ClientDetails client in listOfClients)
            {
                if (client.ClientID == clientID)
                {
                    return client;
                }
            }
            return null;
        }        

        public void main()
        {
            string _ClientID = "";            

            NetworkStream networkStream = clientSocket.GetStream();
            byte[] bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize];
            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
            networkStream.Flush();

            String dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
            _ClientID = dataFromClient.Substring(0, dataFromClient.IndexOf("$$"));            
            ClientID = new ClientDetails(_ClientID, clientSocket);
            
            displayInMainForm("Client with IP " + ClientID.ClientID + " Connected");            

            listOfClients.Add(ClientID);
            
            String exitCode = "";
            String serverResponse;
            byte[] sendBytes;

            try
            {
                while (true)
                {
                    networkStream = clientSocket.GetStream();
                    bytesFrom = new byte[(int)clientSocket.ReceiveBufferSize];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    networkStream.Flush(); 

                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$$"));

                    if (dataFromClient == "ClientList")
                    {
                        String clients = "";
                        foreach (ClientDetails str in listOfClients)
                        {
                            if(str.ClientName =="")
                                clients += str.ClientID + ":";
                            else
                                clients += str.ClientName + ":";
                        }

                        clients.Remove(clients.LastIndexOf(':'));

                        serverResponse = clients;
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();
                    }
                    else if (dataFromClient.Contains("DataFromOthers"))
                    {
                        String[] message = dataFromClient.Split(':');
                        serverResponse = " ";
                        bool gotClient = false;
                        foreach (ClientDetails client in listOfClients)
                        {
                            if (client.ClientID == message[0])
                            {
                                gotClient = true;
                                if (client.messages.Count > 0)
                                {
                                    foreach (String mess in client.messages)
                                        serverResponse += mess + "\r\n";
                                }
                                else
                                    serverResponse = "*";
                                client.messages = new List<string>();
                            }                            
                        }
                        if (!gotClient)
                            serverResponse = "*";

                        serverResponse += "$$";
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();                        
                    }
                    else if(dataFromClient.Contains("SetName"))
                    {
                        try
                        {
                            String[] message = dataFromClient.Split(':');                            
                            String fullName = message[2];
                            if (message.Length > 3)
                            {
                                for (int i = 3; i < message.Length; i++)
                                    fullName += ":" + message[i];
                            }                            

                            foreach (ClientDetails client in listOfClients)
                            {
                                if (client.ClientID == message[0])
                                {
                                    client.updateClientName(fullName);
                                    displayInMainForm("Client with IP " + client.ClientID + " named " + client.ClientName);
                                    break;
                                }
                            }                            
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString() + e.Data);
                        }
                    }
                    else if (dataFromClient.Contains("SendFile"))
                    {
                        try
                        {
                            String[] message = dataFromClient.Split(':');                             

                            byte[] buffer = new byte[1024];
                            int numberOfBytesRead = 0;

                            MemoryStream receivedData = new MemoryStream();

                            int lengthOfFile = int.Parse(message[4]);
                            displayInMainForm("Length of File in bytes: " + lengthOfFile.ToString());


                            foreach (ClientDetails client in listOfClients)
                            {
                                if (client.ClientID == message[2] || client.ClientName == message[2])
                                {
                                    if (!Directory.Exists(client.ClientID))
                                    {
                                        Directory.CreateDirectory(client.ClientID);
                                    }

                                    do
                                    {
                                        int bytesread = networkStream.Read(buffer, 0, buffer.Length);
                                        numberOfBytesRead += bytesread;
                                        if (numberOfBytesRead > 0)
                                            receivedData.Write(buffer, 0, bytesread);
                                    }
                                    while (numberOfBytesRead < lengthOfFile);

                                    File.WriteAllBytes(client.ClientID + "\\" + message[3], receivedData.ToArray());

                                    client.Files.Add(client.ClientID + "\\" + message[3]);

                                    displayInMainForm("Transferring file named:" + message[3] + " to " + message[2]);
                                }
                            }                            
                        }
                        catch (Exception ex)
                        {
                            displayInMainForm(ex.TargetSite + " " + ex.Message);
                        }
                    }
                    else if (dataFromClient == "GetFileX")
                    {
                        FileInfo fileinfo = new FileInfo(ClientID.Files.ElementAt(0));

                        NetworkStream serverStream = clientSocket.GetStream();
                        byte[] outStream = Encoding.ASCII.GetBytes(Path.GetFileName(ClientID.Files.ElementAt(0)) + ":" + fileinfo.Length.ToString() + "$$");
                        serverStream.Write(outStream, 0, outStream.Length);
                        serverStream.Flush();
                        
                        serverStream = clientSocket.GetStream();                        
                        serverStream.Write(File.ReadAllBytes(ClientID.Files.ElementAt(0)), 0, (int)fileinfo.Length);
                        serverStream.Flush();

                        //File.Delete(ClientID.Files.ElementAt(0));
                        ClientID.Files.RemoveAt(0);                        
                    }
                    else if (dataFromClient =="GetFile")
                    {
                        serverResponse = ClientID.Files.Count.ToString();
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();
                    }
                    else
                    {
                        String[] message = dataFromClient.Split(':');

                        String fullMessage = message[2];
                        if (message.Length > 3)
                        {
                            for (int i = 3; i < message.Length; i++)
                                fullMessage += ":" + message[i];
                        }

                        displayInMainForm(message[0] + " To " + message[1] + " : " + fullMessage);

                        bool clientAvailable = false;
                        foreach (ClientDetails client in listOfClients)
                        {
                            if (client.ClientID == message[1] || client.ClientName == message[1])
                            {
                                clientAvailable = true;
                                ClientDetails temp = findClient(message[0]);
                                if (temp != null)
                                    client.messages.Add(temp.ClientName + "> " + fullMessage);
                                else
                                    client.messages.Add(message[0] + "> " + fullMessage);
                            }
                        }

                        exitCode = dataFromClient;

                        if (clientAvailable)
                            serverResponse = "Server Response " + exitCode.Length + "$$";
                        else
                            serverResponse = "NotAvailable$$";
                        sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                        networkStream.Write(sendBytes, 0, sendBytes.Length);
                        networkStream.Flush();

                        displayInMainForm("Server response" + serverResponse);
                    }
                }
            }
            catch (Exception ex)
            {                
                displayInMainForm(ClientID.ClientID + " Disconnected");
                listOfClients.Remove(ClientID);                
            }
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
            {}
        }
    }
}
