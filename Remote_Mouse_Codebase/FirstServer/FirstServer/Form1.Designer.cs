namespace FirstServer
{
    partial class frm_Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Server));
            this.txtb_serverDisplay = new System.Windows.Forms.TextBox();
            this.btn_startListening = new System.Windows.Forms.Button();
            this.btn_stopServer = new System.Windows.Forms.Button();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtb_serverDisplay
            // 
            this.txtb_serverDisplay.BackColor = System.Drawing.Color.Black;
            this.txtb_serverDisplay.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_serverDisplay.ForeColor = System.Drawing.SystemColors.Info;
            this.txtb_serverDisplay.Location = new System.Drawing.Point(12, 12);
            this.txtb_serverDisplay.Multiline = true;
            this.txtb_serverDisplay.Name = "txtb_serverDisplay";
            this.txtb_serverDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_serverDisplay.Size = new System.Drawing.Size(672, 455);
            this.txtb_serverDisplay.TabIndex = 2;
            // 
            // btn_startListening
            // 
            this.btn_startListening.Location = new System.Drawing.Point(698, 12);
            this.btn_startListening.Name = "btn_startListening";
            this.btn_startListening.Size = new System.Drawing.Size(104, 23);
            this.btn_startListening.TabIndex = 1;
            this.btn_startListening.Text = "Start Server";
            this.btn_startListening.UseVisualStyleBackColor = true;
            this.btn_startListening.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_stopServer
            // 
            this.btn_stopServer.Enabled = false;
            this.btn_stopServer.Location = new System.Drawing.Point(698, 41);
            this.btn_stopServer.Name = "btn_stopServer";
            this.btn_stopServer.Size = new System.Drawing.Size(104, 23);
            this.btn_stopServer.TabIndex = 3;
            this.btn_stopServer.Text = "Stop Server";
            this.btn_stopServer.UseVisualStyleBackColor = true;
            this.btn_stopServer.Click += new System.EventHandler(this.btn_stopServer_Click);
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Location = new System.Drawing.Point(698, 70);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(104, 23);
            this.btn_clearLog.TabIndex = 4;
            this.btn_clearLog.Text = "Clear Log";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // frm_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 497);
            this.Controls.Add(this.btn_clearLog);
            this.Controls.Add(this.btn_stopServer);
            this.Controls.Add(this.btn_startListening);
            this.Controls.Add(this.txtb_serverDisplay);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Server";
            this.Text = "Cerebro Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Server_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_serverDisplay;
        private System.Windows.Forms.Button btn_startListening;
        private System.Windows.Forms.Button btn_stopServer;
        private System.Windows.Forms.Button btn_clearLog;
    }
}

