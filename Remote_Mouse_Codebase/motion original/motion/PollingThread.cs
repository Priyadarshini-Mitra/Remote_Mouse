using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace motion
{
    class PollingThread
    {
        MainForm form;
        public PollingThread(MainForm _form)
        {
            form = _form;
        }

        public void main()
        {
            if (readFromServer() == "Wake")
                wakeUp();
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
