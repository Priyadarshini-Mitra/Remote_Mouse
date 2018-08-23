namespace CerebroClient
{
    partial class SetServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_setIP = new System.Windows.Forms.Label();
            this.txtb_serverIP = new System.Windows.Forms.TextBox();
            this.chk_autoConnect = new System.Windows.Forms.CheckBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_setIP
            // 
            this.lbl_setIP.AutoSize = true;
            this.lbl_setIP.Location = new System.Drawing.Point(13, 26);
            this.lbl_setIP.Name = "lbl_setIP";
            this.lbl_setIP.Size = new System.Drawing.Size(82, 13);
            this.lbl_setIP.TabIndex = 0;
            this.lbl_setIP.Text = "Enter Server IP:";
            // 
            // txtb_serverIP
            // 
            this.txtb_serverIP.Location = new System.Drawing.Point(101, 23);
            this.txtb_serverIP.Name = "txtb_serverIP";
            this.txtb_serverIP.Size = new System.Drawing.Size(341, 20);
            this.txtb_serverIP.TabIndex = 1;
            // 
            // chk_autoConnect
            // 
            this.chk_autoConnect.AutoSize = true;
            this.chk_autoConnect.Location = new System.Drawing.Point(16, 54);
            this.chk_autoConnect.Name = "chk_autoConnect";
            this.chk_autoConnect.Size = new System.Drawing.Size(91, 17);
            this.chk_autoConnect.TabIndex = 2;
            this.chk_autoConnect.Text = "Auto Connect";
            this.chk_autoConnect.UseVisualStyleBackColor = true;
            this.chk_autoConnect.CheckedChanged += new System.EventHandler(this.chk_autoConnect_CheckedChanged);
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(113, 50);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 3;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // SetServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 96);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.chk_autoConnect);
            this.Controls.Add(this.txtb_serverIP);
            this.Controls.Add(this.lbl_setIP);
            this.MaximumSize = new System.Drawing.Size(466, 135);
            this.Name = "SetServerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Set Server IP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_setIP;
        private System.Windows.Forms.TextBox txtb_serverIP;
        private System.Windows.Forms.CheckBox chk_autoConnect;
        private System.Windows.Forms.Button btn_connect;
    }
}