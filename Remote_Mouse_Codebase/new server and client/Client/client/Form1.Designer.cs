namespace Client
{
    partial class frm_main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_main));
            this.txtb_clientID = new System.Windows.Forms.TextBox();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.txtb_display = new System.Windows.Forms.TextBox();
            this.lbl_clientID = new System.Windows.Forms.Label();
            this.btn_sleep = new System.Windows.Forms.Button();
            this.txtb_NextClient = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtb_clientID
            // 
            this.txtb_clientID.Location = new System.Drawing.Point(84, 13);
            this.txtb_clientID.Name = "txtb_clientID";
            this.txtb_clientID.Size = new System.Drawing.Size(188, 20);
            this.txtb_clientID.TabIndex = 0;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(13, 40);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 1;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // txtb_display
            // 
            this.txtb_display.Location = new System.Drawing.Point(13, 70);
            this.txtb_display.Multiline = true;
            this.txtb_display.Name = "txtb_display";
            this.txtb_display.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_display.Size = new System.Drawing.Size(259, 179);
            this.txtb_display.TabIndex = 2;
            // 
            // lbl_clientID
            // 
            this.lbl_clientID.AutoSize = true;
            this.lbl_clientID.Location = new System.Drawing.Point(13, 13);
            this.lbl_clientID.Name = "lbl_clientID";
            this.lbl_clientID.Size = new System.Drawing.Size(50, 13);
            this.lbl_clientID.TabIndex = 3;
            this.lbl_clientID.Text = "Client ID:";
            // 
            // btn_sleep
            // 
            this.btn_sleep.Location = new System.Drawing.Point(95, 40);
            this.btn_sleep.Name = "btn_sleep";
            this.btn_sleep.Size = new System.Drawing.Size(75, 23);
            this.btn_sleep.TabIndex = 4;
            this.btn_sleep.Text = "Wake";
            this.btn_sleep.UseVisualStyleBackColor = true;
            this.btn_sleep.Click += new System.EventHandler(this.btn_sleep_Click);
            // 
            // txtb_NextClient
            // 
            this.txtb_NextClient.Location = new System.Drawing.Point(232, 39);
            this.txtb_NextClient.Name = "txtb_NextClient";
            this.txtb_NextClient.Size = new System.Drawing.Size(40, 20);
            this.txtb_NextClient.TabIndex = 5;
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.txtb_NextClient);
            this.Controls.Add(this.btn_sleep);
            this.Controls.Add(this.lbl_clientID);
            this.Controls.Add(this.txtb_display);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.txtb_clientID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_main";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_main_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_clientID;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TextBox txtb_display;
        private System.Windows.Forms.Label lbl_clientID;
        private System.Windows.Forms.Button btn_sleep;
        private System.Windows.Forms.TextBox txtb_NextClient;
    }
}

