using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace firstClient
{
    class PollingThread
    {
        private TcpClient clientSocket;
        String myIP;
        Client form;

        public PollingThread(Client _form,String _myIP, TcpClient _clientSocket)
        {
            clientSocket = _clientSocket;
            myIP = _myIP;
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
                        form.displayClientView(message);
                    });
                }
                else
                {
                    form.displayClientView(message);
                }
            }
            catch (Exception)
            {                
            }
        }

        public void poll()
        {
            try
            {
                while (true)
                {
                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(myIP + ":DataFromOthers" + "$$");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();                    

                    byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                    string returnData = Encoding.ASCII.GetString(inStream);

                    returnData = returnData.Substring(0, returnData.IndexOf("$$"));
                    if (returnData != "*")
                    {
                        displayInMainForm(returnData.Trim());
                        SoundPlayer simpleSound = new SoundPlayer(CerebroClient.Properties.Resources.BEEP);
                        simpleSound.Play();
                    }

                    serverStream = clientSocket.GetStream();
                    outStream = Encoding.ASCII.GetBytes("GetFile$$");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();    

                    inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                    returnData = Encoding.ASCII.GetString(inStream);

                    int numberOfFiles = int.Parse(returnData);

                    if (numberOfFiles > 0)
                    {
                        for (int i = numberOfFiles; i >= 1; i--)
                        {
                            displayInMainForm("Pending number of files: " + i.ToString());

                            serverStream = clientSocket.GetStream();
                            outStream = Encoding.ASCII.GetBytes("GetFileX$$");
                            serverStream.Write(outStream, 0, outStream.Length);
                            serverStream.Flush();

                            inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                            String Filename = Encoding.ASCII.GetString(inStream);
                            Filename = Filename.Substring(0, Filename.IndexOf("$$"));

                            String[] filenameAndSize = Filename.Split(':');

                            displayInMainForm("Downloading file: " + filenameAndSize[0]);

                            byte[] buffer = new byte[1024];
                            int numberOfBytesRead = 0;

                            MemoryStream receivedData = new MemoryStream();

                            int lengthOfFile = int.Parse(filenameAndSize[1]);
                            displayInMainForm("Length of File in bytes: " + lengthOfFile.ToString());

                            NetworkStream networkStream = clientSocket.GetStream();

                            do
                            {
                                int bytesread = networkStream.Read(buffer, 0, buffer.Length);
                                numberOfBytesRead += bytesread;
                                if (numberOfBytesRead > 0)
                                    receivedData.Write(buffer, 0, bytesread);
                            }
                            while (numberOfBytesRead < lengthOfFile);

                            File.WriteAllBytes(filenameAndSize[0], receivedData.ToArray());
                        }
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception)
            {
                displayInMainForm("Unable to fetch messages");
                return;
            }
        }
    }
}
