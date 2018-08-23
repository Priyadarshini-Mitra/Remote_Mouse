using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    class PollingThread
    {
        frm_main form;
        TcpClient clientSocket = null;
        String id;
        public PollingThread(frm_main _form, TcpClient _clientSocket,String _id)
        {
            form = _form;
            clientSocket = _clientSocket;
            id = _id;
        }

        public void main()
        {
            try
            {
                while (true)
                {
                    NetworkStream serverStream = clientSocket.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(id + ":DataFromOthers$$");
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    byte[] inStream = new byte[(int)clientSocket.ReceiveBufferSize];
                    serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                    string returnData = Encoding.ASCII.GetString(inStream);
                    returnData = returnData.Substring(0, returnData.IndexOf("$$"));

                    displayLine(returnData);

                    if (returnData == "Awake")
                    {
                        if(form.isAsleep)
                            wakeUp();
                    }
                    else if (returnData == "Asleep")
                    {
                        if(!form.isAsleep)
                            sleep();
                    }

                    Thread.Sleep(2000);
                }                
            }
            catch (Exception)
            {
                
            }
        }

        public void sleep()
        {
            try
            {
                if (form.InvokeRequired)
                {
                    form.Invoke((MethodInvoker)delegate
                    {
                        form.sleep();
                    });
                }
                else
                {
                    form.sleep();
                }
            }
            catch (Exception)
            { }
        }

        public void displayLine(String message)
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

        public String readFromServer()
        {            
            try
            {
                String returnvalue="";
                if (form.InvokeRequired)
                {
                    form.Invoke((MethodInvoker)delegate
                    {
                        returnvalue= form.readFromServer();
                    });
                }
                else
                {
                    returnvalue= form.readFromServer();
                }

                return returnvalue;
            }
            catch (Exception)
            { }

            return null;
        }

        public void wakeUp()
        {
            try
            {
                if (form.InvokeRequired)
                {
                    form.Invoke((MethodInvoker)delegate
                    {
                        form.wake();
                    });
                }
                else
                {
                    form.wake();
                }
            }
            catch (Exception)
            { }
        }
    }
}
