using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CerebroClient
{
    public partial class SetServerForm : Form
    {
        firstClient.Client form;
        public SetServerForm(firstClient.Client _form)
        {
            InitializeComponent();
            form = _form;

            txtb_serverIP.Text = CerebroClient.Properties.Settings.Default.ServerIP;
            chk_autoConnect.Checked = CerebroClient.Properties.Settings.Default.AutoConnect;
        }

        private void chk_autoConnect_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            CerebroClient.Properties.Settings.Default.ServerIP = txtb_serverIP.Text;
            if (chk_autoConnect.Checked)
                CerebroClient.Properties.Settings.Default.AutoConnect = true;
            else
                CerebroClient.Properties.Settings.Default.AutoConnect = false;            

            CerebroClient.Properties.Settings.Default.Save();
            form.setupConnection();
            Dispose();
        }
    }
}
